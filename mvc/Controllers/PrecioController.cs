using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Infrastructure.DataAccess;
using ceya.Model.Models;
using ceya.Domain.Service;
using AutoMapper;

namespace mvc.Controllers
{
    public class PrecioController : Controller
    {
        private readonly IProductoService productoService;

        private readonly IPrecioService precioService;

        private readonly IListaPrecioService listaPrecioService;

        public PrecioController(IProductoService productoService,
            IPrecioService precioService,
            IListaPrecioService listaPrecioService)
        {
            this.productoService = productoService;
            this.precioService = precioService;
            this.listaPrecioService = listaPrecioService;
        }

        [HttpGet]
        public JsonResult GetProductosRelacionados(string sortBy = "FechaDesde", string filterBy = "All",
        string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.precioService.GetPreciosByPage(page, pageSize, sortBy, filterBy, searchString);
            var precioVM = Mapper.Map<IEnumerable<Precio>, IEnumerable<PrecioListViewModel>>(pageList.ToList()).ToList();

            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = precioVM
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductoPrecioRelacionados(Guid ListaPrecioId, int? page, int? limit)
        {
            List<PrecioListViewModel> records = null;
            records = listaPrecioService
                 .GetPrecioProductosRelacionados(ListaPrecioId)
                 .Select(x => new PrecioListViewModel
                 {
                     Id = x.Id,
                     CodigoCompuesto = x.Producto != null ? x.Producto.CodigoCompuesto : string.Empty,
                     Descripcion = x.Producto != null ? x.Producto.Descripcion : string.Empty,
                     RubroMaestro = x.Producto != null ? x.Producto.RubroMaestro != null ? x.Producto.RubroMaestro.Descripcion : string.Empty : string.Empty,
                     Rubro = x.Producto != null ? x.Producto.Rubro != null ? x.Producto.Rubro.Descripcion : string.Empty : string.Empty,
                     Subrubro = x.Producto != null ? x.Producto.Subrubro != null ? x.Producto.Subrubro.Descripcion : string.Empty : string.Empty,
                     Desde = x.FechaDesde.ToShortDateString(),
                     Hasta = x.FechaHasta.ToShortDateString(),
                     Precio = x.PrecioProducto.ToString("F")
                 })
                 .OrderBy(x=>x.Descripcion)
                 .ToList();

            int total;
            total = records.Count();
            int pagecount = total / 10;
            var result = new
            {
                PageSize = 10,
                Pages = pagecount,
                CurrentPage = 1,
                Total = total,
                Records = records
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid PrecioId)
        {

            Precio precio = precioService.GetPrecio(PrecioId);

            if (precio == null)
            {
                return HttpNotFound();
            }

            var editVM = Mapper.Map<Precio, PrecioFormModel>(precio);
            
            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(PrecioFormModel precioVM)
        {
            var editVM = Mapper.Map<PrecioFormModel, Precio>(precioVM);

            if (ModelState.IsValid)
            {
                precioService.Update(editVM);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult QuitarAsignacion(Guid Id)
        {
            ListaPrecioFormModel precioVM = new ListaPrecioFormModel();
            precioVM.Id = Id;
      
            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_DeletePrecioProducto", precioVM);
        }

        [HttpPost]
        public JsonResult EliminarAsignacion(ListaPrecioFormModel precioVM)
        {

            precioService.Delete(precioVM.Id);
            
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }


    }
}
