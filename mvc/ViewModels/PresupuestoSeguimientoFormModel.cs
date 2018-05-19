using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ceya.Infrastructure.DataAccess;

namespace mvc.ViewModels
{
    public class PresupuestoSeguimientoFormModel
    {
        public PresupuestoSeguimientoFormModel()
        {
            Fecha = DateTime.Now;
            FechaAlerta = DateTime.Now;
        }
        public System.Guid Id { get; set; }
        public System.Guid PresupuestoId { get; set; }
        public System.Guid EstadoId { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime FechaAlerta { get; set; }
        public string Observacion { get; set; }

        [Display(Name = "Cliente")]
        public virtual string NombreCliente { get; set; }
        [Display(Name = "Obra")]
        public virtual string NombreObra { get; set; }
        [Display(Name = "Estado Actual")]
        public virtual string Estado { get; set; }

        public virtual IEnumerable<SelectListItem> Estados { get; set; }
    }
}