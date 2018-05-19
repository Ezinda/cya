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
    public class ClaseController : Controller
    {
        private readonly IClaseService claseService;

        private readonly IClaseRepository claseRepository;

        private readonly IColorService colorService;

        public ClaseController(IClaseService claseService,
            IClaseRepository claseRepository,
            IColorService colorService)
        {
            this.claseService = claseService;
            this.claseRepository = claseRepository;
            this.colorService = colorService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.claseService.GetClasesByPage(page, pageSize, sortBy, filterBy, searchString);
            var clasesVM = Mapper.Map<IEnumerable<Clase>, IEnumerable<ClaseListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = clasesVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ClasePageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = clasesVM;

            return View("List", pageVM);
        }

        public ActionResult Lookup()
        {
            return View();
        }

        public ActionResult Create()
        {
            var claseVM = new ClaseFormModel();

            return PartialView(claseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClaseFormModel claseVM)
        {
            if (ModelState.IsValid)
            {
                Clase clase;
                clase = new Clase();
                clase.Id = Guid.NewGuid();
                clase.Codigo = claseRepository.MaxCodigo();
                clase.Descripcion = claseVM.Descripcion;
                claseService.Add(clase);

                return Json(new { success = true });
            }
            return Json(claseVM, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClaseColor(ClaseColorFormModel claseVM)
        {
            if (ModelState.IsValid)
            {
                Clase clase = claseService.GetClase(claseVM.Id);

                foreach (var item in claseVM.ColoresId)
                {
                    Color color = colorService.GetColor(item);
                    clase.Colores.Add(color);
                }
                claseService.Update(clase);

                return Json(new { success = true });
            }
            return Json(new { success = true });

        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clase clase = claseService.GetClase(id);

            if (clase == null)
            {
                return HttpNotFound();
            }

            var editVM = new ClaseFormModel();
            editVM.Id = clase.Id;
            editVM.Codigo = clase.Codigo;
            editVM.Descripcion = clase.Descripcion;

            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(ClaseFormModel claseVM)
        {
            if (ModelState.IsValid)
            {
                Clase clase = new Clase();
                clase.Id = claseVM.Id;
                clase.Codigo = claseVM.Codigo;
                clase.Descripcion = claseVM.Descripcion;
                claseService.Update(clase);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!claseService.GetClaseAny(id))
            {
                claseService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetClases(string sortBy = "Codigo", string filterBy = "All",
                string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.claseService.GetClasesByPage(page, pageSize, sortBy, filterBy, searchString);
            var clasesVM = Mapper.Map<IEnumerable<Clase>, IEnumerable<ClaseListViewModel>>(pageList.ToList()).ToList();

            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = clasesVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ConfirmDelete(Guid id)
        {
            Clase clase = claseService.GetClase(id);

            if (clase == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ClaseFormModel();
            deleteVM.Id = clase.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteClase(Guid id)
        {
            Clase clase = claseService.GetClase(id);

            if (clase == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ClaseFormModel();
            deleteVM.Id = clase.Id;

            ViewBag.Message = "No es posible eliminar dicha clase porque tiene subrubro asociados.";

            return PartialView("_Validation", deleteVM);
        }

        [HttpGet]
        public ActionResult QuitarAsignacion(Guid ClaseId, Guid ColorId)
        {
            ClaseColorFormModel claseVM = new ClaseColorFormModel();
            claseVM.Id = ClaseId;
            claseVM.ColorId = ColorId;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_DeleteClaseColor", claseVM);
        }

        [HttpPost]
        public JsonResult EliminarAsignacion(ClaseColorFormModel ClaseVM)
        {
            Clase clase = claseService.GetClase(ClaseVM.Id);

            Color color = colorService.GetColor(ClaseVM.ColorId.Value);
            clase.Colores.Remove(color);
            claseService.Update(clase);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }


    }
}
