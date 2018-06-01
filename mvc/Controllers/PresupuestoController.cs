using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc.ViewModels;
using System.IO;
using mvc.Helpers;
using ceya.Model.Models;
using ceya.Infrastructure.DataAccess;
using ceya.Domain.Service;
using ceya.Web.Core.Extensions;
using AutoMapper;
using X.PagedList;
using ceya.Web.Core.ActionFilters;

using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;

using Microsoft.Reporting.WebForms;
using System.Globalization;
using ceya.Domain.Model.DTOs;

namespace mvc.Controllers
{
    public class PresupuestoController : Controller
    {
        private IPresupuestoService presupuestoService;
        private IPresupuestoItemService presupuestoItemService;
        private IArchivoService archivoService;
        private IPresupuestoCategoriaService presupuestoCategoriaService;
        private IPresupuestoEstadoService presupuestoEstadoService;
        private IVWPrecioProductoService vwPrecioProductoService;
        private ISubrubroService subrubroService;
        private IColorService colorService;
        private IMonedaService monedaService;
        private IProductoService productoService;
        private IPrecioService precioService;

        private GestionComercialWebEntities db;
        private string ObservacionHeater;

        public PresupuestoController(
            IDatabaseFactory dbFactory,
            IPresupuestoService presupuestoService,
            IPresupuestoItemService presupuestoItemService,
            IArchivoService archivoService,
            IPresupuestoCategoriaService presupuestoCategoriaService,
            IPresupuestoEstadoService presupuestoEstadoService,
            IVWPrecioProductoService vwPrecioProductoService,
            ISubrubroService subrubroService,
            IColorService colorService,
            IMonedaService monedaService,
            IProductoService productoService,
            IPrecioService precioService)
        {
            this.db = dbFactory.Get();
            this.presupuestoService = presupuestoService;
            this.presupuestoItemService = presupuestoItemService;
            this.archivoService = archivoService;
            this.presupuestoCategoriaService = presupuestoCategoriaService;
            this.presupuestoEstadoService = presupuestoEstadoService;
            this.subrubroService = subrubroService;
            this.colorService = colorService;
            this.monedaService = monedaService;
            this.productoService = productoService;
            this.precioService = precioService;

            // Vistas para listar precios de Vidrios y Colocaciones
            this.vwPrecioProductoService = vwPrecioProductoService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string direction = "desc", string filterBy = "All", string searchString = "", Guid? estadoId = null,
            int pageSize = 10, int page = 1)
        {
            ViewBag.Estados = new SelectList(db.PresupuestoEstado, "Id", "Descripcion");
            var pageList = this.presupuestoService.GetPresupuestosByPage(page, pageSize, sortBy, direction, filterBy, searchString, estadoId);
            var presupuestosVM = Mapper.Map<IEnumerable<Presupuesto>, IEnumerable<PresupuestoListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = presupuestosVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new PresupuestoPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = presupuestosVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var createVM = new PresupuestoFormModel();

            createVM.ArchivoTransaccionId = Guid.NewGuid();

            // items
            createVM.Items = new List<PresupuestoItemFormModel>();

            // Datos seleccionables en cabecera
            var categorias = presupuestoCategoriaService.GetCategorias();
            createVM.Categorias = categorias.ToSelectListItems(Guid.Empty);
            var estados = presupuestoEstadoService.GetEstados();
            createVM.Estados = estados.ToSelectListItems(Guid.Empty);
            var lineas = vwPrecioProductoService.GetLineasPresupuesto();
            createVM.Lineas = lineas.ToSelectListItemsColocaciones(Guid.Empty);
            var subrubros = subrubroService.GetSubrubros();
            createVM.Subrubros = subrubros.ToSelectListItems(Guid.Empty);
            var colores = colorService.GetColores();
            createVM.Colores = colores.ToSelectListItems(Guid.Empty);

            var colocaciones = vwPrecioProductoService.GetColocaciones();
            createVM.PresupuestoColocaciones = colocaciones.ToSelectListItems(Guid.Empty);

            var vidrios = vwPrecioProductoService.GetVidrios();
            createVM.PresupuestoVidrios = vidrios.ToSelectListItems(Guid.Empty);

            var monedas = monedaService.GetMonedas();
            var moneda = monedaService.GetMonedaFilter("PESOS").FirstOrDefault();
            createVM.Monedas = monedas.ToSelectListItems(moneda.Id);
            createVM.MonedaId = moneda.Id;
            createVM.NombreMoneda = moneda.Nombre;
            decimal cotizacion = Convert.ToDecimal("1");
            createVM.Cotizacion = cotizacion;

            // Datos seleccionables en items
            var preciosProductos = vwPrecioProductoService.GetProductos();
            createVM.Productos = preciosProductos;
            var preciosColocaciones = vwPrecioProductoService.GetColocaciones();
            createVM.Colocaciones = preciosColocaciones;
            var preciosVidrios = vwPrecioProductoService.GetVidrios();
            createVM.Vidrios = preciosVidrios;

            ViewBag.Title = "Alta de Presupuesto";

            return View("Form2", createVM);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public JsonResult Create(PresupuestoFormModel vm)
        {
            var presupuesto = Mapper.Map<PresupuestoFormModel, Presupuesto>(vm);
            var errors = this.presupuestoService.CanAddPresupuesto(presupuesto).ToList();
            ModelState.AddModelErrors(errors);
            if (ModelState.IsValid)
            {

                this.presupuestoService.CreatePresupuesto(presupuesto);
                return Json(new { estado = "OK" });
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.AjaxModelErrors();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var presupuesto = this.presupuestoService.GetPresupuesto(id);

            // items
            var presupuestoItems = this.presupuestoItemService.GetItems(id);

            if (presupuesto == null)
            {
                return HttpNotFound();
            }

            // archivos
            var presupuestoArchivos = this.archivoService.GetArchivosPorTransaccion(presupuesto.ArchivoTransaccionId);

            // mapeos
            var editVM = Mapper.Map<Presupuesto, PresupuestoFormModel>(presupuesto);
            editVM.Items = Mapper.Map<IEnumerable<PresupuestoItem>, IEnumerable<PresupuestoItemFormModel>>(presupuestoItems,
                options => options.AfterMap((src, dest) =>
                {
                    foreach (var itemModel in dest)
                    {
                        if (itemModel.TipologiaId == Guid.Empty || itemModel.TipologiaId == null)
                        {
                            return;
                        }
                        var archivo = presupuestoArchivos.Where(x => x.Id == itemModel.TipologiaId.Value).FirstOrDefault();
                        if (archivo != null)
                        {
                            itemModel.TipologiaUrl = Url.Action("Get", "Archivo", new { Id = archivo.Id });
                            itemModel.TipologiaThumbnailUrl = Url.Action("GetThumbnail", "Archivo", new { Id = archivo.Id, sizeClass = 3 });
                        }
                    }
                }));

            // Datos seleccionables en la cabecera del presupuesto
            var categorias = presupuestoCategoriaService.GetCategorias();

            if (presupuesto.PresupuestoCategoria != null)
            {
                editVM.Categorias = categorias.ToSelectListItems(presupuesto.PresupuestoCategoria.Id);
            }
            else
            {
                editVM.Categorias = categorias.ToSelectListItems(Guid.Empty);
            }

            var estados = presupuestoEstadoService.GetEstados();

            if (presupuesto.PresupuestoEstado != null)
            {
                editVM.Estados = estados.ToSelectListItems(presupuesto.PresupuestoEstado.Id);
            }
            else
            {
                editVM.Estados = estados.ToSelectListItems(Guid.Empty);
            }

            if (presupuesto.MonedaId != null)
            {
                editVM.NombreMoneda = monedaService.GetMoneda(presupuesto.MonedaId.Value).Nombre;
            }

            // Valores Predeterminados
            if (presupuesto.SubrubroId != null)
            {
                editVM.NombreSubrubro = subrubroService.GetSubrubro(presupuesto.SubrubroId.Value).Descripcion;
            }

            if (presupuesto.ColorId != null)
            {
                editVM.NombreColor = colorService.GetColor(presupuesto.ColorId.Value).Descripcion;
            }

            if (presupuesto.ColocacionId != null)
            {
                var colocacion = vwPrecioProductoService.GetColocacion(presupuesto.ColocacionId.Value);
                editVM.NombreColocacion = colocacion.ProductoDescripcion;
                editVM.PrecioColocacion = colocacion.PrecioProducto;
            }

            if (presupuesto.VidrioId != null)
            {
                var vidrio = vwPrecioProductoService.GetVidrio(presupuesto.VidrioId.Value);
                editVM.NombreVidrio = vidrio.ProductoDescripcion;
                editVM.PrecioVidrio = vidrio.PrecioProducto;
            }

            // Datos seleccionables en items
            var preciosColocaciones = vwPrecioProductoService.GetColocaciones();
            editVM.Colocaciones = preciosColocaciones;

            var preciosVidrios = vwPrecioProductoService.GetVidrios();
            editVM.Vidrios = preciosVidrios;

            ViewBag.Title = "Modificación de Presupuesto";

            return View("Form", editVM);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PresupuestoFormModel editVM)
        {
            var presupuestoPorEditar = Mapper.Map<PresupuestoFormModel, Presupuesto>(editVM);
            var errors = this.presupuestoService.CanAddNewRevisionPresupuesto(presupuestoPorEditar);
            // ModelState.AddModelErrors(errors);

            if (ModelState.IsValid)
            {
                this.presupuestoService.EditPresupuesto(presupuestoPorEditar);

                var result = new
                {
                    Success = "Edicion completa",
                    Id = presupuestoPorEditar.Id
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    Failure = "Algun error ha sucedido"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult LoadFileInPresupuesto(Guid archivoTransaccionId)
        {
            bool isSavedSuccessfully = true;
            object result = null;
            Guid? SubrubroId = null;
            string __subrubroRequest = Request["SubrubroId"];
            try
            {
                if (Request["SubrubroId"] != string.Empty || Request["SubrubroId"] != null)
                {
                    SubrubroId = Guid.Parse(Request["SubrubroId"]);
                }
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var storeDirectory = new DirectoryInfo(Server.MapPath(Constants.PathExcelsDePresupuesto));

                        if (!storeDirectory.Exists)
                        {
                            storeDirectory.Create();
                        }

                        var nombreOriginal = file.FileName;
                        var nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(nombreOriginal);
                        var ubicacion = Path.Combine(Server.MapPath(Constants.PathExcelsDePresupuesto), nombre);
                        var fechaHoraSubida = DateTime.Now;

                        file.SaveAs(ubicacion);

                        var mimeType = MimeMapping.GetMimeMapping(ubicacion);

                        var archivo = new Archivo
                        {
                            Id = Guid.NewGuid(),
                            Nombre = nombre,
                            NombreOriginal = nombreOriginal,
                            MimeType = mimeType,
                            Ubicacion = ubicacion,
                            FechaHoraSubida = fechaHoraSubida,
                            TransaccionId = archivoTransaccionId,
                            TransaccionCompletada = false
                        };

                        this.db.Archivo.Add(archivo);
                        this.db.SaveChanges();
                        var rubro = db.Subrubro
                            .Where(x => x.Id == SubrubroId)
                            .Select(x => x.Rubro)
                            .FirstOrDefault();

                        // IMPORTAR A PRESUPUESTO
                        var items = new List<PresupuestoItem>();
                        var contador = 0;
                        var itemTieneArchivo = false;

                        if (rubro.Descripcion == "PVC")
                        {
                            var metadataRehauVM = this.ProcesarExcelRehau(archivo.Id);

                            foreach (var itemMetadataVM in metadataRehauVM.Items)
                            {
                                contador++;

                                try
                                {


                                    if (itemMetadataVM.Tipologia != null && itemMetadataVM.Tipologia.Image != null)
                                    {
                                        // CODIGO DE SUBIDA DE TIPOLOGIA
                                        storeDirectory = new DirectoryInfo(Server.MapPath(Constants.PathTipologia));
                                        if (!storeDirectory.Exists)
                                        {
                                            storeDirectory.Create();
                                        }
                                        nombreOriginal = itemMetadataVM.Tipologia.Name;
                                        nombre = DateTime.Now.ToString("yyyyMMddhhmmssffff") + contador.ToString().PadLeft(4, '0') + Path.GetExtension("X.png");
                                        ubicacion = Path.Combine(Server.MapPath(Constants.PathTipologia), nombre);
                                        fechaHoraSubida = DateTime.Now;

                                        Bitmap bm3 = new Bitmap(itemMetadataVM.Tipologia.Image);
                                        bm3.Save(ubicacion);

                                        Thumbnail.GenerateThumbnail(new FileInfo(ubicacion), Constants.ThumbnailSizes[3], new DirectoryInfo(Constants.PathTipologiaThumbnailDirs[3]));

                                        mimeType = MimeMapping.GetMimeMapping(ubicacion);

                                        archivo = new Archivo
                                        {
                                            Id = Guid.NewGuid(),
                                            Nombre = nombre,
                                            NombreOriginal = nombreOriginal,
                                            MimeType = mimeType,
                                            Ubicacion = ubicacion,
                                            FechaHoraSubida = fechaHoraSubida,
                                            TransaccionId = archivoTransaccionId,
                                            TransaccionCompletada = false
                                        };

                                        this.db.Archivo.Add(archivo);
                                        this.db.SaveChanges();

                                        itemTieneArchivo = true;

                                        //result = new
                                        //{
                                        //    id = archivo.Id,
                                        //    url = Url.Action("Get", "Archivo", new { Id = archivo.Id }),
                                        //    thumbnailUrl = Url.Action("GetThumbnail", "Archivo", new { Id = archivo.Id, sizeClass = 3 })
                                        //};
                                    }
                                    else
                                    {
                                        itemTieneArchivo = false;
                                    }

                                    var archivoTipologiaId = itemTieneArchivo ? archivo.Id : new Nullable<Guid>();
                                    var posicion = itemMetadataVM.Posicion.Trim().TrimStart("POSICIÓN: ".ToCharArray());
                                    var unidades = Convert.ToInt32(itemMetadataVM.Unidades.Trim());
                                    //var carpinteria = Convert.ToDecimal(itemMetadataVM.PrecioUnitario.Trim().TrimEnd("U$S".ToCharArray()).Replace(",", "").Replace('.', ','));
                                    var carpinteria = itemMetadataVM.PrecioUnitario == null ? 0 : Convert.ToDecimal(itemMetadataVM.PrecioUnitario.Trim().TrimEnd("U$S".ToCharArray()));

                                    var item = new PresupuestoItem()
                                    {
                                        Id = Guid.Empty,
                                        PresupuestoId = Guid.Empty,
                                        ArchivoTipologiaId = archivoTipologiaId,
                                        NumeroPosicion = contador,
                                        Posicion = posicion,
                                        Descripcion = itemMetadataVM.Descripcion,
                                        Unidades = unidades,
                                        PrecioUnitario = carpinteria,
                                        VidriosId = new Nullable<Guid>(),
                                        VidriosPrecio = 0m,
                                        ColocacionId = new Nullable<Guid>(),
                                        ColocacionPrecio = 0m,
                                        Ancho = 0m,
                                        Alto = 0m,
                                        Carpinteria = carpinteria,
                                        Tapajuntas = 0m,
                                        VidriosCalculado = 0m,
                                        ColocacionCalculado = 0m,
                                        Detalle = String.Empty,
                                        Importe = carpinteria * unidades,
                                        Estado = "NUEVO"

                                        //public Nullable<Guid> PresupuestoItemAnteriorId { get; set; }
                                        //public virtual PresupuestoItem PresupuestoItemAnterior { get; set; }
                                        //public Nullable<Guid> PresupuestoItemNuevoId { get; set; }
                                        //public virtual PresupuestoItem PresupuestoItemNuevo { get; set; }

                                        //public virtual Archivo Archivo { get; set; }
                                        //public virtual Precio PrecioVidrio { get; set; }
                                        //public virtual Precio PrecioColocacion { get; set; }
                                        //public virtual Presupuesto Presupuesto { get; set; }
                                    };

                                    items.Add(item);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
                                        "Loop Time " + contador + System.Environment.NewLine
                                        + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + System.Environment.NewLine,
                                        System.Diagnostics.EventLogEntryType.Error,
                                        1,
                                        short.MaxValue);
                                }
                            }
                        }
                        else
                        {
                            var metadataAlcemarVM = this.ProcesarExcelAlcemar(archivo.Id);

                            foreach (var itemMetadataVM in metadataAlcemarVM.Items)
                            {
                                contador++;

                                try
                                {


                                    if (itemMetadataVM.Tipologia != null && itemMetadataVM.Tipologia.Image != null)
                                    {
                                        // CODIGO DE SUBIDA DE TIPOLOGIA
                                        storeDirectory = new DirectoryInfo(Server.MapPath(Constants.PathTipologia));
                                        if (!storeDirectory.Exists)
                                        {
                                            storeDirectory.Create();
                                        }
                                        nombreOriginal = itemMetadataVM.Tipologia.Name;
                                        nombre = DateTime.Now.ToString("yyyyMMddhhmmssffff") + contador.ToString().PadLeft(4, '0') + Path.GetExtension("X.png");
                                        ubicacion = Path.Combine(Server.MapPath(Constants.PathTipologia), nombre);
                                        fechaHoraSubida = DateTime.Now;

                                        Bitmap bm3 = new Bitmap(itemMetadataVM.Tipologia.Image);
                                        bm3.Save(ubicacion);

                                        Thumbnail.GenerateThumbnail(new FileInfo(ubicacion), Constants.ThumbnailSizes[3], new DirectoryInfo(Constants.PathTipologiaThumbnailDirs[3]));

                                        mimeType = MimeMapping.GetMimeMapping(ubicacion);

                                        archivo = new Archivo
                                        {
                                            Id = Guid.NewGuid(),
                                            Nombre = nombre,
                                            NombreOriginal = nombreOriginal,
                                            MimeType = mimeType,
                                            Ubicacion = ubicacion,
                                            FechaHoraSubida = fechaHoraSubida,
                                            TransaccionId = archivoTransaccionId,
                                            TransaccionCompletada = false
                                        };

                                        this.db.Archivo.Add(archivo);
                                        this.db.SaveChanges();

                                        itemTieneArchivo = true;

                                        //result = new
                                        //{
                                        //    id = archivo.Id,
                                        //    url = Url.Action("Get", "Archivo", new { Id = archivo.Id }),
                                        //    thumbnailUrl = Url.Action("GetThumbnail", "Archivo", new { Id = archivo.Id, sizeClass = 3 })
                                        //};
                                    }
                                    else
                                    {
                                        itemTieneArchivo = false;
                                    }

                                    var archivoTipologiaId = itemTieneArchivo ? archivo.Id : new Nullable<Guid>();
                                    var posicion = itemMetadataVM.Posicion.Trim();
                                    var unidades = Convert.ToInt32(itemMetadataVM.Cantidad.Replace("Cantidad:", "").Trim());
                                    //var carpinteria = Convert.ToDecimal(itemMetadataVM.PrecioUnitario.Trim().TrimEnd("U$S".ToCharArray()).Replace(",", "").Replace('.', ','));
                                    var carpinteria = itemMetadataVM.UnitarioCarpinteria == null ? 0 : Convert.ToDecimal(itemMetadataVM.UnitarioCarpinteria.Replace("$", "").Replace("€", "").Trim());
                                    var tapajuntas = itemMetadataVM.UnitarioTapajuntas == null ? 0 : Convert.ToDecimal(itemMetadataVM.UnitarioTapajuntas.Replace("$", "").Replace("€", "").Trim());

                                    var item = new PresupuestoItem()
                                    {
                                        Id = Guid.Empty,
                                        PresupuestoId = Guid.Empty,
                                        ArchivoTipologiaId = archivoTipologiaId,
                                        NumeroPosicion = contador,
                                        Posicion = posicion,
                                        Descripcion = itemMetadataVM.Descripcion,
                                        Unidades = unidades,
                                        PrecioUnitario = carpinteria,
                                        VidriosId = new Nullable<Guid>(),
                                        VidriosPrecio = 0m,
                                        ColocacionId = new Nullable<Guid>(),
                                        ColocacionPrecio = 0m,
                                        Ancho = 0m,
                                        Alto = 0m,
                                        Carpinteria = carpinteria,
                                        Tapajuntas = tapajuntas,
                                        VidriosCalculado = 0m,
                                        ColocacionCalculado = 0m,
                                        Detalle = String.Empty,
                                        Importe = (carpinteria + tapajuntas) * unidades,
                                        Estado = "NUEVO"

                                        //public Nullable<Guid> PresupuestoItemAnteriorId { get; set; }
                                        //public virtual PresupuestoItem PresupuestoItemAnterior { get; set; }
                                        //public Nullable<Guid> PresupuestoItemNuevoId { get; set; }
                                        //public virtual PresupuestoItem PresupuestoItemNuevo { get; set; }

                                        //public virtual Archivo Archivo { get; set; }
                                        //public virtual Precio PrecioVidrio { get; set; }
                                        //public virtual Precio PrecioColocacion { get; set; }
                                        //public virtual Presupuesto Presupuesto { get; set; }
                                    };

                                    items.Add(item);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
                                        "Loop Time " + contador + System.Environment.NewLine
                                        + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + System.Environment.NewLine,
                                        System.Diagnostics.EventLogEntryType.Error,
                                        1,
                                        short.MaxValue);
                                }
                            }
                        }

                        result = new
                        {
                            Items = items
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                result = new { error = ex.Message + ex.InnerException + ex.TargetSite + "  Error al intentar procesar el archivo" };
            }

            if (isSavedSuccessfully)
            {

                return Json(result);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(result);
            }
        }

        private RehauExcelMetadataViewModel ProcesarExcelRehau(Guid archivoId)
        {
            // Obtener archivo
            var archivo = this.db.Archivo.Find(archivoId);
            var filename = archivo.Ubicacion; // Path.Combine(Server.MapPath(Constants.PathTipologia), archivo.Nombre);//archivo.Ubicacion;

            Excel.Application excel = null;
            Excel.Workbook wb = null;

            try
            {
                // Instancia de Excel y apertura de archivo
                // DA ERROR excel = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                // Matar procesos de EXCEL
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
                }
                if (excel == null)
                {
                    excel = new Excel.Application();
                }
                // ERROR wb = ExcelTools.OpenBook(excel, filename);
                try
                {
                    wb = excel.Workbooks.Add(filename);
                }
                catch
                {
                    wb = excel.Workbooks.Open(filename);
                }

                // La primer hoja es 1 no 0
                Excel.Worksheet sheet = wb.Sheets[1] as Excel.Worksheet;
                Excel.Range range = null;
                Excel.Range currentFind = null;

                int find01FirstRowNumber = -1;
                int find01LastRowNumber = -1;
                int itemStartRowNumber = -1;

                if (sheet == null)
                {
                    throw new Exception("No se encontro hoja en libro Excel");
                }

                // Extraccion de datos
                var result = new RehauExcelMetadataViewModel();
                // NO SE PUEDE HACER COPIA AL CLIPBOARD EN UNA APP WEB
                var imagenes = new List<RehauItemShapeMetadataViewModel>();
                //int count = 0;

                foreach (Excel.Shape shape in sheet.Shapes)
                {
                    //count = count + 1;
                    //if (count <= sheet.Shapes.Count - 1)
                    //{
                    shape.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
                    Bitmap image = null;
                    var thread = new System.Threading.Thread(() =>
                    {
                        var bitmapSource = (System.Windows.Clipboard.ContainsImage() ? System.Windows.Clipboard.GetImage() : null);
                        image = GetBitmap(bitmapSource);
                    });
                    thread.SetApartmentState(System.Threading.ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    imagenes.Add(new RehauItemShapeMetadataViewModel()
                    {
                        Top = shape.Top,
                        Left = shape.Left,
                        Name = shape.Name,
                        Image = image
                    });
                    //}
                }
                var headerEmpresa = new RehauHeaderEmpresaMetadataViewModel();
                range = sheet.get_Range("R2", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Nombre = range.Text.ToString();
                }
                range = sheet.get_Range("R4", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Direccion = range.Text.ToString();
                }
                range = sheet.get_Range("R5", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.CpLocalidadProvincia = range.Text.ToString();
                }
                range = sheet.get_Range("R6", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.TelefonoFax = range.Text.ToString();
                }
                range = sheet.get_Range("R7", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Email = range.Text.ToString();
                }
                result.Empresa = headerEmpresa;
                var headerCliente = new RehauHeaderClienteMetadataViewModel();
                range = sheet.get_Range("A10", Missing.Value);
                if (range != null)
                {
                    headerCliente.Nombre = range.Text.ToString();
                }
                range = sheet.get_Range("T10", Missing.Value);
                if (range != null)
                {
                    headerCliente.NumeroPresupuesto = range.Text.ToString();
                }
                range = excel.get_Range("A10", "R65536");
                currentFind = range.Find("NOMBRE PROYECTO:*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    headerCliente.NombreProyecto = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion NOMBRE PROYECTO");
                }
                range = sheet.get_Range("T" + find01FirstRowNumber, Missing.Value);
                if (range != null)
                {
                    headerCliente.Version = range.Text.ToString();
                }
                range = excel.get_Range("A" + find01LastRowNumber, "R65536");
                currentFind = range.Find("UBICACIÓN DEL PROYECTO:*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    headerCliente.UbicacionProyecto = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion UBICACIÓN DEL PROYECTO");
                }
                range = sheet.get_Range("T" + find01FirstRowNumber, Missing.Value);
                if (range != null)
                {
                    headerCliente.Fecha = range.Text.ToString();
                }
                range = excel.get_Range("A" + find01LastRowNumber, "R65536");
                currentFind = range.Find("ATENCIÓN A:*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    headerCliente.AtencionA = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion ATENCIÓN A");
                }
                result.Cliente = headerCliente;
                range = excel.get_Range("A" + find01LastRowNumber, "AK65536");
                currentFind = range.Find("De acuerdo a su requerimiento*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    result.ParrafoIntroductorio = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion Parrafo Introductorio de Items");
                }
                var items = new List<RehauItemMetadataViewModel>();
                int firstItemRowNumber = -1;
                while (true)
                {
                    range = excel.get_Range("A" + (find01LastRowNumber + 1), "G65536");
                    currentFind = range.Find("ITEM:*", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        itemStartRowNumber = find01FirstRowNumber;

                        if (firstItemRowNumber == -1)
                        {
                            firstItemRowNumber = itemStartRowNumber;
                        }
                        else if (itemStartRowNumber == firstItemRowNumber)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    var item = new RehauItemMetadataViewModel();
                    range = sheet.get_Range("A" + itemStartRowNumber, Missing.Value);
                    if (range != null)
                    {
                        item.NumeroItem = range.Text.ToString();
                    }
                    range = sheet.get_Range("L" + itemStartRowNumber, Missing.Value);
                    if (range != null)
                    {
                        item.Posicion = range.Text.ToString();
                    }
                    range = sheet.get_Range("L" + (itemStartRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.Color = range.Text.ToString();
                    }
                    range = sheet.get_Range("L" + (itemStartRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.Color = range.Text.ToString();
                    }
                    range = sheet.get_Range("AN" + (itemStartRowNumber + 4), Missing.Value);
                    if (range != null)
                    {
                        item.HeaderDescripcion = range.Text.ToString();
                    }
                    range = sheet.get_Range("AN" + (itemStartRowNumber + 6), Missing.Value);
                    if (range != null)
                    {
                        item.Descripcion = range.Text.ToString();
                    }
                    range = excel.get_Range("AN" + (itemStartRowNumber + 7), "AP65536");
                    currentFind = range.Find("*UNITARIO*", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        item.HeaderUnitario = currentFind.Text.ToString();
                    }
                    range = sheet.get_Range("AQ" + find01FirstRowNumber, Missing.Value);
                    if (range != null)
                    {
                        item.HeaderUnidades = range.Text.ToString();
                    }
                    range = sheet.get_Range("AS" + find01FirstRowNumber, Missing.Value);
                    if (range != null)
                    {
                        item.HeaderTotal = range.Text.ToString();
                    }
                    range = sheet.get_Range("AN" + (find01FirstRowNumber + 2), Missing.Value);
                    if (range != null)
                    {
                        item.PrecioUnitario = range.Text.ToString();
                    }
                    range = sheet.get_Range("AQ" + (find01FirstRowNumber + 2), Missing.Value);
                    if (range != null)
                    {
                        item.Unidades = range.Text.ToString();
                    }
                    range = sheet.get_Range("AS" + (find01FirstRowNumber + 2), Missing.Value);
                    if (range != null)
                    {
                        item.PrecioTotal = range.Text.ToString();
                    }
                    item.Tipologia = imagenes.Where(x =>
                        sheet.get_Range("A" + itemStartRowNumber, Missing.Value).Top < x.Top &&
                        sheet.get_Range("A" + (itemStartRowNumber + 17), Missing.Value).Top > x.Top).Single();
                    items.Add(item);
                }
                result.Items = items;
                var resumen = new RehauResumenMetadataViewModel();
                range = excel.get_Range("Q" + find01LastRowNumber, "AM65536");
                currentFind = range.Find("TOTAL NETO:*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    resumen.TotalNeto = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion TOTAL NETO");
                }
                range = sheet.get_Range("C" + (find01FirstRowNumber + 1), Missing.Value);
                if (range != null)
                {
                    resumen.TotalUnidades = range.Text.ToString();
                }
                range = sheet.get_Range("C" + (find01FirstRowNumber + 3), Missing.Value);
                if (range != null)
                {
                    resumen.TotalM2 = range.Text.ToString();
                }
                range = sheet.get_Range("C" + (find01FirstRowNumber + 5), Missing.Value);
                if (range != null)
                {
                    resumen.TotalML = range.Text.ToString();
                }
                range = sheet.get_Range("Q" + (find01FirstRowNumber + 6), Missing.Value);
                if (range != null)
                {
                    resumen.Iva = range.Text.ToString();
                }
                range = sheet.get_Range("Q" + (find01FirstRowNumber + 7), Missing.Value);
                if (range != null)
                {
                    resumen.TotalProyecto = range.Text.ToString();
                }
                result.Resumen = resumen;

                // Guardar Datos
                var directory = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
                var storeDirectory = new DirectoryInfo(directory);
                if (!storeDirectory.Exists)
                {
                    storeDirectory.Create();
                    using (StreamWriter file = System.IO.File.CreateText(Path.Combine(directory, "metadata.json")))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, result);
                    }
                    imagenes.ForEach(x => x.Image.Save(Path.Combine(directory, x.Name + ".png"), ImageFormat.Png));
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
                        Newtonsoft.Json.JsonConvert.SerializeObject(ex) + System.Environment.NewLine, System.Diagnostics.EventLogEntryType.Error, 1, short.MaxValue);

                return null;
            }
            finally
            {
                if (wb != null)
                {
                    ExcelTools.ReleaseRCM(wb);
                }
                if (excel != null)
                {
                    ExcelTools.ReleaseRCM(excel);
                }
            }
        }

        public JsonResult ProbarAlcemar()
        {
            var metadata = this.ProcesarExcelAlcemar(new Guid("EF8284D7-384C-4966-AA15-6D317E8A7AED"));

            return Json(metadata, JsonRequestBehavior.AllowGet);
        }

        private AlcemarExcelMetadataViewModel ProcesarExcelAlcemar(Guid archivoId)
        {
            // Obtener archivo
            var archivo = this.db.Archivo.Find(archivoId);
            var filename = archivo.Ubicacion; // Path.Combine(Server.MapPath(Constants.PathTipologia), archivo.Nombre);//archivo.Ubicacion;

            Excel.Application excel = null;
            Excel.Workbook wb = null;


            try
            {
                // Instancia de Excel y apertura de archivo
                // DA ERROR excel = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                // Matar procesos de EXCEL
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
                }
                if (excel == null)
                {
                    excel = new Excel.Application();
                }
                // ERROR wb = ExcelTools.OpenBook(excel, filename);
                try
                {
                    wb = excel.Workbooks.Add(filename);
                }
                catch
                {
                    wb = excel.Workbooks.Open(filename);
                }

                // La primer hoja es 1 no 0
                Excel.Worksheet sheet = wb.Sheets[1] as Excel.Worksheet;
                Excel.Range range = null;
                Excel.Range currentFind = null;

                int find01FirstRowNumber = -1;
                int find01LastRowNumber = -1;
                int itemStartRowNumber = -1;

                if (sheet == null)
                {
                    throw new Exception("No se encontro hoja en libro Excel");
                }

                // Extraccion de datos
                var result = new AlcemarExcelMetadataViewModel();
                // NO SE PUEDE HACER COPIA AL CLIPBOARD EN UNA APP WEB
                var imagenes = new List<AlcemarItemShapeMetadataViewModel>();
                //int count = 0;

                foreach (Excel.Shape shape in sheet.Shapes)
                {
                    //count = count + 1;
                    //if (count <= sheet.Shapes.Count - 1)
                    //{
                    shape.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
                    Bitmap image = null;
                    var thread = new System.Threading.Thread(() =>
                    {
                        var bitmapSource = (System.Windows.Clipboard.ContainsImage() ? System.Windows.Clipboard.GetImage() : null);
                        image = GetBitmap(bitmapSource);
                    });
                    thread.SetApartmentState(System.Threading.ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    imagenes.Add(new AlcemarItemShapeMetadataViewModel()
                    {
                        Top = shape.Top,
                        Left = shape.Left,
                        Name = shape.Name,
                        Image = image
                    });
                    //}
                }
                range = sheet.get_Range("E1", Missing.Value);
                if (range != null)
                {
                    result.Fecha = range.Text.ToString();
                }
                var headerEmpresa = new AlcemarHeaderEmpresaMetadataViewModel();
                range = sheet.get_Range("E7", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Nombre = range.Text.ToString();
                }
                range = sheet.get_Range("E8", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Direccion = range.Text.ToString();
                }
                range = sheet.get_Range("E9", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.LocalidadProvincia = range.Text.ToString();
                }
                range = sheet.get_Range("E10", Missing.Value);
                if (range != null)
                {
                    headerEmpresa.Contacto = range.Text.ToString();
                }
                result.Empresa = headerEmpresa;
                var headerCliente = new AlcemarHeaderClienteMetadataViewModel();
                range = sheet.get_Range("B7", Missing.Value);
                if (range != null)
                {
                    headerCliente.Nombre = range.Text.ToString();
                }
                range = sheet.get_Range("B8", Missing.Value);
                if (range != null)
                {
                    headerCliente.Obra = range.Text.ToString();
                }
                range = sheet.get_Range("B12", Missing.Value);
                if (range != null)
                {
                    headerCliente.NumeroPresupuesto = range.Text.ToString();
                }
                range = sheet.get_Range("B14", Missing.Value);
                if (range != null)
                {
                    headerCliente.Tratamiento = range.Text.ToString();
                }
                //range = excel.get_Range("A10", "R65536");
                //currentFind = range.Find("NOMBRE PROYECTO:*", Missing.Value,
                //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                //    Missing.Value, Missing.Value);
                //if (currentFind != null)
                //{
                //    find01FirstRowNumber = currentFind.Row;
                //    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                //    headerCliente.NombreProyecto = currentFind.Text.ToString();
                //}
                //else
                //{
                //    throw new Exception("No se ha podido encontrar la seccion NOMBRE PROYECTO");
                //}
                //range = sheet.get_Range("T" + find01FirstRowNumber, Missing.Value);
                //if (range != null)
                //{
                //    headerCliente.Version = range.Text.ToString();
                //}
                //range = excel.get_Range("A" + find01LastRowNumber, "R65536");
                //currentFind = range.Find("UBICACIÓN DEL PROYECTO:*", Missing.Value,
                //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                //    Missing.Value, Missing.Value);
                //if (currentFind != null)
                //{
                //    find01FirstRowNumber = currentFind.Row;
                //    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                //    headerCliente.UbicacionProyecto = currentFind.Text.ToString();
                //}
                //else
                //{
                //    throw new Exception("No se ha podido encontrar la seccion UBICACIÓN DEL PROYECTO");
                //}
                //range = sheet.get_Range("T" + find01FirstRowNumber, Missing.Value);
                //if (range != null)
                //{
                //    headerCliente.Fecha = range.Text.ToString();
                //}
                //range = excel.get_Range("A" + find01LastRowNumber, "R65536");
                //currentFind = range.Find("ATENCIÓN A:*", Missing.Value,
                //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                //    Missing.Value, Missing.Value);
                //if (currentFind != null)
                //{
                //    find01FirstRowNumber = currentFind.Row;
                //    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                //    headerCliente.AtencionA = currentFind.Text.ToString();
                //}
                //else
                //{
                //    throw new Exception("No se ha podido encontrar la seccion ATENCIÓN A");
                //}
                result.Cliente = headerCliente;
                //range = excel.get_Range("A" + find01LastRowNumber, "AK65536");
                //currentFind = range.Find("De acuerdo a su requerimiento*", Missing.Value,
                //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                //    Missing.Value, Missing.Value);
                //if (currentFind != null)
                //{
                //    find01FirstRowNumber = currentFind.Row;
                //    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                //    result.ParrafoIntroductorio = currentFind.Text.ToString();
                //}
                //else
                //{
                //    throw new Exception("No se ha podido encontrar la seccion Parrafo Introductorio de Items");
                //}

                // Buscando el punto de partida para encontrar los items
                range = excel.get_Range("B1", "B65536");
                currentFind = range.Find("Detalle de Tip*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                }
                else
                {
                    throw new Exception("No se ha podido encontrar punto de partida para iterar sobre items");
                }

                var items = new List<AlcemarItemMetadataViewModel>();
                int firstItemRowNumber = -1;
                while (true)
                {
                    range = excel.get_Range("B" + (find01LastRowNumber + 1), "B65536");
                    currentFind = range.Find("Cantidad", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        itemStartRowNumber = find01FirstRowNumber - 1;

                        if (firstItemRowNumber == -1)
                        {
                            firstItemRowNumber = itemStartRowNumber;
                        }
                        else if (itemStartRowNumber == firstItemRowNumber)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    var item = new AlcemarItemMetadataViewModel();
                    range = sheet.get_Range("B" + itemStartRowNumber, Missing.Value);
                    if (range != null)
                    {
                        item.Posicion = range.Text.ToString();
                    }
                    range = sheet.get_Range("B" + (itemStartRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.Cantidad = range.Text.ToString();
                    }
                    range = sheet.get_Range("B" + (itemStartRowNumber + 2), Missing.Value);
                    if (range != null)
                    {
                        item.Descripcion = range.Text.ToString();
                    }

                    // Carpiteria
                    range = excel.get_Range("B" + itemStartRowNumber, "B" + (itemStartRowNumber + 5));
                    currentFind = range.Find("Valor Carpin*", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        item.HeaderCarpinteria = currentFind.Text.ToString();
                    }
                    range = sheet.get_Range("D" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.UnitarioCarpinteria = range.Text.ToString();
                    }
                    range = sheet.get_Range("E" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.TotalCarpinteria = range.Text.ToString();
                    }
                    //range = sheet.get_Range("B" + (itemStartRowNumber + 3), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.HeaderCarpinteria = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("D" + (itemStartRowNumber + 3), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.UnitarioCarpinteria = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("E" + (itemStartRowNumber + 3), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.TotalCarpinteria = range.Text.ToString();
                    //}

                    // Vidrios
                    range = sheet.get_Range("B" + (find01LastRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.HeaderVidrios = range.Text.ToString();
                    }
                    range = sheet.get_Range("D" + (find01LastRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.UnitarioVidrios = range.Text.ToString();
                    }
                    range = sheet.get_Range("E" + (find01LastRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.TotalVidrios = range.Text.ToString();
                    }

                    // Tapajuntas
                    range = excel.get_Range("B" + find01LastRowNumber, "B" + (find01LastRowNumber + 5));
                    currentFind = range.Find("Tapajun*", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        item.HeaderTapajuntas = currentFind.Text.ToString();
                    }
                    range = sheet.get_Range("D" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.UnitarioTapajuntas = range.Text.ToString();
                    }
                    range = sheet.get_Range("E" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.TotalTapajuntas = range.Text.ToString();
                    }
                    //range = sheet.get_Range("B" + (itemStartRowNumber + 5), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.HeaderTapajuntas = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("D" + (itemStartRowNumber + 5), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.UnitarioTapajuntas = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("E" + (itemStartRowNumber + 5), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.TotalTapajuntas = range.Text.ToString();
                    //}

                    // Colocacion
                    range = excel.get_Range("B" + find01LastRowNumber, "B" + (find01LastRowNumber + 5));
                    currentFind = range.Find("Colocac*", Missing.Value,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        Missing.Value, Missing.Value);
                    if (currentFind != null)
                    {
                        find01FirstRowNumber = currentFind.Row;
                        find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                        item.HeaderColocacion = currentFind.Text.ToString();
                    }
                    range = sheet.get_Range("D" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.UnitarioColocacion = range.Text.ToString();
                    }
                    range = sheet.get_Range("E" + (find01LastRowNumber), Missing.Value);
                    if (range != null)
                    {
                        item.TotalColocacion = range.Text.ToString();
                    }
                    //range = sheet.get_Range("B" + (itemStartRowNumber + 6), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.HeaderColocacion = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("D" + (itemStartRowNumber + 6), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.UnitarioColocacion = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("E" + (itemStartRowNumber + 6), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.TotalColocacion = range.Text.ToString();
                    //}

                    // Item
                    range = sheet.get_Range("D" + (find01LastRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.Unitario = range.Text.ToString();
                    }
                    range = sheet.get_Range("E" + (find01LastRowNumber + 1), Missing.Value);
                    if (range != null)
                    {
                        item.Total = range.Text.ToString();
                    }

                    //range = excel.get_Range("AN" + (itemStartRowNumber + 7), "AP65536");
                    //currentFind = range.Find("*UNITARIO*", Missing.Value,
                    //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    //    Missing.Value, Missing.Value);
                    //if (currentFind != null)
                    //{
                    //    find01FirstRowNumber = currentFind.Row;
                    //    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    //    item.HeaderUnitario = currentFind.Text.ToString();
                    //}
                    //range = sheet.get_Range("AQ" + find01FirstRowNumber, Missing.Value);
                    //if (range != null)
                    //{
                    //    item.HeaderUnidades = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("AS" + find01FirstRowNumber, Missing.Value);
                    //if (range != null)
                    //{
                    //    item.HeaderTotal = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("AN" + (find01FirstRowNumber + 2), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.PrecioUnitario = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("AQ" + (find01FirstRowNumber + 2), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.Unidades = range.Text.ToString();
                    //}
                    //range = sheet.get_Range("AS" + (find01FirstRowNumber + 2), Missing.Value);
                    //if (range != null)
                    //{
                    //    item.PrecioTotal = range.Text.ToString();
                    //}
                    item.Tipologia = imagenes.Where(x =>
                        sheet.get_Range("A" + itemStartRowNumber, Missing.Value).Top < x.Top &&
                        sheet.get_Range("A" + (itemStartRowNumber + 13), Missing.Value).Top > x.Top).Single();
                    items.Add(item);
                }
                result.Items = items;
                var resumen = new AlcemarResumenMetadataViewModel();
                range = excel.get_Range("B" + find01LastRowNumber, "B65536");
                currentFind = range.Find("Total Carp*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    resumen.TotalCarpinteria = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion Total Carpinteria");
                }
                range = sheet.get_Range("B" + (find01FirstRowNumber + 1), Missing.Value);
                if (range != null)
                {
                    resumen.TotalVidrios = range.Text.ToString();
                }
                range = sheet.get_Range("B" + (find01FirstRowNumber + 2), Missing.Value);
                if (range != null)
                {
                    resumen.TotalContramarcos = range.Text.ToString();
                }
                range = sheet.get_Range("B" + (find01FirstRowNumber + 3), Missing.Value);
                if (range != null)
                {
                    resumen.TotalColocacion = range.Text.ToString();
                }

                range = excel.get_Range("D" + find01LastRowNumber, "D65536");
                currentFind = range.Find("Precio*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    resumen.HeaderPrecio = currentFind.Text.ToString();
                }
                else
                {
                    throw new Exception("No se ha podido encontrar la seccion Precio");
                }
                range = sheet.get_Range("E" + find01FirstRowNumber, Missing.Value);
                if (range != null)
                {
                    resumen.Precio = range.Text.ToString();
                }
                range = sheet.get_Range("D" + (find01FirstRowNumber + 1), Missing.Value);
                if (range != null)
                {
                    resumen.HeaderIva = range.Text.ToString();
                }
                range = sheet.get_Range("E" + (find01FirstRowNumber + 1), Missing.Value);
                if (range != null)
                {
                    resumen.Iva = range.Text.ToString();
                }
                range = sheet.get_Range("D" + (find01FirstRowNumber + 2), Missing.Value);
                if (range != null)
                {
                    resumen.HeaderFinal = range.Text.ToString();
                }
                range = sheet.get_Range("E" + (find01FirstRowNumber + 2), Missing.Value);
                if (range != null)
                {
                    resumen.Final = range.Text.ToString();
                }

                range = excel.get_Range("B" + find01LastRowNumber, "B65536");
                currentFind = range.Find("Validez*", Missing.Value,
                    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                    Missing.Value, Missing.Value);
                if (currentFind != null)
                {
                    find01FirstRowNumber = currentFind.Row;
                    find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
                    resumen.LapsoValidez = currentFind.Text.ToString();
                }
                range = sheet.get_Range("B" + (find01FirstRowNumber + 1), Missing.Value);
                if (range != null)
                {
                    resumen.PlazoEntrega = range.Text.ToString();
                }

                result.Resumen = resumen;

                //// Guardar Datos
                var directory = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
                var storeDirectory = new DirectoryInfo(directory);
                if (!storeDirectory.Exists)
                {
                    storeDirectory.Create();
                    using (StreamWriter file = System.IO.File.CreateText(Path.Combine(directory, "metadata.json")))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, result);
                    }
                    imagenes.ForEach(x => x.Image.Save(Path.Combine(directory, x.Name + ".png"), ImageFormat.Png));
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
                        Newtonsoft.Json.JsonConvert.SerializeObject(ex) + System.Environment.NewLine, System.Diagnostics.EventLogEntryType.Error, 1, short.MaxValue);

                return null;
            }
            finally
            {
                if (wb != null)
                {
                    ExcelTools.ReleaseRCM(wb);
                }
                if (excel != null)
                {
                    ExcelTools.ReleaseRCM(excel);
                }
            }
        }

        //private RehauExcelMetadataViewModel __ProcesarExcelAlcemar(Guid archivoId)
        //{
        //    // Obtener archivo
        //    var archivo = this.db.Archivo.Find(archivoId);
        //    var filename = archivo.Ubicacion;

        //    Excel.Application excel = null;
        //    Excel.Workbook wb = null;

        //    try
        //    {
        //        // Instancia de Excel y apertura de archivo
        //        // DA ERROR excel = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
        //        System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
        //        // Matar procesos de EXCEL
        //        foreach (System.Diagnostics.Process p in process)
        //        {
        //            if (!string.IsNullOrEmpty(p.ProcessName))
        //            {
        //                try
        //                {
        //                    p.Kill();
        //                }
        //                catch { }
        //            }
        //        }
        //        if (excel == null)
        //        {
        //            excel = new Excel.Application();
        //        }
        //        // ERROR wb = ExcelTools.OpenBook(excel, filename);
        //        try
        //        {
        //            wb = excel.Workbooks.Add(filename);
        //        }
        //        catch
        //        {
        //            wb = excel.Workbooks.Open(filename);
        //        }

        //        // La primer hoja es 1 no 0
        //        Excel.Worksheet sheet = wb.Sheets[1] as Excel.Worksheet;
        //        Excel.Range range = null;
        //        Excel.Range currentFind = null;

        //        int find01FirstRowNumber = -1;
        //        int find01LastRowNumber = -1;
        //        int itemStartRowNumber = -1;

        //        if (sheet == null)
        //        {
        //            throw new Exception("No se encontro hoja en libro Excel");
        //        }

        //        // Extraccion de datos
        //        var result = new RehauExcelMetadataViewModel();
        //        // NO SE PUEDE HACER COPIA AL CLIPBOARD EN UNA APP WEB
        //        var imagenes = new List<RehauItemShapeMetadataViewModel>();
        //        //int count = 0;

        //        foreach (Excel.Shape shape in sheet.Shapes)
        //        {
        //            //count = count + 1;
        //            //if (count <= sheet.Shapes.Count - 1)
        //            //{
        //                shape.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
        //                Bitmap image = null;
        //                var thread = new System.Threading.Thread(() =>
        //                {
        //                    var bitmapSource = (System.Windows.Clipboard.ContainsImage() ? System.Windows.Clipboard.GetImage() : null);
        //                    image = GetBitmap(bitmapSource);
        //                });
        //                thread.SetApartmentState(System.Threading.ApartmentState.STA);
        //                thread.Start();
        //                thread.Join();
        //                imagenes.Add(new RehauItemShapeMetadataViewModel()
        //                {
        //                    Top = shape.Top,
        //                    Left = shape.Left,
        //                    Name = shape.Name,
        //                    Image = image
        //                });
        //            //}
        //        }
        //        var headerEmpresa = new RehauHeaderEmpresaMetadataViewModel();
        //        range = sheet.get_Range("E7", Missing.Value);
        //        if (range != null)
        //        {
        //            headerEmpresa.Nombre = range.Text.ToString();
        //        }
        //        range = sheet.get_Range("E8", Missing.Value);
        //        if (range != null)
        //        {
        //            headerEmpresa.Direccion = range.Text.ToString();
        //        }
        //        range = sheet.get_Range("E8", Missing.Value);
        //        if (range != null)
        //        {
        //            string textDir = range.Text.ToString();
        //            string CpLocalidadProvincia = textDir.Split('(', ')')[1];
        //            headerEmpresa.CpLocalidadProvincia = CpLocalidadProvincia;
        //        }
        //        range = sheet.get_Range("E10", Missing.Value);
        //        if (range != null)
        //        {
        //            string textContacto = range.Text.ToString();
        //            string telefono = textContacto.Substring(0, 12);
        //            headerEmpresa.TelefonoFax = telefono;
        //        }
        //        range = sheet.get_Range("E10", Missing.Value);
        //        if (range != null)
        //        {
        //            string textContacto = range.Text.ToString();
        //            string email = textContacto.Substring(13,37);
        //            headerEmpresa.Email = range.Text.ToString();
        //        }
        //        result.Empresa = headerEmpresa;
        //        var headerCliente = new RehauHeaderClienteMetadataViewModel();
        //        range = sheet.get_Range("B7", Missing.Value);
        //        if (range != null)
        //        {
        //            string textCliente = range.Text.ToString();
        //            string cliente = textCliente.Replace("Sr:", "");
        //            headerCliente.Nombre = cliente;
        //        }
        //        range = sheet.get_Range("B12", Missing.Value);
        //        if (range != null)
        //        {
        //            string textPresupuesto = range.Text.ToString();
        //            string numPresupuesto = textPresupuesto.Replace("Presupuesto n°", "");
        //            headerCliente.NumeroPresupuesto = numPresupuesto;
        //        }
        //        range = excel.get_Range("B8");
        //        currentFind = range.Find("Obra:*", Missing.Value,
        //            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //            Missing.Value, Missing.Value);
        //        if (currentFind != null)
        //        {
        //            find01FirstRowNumber = currentFind.Row;
        //            find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
        //            headerCliente.NombreProyecto = currentFind.Text.ToString();
        //        }
        //        else
        //        {
        //            throw new Exception("No se ha podido encontrar la seccion NOMBRE PROYECTO");
        //        }
        //        range = sheet.get_Range("T" + find01FirstRowNumber, Missing.Value);
        //        if (range != null)
        //        {
        //            headerCliente.Version = range.Text.ToString();
        //        }
        //        range = excel.get_Range("A" + find01LastRowNumber, "R65536");
        //        currentFind = range.Find("Obra:*", Missing.Value,
        //            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //            Missing.Value, Missing.Value);
        //        if (currentFind != null)
        //        {
        //            find01FirstRowNumber = currentFind.Row;
        //            find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
        //            headerCliente.UbicacionProyecto = currentFind.Text.ToString();
        //        }
        //        else
        //        {
        //            throw new Exception("No se ha podido encontrar la seccion UBICACIÓN DEL PROYECTO");
        //        }
        //        range = sheet.get_Range("E1", Missing.Value);
        //        if (range != null)
        //        {
        //            headerCliente.Fecha = range.Text.ToString();
        //        }
        //        range = excel.get_Range("A" + find01LastRowNumber, "R65536");

        //        var items = new List<RehauItemMetadataViewModel>();
        //        int firstItemRowNumber = -1;

        //        int i = 1;
        //        while (true)
        //        {
        //            range = excel.get_Range("A" + (find01LastRowNumber + 1), "G65536");
        //            currentFind = range.Find("Cantidad:*", Missing.Value,
        //                Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //                Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //                Missing.Value, Missing.Value);
        //            if (currentFind != null)
        //            {
        //                find01FirstRowNumber = currentFind.Row;
        //                find01LastRowNumber = currentFind.Row + currentFind.Rows.Count - 1;
        //                itemStartRowNumber = find01FirstRowNumber;

        //                if (firstItemRowNumber == -1)
        //                {
        //                    firstItemRowNumber = itemStartRowNumber;
        //                }
        //                else if (itemStartRowNumber == firstItemRowNumber)
        //                {
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                break;
        //            }
        //            var item = new RehauItemMetadataViewModel();
        //            range = sheet.get_Range("B" + (itemStartRowNumber - 1), Missing.Value);
        //            if (range != null)
        //            {
        //                item.NumeroItem = range.Text.ToString();
        //            }
        //            range = sheet.get_Range("B" + (itemStartRowNumber - 1), Missing.Value);
        //            if (range != null)
        //            {
        //                item.Posicion = range.Text.ToString();
        //            }
        //            range = sheet.get_Range("B" + (itemStartRowNumber + 1), Missing.Value);
        //            if (range != null)
        //            {
        //                item.Color = range.Text.ToString();
        //            }
        //            range = sheet.get_Range("B" + (itemStartRowNumber + 2), Missing.Value);
        //            if (range != null)
        //            {
        //                item.HeaderDescripcion = range.Text.ToString();
        //            }
        //            range = sheet.get_Range("B" + (find01FirstRowNumber), Missing.Value);
        //            if (range != null)
        //            {
        //                string textUnidad = range.Text.ToString();
        //                string unidad = textUnidad.Replace("Cantidad:","").Replace(" ","");
        //                item.HeaderUnidades = unidad;
        //                item.Unidades = unidad;
        //            }
        //            range = sheet.get_Range("D" + (find01FirstRowNumber + 2), Missing.Value);
        //            if (range != null)
        //            {
        //                string textCarpinteria = range.Text.ToString();
        //                string carpinteria = textCarpinteria.Replace("€", "");
        //                item.Carpinteria = carpinteria;
        //            }
        //            range = sheet.get_Range("D" + (find01FirstRowNumber + 4), Missing.Value);
        //            if (range != null)
        //            {
        //                string textTapajunta = range.Text.ToString();
        //                string tapajunta = textTapajunta.Replace("€", "");
        //                item.Tapajunta = tapajunta;
        //            }
        //            if(item.Tapajunta != string.Empty && item.Carpinteria != string.Empty)
        //            {
        //                var unitario = Convert.ToDecimal(item.Tapajunta) + Convert.ToDecimal(item.Carpinteria);
        //                item.PrecioUnitario = unitario.ToString();
        //            }
        //           i = i + 1;
        //            item.Tipologia = imagenes
        //                .Where(x => x.Name.Replace("Imagen","").Contains(i.ToString()))
        //                .FirstOrDefault();
        //            items.Add(item);
        //        }
        //        result.Items = items;
        //        var resumen = new RehauResumenMetadataViewModel();
        //        range = excel.get_Range("D" + find01LastRowNumber, "D65536");
        //        currentFind = range.Find("Precio:*", Missing.Value,
        //            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //            Missing.Value, Missing.Value);
        //        if (currentFind != null)
        //        {
        //            string totalNeto = (excel.Cells[currentFind.Row, currentFind.Column + 1] as Excel.Range).Value.ToString();
        //            resumen.TotalNeto = totalNeto;
        //        }
        //        else
        //        {
        //            throw new Exception("No se ha podido encontrar la seccion TOTAL NETO");
        //        }
        //        resumen.TotalUnidades = items.Count.ToString();
        //        range = excel.get_Range("D" + find01LastRowNumber, "D65536");
        //        currentFind = range.Find("Iva:*", Missing.Value,
        //            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //            Missing.Value, Missing.Value);
        //        if (currentFind != null)
        //        {
        //            string iva = (excel.Cells[currentFind.Row, currentFind.Column + 1] as Excel.Range).Value.ToString();
        //            resumen.Iva = iva;
        //        }
        //        else
        //        {
        //            throw new Exception("No se ha podido encontrar la seccion TOTAL NETO");
        //        }
        //        range = excel.get_Range("D" + find01LastRowNumber, "D65536");
        //        currentFind = range.Find("Final:*", Missing.Value,
        //            Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        //            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
        //            Missing.Value, Missing.Value);
        //        if (currentFind != null)
        //        {
        //            string total = (excel.Cells[currentFind.Row, currentFind.Column + 1] as Excel.Range).Value.ToString();
        //            resumen.TotalProyecto = total;
        //        }
        //        else
        //        {
        //            throw new Exception("No se ha podido encontrar la seccion TOTAL NETO");
        //        }

        //        result.Resumen = resumen;

        //        // Guardar Datos
        //        var directory = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
        //        var storeDirectory = new DirectoryInfo(directory);
        //        if (!storeDirectory.Exists)
        //        {
        //            storeDirectory.Create();
        //            using (StreamWriter file = System.IO.File.CreateText(Path.Combine(directory, "metadata.json")))
        //            {
        //                JsonSerializer serializer = new JsonSerializer();
        //                //serialize object directly into file stream
        //                serializer.Serialize(file, result);
        //            }
        //            imagenes.ForEach(x => x.Image.Save(Path.Combine(directory, x.Name + ".png"), ImageFormat.Png));
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
        //                Newtonsoft.Json.JsonConvert.SerializeObject(ex) + System.Environment.NewLine, System.Diagnostics.EventLogEntryType.Error, 1, short.MaxValue);

        //        return null;
        //    }
        //    finally
        //    {
        //        if (wb != null)
        //        {
        //            ExcelTools.ReleaseRCM(wb);
        //        }
        //        if (excel != null)
        //        {
        //            ExcelTools.ReleaseRCM(excel);
        //        }
        //    }
        //}

        private static Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);

            bmp.UnlockBits(data);
            return bmp;
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuesto presupuesto = db.Presupuesto.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Presupuesto presupuesto = db.Presupuesto.Find(id);
            db.Presupuesto.Remove(presupuesto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult HtmlCamposDeItem()
        {
            return PartialView("HtmlCamposDeItem");
        }

        public ActionResult HtmlFilaDeItem()
        {
            return PartialView("HtmlFilaDeItem");
        }

        [HttpPost]
        public JsonResult JsonUploadTipologia(Guid transaccionId)
        {
            bool isSavedSuccessfully = true;
            object result = null;

            try
            {
                foreach (string key in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[key];

                    if (file != null && file.ContentLength > 0)
                    {
                        var storeDirectory = new DirectoryInfo(Constants.PathTipologia);

                        if (!storeDirectory.Exists)
                        {
                            storeDirectory.Create();
                        }

                        var nombreOriginal = file.FileName;
                        var nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(nombreOriginal);
                        var ubicacion = Path.Combine(Server.MapPath(storeDirectory.ToString()), nombre);
                        var fechaHoraSubida = DateTime.Now;

                        file.SaveAs(ubicacion);
                        Thumbnail.GenerateThumbnail(new FileInfo(ubicacion), Constants.ThumbnailSizes[3], new DirectoryInfo(Constants.PathTipologiaThumbnailDirs[3]));

                        var mimeType = MimeMapping.GetMimeMapping(ubicacion);

                        var archivo = new Archivo
                        {
                            Id = Guid.NewGuid(),
                            Nombre = nombre,
                            NombreOriginal = nombreOriginal,
                            MimeType = mimeType,
                            Ubicacion = ubicacion,
                            FechaHoraSubida = fechaHoraSubida,
                            TransaccionId = transaccionId,
                            TransaccionCompletada = false
                        };

                        this.db.Archivo.Add(archivo);
                        this.db.SaveChanges();

                        result = new
                        {
                            id = archivo.Id,
                            url = Url.Action("Get", "Archivo", new { Id = archivo.Id }),
                            thumbnailUrl = Url.Action("GetThumbnail", "Archivo", new { Id = archivo.Id, sizeClass = 3 })
                        };
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                result = new { error = "Error al intentar procesar el archivo" };
            }

            if (isSavedSuccessfully)
            {
                return Json(result);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(result);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ReportePresupuesto(Guid PresupuestoId)
        {
            Presupuesto presupuesto = presupuestoService.GetPresupuesto(PresupuestoId);

            Subrubro subrubro = null;

            if (presupuesto.SubrubroId != null)
            {
                subrubro = subrubroService.GetSubrubro(presupuesto.SubrubroId.Value);
            }

            ceya.Model.Models.Color color = null;

            if (presupuesto.ColorId != null)
            {
                color = colorService.GetColor(presupuesto.ColorId.Value);
            }

            Precio precio = null;

            if (presupuesto.ColocacionId != null)
            {
                precio = precioService.GetPrecio(presupuesto.ColocacionId.Value);
            }

            string SubrubroNombre = subrubro != null ? subrubro.Descripcion.ToUpper() : string.Empty;

            string ColorNombre = color != null ? color.Descripcion.ToUpper() : string.Empty;

            string ProductoNombre = precio != null ? precio.Producto.Descripcion.ToUpper() : string.Empty;

            string Obra = presupuesto.Obra != null && presupuesto.Obra.Nombre != null ? presupuesto.Obra.Nombre.ToUpper() : string.Empty;

            string Ubicacion = presupuesto.Obra != null && presupuesto.Obra.Domicilio != null ? presupuesto.Obra.Domicilio.ToUpper() : string.Empty;

            string Email = (presupuesto.Solicita == null ? presupuesto.Cliente.Email : presupuesto.Email ?? String.Empty).ToUpper();

            string Fecha = presupuesto.Fecha.ToShortDateString();

            string NombreCliente = presupuesto.Cliente.RazonSocial != String.Empty ? presupuesto.Cliente.Apellido + "," + presupuesto.Cliente.Nombre : presupuesto.Cliente.RazonSocial;
            string Solicita = ((presupuesto.Solicita == null ? NombreCliente : presupuesto.Solicita) ?? String.Empty).ToUpper();
            
            string Telefono = presupuesto.Solicita == null ? presupuesto.Cliente.Telefono + " " +  presupuesto.Cliente.Celular : presupuesto.Telefono ?? String.Empty;

            string LineaColor = ProductoNombre + " - " + SubrubroNombre + " - " + ColorNombre;

            //string ObservacionFooter = "Observación: " + presupuesto.DescripcionFooter;
            string ObservacionFooter = presupuesto.DescripcionFooter;

            string TotalVidrio = "$ " + presupuesto.ResumenVidrios.ToString("F");

            string TotalColocacion = "$ " + presupuesto.ResumenColocacion.ToString("F");

            string TotalTapajuntas = "$ " + presupuesto.ResumenTapajuntas.ToString("F");

            string ConceptosVarios = presupuesto.ConceptosVarios != null ? "TOTAL " + presupuesto.ConceptosVarios.ToUpper() : string.Empty;

            string TotalVarios = presupuesto.ResumenVarios > 0 ? "$ " + presupuesto.ResumenVarios.ToString("F") : string.Empty;

            string SubtotalSinIva = "$ " + presupuesto.ResumenSubtotal.ToString("F");

            string TotalIva = "$ " + presupuesto.ResumenIva.ToString("F");

            string TotalCarpinteria = "$ " + presupuesto.ResumenCarpinteria.ToString("F");

            string SubTotalComplementos = "$ " + (presupuesto.ResumenSubtotal + presupuesto.ResumenVidrios +
                                    presupuesto.ResumenColocacion + presupuesto.ResumenCarpinteria +
                                    presupuesto.ResumenTapajuntas).ToString("F");

            string Total = "$ " + (presupuesto.ResumenSubtotal + presupuesto.ResumenIva).ToString("F");

            string ObservacionHeader = presupuesto.DescripcionHeader;
            string CodPresupuesto = string.Concat("00000000", Convert.ToString(presupuesto.Codigo));
            string CodigoPresupuesto = CodPresupuesto.Substring(CodPresupuesto.Length - 8);
            string CodigoObra = presupuesto.Obra.CodigoObra;

                                            
            string DirectorioReportesRelativo = "~/";

            string urlArchivo = string.Format("{0}.{1}", "PresupuestoReport", "rdlc");

            string FullPathReport = string.Format("{0}{1}",
                                    this.HttpContext.Server.MapPath(DirectorioReportesRelativo),
                                     urlArchivo);

            ReportViewer Reporte = new ReportViewer();

            Reporte.LocalReport.ReportPath = "Reports/PresupuestoReport.rdlc";//FullPathReport;
            /*Configuramos los datos al dataset dentro del reporte*/

            ReportParameter[] p = new ReportParameter[20];

            p[0] = new ReportParameter("OBRA", Obra);
            p[1] = new ReportParameter("UBICACION", Ubicacion);
            p[2] = new ReportParameter("MAIL", Email);
            p[3] = new ReportParameter("FECHA", Fecha);
            p[4] = new ReportParameter("SOLICITA", Solicita);
            p[5] = new ReportParameter("TELEFONO", Telefono);
            p[6] = new ReportParameter("LINEACOLOR", LineaColor);
            p[7] = new ReportParameter("OBSERVACIONFOOTER", ObservacionFooter);
            p[8] = new ReportParameter("TOTALITEMS", TotalCarpinteria);
            p[9] = new ReportParameter("TOTALVIDRIO", TotalVidrio);
            p[10] = new ReportParameter("TOTALCOLOCACION", TotalColocacion);
            p[11] = new ReportParameter("TOTALTAPAJUNTA", TotalTapajuntas);
            p[12] = new ReportParameter("CONCEPTOSVARIOS", ConceptosVarios);
            p[13] = new ReportParameter("TOTALVARIOS", TotalVarios);
            p[14] = new ReportParameter("SUBTOTAL", SubtotalSinIva);
            p[15] = new ReportParameter("IVA", TotalIva);
            p[16] = new ReportParameter("PTOTAL", Total);
            p[17] = new ReportParameter("OBSERVACIONHEADER", ObservacionHeader);
            p[18] = new ReportParameter("CODIGOPRESUPUESTO", CodigoPresupuesto);
            p[19] = new ReportParameter("CODIGOOBRA", CodigoObra);

            Reporte.LocalReport.EnableExternalImages = true;
            Reporte.LocalReport.SetParameters(p);
            List<PresupuestoItem> items = null;
            if (presupuesto.PresupuestoItem.All(x => x.Archivo != null))
            {
                items = presupuesto.PresupuestoItem.OrderBy(x => x.Archivo.FechaHoraSubida)
                    .Select(x =>
                    new PresupuestoItem
                    {
                        Id = x.Id,
                        PresupuestoId = x.PresupuestoId,
                        ArchivoTipologiaId = x.ArchivoTipologiaId,
                        NumeroPosicion = x.NumeroPosicion,
                        Posicion = x.Posicion,
                        //                        Descripcion = x.Posicion + "\r\n" + (x.PrecioVidrio != null ? x.PrecioVidrio.Producto.Descripcion : string.Empty) + "\r\n" +
                        //                                  (x.PrecioColocacion != null ? x.PrecioColocacion.Producto.Descripcion : string.Empty) + "\r\n" +
                        //                                  x.Descripcion + "\r\n" + x.Ancho.ToString("N2") + " x " + x.Alto.ToString("N2") + "\r\n" + x.Detalle,

                        Descripcion = x.Posicion + "\r\n" + x.Descripcion + "\r\n" + x.Ancho.ToString("N2") + " x " + x.Alto.ToString("N2") + "\r\n" + x.Detalle +
                                      (x.PrecioVidrio != null ? x.PrecioVidrio.Producto.Descripcion : string.Empty),


                        Unidades = x.Unidades,
                        PrecioUnitario = Decimal.Round(x.PrecioUnitario, 2),
                        VidriosId = x.VidriosId,
                        VidriosPrecio = x.VidriosPrecio,
                        ColocacionId = x.ColocacionId,
                        ColocacionPrecio = x.ColocacionPrecio,
                        Ancho = x.Ancho,
                        Alto = x.Alto,
                        Carpinteria = x.Carpinteria,
                        Tapajuntas = x.Tapajuntas,
                        VidriosCalculado = x.VidriosCalculado,
                        ColocacionCalculado = x.ColocacionCalculado,
                        Detalle = x.Detalle,
                        Importe = Decimal.Round(x.Importe, 2),
                        Estado = x.Estado

                    }).ToList();
            }
            else
            {
                var lineas = presupuesto.PresupuestoItem.OrderBy(x => x.Posicion);
                items = new List<PresupuestoItem>();
                foreach (var x in lineas)
                {
                    PresupuestoItem item = new PresupuestoItem();
                    item.Id = x.Id;
                    item.PresupuestoId = x.PresupuestoId;
                    item.ArchivoTipologiaId = x.ArchivoTipologiaId;
                    item.NumeroPosicion = x.NumeroPosicion;
                    item.Posicion = x.Posicion;
                    if (x.PrecioVidrio != null && x.PrecioColocacion != null)
                    {
                        //  item.Descripcion = x.Posicion + "\r\n" + (x.PrecioVidrio != null ? x.PrecioVidrio.Producto.Descripcion : string.Empty) + "\r\n" +
                        //                     (x.PrecioColocacion != null ? x.PrecioColocacion.Producto.Descripcion : string.Empty) + "\r\n" +
                        //                     x.Descripcion + "\r\n" + x.Ancho.ToString("N2") + " x " + x.Alto.ToString("N2") + "\r\n" + x.Detalle;

                        item.Descripcion = x.Posicion + "\r\n" + x.Descripcion + "\r\n" + x.Ancho.ToString("N2") + " x " + x.Alto.ToString("N2") + "\r\n" + x.Detalle +
                                           (x.PrecioVidrio != null ? x.PrecioVidrio.Producto.Descripcion : string.Empty);
                                             

                    }
                    else
                    {
                        item.Descripcion = x.Posicion + "\r\n" + x.Descripcion + "\r\n" + x.Ancho.ToString("N2") + " x " + x.Alto.ToString("N2") + "\r\n" + x.Detalle;

                    }
                    item.Unidades = x.Unidades;
                    item.PrecioUnitario = Decimal.Round(x.PrecioUnitario, 2);
                    item.VidriosId = x.VidriosId;
                    item.VidriosPrecio = x.VidriosPrecio;
                    item.ColocacionId = x.ColocacionId;
                    item.ColocacionPrecio = x.ColocacionPrecio;
                    item.Ancho = x.Ancho;
                    item.Alto = x.Alto;
                    item.Carpinteria = x.Carpinteria;
                    item.Tapajuntas = x.Tapajuntas;
                    item.VidriosCalculado = x.VidriosCalculado;
                    item.ColocacionCalculado = x.ColocacionCalculado;
                    item.Detalle = x.Detalle;
                    item.Importe = Decimal.Round(x.Importe, 2);
                    item.Estado = x.Estado;
                    items.Add(item);
                }
            }
            ReportDataSource DataSource = new ReportDataSource("DSPresupuestoItem", items);
            Reporte.LocalReport.DataSources.Add(DataSource);
            Reporte.LocalReport.Refresh();

            var archivos = archivoService.GetArchivosPorTransaccion(presupuesto.ArchivoTransaccionId).OrderBy(x => x.FechaHoraSubida);

            ReportDataSource DataSource1 = new ReportDataSource("DataSet1", archivos);
            Reporte.LocalReport.DataSources.Add(DataSource1);
            Reporte.LocalReport.Refresh();
            /*Puedes seleccionar otro formato si quieres*/
            byte[] file = Reporte.LocalReport.Render("PDF");

            return File(new MemoryStream(file).ToArray(),
                      System.Net.Mime.MediaTypeNames.Application.Octet,
            /*Esto para forzar la descarga del archivo*/
            string.Format("{0}{1}", "Presupuesto" + presupuesto.Cliente.RazonSocial + presupuesto.Cliente.Apellido + presupuesto.Cliente.Nombre, ".PDF"));


        }

        public ActionResult EnBase(Guid id)
        {
            var presupuesto = this.presupuestoService.GetPresupuesto(id);

            // items
            var presupuestoItems = this.presupuestoItemService.GetItems(id);

            if (presupuesto == null)
            {
                return HttpNotFound();
            }

            // archivos
            var presupuestoArchivos = this.archivoService.GetArchivosPorTransaccion(presupuesto.ArchivoTransaccionId);

            // mapeos
            var editVM = Mapper.Map<Presupuesto, PresupuestoFormModel>(presupuesto);
            editVM.Items = Mapper.Map<IEnumerable<PresupuestoItem>, IEnumerable<PresupuestoItemFormModel>>(presupuestoItems,
                options => options.AfterMap((src, dest) =>
                {
                    foreach (var itemModel in dest)
                    {
                        if (itemModel.TipologiaId == Guid.Empty || itemModel.TipologiaId == null)
                        {
                            return;
                        }
                        var archivo = presupuestoArchivos.Where(x => x.Id == itemModel.TipologiaId.Value).FirstOrDefault();
                        if (archivo != null)
                        {
                            itemModel.TipologiaUrl = Url.Action("Get", "Archivo", new { Id = archivo.Id });
                            itemModel.TipologiaThumbnailUrl = Url.Action("GetThumbnail", "Archivo", new { Id = archivo.Id, sizeClass = 3 });
                        }
                    }
                }));

            // Datos seleccionables en la cabecera del presupuesto
            var categorias = presupuestoCategoriaService.GetCategorias();

            if (presupuesto.PresupuestoCategoria != null)
            {
                editVM.Categorias = categorias.ToSelectListItems(presupuesto.PresupuestoCategoria.Id);
            }
            else
            {
                editVM.Categorias = categorias.ToSelectListItems(Guid.Empty);
            }

            var estados = presupuestoEstadoService.GetEstados();

            if (presupuesto.PresupuestoEstado != null)
            {
                editVM.Estados = estados.ToSelectListItems(presupuesto.PresupuestoEstado.Id);
            }
            else
            {
                editVM.Estados = estados.ToSelectListItems(Guid.Empty);
            }

            if (presupuesto.MonedaId != null)
            {
                editVM.NombreMoneda = monedaService.GetMoneda(presupuesto.MonedaId.Value).Nombre;
            }

            // Valores Predeterminados
            if (presupuesto.SubrubroId != null)
            {
                editVM.NombreSubrubro = subrubroService.GetSubrubro(presupuesto.SubrubroId.Value).Descripcion;
            }

            if (presupuesto.ColorId != null)
            {
                editVM.NombreColor = colorService.GetColor(presupuesto.ColorId.Value).Descripcion;
            }

            if (presupuesto.ColocacionId != null)
            {
                var colocacion = vwPrecioProductoService.GetColocacion(presupuesto.ColocacionId.Value);
                editVM.NombreColocacion = colocacion.ProductoDescripcion;
                editVM.PrecioColocacion = colocacion.PrecioProducto;
            }

            if (presupuesto.VidrioId != null)
            {
                var vidrio = vwPrecioProductoService.GetVidrio(presupuesto.VidrioId.Value);
                editVM.NombreVidrio = vidrio.ProductoDescripcion;
                editVM.PrecioVidrio = vidrio.PrecioProducto;
            }

            // Datos seleccionables en items
            var preciosColocaciones = vwPrecioProductoService.GetColocaciones();
            editVM.Colocaciones = preciosColocaciones;

            var preciosVidrios = vwPrecioProductoService.GetVidrios();
            editVM.Vidrios = preciosVidrios;

            ViewBag.Title = "Modificación de Presupuesto";

            return View("Form", editVM);
        }

        public JsonResult Obtener(string sortBy = "Codigo", string direction = "desc", string filterBy = "All", string searchString = "", Guid? estadoId = null,
            int pageSize = 10, int page = 1)
        {
            ViewBag.Estados = new SelectList(db.PresupuestoEstado, "Id", "Descripcion");
            var pageList = db.Presupuesto.Where(x => x.PresupuestoNuevoId == null).Distinct().ToList();// this.presupuestoService.GetPresupuestosByPage(page, pageSize, sortBy, direction, filterBy, searchString, estadoId);
            var presupuestosVM = Mapper.Map<IEnumerable<Presupuesto>, IEnumerable<CuboPresupuestoViewModel>>(pageList.ToList()).ToList();


            return Json(new { data = presupuestosVM }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CuboPresupuesto()
        {
            return View();
        }

        public JsonResult ObtenerPreciosActualizados(Guid presupuestoId)
        {
            var resultado = new List<PrecioActualizado>();
            var exito = true;
            var mensaje = "";
            try
            {
                var preciosActualizados = presupuestoService.ObtenerPreciosActualizados(presupuestoId);
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = ex.Message;
            }
            var respuesta = new
            {
                exito = exito,
                preciosActualizados = resultado,
                mensaje = mensaje
            };
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
    }
}
