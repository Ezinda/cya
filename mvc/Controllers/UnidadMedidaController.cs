using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Model.Models;
using ceya.Domain.Service;
using ceya.Domain.Repository;
using AutoMapper;

namespace mvc.Controllers
{
    public class UnidadMedidaController : Controller
    {
        private readonly IUnidadMedidaService unidadMedidaService;

        private readonly IUnidadMedidaRepository unidadMedidaRepository;

        private readonly IProductoService productoService;

        public UnidadMedidaController(IUnidadMedidaService unidadMedidaService,
            IUnidadMedidaRepository unidadMedidaRepository,
            IProductoService productoService)
        {
            this.unidadMedidaService = unidadMedidaService;
            this.unidadMedidaRepository = unidadMedidaRepository;
            this.productoService = productoService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.unidadMedidaService.GetUnidadMedidasByPage(page, pageSize, sortBy, filterBy, searchString);
            var unidadesVM = Mapper.Map<IEnumerable<UnidadMedida>, IEnumerable<UnidadMedidaListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = unidadesVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new UnidadMedidaPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = unidadesVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var unidadVM = new UnidadMedidaFormModel();

            return PartialView(unidadVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Abreviacion,Descripcion")]
        UnidadMedidaFormModel unidadMedidaVM)
        {
            if (ModelState.IsValid)
            {
                UnidadMedida unidad;
                unidad = new UnidadMedida();
                unidad.Id = Guid.NewGuid();
                unidad.Codigo = unidadMedidaRepository.MaxCodigo();
                unidad.Abreviacion = unidadMedidaVM.Abreviacion;
                unidad.Descripcion = unidadMedidaVM.Descripcion;
                unidadMedidaService.Add(unidad);
                
                return Json(new { success = true });
            }
            return Json(unidadMedidaVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnidadMedida unidad = unidadMedidaService.GetUnidadMedida(id);

            if (unidad == null)
            {
                return HttpNotFound();
            }

            var editVM = new UnidadMedidaFormModel();
            editVM.Id = unidad.Id;
            editVM.Codigo = unidad.Codigo;
            editVM.Abreviacion = unidad.Abreviacion;
            editVM.Descripcion = unidad.Descripcion;

            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Abreviacion,Descripcion")]
        UnidadMedidaFormModel unidadVM)
        {
            if (ModelState.IsValid)
            {
                UnidadMedida unidad = new UnidadMedida();
                unidad.Id = unidadVM.Id;
                unidad.Codigo = unidadVM.Codigo;
                unidad.Abreviacion = unidadVM.Abreviacion;
                unidad.Descripcion = unidadVM.Descripcion;
                unidadMedidaService.Update(unidad);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!unidadMedidaService.GetUnidadMedidaAny(id))
            {
                unidadMedidaService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            UnidadMedida unidad = unidadMedidaService.GetUnidadMedida(id);

            if (unidad == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new UnidadMedidaFormModel();
            deleteVM.Id = unidad.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteUnidad(Guid id)
        {
            UnidadMedida unidad = unidadMedidaService.GetUnidadMedida(id);

            if (unidad == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new UnidadMedidaFormModel();
            deleteVM.Id = unidad.Id;

            ViewBag.Message = "No es posible eliminar dicha unidad de medida porque tiene productos asociados.";

            return PartialView("_Validation", deleteVM);
        }
    }
}
