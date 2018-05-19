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
    public class SubrubroController : Controller
    {
        private readonly ISubrubroService subrubroService;

        private readonly ISubrubroRepository subrubroRepository;

        private readonly IRubroService rubroService;

        public SubrubroController(ISubrubroService subrubroService,
            ISubrubroRepository subrubroRepository,
            IRubroService rubroService)
        {
            this.subrubroService = subrubroService;
            this.subrubroRepository = subrubroRepository;
            this.rubroService = rubroService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.subrubroService.GetSubrubrosByPage(page, pageSize, sortBy, filterBy, searchString);
            var subrubroVM = Mapper.Map<IEnumerable<Subrubro>, IEnumerable<SubrubroListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = subrubroVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new SubrubroPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = subrubroVM;

            return View("List", pageVM);
        }

        public ActionResult Lookup(Guid RubroId)
        {
            var rubroVM = new RubroSubrubroFormModel();
            rubroVM.Id = RubroId;
            return View(rubroVM);
        }

        public ActionResult Create()
        {
            var SubrubroVM = new SubrubroFormModel();

            return PartialView(SubrubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubrubroFormModel SubrubroVM)
        {
            if (ModelState.IsValid)
            {
                Subrubro Subrubro;
                Subrubro = new Subrubro();
                Subrubro.Id = Guid.NewGuid();
                Subrubro.Codigo = subrubroRepository.MaxCodigo();
                Subrubro.Descripcion = SubrubroVM.Descripcion;
                Subrubro.ClaseId = SubrubroVM.ClaseId != null ? SubrubroVM.ClaseId : null;
                subrubroService.Add(Subrubro);

                return Json(new { success = true });
            }
            return Json(SubrubroVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subrubro Subrubro = subrubroService.GetSubrubro(id);

            if (Subrubro == null)
            {
                return HttpNotFound();
            }

            var editVM = new SubrubroFormModel();
            editVM.Id = Subrubro.Id;
            editVM.Codigo = Subrubro.Codigo;
            editVM.Descripcion = Subrubro.Descripcion;
            editVM.ClaseId = Subrubro.Clase != null ? Subrubro.ClaseId : null;
            editVM.Clase = Subrubro.Clase != null ? Subrubro.Clase.Descripcion : null;
            editVM.RubroId = Subrubro.RubroId;
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(SubrubroFormModel SubrubroVM)
        {
            if (ModelState.IsValid)
            {
                Subrubro Subrubro = new Subrubro();
                Subrubro.Id = SubrubroVM.Id;
                Subrubro.Codigo = SubrubroVM.Codigo;
                Subrubro.Descripcion = SubrubroVM.Descripcion;
                Subrubro.ClaseId = SubrubroVM.ClaseId;
                Subrubro.RubroId = SubrubroVM.RubroId;
                subrubroService.Update(Subrubro);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!subrubroService.GetSubrubroAny(id))
            {
                subrubroService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Subrubro Subrubro = subrubroService.GetSubrubro(id);

            if (Subrubro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new SubrubroFormModel();
            deleteVM.Id = Subrubro.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteSubrubro(Guid id)
        {
            Subrubro Subrubro = subrubroService.GetSubrubro(id);

            if (Subrubro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new SubrubroFormModel();
            deleteVM.Id = Subrubro.Id;

            ViewBag.Message = "No es posible eliminar dicho subrubro porque tiene un rubro asociado.";

            return PartialView("_Validation", deleteVM);
        }


        [HttpGet]
        public JsonResult GetSubrubros(string sortBy = "Codigo", string filterBy = "All",
               string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.subrubroService.GetSubrubrosByPage(page, pageSize, sortBy, filterBy, searchString);
            var subrubroVM = Mapper.Map<IEnumerable<Subrubro>, IEnumerable<SubrubroListViewModel>>(pageList.ToList()).ToList();

            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = subrubroVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GetSubrubrosRelacionados(Guid RubroId, int? page, int? limit)
        {
           List<SubrubroListViewModel> records = null;
           records = rubroService
                .GetSubrubrosRelacionados(RubroId)
                .Select(x => new SubrubroListViewModel
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Clase = x.Clase!= null ? x.Clase.Descripcion : string.Empty
                })
                .ToList();

            int total;
            total = records.Count();

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult JsonAutocomplete(Guid? RubroId)
        {
            if (RubroId != null)
            {
                var subrubros = rubroService.GetSubrubrosRelacionados(RubroId.Value);
                var sourceData = subrubros
                    .Select(x => new
                    {
                        key = x.Id,
                        value = x.Descripcion,
                    })
                    .ToList();
                return Json(sourceData, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocompletePresupuesto(string valor)
        {
            var subrubros = subrubroService.GetSubrubroFilter(valor);
            var sourceData = subrubros
                .Select(x => new
                {
                    key = x.Id,
                    value = x.Descripcion,
                })
                .ToList();
            return Json(sourceData, JsonRequestBehavior.AllowGet);

        }
    }
}
