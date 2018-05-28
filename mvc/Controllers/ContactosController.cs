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
    public class ContactosController : Controller
    {

        IContactoService ContactoService;
        IConstructoraService _constructoraService;
        private readonly _contactoRepository contactoRepository;

        public ContactosController(IContactoService contactoService, IConstructoraService constructoraService)
        {
            _constructoraService = constructoraService;
            this.ContactoService = contactoService;
        }
        // GET: Contactos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var contactoVM = new ContactosFormModel();

            return View(contactoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactosFormModel contactoVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Contacto contacto = new Contacto();
                    contacto.Id = Guid.NewGuid();
                    contacto.Nombre = contactoVM.Nombre;
                    contacto.Domicilio = contactoVM.Domicilio;
                    contacto.Telefono = contactoVM.Telefono;
                    contacto.Email = contactoVM.Email;
                    contacto.ConstructoraId = contactoVM.ConstructoraId;
                    ContactoService.Add(contacto);

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "No se pudo guardar el contacto." });
                }
            }
            return Json(contactoVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contacto contacto = ContactoService.GetContactos(id);

            if (contacto == null)
            {
                return HttpNotFound();
            }

            var editVM = new ContactosFormModel();
            editVM.Id = contacto.Id;
            editVM.Nombre = contacto.Nombre;
            editVM.Domicilio = contacto.Domicilio;
            editVM.Telefono = contacto.Telefono;
            editVM.Email = contacto.Email;
            editVM.ConstructoraId = contacto.ConstructoraId;
          //  editVM.Constructora = contacto.Constructora.Nombre;

            return PartialView(editVM);
        }

        // POST: Contactos/Edit/5
        [HttpPost]
        public ActionResult Edit(ContactosFormModel contactoVM)
        {
            if (ModelState.IsValid)
            {
                Contacto contacto = new Contacto();
                contacto.Id = contactoVM.Id;

                contacto.Nombre = contactoVM.Nombre;
                contacto.Domicilio = contactoVM.Domicilio;
                contacto.ConstructoraId = contactoVM.ConstructoraId;
                ContactoService.Update(contacto);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetContactos(int? page, int? limit, string sortBy, string direction, string search = null)
        {
            int total;
            var records = GetContactos(page, limit, sortBy, direction, search, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public List<ContactoListViewModel> GetContactos(int? page, int? limit, string sortBy, string direction, string search, out int total)
        {
            var contactos = ContactoService.GetContactoFilter(search);
            total = contactos.Count();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = total;
            var records = (from x in contactos
                           select new ContactoListViewModel
                           {
                               Id = x.Id,

                               Nombre = x.Nombre,
                               Domicilio = x.Domicilio,
                               Telefono = x.Telefono,
                               Email = x.Email,
                               Constructora = x.Constructora.Nombre
                           })
                           .AsQueryable();

            var totalPages = (int)Math.Ceiling((float)total / (float)total);

            direction = direction == null ? "ASC" : direction;

            if (direction.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.Codigo);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                records = records.OrderBy(s => s.Codigo);
                records = records.Skip(pageIndex * pageSize).Take(pageSize);
            }

            return records.ToList();
        }


        public ActionResult List(string sortBy = "Nombre", string direction = "asc", string filterBy = "All", string searchString = "",
                int pageSize = 10, int page = 1)
        {
            var pageList = ContactoService.GetContactoByPage(page, pageSize, sortBy, direction, filterBy, searchString);
            var contactosVM = Mapper.Map<IEnumerable<Contacto>, IEnumerable<ContactoListViewModel>>(pageList.ToList()).ToList();


            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = contactosVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var pageVM = new ContactoPageViewModel(filterBy, sortBy);
            pageVM.SearchString = searchString;
            pageVM.List = contactosVM;

            return View("List", pageVM);
        }



        // GET: Contactos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contactos/Delete/5
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

        public JsonResult JsonAutocomplete(string valor, Guid? constructoraId)
        {
            Expression<Func<Contacto, bool>> whereExpression = x => true;
            var contactos = ContactoService.GetContactoFilterWithConstructora(valor, constructoraId);
            var sourceData = contactos
                .Select(x => new
                {
                    key = x.Id,
                    value = Codificable.GenerateStringForSearchWithoutParseCode(
                        x.Nombre,
                        x.Domicilio),
                    data = new { nombre = x.Nombre, domicilio = x.Domicilio, telefono = x.Telefono, email = x.Email }
                })
                .ToList();
            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }

    }
}
