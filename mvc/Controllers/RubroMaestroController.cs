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
    public class RubroMaestroController : Controller
    {
        private readonly IRubroMaestroService rubroMaestroService;

        private readonly IRubroService rubroService;

        private readonly IRubroMaestroRepository rubroMaestroRepository;

        private readonly IProductoService productoService;

        public RubroMaestroController(IRubroMaestroService rubroMaestroService, 
            IRubroService rubroService,
            IRubroMaestroRepository rubroMaestroRepository,
            IProductoService productoService)
        {
            this.rubroMaestroService = rubroMaestroService;
            this.rubroService = rubroService;
            this.rubroMaestroRepository = rubroMaestroRepository;
            this.productoService = productoService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.rubroMaestroService.GetRubroMaestrosByPage(page, pageSize, sortBy, filterBy, searchString);
            var RubroMaestroVM = Mapper.Map<IEnumerable<RubroMaestro>, IEnumerable<RubroMaestroListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = RubroMaestroVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new RubroMaestroPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = RubroMaestroVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var RubroMaestroVM = new RubroMaestroFormModel();

            return PartialView(RubroMaestroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RubroMaestroFormModel RubroMaestroVM)
        {
            if (ModelState.IsValid)
            {
                RubroMaestro RubroMaestro;
                RubroMaestro = new RubroMaestro();
                RubroMaestro.Id = Guid.NewGuid();
                RubroMaestro.Codigo = rubroMaestroRepository.MaxCodigo();
                RubroMaestro.Descripcion = RubroMaestroVM.Descripcion;
               rubroMaestroService.Add(RubroMaestro);

                return Json(new { success = true });
            }
            return Json(RubroMaestroVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RubroMaestro RubroMaestro = rubroMaestroService.GetRubroMaestro(id);

            if (RubroMaestro == null)
            {
                return HttpNotFound();
            }
            var editVM = new RubroMaestroFormModel();
            editVM.Id = RubroMaestro.Id;
            editVM.Codigo = RubroMaestro.Codigo;
            editVM.Descripcion = RubroMaestro.Descripcion;
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(RubroMaestroFormModel RubroMaestroVM)
        {
            if (ModelState.IsValid)
            {
                RubroMaestro RubroMaestro = new RubroMaestro();
                RubroMaestro.Id = RubroMaestroVM.Id;
                RubroMaestro.Codigo = RubroMaestroVM.Codigo;
                RubroMaestro.Descripcion = RubroMaestroVM.Descripcion;
                rubroMaestroService.Update(RubroMaestro);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!rubroMaestroService.GetRubroAny(id))
            {
                rubroMaestroService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            RubroMaestro RubroMaestro = rubroMaestroService.GetRubroMaestro(id);

            if (RubroMaestro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new RubroMaestroFormModel();
            deleteVM.Id = RubroMaestro.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteRubroMaestro(Guid id)
        {
            RubroMaestro RubroMaestro = rubroMaestroService.GetRubroMaestro(id);

            if (RubroMaestro == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new RubroMaestroFormModel();
            deleteVM.Id = RubroMaestro.Id;
            
            ViewBag.Message = "No es posible eliminar dicho rubro primario porque tiene productos asociados.";

            return PartialView("_Validation", deleteVM);
        }

        [HttpGet]
        public ActionResult QuitarAsignacion(Guid RubroMaestroId, Guid RubroId)
        {
            RubroSubrubroFormModel RubroMaestroVM = new RubroSubrubroFormModel();
            RubroMaestroVM.Id = RubroMaestroId;
            RubroMaestroVM.SubrubroId = RubroId;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_DeleteRubroMaestroRubro", RubroMaestroVM);
        }

        [HttpPost]
        public JsonResult EliminarAsignacion(RubroSubrubroFormModel RubroMaestroVM)
        {
            RubroMaestro RubroMaestro = rubroMaestroService.GetRubroMaestro(RubroMaestroVM.Id);
            Rubro rubro = rubroService.GetRubro(RubroMaestroVM.SubrubroId.Value);
            RubroMaestro.Rubros.Remove(rubro);
            rubroMaestroService.Update(RubroMaestro);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete(string valor)
        {
            var RubroMaestros = rubroMaestroService.GetRubroMaestroFilter(valor);

            var sourceData = RubroMaestros
                .Select(x => new
                {
                    key = x.Id,
                    value = x.Descripcion
                })
                .ToList();

            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }
    }
}
