using AutoMapper;
using ceya.Application.Service;
using ceya.Domain.Service;
using ceya.Model.Models;
using mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using ceya.Core.Helpers;
using ceya.Domain.Model.Extensions;
using ceya.Domain.Repository;
using System.Net;

namespace mvc.Controllers
{
    public class VendedorController : Controller
    {

        IVendedorService VendedorService;
        private readonly _vendedorRepository vendedorRepository;

        public VendedorController(IVendedorService vendedorService)
        {
            this.VendedorService = vendedorService;
        }
        // GET: Vendedor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Vendedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var vendedorVM = new VendedorFormModel();

            return View(vendedorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendedorFormModel vendedorVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vendedor vendedor = new Vendedor();
                    vendedor.Id = Guid.NewGuid();
                    vendedor.Nombre = vendedorVM.Nombre;
                    vendedor.Domicilio = vendedorVM.Domicilio;
                    vendedor.Telefono = vendedorVM.Telefono;
                    vendedor.Email = vendedorVM.Email;
                    VendedorService.Add(vendedor);

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "No se pudo guardar el vendedor." });
                }
            }
            return Json(vendedorVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Vendedor/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendedor vendedor = VendedorService.GetVendedor(id);

            if (vendedor == null)
            {
                return HttpNotFound();
            }

            var editVM = new VendedorFormModel();
            editVM.Id = vendedor.Id;
            editVM.Nombre = vendedor.Nombre;
            editVM.Domicilio = vendedor.Domicilio;
            editVM.Telefono = vendedor.Telefono;
            editVM.Email = vendedor.Email;

            return PartialView(editVM);
        }

        // POST: Vendedor/Edit/5
        [HttpPost]
        public ActionResult Edit(VendedorFormModel vendedorVM)
        {
            if (ModelState.IsValid)
            {
                Vendedor vendedor = new Vendedor();
                vendedor.Id = vendedorVM.Id;

                vendedor.Nombre = vendedorVM.Nombre;
                vendedor.Domicilio = vendedorVM.Domicilio;
                vendedor.Telefono = vendedorVM.Telefono;
                vendedor.Email = vendedorVM.Email;
                VendedorService.Update(vendedor);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetVendedor(int? page, int? limit, string sortBy, string direction, string search = null)
        {
            int total;
            var records = GetVendedor(page, limit, sortBy, direction, search, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public List<VendedorListViewModel> GetVendedor(int? page, int? limit, string sortBy, string direction, string search, out int total)
        {
            var vendedors = VendedorService.GetVendedorFilter(search);
            total = vendedors.Count();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = total;
            var records = (from x in vendedors
                           select new VendedorListViewModel
                           {
                               Id = x.Id,

                               Nombre = x.Nombre,
                               Domicilio = x.Domicilio,
                               Telefono = x.Telefono,
                               Email = x.Email,
                           })
                           .AsQueryable();

            var totalPages = (int)Math.Ceiling((float)total / (float)total);

            direction = direction == null ? "ASC" : direction;

            if (direction.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.Nombre);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                records = records.OrderBy(s => s.Nombre);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }

            return records.ToList();
        }


        public ActionResult List(string sortBy = "Nombre", string direction = "asc", string filterBy = "All", string searchString = "",
                int pageSize = 10, int page = 1)
        {
            var pageList = VendedorService.GetVendedorByPage(page, pageSize, sortBy, direction, filterBy, searchString);
            var vendedorsVM = Mapper.Map<IEnumerable<Vendedor>, IEnumerable<VendedorListViewModel>>(pageList.ToList()).ToList();


            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = vendedorsVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new VendedorPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = vendedorsVM;

            return View("List", pageVM);
        }



        // GET: Vendedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vendedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

      }
}
