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
    public class TipoProductoController : Controller
    {
        private readonly ITipoProductoService tipoProductoService;

        private readonly ITipoProductoRepository tipoProductoRepository;

        private readonly IProductoService productoService;

        public TipoProductoController(ITipoProductoService tipoProductoService,
            ITipoProductoRepository tipoProductoRepository,
            IProductoService productoService)
        {
            this.tipoProductoService = tipoProductoService;
            this.tipoProductoRepository = tipoProductoRepository;
            this.productoService = productoService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.tipoProductoService.GetTipoProductosByPage(page, pageSize, sortBy, filterBy, searchString);
            var tipoProductosVM = Mapper.Map<IEnumerable<TipoProducto>, IEnumerable<TipoProductoListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = tipoProductosVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new TipoProductoPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = tipoProductosVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var tipoProductoVM = new TipoProductoFormModel();

            return PartialView(tipoProductoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoProductoFormModel tipoProductoVM)
        {
            if (ModelState.IsValid)
            {
                TipoProducto tipoProducto;
                tipoProducto = new TipoProducto();
                tipoProducto.Id = Guid.NewGuid();
                tipoProducto.Codigo = tipoProductoRepository.MaxCodigo();
                tipoProducto.Descripcion = tipoProductoVM.Descripcion;
                tipoProductoService.Add(tipoProducto);

                return Json(new { success = true });
            }
            return Json(tipoProductoVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProducto tipoProducto = tipoProductoService.GetTipoProducto(id);

            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            var editVM = new TipoProductoFormModel();
            editVM.Id = tipoProducto.Id;
            editVM.Codigo = tipoProducto.Codigo;
            editVM.Descripcion = tipoProducto.Descripcion;
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(TipoProductoFormModel tipoProductoVM)
        {
            if (ModelState.IsValid)
            {
                TipoProducto tipoProducto = new TipoProducto();
                tipoProducto.Id = tipoProductoVM.Id;
                tipoProducto.Codigo = tipoProductoVM.Codigo;
                tipoProducto.Descripcion = tipoProductoVM.Descripcion;
                tipoProductoService.Update(tipoProducto);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!tipoProductoService.GetTipoProductoAny(id))
            {
                tipoProductoService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            TipoProducto tipoProducto = tipoProductoService.GetTipoProducto(id);

            if (tipoProducto == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new TipoProductoFormModel();
            deleteVM.Id = tipoProducto.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteTipoProducto(Guid id)
        {
            TipoProducto tipoProducto = tipoProductoService.GetTipoProducto(id);

            if (tipoProducto == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new TipoProductoFormModel();
            deleteVM.Id = tipoProducto.Id;

            ViewBag.Message = "No es posible eliminar dicho tipo porque tiene productos asociados.";

            return PartialView("_Validation", deleteVM);
        }
    }
}
