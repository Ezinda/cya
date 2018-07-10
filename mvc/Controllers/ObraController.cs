using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc.ViewModels;
using System.Linq.Expressions;
using mvc.Helpers;
using ceya.Model.Models;
using ceya.Domain.Model.Extensions;
using ceya.Infrastructure.DataAccess;
using ceya.Domain.Service;
using ceya.Domain.Repository;
using ceya.Core.Helpers;
using AutoMapper;

namespace mvc.Controllers
{
    public class ObraController : Controller
    {
        private readonly IObraService obraService;

        private readonly IObraRepository obraRepository;

        private readonly IPresupuestoService presupuestoService;

        public ObraController(IObraService obraService,
            IObraRepository obraRepository,
            IPresupuestoService presupuestoService)
        {
            this.obraService = obraService;
            this.obraRepository = obraRepository;
            this.presupuestoService = presupuestoService;
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var obraVM = new ObraFormModel();

            return View(obraVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ObraFormModel obraVM)
        {
            if (ModelState.IsValid)
            {
                Obra obra = new Obra();
                obra.Id = Guid.NewGuid();
                obra.Codigo = obraRepository.MaxCodigo();
                obra.CodigoObra = obraVM.Codigo;
                obra.Nombre = obraVM.Nombre;
                obra.Domicilio = obraVM.Domicilio;
                obra.estado = false;
                obra.ClienteId = obraVM.ClienteId;
                obraService.Add(obra);

                return Json(new { success = true });
            }
            return Json(obraVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Obra obra = obraService.GetObra(id);

            if (obra == null)
            {
                return HttpNotFound();
            }

            var editVM = new ObraFormModel();
            editVM.Id = obra.Id;
            editVM.Codigo = obra.CodigoObra;
            editVM.Nombre = obra.Nombre;
            editVM.Domicilio = obra.Domicilio;
            editVM.estado = obra.estado;
            editVM.ClienteId = obra.ClienteId;
            editVM.Cliente = obra.Cliente.RazonSocial + 
                obra.Cliente.Apellido + " " + 
                obra.Cliente.Nombre;

            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(ObraFormModel obraVM)
        {
            if (ModelState.IsValid)
            {
                Obra obra = new Obra();
                obra.Id = obraVM.Id;
                obra.CodigoObra = obraVM.Codigo;
                obra.Nombre = obraVM.Nombre;
                obra.Domicilio = obraVM.Domicilio;
                obra.estado = obraVM.estado;
                obra.ClienteId = obraVM.ClienteId;
                obraService.Update(obra);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetObras(int? page, int? limit, string sortBy, string direction, string search = null)
        {
            int total;
            var records = GetObras(page, limit, sortBy, direction, search, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public List<ObraListViewModel> GetObras(int? page, int? limit, string sortBy, string direction, string search, out int total)
        {
            var obras = obraService.GetObraFilter(search);
            total = obras.Count();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = total;
            var records = (from x in obras
                           select new ObraListViewModel
                           {
                               Id = x.Id,
                               Codigo = x.CodigoObra,
                               Nombre = x.Nombre,
                               Domicilio = x.Domicilio,
                               Cliente = x.Cliente.RazonSocial + "" + x.Cliente.Apellido + " " + x.Cliente.Nombre
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

        public ActionResult List(string sortBy = "Nombre", string direction = "asc", string filterBy = "All", string searchString = "",
            int pageSize = 10, int page = 1)
        { 
            var pageList = this.obraService.GetObrasByPage(page, pageSize, sortBy, direction, filterBy, searchString);
            var obrasVM = Mapper.Map<IEnumerable<Obra>, IEnumerable<ObraListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = obrasVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ObraPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = obrasVM;

            return View("List", pageVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!obraService.GetObraAny(id))
            {
                obraService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete(string valor, Guid? clienteId)
        {
           Expression<Func<Obra, bool>> whereExpression = x => true;

            //whereExpression = whereExpression.And(x =>
            //    (x.Codigo.Contains(valor) ||
            //    x.Nombre.Contains(valor) ||
            //    x.Domicilio.Contains(valor))
            //);

            //if (clienteId != null)
            //{
            //    whereExpression = whereExpression.And(x => x.ClienteId == clienteId);
            //}

            var obras = obraService.GetObraFilterWithCliente(valor, clienteId);
            var sourceData = obras
                .Select(x => new
                {
                    key = x.Id,
                    value = Codificable.GenerateStringForSearchWithoutParseCode(
                        x.CodigoObra,
                        x.Nombre,
                        x.Domicilio),
                    data = new { domicilio = x.Domicilio, cliente = x.Cliente.ToSearchNameString(), clienteId = x.ClienteId }
                })
                .ToList();
            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Obra obra = obraService.GetObra(id);

            if (obra == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ObraFormModel();
            deleteVM.Id = obra.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteObra(Guid id)
        {
            Obra obra = obraService.GetObra(id);

            if (obra == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ObraFormModel();
            deleteVM.Id = obra.Id;

            ViewBag.Message = "No es posible eliminar dicha lista de precio porque tiene productos asociados.";

            return PartialView("_Validation", deleteVM);
        }
    }
}
