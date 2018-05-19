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
using ceya.Domain.Repository;

namespace mvc.Controllers
{
    public class RubroController : Controller
    {
        private readonly IRubroService rubroService;

        private readonly IRubroRepository rubroRepository;

        private readonly ISubrubroService subrubroService;

        private readonly IProductoService productoService;

        private readonly IRubroMaestroService rubroMaestroService;

        public RubroController(IRubroService rubroService,
            IRubroRepository rubroRepository,
            ISubrubroService subrubroService,
            IProductoService productoService,
            IRubroMaestroService rubroMaestroService)
        {
            this.rubroService = rubroService;
            this.rubroRepository = rubroRepository;
            this.subrubroService = subrubroService;
            this.productoService = productoService;
            this.rubroMaestroService = rubroMaestroService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.rubroService.GetRubrosByPage(page, pageSize, sortBy, filterBy, searchString);
            var rubroVM = Mapper.Map<IEnumerable<Rubro>, IEnumerable<RubroListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = rubroVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new RubroPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = rubroVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var rubroVM = new RubroFormModel();

            return PartialView(rubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RubroFormModel rubroVM)
        {
            if (ModelState.IsValid)
            {
                Rubro rubro;
                rubro = new Rubro();
                rubro.Id = Guid.NewGuid();
                rubro.Codigo = rubroRepository.MaxCodigo();
                rubro.Descripcion = rubroVM.Descripcion;
                rubro.Sistema = rubroVM.Sistema;
                rubroService.Add(rubro);

                return Json(new { success = true });
            }
            return Json(rubroVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = rubroService.GetRubro(id);

            if (rubro == null)
            {
                return HttpNotFound();
            }
            var editVM = new RubroFormModel();
            editVM.Id = rubro.Id;
            editVM.Codigo = rubro.Codigo;
            editVM.Descripcion = rubro.Descripcion;
            editVM.Sistema = rubro.Sistema;
            editVM.RubroMaestroId = rubro.RubroMaestroId;
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(RubroFormModel rubroVM)
        {
            if (ModelState.IsValid)
            {
                Rubro rubro = new Rubro();
                rubro.Id = rubroVM.Id;
                rubro.Codigo = rubroVM.Codigo;
                rubro.Descripcion = rubroVM.Descripcion;
                rubro.Sistema = rubroVM.Sistema;
                rubro.RubroMaestroId = rubroVM.RubroMaestroId;
                rubroService.Update(rubro);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!rubroService.GetRubroAny(id))
            {
                rubroService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Rubro rubro = rubroService.GetRubro(id);

            if (rubro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new RubroFormModel();
            deleteVM.Id = rubro.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteRubro(Guid id)
        {
            Rubro rubro = rubroService.GetRubro(id);

            if (rubro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new RubroFormModel();
            deleteVM.Id = rubro.Id;

            ViewBag.Message = "No es posible eliminar dicho rubro secundario porque tiene un rubro primario asociado.";

            return PartialView("_Validation", deleteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRubroSubrubro(RubroSubrubroFormModel rubroVM)
        {
            if (ModelState.IsValid)
            {
                Rubro rubro = rubroService.GetRubro(rubroVM.Id);

                foreach (var item in rubroVM.SubrubrosId)
                {
                    Subrubro subrubro = subrubroService.GetSubrubro(item);
                    subrubro.RubroId = rubro.Id;
                    subrubroService.Update(subrubro);
                }

                return Json(new { success = true });
            }
            return Json(new { success = true });

        }
        
        [HttpGet]
        public ActionResult QuitarAsignacion(Guid RubroId, Guid SubrubroId)
        {
            RubroSubrubroFormModel rubroVM = new RubroSubrubroFormModel();
            rubroVM.Id = RubroId;
            rubroVM.SubrubroId = SubrubroId;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_DeleteRubroSubrubro", rubroVM);
        }

        [HttpPost]
        public JsonResult EliminarAsignacion(RubroSubrubroFormModel rubroVM)
        {
            Rubro rubro = rubroService.GetRubro(rubroVM.Id);
            Subrubro subrubro = subrubroService.GetSubrubro(rubroVM.SubrubroId.Value);
            rubro.Subrubro.Remove(subrubro);
            rubroService.Update(rubro);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete(string valor)
        {
            var rubros = rubroService.GetRubroFilter(valor);

            var sourceData = rubros
                .Select(x => new
                {
                    key = x.Id,
                    value = x.Descripcion
                })
                .ToList();

            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Lookup(Guid RubroMaestroId)
        {
            var rubroVM = new RubroSubrubroFormModel();
            rubroVM.Id = RubroMaestroId;
            return View(rubroVM);
        }

        [HttpGet]
        public JsonResult GetRubros(string sortBy = "Codigo", string filterBy = "All",
          string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.rubroService.GetRubrosByPage(page, pageSize, sortBy, filterBy, searchString);
            var rubroVM = Mapper.Map<IEnumerable<Rubro>, IEnumerable<RubroListViewModel>>(pageList.ToList()).ToList();

            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = rubroVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRubroRubroMaestro(RubroSubrubroFormModel rubroVM)
        {
            if (ModelState.IsValid)
            {
                RubroMaestro rubroMaestro = rubroMaestroService.GetRubroMaestro(rubroVM.Id);

                foreach (var item in rubroVM.SubrubrosId)
                {
                    Rubro rubro = rubroService.GetRubro(item);
                    rubro.RubroMaestro = rubroMaestro;
                    rubroService.Update(rubro);
                }

                return Json(new { success = true });
            }
            return Json(new { success = true });
        }
        
        [HttpGet]
        public JsonResult GetRubrosRelacionados(Guid RubroMaestroId, int? page, int? limit)
        {
            List<RubroListViewModel> records = null;
            records = rubroService
                 .GetRubrosRelacionados(RubroMaestroId)
                 .Select(x => new RubroListViewModel
                 {
                     Id = x.Id,
                     Codigo = x.Codigo,
                     Descripcion = x.Descripcion
                 })
                 .ToList();

            int total;
            total = records.Count();

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult JsonAutocompleteRubrosRelacionados(Guid? RubroMaestroId)
        {
            var rubro = rubroService.GetRubrosRelacionados(RubroMaestroId.Value);
            var sourceData = rubro
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
