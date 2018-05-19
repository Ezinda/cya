using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Model.Models;
using ceya.Domain.Model.Extensions;
using ceya.Domain.Service;
using ceya.Web.Core.Extensions;
using ceya.Domain.Repository;
using AutoMapper;

namespace mvc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository clienteRepository;

        private readonly IClienteService clienteService;

        private readonly ITipoDocumentoService tipoDocumentoService;

        private readonly IObraService obraService;

        public ClienteController(IClienteRepository clienteRepository, 
            IClienteService clienteService, 
            ITipoDocumentoService tipoDocumentoService, 
            IObraService obraService)
        {
            this.clienteRepository = clienteRepository;
            this.clienteService = clienteService;
            this.tipoDocumentoService = tipoDocumentoService;
            this.obraService = obraService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lookup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clienteVM = new ClienteFormModel();
            var tipoDocumento = tipoDocumentoService.GetTipoDocumentos();
            clienteVM.TipoDocumentos = tipoDocumento.ToSelectListItems(Guid.Empty);

            return PartialView("Create",clienteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "RazonSocial,Apellido,Nombre,Documento," +
            "TipoDocumentoId,Domicilio,Telefono,Celular,Email,CreatedDate")]
        ClienteFormModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente;
                cliente = new Cliente();
                cliente.Id = Guid.NewGuid();
                cliente.Codigo = clienteRepository.MaxCodigo();
                cliente.RazonSocial = clienteVM.RazonSocial;
                cliente.Apellido = clienteVM.Apellido;
                cliente.Nombre = clienteVM.Nombre;
                cliente.TipoDocumentoId = clienteVM.TipoDocumentoId;
                cliente.Documento = clienteVM.Documento;
                cliente.Domicilio = clienteVM.Domicilio;
                cliente.Telefono = clienteVM.Telefono;
                cliente.Celular = clienteVM.Celular;
                cliente.Email = clienteVM.Email;
                cliente.CreatedDate = DateTime.Now.Date;

                clienteService.Add(cliente);

                return Json(new { success = true });
            }
            return Json(clienteVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteService.GetCliente(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var editVM = new ClienteFormModel();
            editVM.Id = cliente.Id;
            editVM.Codigo = cliente.Codigo;
            editVM.RazonSocial = cliente.RazonSocial;
            editVM.Apellido = cliente.Apellido;
            editVM.Nombre = cliente.Nombre;
            editVM.TipoDocumentoId = cliente.TipoDocumentoId;
            editVM.Documento = cliente.Documento;
            editVM.Domicilio = cliente.Domicilio;
            editVM.Telefono = cliente.Telefono;
            editVM.Celular = cliente.Celular;
            editVM.Email = cliente.Email;
            editVM.CreatedDate = cliente.CreatedDate;

            var tipoDocumentos = tipoDocumentoService.GetTipoDocumentos();

            if (cliente.TipoDocumentoId != null)
            {
                editVM.TipoDocumentos = tipoDocumentos.ToSelectListItems(cliente.TipoDocumentoId.Value);
            }
            else
            {
                editVM.TipoDocumentos = tipoDocumentos.ToSelectListItems(Guid.Empty);
            }

            return PartialView(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,RazonSocial,Apellido,Nombre,Documento," + 
            "TipoDocumentoId,Domicilio,Telefono,Celular,Email,CreatedDate")] ClienteFormModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Id = clienteVM.Id;
                cliente.Codigo = clienteVM.Codigo;
                cliente.RazonSocial = clienteVM.RazonSocial;
                cliente.Apellido = clienteVM.Apellido;
                cliente.Nombre = clienteVM.Nombre;
                cliente.TipoDocumentoId = clienteVM.TipoDocumentoId;
                cliente.Documento = clienteVM.Documento;
                cliente.Domicilio = clienteVM.Domicilio;
                cliente.Telefono = clienteVM.Telefono;
                cliente.Celular = clienteVM.Celular;
                cliente.Email = clienteVM.Email;
                clienteService.Update(cliente);

                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(tipoDocumentoService.GetTipoDocumentos(), "Id", "Codigo", clienteVM.TipoDocumentoId);
            return View(clienteVM);
        }
     
        [HttpGet]
        public JsonResult GetClientes(int? page, int? limit, string sortBy, string direction, 
            string search)
        {
            int total;
            var records = GetClientes(page, limit, sortBy, direction, search, out total);
            var totalPages = (int)Math.Ceiling((float)records.Count/ (float)limit);
            direction = direction == null ? "ASC" : direction;

            return Json(new { records, totalPages }, JsonRequestBehavior.AllowGet);
        }

        public List<ClienteListViewModel> GetClientes(int? page, int? limit, string sortBy,
            string direction, string search, out int total)
        {
            var clientes = clienteService.GetClientesFilter(search);
            total = clientes.Count();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = total;
            var records = (from x in clientes
                           select new ClienteListViewModel
                           {
                               Id = x.Id,
                               Codigo = x.Codigo,
                               Cliente = x.RazonSocial + x.Apellido + " " + x.Nombre,
                               Documento = x.Documento,
                               Domicilio = x.Domicilio,
                               Telefono = x.Telefono,
                               Celular = x.Celular,
                               Email = x.Email
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

        public ActionResult List(string sortBy = "Codigo", string direction = "asc", string filterBy = "All", string searchString = "",
         int pageSize = 10, int page = 1)
        {
            var pageList = this.clienteService.GetClientesByPage(page, pageSize, sortBy, direction, filterBy, searchString);
            var obrasVM = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteListViewModel>>(pageList.ToList()).ToList();

            //if (Request.IsAjaxRequest())
            //{
            var result = new
            {
                PageSize = pageList.PageSize,
                Pages = pageList.PageCount,
                CurrentPage = pageList.PageNumber,
                Total = pageList.TotalItemCount,
                Records = obrasVM
            };
            return Json(result, JsonRequestBehavior.AllowGet);
            //}

            //var pageVM = new ObraPageViewModel(filterBy, sortBy);
            //pageVM.SearchString = searchString;
            //pageVM.List = obrasVM;

            //return View("List", pageVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!clienteService.GetClienteAny(id))
            {
                clienteService.Delete(id);

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelete(Guid id)
        {
            Cliente cliente = clienteService.GetCliente(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ClienteFormModel();
            deleteVM.Id = cliente.Id;
            
            ViewBag.Message = "Confirma que desea realizar esta operación?";
            
            return PartialView("_Delete",deleteVM);
        }

        public ActionResult ValidationDeleteCliente(Guid id)
        {
            Cliente cliente = clienteService.GetCliente(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var deleteVM = new ClienteFormModel();
            deleteVM.Id = cliente.Id;

            ViewBag.Message = "No es posible eliminar dicho cliente porque tiene obras asociadas.";

            return PartialView("_Validation", deleteVM);
        }

        public JsonResult JsonAutocomplete(string valor)
        {
            var clientes = clienteService.GetClientesFilter(valor);

            var sourceData = clientes
                .Select(x => new
                {
                    key = x.Id,
                    value = x.ToSearchNameString()
                })
                .ToList();

            return Json(sourceData, JsonRequestBehavior.AllowGet);
        }
    }
}
