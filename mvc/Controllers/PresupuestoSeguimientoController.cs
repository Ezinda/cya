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
    public class PresupuestoSeguimientoController : Controller
    {
        private readonly IPresupuestoSeguimientoService _presupuestoSeguimientoService;
        private readonly IPresupuestoEstadoService _presupuestoEstadoService;
        private readonly IPresupuestoService _presupuestoService;

        public PresupuestoSeguimientoController(
            IPresupuestoSeguimientoService presupuestoSeguimientoService,
            IPresupuestoEstadoService presupuestoEstadoService,
            IPresupuestoService presupuestoService)
        {
            _presupuestoSeguimientoService = presupuestoSeguimientoService;
            _presupuestoEstadoService = presupuestoEstadoService;
            _presupuestoService = presupuestoService;
        }

        public ActionResult Index(Guid? PresupuestoId = null)
        {
            SeguimientoPageViewModel vm = new SeguimientoPageViewModel();
            vm.PresupuestoId = PresupuestoId.Value;
            return RedirectToAction("List",vm.PresupuestoId);
        }

        public ActionResult Lookup(Guid PresupuestoId)
        {
            SeguimientoPageViewModel vm = new SeguimientoPageViewModel();
            vm.PresupuestoId = PresupuestoId;
            Presupuesto presupuesto = _presupuestoService.GetPresupuesto(PresupuestoId);
            vm.Cliente = presupuesto.Cliente.RazonSocial + 
                    presupuesto.Cliente.Apellido + " " + 
                    presupuesto.Cliente.Nombre;
            vm.Constructora = presupuesto.Constructora != null ? presupuesto.Constructora.RazonSocial +
                   presupuesto.Constructora.Apellido + " " +
                   presupuesto.Constructora.Nombre : string.Empty;
            vm.Email = presupuesto.Email;
            vm.Telefono = presupuesto.Telefono;
            vm.Solicita = presupuesto.Solicita;
            vm.Obra = presupuesto.Obra.Nombre;
            vm.Domicilio = presupuesto.Obra.Domicilio;
            return View(vm);
        }
 
        [HttpGet]
        public JsonResult GetSeguimiento(int page = 1, int pageSize = 10, string sortBy = "Fecha_desc", string filterBy = "All", Guid? PresupuestoId = null)
        {
            var pageList = this._presupuestoSeguimientoService.GetSeguimientosByPage(page, pageSize, sortBy, filterBy, PresupuestoId.Value);
            var seguimientosVM = Mapper.Map<IEnumerable<PresupuestoSeguimiento>, IEnumerable<SeguimientoListViewModel>>(pageList.ToList()).ToList();

            if (Request.IsAjaxRequest())
            {
                var result = new
                {
                    PageSize = pageList.PageSize,
                    Pages = pageList.PageCount,
                    CurrentPage = pageList.PageNumber,
                    Total = pageList.TotalItemCount,
                    Records = seguimientosVM
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(Guid Id)
        {
            Presupuesto presupuesto = _presupuestoService.GetPresupuesto(Id);
            string cliente = presupuesto.Cliente.RazonSocial + presupuesto.Cliente.Apellido + " " + presupuesto.Cliente.Nombre;
            string obra = presupuesto.Obra.Nombre;
            PresupuestoSeguimiento seguimientoActual = _presupuestoSeguimientoService.GetUltimoSeguimiento(Id);
            var estado = _presupuestoEstadoService.GetEstado(seguimientoActual.PresupuestoEstadoId).Descripcion;
            PresupuestoSeguimientoFormModel seguimientoVM = new PresupuestoSeguimientoFormModel();
            var estados = _presupuestoEstadoService.GetEstados(estado);
            seguimientoVM.Estados = estados.ToSelectListItems(Guid.Empty);
            seguimientoVM.NombreCliente = cliente;
            seguimientoVM.NombreObra = obra;
            seguimientoVM.Estado = estado;
            seguimientoVM.PresupuestoId = presupuesto.Id;
            return PartialView("Create", seguimientoVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(PresupuestoSeguimientoFormModel seguimientoVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _presupuestoSeguimientoService.UpdateActivo(seguimientoVM.PresupuestoId);

                    PresupuestoSeguimiento seguimiento;
                    seguimiento = new PresupuestoSeguimiento();
                    seguimiento.Id = Guid.NewGuid();
                    seguimiento.PresupuestoEstadoId = seguimientoVM.EstadoId;
                    seguimiento.PresupuestoId = seguimientoVM.PresupuestoId;
                    seguimiento.Fecha = seguimientoVM.Fecha;
                    seguimiento.FechaAlerta = seguimientoVM.FechaAlerta;
                    seguimiento.Observacion = seguimientoVM.Observacion;
                    seguimiento.Activo = true;
                    _presupuestoSeguimientoService.Add(seguimiento);
                    _presupuestoService.UpdateEstado(seguimiento);
                    return Json(new { success = true });
                }
                return Json(seguimientoVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

       
    }
}
