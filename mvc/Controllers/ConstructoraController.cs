using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Model.Models;
using ceya.Domain.Model.Extensions;
using ceya.Domain.Service;
using ceya.Web.Core.Extensions;
using ceya.Domain.Repository;
using AutoMapper;

namespace mvc.Controllers
{
    public class ConstructoraController : Controller
    {
        private readonly IConstructoraRepository constructoraRepository;

        private readonly IConstructoraService constructoraService;
        
        private readonly ITipoDocumentoService tipoDocumentoService;

        private readonly IObraService obraService;

        public ConstructoraController(IConstructoraRepository constructoraRepository, 
            IConstructoraService constructoraService, 
            ITipoDocumentoService tipoDocumentoService, 
            IObraService obraService)
        {
            this.constructoraRepository = constructoraRepository;
            this.constructoraService = constructoraService;
            this.tipoDocumentoService = tipoDocumentoService;
            this.obraService = obraService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            var constructoraVM = new ConstructoraFormModel();
            var tipoDocumento = tipoDocumentoService.GetTipoDocumentos();
            constructoraVM.TipoDocumentos = tipoDocumento.ToSelectListItems(Guid.Empty);

            return PartialView("Create", constructoraVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "RazonSocial,Apellido,Nombre,Documento," +
            "TipoDocumentoId,Domicilio,Telefono,Celular,Email,CreatedDate")] ConstructoraFormModel constructoraVM)
        {
            if (ModelState.IsValid)
            {
                Constructora constructora;
                constructora = new Constructora();
                constructora.Id = Guid.NewGuid();
                constructora.Codigo = constructoraRepository.MaxCodigo();
                constructora.RazonSocial = constructoraVM.RazonSocial;
                constructora.Apellido = constructoraVM.Apellido;
                constructora.Nombre = constructoraVM.Nombre;
                constructora.TipoDocumentoId = constructoraVM.TipoDocumentoId;
                constructora.Documento = constructoraVM.Documento;
                constructora.Domicilio = constructoraVM.Domicilio;
                constructora.Telefono = constructoraVM.Telefono;
                constructora.Celular = constructoraVM.Celular;
                constructora.Email = constructoraVM.Email;
                constructora.CreatedDate = DateTime.Now.Date;
                constructoraService.Add(constructora);

                return Json(new { success = true });
            }
            return Json(constructoraVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Constructora constructora = constructoraService.GetConstructora(id);
            if (constructora == null)
            {
                return HttpNotFound();
            }

            var editVM = new ConstructoraFormModel();
            editVM.Id = constructora.Id;
            editVM.Codigo = constructora.Codigo;
            editVM.RazonSocial = constructora.RazonSocial;
            editVM.Apellido = constructora.Apellido;
            editVM.Nombre = constructora.Nombre;
            editVM.TipoDocumentoId = constructora.TipoDocumentoId;
            editVM.Documento = constructora.Documento;
            editVM.Domicilio = constructora.Domicilio;
            editVM.Telefono = constructora.Telefono;
            editVM.Celular = constructora.Celular;
            editVM.Email = constructora.Email;
            editVM.CreatedDate = constructora.CreatedDate;

            var tipoDocumentos = tipoDocumentoService.GetTipoDocumentos();

            if (constructora.TipoDocumentoId != null)
            {
                editVM.TipoDocumentos = tipoDocumentos.ToSelectListItems(constructora.TipoDocumentoId.Value);
            }
            else
            {
                editVM.TipoDocumentos = tipoDocumentos.ToSelectListItems(Guid.Empty);
            }

            return PartialView(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,RazonSocial,Apellido,Nombre,Documento," + 
            "TipoDocumentoId,Domicilio,Telefono,Celular,Email,CreatedDate")] ConstructoraFormModel constructoraVM)
        {
            if (ModelState.IsValid)
            {
                Constructora constructora = new Constructora();
                constructora.Id = constructoraVM.Id;
                constructora.Codigo = constructoraVM.Codigo;
                constructora.RazonSocial = constructoraVM.RazonSocial;
                constructora.Apellido = constructoraVM.Apellido;
                constructora.Nombre = constructoraVM.Nombre;
                constructora.TipoDocumentoId = constructoraVM.TipoDocumentoId;
                constructora.Documento = constructoraVM.Documento;
                constructora.Domicilio = constructoraVM.Domicilio;
                constructora.Telefono = constructoraVM.Telefono;
                constructora.Celular = constructoraVM.Celular;
                constructora.Email = constructoraVM.Email;
                constructora.CreatedDate = constructoraVM.CreatedDate;
                constructoraService.Update(constructora);
                 return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(tipoDocumentoService.GetTipoDocumentos(), "Id", "Codigo", constructoraVM.TipoDocumentoId);
            return View(constructoraVM);
        }
     
        [HttpGet]
        public JsonResult GetConstructoras(int? page, int? limit, string sortBy, string direction, 
            string search)
        {
            int total;
            var records = GetConstructoras(page, limit, sortBy, direction, search, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public List<ConstructoraViewModel> GetConstructoras(int? page, int? limit, string sortBy,
            string direction, string search, out int total)
        {
            var constructoras = constructoraService.GetConstructorasFilter(search);
            total = constructoras.Count();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = total;
            var records = (from x in constructoras
                           select new ConstructoraViewModel
                           {
                               Id = x.Id,
                               Codigo = x.Codigo,
                               Constructora = x.RazonSocial + x.Apellido + " " + x.Nombre,
                               Documento = x.Documento,
                               Domicilio = x.Domicilio,
                               Telefono = x.Telefono,
                               Celular = x.Celular,
                               Email = x.Email
                           })
                           .AsQueryable();

            var totalPages = (int)Math.Ceiling((float)total / (float)total);
            direction = direction == null ? "ASC" : direction;

            if (direction.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.Codigo);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                records = records.OrderBy(s => s.Codigo);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }
            return records.ToList();           
        }

        public ActionResult List(string sortBy = "Codigo", string direction = "asc", string filterBy = "All", string searchString = "",
            int pageSize = 10, int page = 1)
        {
            var pageList = this.constructoraService.GetConstructorasByPage(page, pageSize, sortBy, direction, filterBy, searchString);
            var obrasVM = Mapper.Map<IEnumerable<Constructora>, IEnumerable<ConstructoraListViewModel>>(pageList.ToList()).ToList();

            //if (Request.IsAjaxRequest())
            //{
            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = obrasVM
            };
            return Json(result, JsonRequestBehavior.AllowGet);
            //}

            //var pageVM = new ObraPageViewModel(filterBy, sortBy);
            //pageVM.SearchString = searchString;
            //pageVM.List = obrasVM;

            //return View("List", pageVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!constructoraService.GetConstructoraAny(id))
            {
                constructoraService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Constructora constructora = constructoraService.GetConstructora(id);
            if (constructora == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ConstructoraFormModel();
            deleteVM.Id = constructora.Id;
            
            ViewBag.Message = "Confirma que desea realizar esta operación?";
            
            return PartialView("_Delete",deleteVM);
        }

        public ActionResult ValidationDeleteConstructora(Guid id)
        {
            Constructora constructora = constructoraService.GetConstructora(id);
            if (constructora == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ConstructoraFormModel();
            deleteVM.Id = constructora.Id;

            ViewBag.Message = "No es posible eliminar dicha constructora porque tiene presupuestos asociados.";

            return PartialView("_Validation", deleteVM);
        }

        public JsonResult JsonAutocomplete(string valor)
        {
            var constructoras = constructoraService.GetConstructorasFilter(valor);
            var sourceData = constructoras
                .Select(x => new
                {
                    key = x.Id,
                    value = x.ToSearchNameString()
                })
                .ToList();
            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }
    }
}
