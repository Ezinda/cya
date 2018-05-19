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

namespace mvc.Controllers
{
    public class ListaPrecioController : Controller
    {

        private readonly IListaPrecioService listaPrecioService;

        private readonly IProductoService productoService;

        private readonly IPrecioService precioService;


        public ListaPrecioController(IListaPrecioService listaPrecioService,
            IProductoService productoService,
            IPrecioService precioService)
        {
            this.listaPrecioService = listaPrecioService;
            this.productoService = productoService;
            this.precioService = precioService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string sortBy = "Descripcion",string direction="asc", string filterBy = "All",
            string searchString = "", int pageSize = 10, int page = 1)
        {
            var pageList = this.listaPrecioService.GetListaPreciosByPage(page,pageSize, sortBy, direction,filterBy, searchString);
            var listaVM = Mapper.Map<IEnumerable<ListaPrecio>, IEnumerable<ListaPrecioListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = listaVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ListaPrecioPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = listaVM;

            return View("List", pageVM);
        }

        public ActionResult Create()
        {
            var listaVM = new ListaPrecioFormModel();

            return PartialView("Create", listaVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListaPrecioFormModel listaVM)
        {
            var listaPrecio = Mapper.Map<ListaPrecioFormModel, ListaPrecio>(listaVM);

            if (ModelState.IsValid)
            {

                listaPrecio.Id = Guid.NewGuid();
                listaPrecio.Activo = true;
                listaPrecio.Predefinida = false;
                listaPrecioService.Add(listaPrecio);

                return Json(new { success = true });
            }
            return Json(listaVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaPrecio lista = listaPrecioService.GetListaPrecio(id);

            if (lista == null)
            {
                return HttpNotFound();
            }

            var editVM = Mapper.Map<ListaPrecio, ListaPrecioFormModel>(lista);

            return PartialView(editVM);
        }

        [HttpPost]
        public ActionResult Edit(ListaPrecioFormModel listaVM)
        {
            var editVM = Mapper.Map<ListaPrecioFormModel, ListaPrecio>(listaVM);

            if (ModelState.IsValid)
            {
                listaPrecioService.Update(editVM);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            ListaPrecio lista = listaPrecioService.GetListaPrecio(id);

            if (lista == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ListaPrecioFormModel();
            deleteVM.Id = lista.Id;

            ViewBag.Message = "Confirma que desea realizar esta operación?";

            return PartialView("_Delete", deleteVM);
        }

        public ActionResult ValidationDeleteListaPrecio(Guid id)
        {
            ListaPrecio lista = listaPrecioService.GetListaPrecio(id);

            if (lista == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ListaPrecioFormModel();
            deleteVM.Id = lista.Id;

            ViewBag.Message = "No es posible eliminar dicha lista de precio porque tiene productos asociados.";

            return PartialView("_Validation", deleteVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!listaPrecioService.GetListaPrecioAny(id))
            {
                listaPrecioService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateListaPrecioProducto(ListaPrecioProductoFormModel listaVM)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in listaVM.ProductosId)
                {
                    Precio precio = new Precio();
                    precio.Id = Guid.NewGuid();
                    precio.ProductoId = item;
                    precio.ListaPrecioId = listaVM.Id;
                    precio.FechaDesde = DateTime.Now.Date;
                    precio.FechaHasta = DateTime.Now.Date;
                    precioService.Add(precio);
                }
                return Json(new { success = true });
            }
            return Json(new { success = true });
        }
    }
}
