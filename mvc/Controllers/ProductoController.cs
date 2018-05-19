using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Model.Models;
using ceya.Infrastructure.DataAccess;
using ceya.Domain.Service;
using AutoMapper;
using ceya.Web.Core.Extensions;
using ceya.Domain.Repository;

namespace mvc.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService productoService;

        private readonly IProductoRepository productoRepository;

        private readonly ITipoProductoService tipoProductoService;

        private readonly IUnidadMedidaService unidadMedidadService;

        private readonly IRubroService rubroService;

        private readonly IRubroMaestroService rubroMaestroService;

        private readonly ISubrubroService subrubroService;

        public ProductoController(IProductoService productoService,
            IProductoRepository productoRepository, 
            ITipoProductoService tipoProductoService,
            IUnidadMedidaService unidadMedidadService,
            IRubroService rubroService,
            IRubroMaestroService rubroMaestroService,
            ISubrubroService subrubroService)
        {
            this.productoService = productoService;
            this.productoRepository = productoRepository;
            this.tipoProductoService = tipoProductoService;
            this.unidadMedidadService = unidadMedidadService;
            this.rubroService = rubroService;
            this.rubroMaestroService = rubroMaestroService;
            this.subrubroService = subrubroService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.productoService.GetProductosByPage(page, pageSize, sortBy, filterBy, searchString);
            var productoVM = Mapper.Map<IEnumerable<Producto>, IEnumerable<ProductoListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = productoVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ProductoPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = productoVM;

            return View("List", pageVM);
        }

        public ActionResult Lookup(Guid ListaPrecioId)
        {
            var listaVM = new ListaPrecioProductoFormModel();
            listaVM.Id = ListaPrecioId;
            return View(listaVM);
        }
        public ActionResult Create()
        {
            var productoVM = new ProductoFormModel();

            var tipo = tipoProductoService.GetTipoProductos();
            productoVM.TipoProductos = tipo.ToSelectListItems(Guid.Empty);

            var unidad = unidadMedidadService.GetUnidadMedidas();
            productoVM.UnidadMedidas = unidad.ToSelectListItems(Guid.Empty);

            var rubroMaestros = rubroMaestroService.GetRubroMaestros();
            productoVM.RubroMaestros = rubroMaestros.ToSelectListItems(Guid.Empty);

            var rubro = rubroService.GetRubros();
            productoVM.Rubros = rubro.ToSelectListItems(Guid.Empty);

            var subrubro = subrubroService.GetSubrubros();
            productoVM.Subrubros = subrubro.ToSelectListItems(Guid.Empty);

            return PartialView("Create", productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoFormModel productoVM)
        {
            if (ModelState.IsValid)
            {
                Producto producto;
                producto = new Producto();
                producto.Id = Guid.NewGuid();
                producto.Codigo = productoRepository.MaxCodigo();
                producto.CodigoProveedor = productoVM.CodigoProveedor;
                producto.CodigoCompuesto = productoService.GetCodigoCompuesto(producto.Codigo,
                    productoVM.RubroMaestroId.Value,
                    productoVM.SubrubroId != null ? productoVM.SubrubroId : null);
                producto.Descripcion = productoVM.Descripcion;
                producto.UnidadMedidaId = productoVM.UnidadMedidaId;
                producto.TipoProductoId = productoVM.TipoProductoId;
                producto.RubroMaestroId = productoVM.RubroMaestroId;
                producto.RubroId = productoVM.RubroId;
                producto.SubrubroId = productoVM.SubrubroId;
                productoService.Add(producto);

                return Json(new { success = true });
            }
            return Json(productoVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = productoService.GetProducto(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            var editVM = Mapper.Map<Producto, ProductoFormModel>(producto);
            
            var unidadMedidas = unidadMedidadService.GetUnidadMedidas();

            if (producto.UnidadMedidaId != null)
            {
                editVM.UnidadMedidas = unidadMedidas.ToSelectListItems(producto.UnidadMedidaId.Value);
            }
            else
            {
                editVM.UnidadMedidas = unidadMedidas.ToSelectListItems(Guid.Empty);
            }

            var tipoProductos = tipoProductoService.GetTipoProductos();

            if (producto.TipoProductoId != null)
            {
                editVM.TipoProductos = tipoProductos.ToSelectListItems(producto.TipoProductoId.Value);
            }
            else
            {
                editVM.TipoProductos = tipoProductos.ToSelectListItems(Guid.Empty);
            }
            
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(ProductoFormModel productoVM)
        {
            var editVM = Mapper.Map<ProductoFormModel, Producto>(productoVM);
            editVM.CodigoCompuesto = productoService.GetCodigoCompuesto(productoVM.Codigo,
                    productoVM.RubroMaestroId.Value,
                    productoVM.SubrubroId != null ? productoVM.SubrubroId : null);

            if (ModelState.IsValid)
            {
                productoService.Update(editVM);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Producto producto = productoService.GetProducto(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ProductoFormModel();
            deleteVM.Id = producto.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            //if (!productoService.Exists(id))
            //{
            productoService.Delete(id);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductos(string sortBy = "Codigo", string filterBy = "All",
          string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.productoService.GetProductosByPage(page, pageSize, sortBy, filterBy, searchString);
            var productoVM = Mapper.Map<IEnumerable<Producto>, IEnumerable<ProductoListViewModel>>(pageList.ToList()).ToList();

            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = productoVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
