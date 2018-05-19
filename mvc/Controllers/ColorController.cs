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
    public class ColorController : Controller
    {
        private readonly IColorService colorService;

        private readonly IColorRepository colorRepository;

        private readonly IClaseService claseService;
        
        private readonly ISubrubroService subrubroService;

        public ColorController(IColorService colorService,
            IColorRepository colorRepository,
            IClaseService claseService,
            ISubrubroService subrubroService)
        {
            this.colorService = colorService;
            this.colorRepository = colorRepository;
            this.claseService = claseService;
            this.subrubroService = subrubroService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Codigo", string filterBy = "All", 
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.colorService.GetColoresByPage(page, pageSize, sortBy, filterBy, searchString);
            var coloresVM = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = coloresVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ColorPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = coloresVM;

            return View("List", pageVM);
        }

        public ActionResult Lookup(Guid ClaseId)
        {
            var claseVM = new ClaseColorFormModel();
            claseVM.Id = ClaseId;
            return View(claseVM);
        }

        public ActionResult Create()
        {
            var ColorVM = new ColorFormModel();

            return PartialView(ColorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColorFormModel ColorVM)
        {
            if (ModelState.IsValid)
            {
                Color Color;
                Color = new Color();
                Color.Id = Guid.NewGuid();
                Color.Codigo = colorRepository.MaxCodigo();
                Color.Descripcion = ColorVM.Descripcion;
                colorService.Add(Color);
                
                return Json(new { success = true });
            }
            return Json(ColorVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Color Color = colorService.GetColor(id);

            if (Color == null)
            {
                return HttpNotFound();
            }

            var editVM = new ColorFormModel();
            editVM.Id = Color.Id;
            editVM.Codigo = Color.Codigo;
            editVM.Descripcion = Color.Descripcion;

            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(ColorFormModel ColorVM)
        {
            if (ModelState.IsValid)
            {
                Color Color = new Color();
                Color.Id = ColorVM.Id;
                Color.Codigo = ColorVM.Codigo;
                Color.Descripcion = ColorVM.Descripcion;
                colorService.Update(Color);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
          if (!colorService.GetColorAny(id))
          {
            colorService.Delete(id);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
          }
          return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetColores(string sortBy = "Codigo", string filterBy = "All", 
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.colorService.GetColoresByPage(page, pageSize, sortBy, filterBy, searchString);
            var coloresVM = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorListViewModel>>(pageList.ToList()).ToList();
            
            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = coloresVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GetColoresRelacionados(Guid? ClaseId, Guid? SubrubroId, int? page, int? limit)
        {
            List<ColorListViewModel> records = null;
            if (ClaseId != null)
            {
                records = claseService
                    .GetClase(ClaseId.Value)
                    .Colores
                    .Select(x => new ColorListViewModel
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
            else
            {
                IEnumerable<Color> colores = subrubroService.GetColoresRelacionados(SubrubroId.Value);

                if (colores != null)
                {
                    records = colores.Select(x => new ColorListViewModel
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion
                    })
                    .ToList();
                }

                int total;
                total = records == null ? 0 : records.Count();

                return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult ConfirmDelete(Guid id)
        {
            Color Color = colorService.GetColor(id);

            if (Color == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ColorFormModel();
            deleteVM.Id = Color.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public JsonResult JsonAutocomplete(Guid? SubrubroId)
        {
            if (SubrubroId.HasValue)
            {
                IEnumerable<Color> colores = subrubroService.GetColoresRelacionados(SubrubroId.Value);

                var sourceData = colores
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
        
        public ActionResult ValidationDeleteColor(Guid id)
        {
            Color Color = colorService.GetColor(id);

            if (Color == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ColorFormModel();
            deleteVM.Id = Color.Id;

            ViewBag.Message = "No es posible eliminar dicho color porque tiene clases asociadas.";

            return PartialView("_Validation", deleteVM);
        }

    }
}
