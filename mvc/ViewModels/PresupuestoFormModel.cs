using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class PresupuestoFormModel
    {
        public PresupuestoFormModel()
        {
            Fecha = DateTime.Now;
        }

        // Header
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public Guid ArchivoTransaccionId { get; set; }
        public DateTime Fecha { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ObraId { get; set; }
        public Guid? PresupuestoCategoriaId { get; set; }
        public Guid? ConstructoraId { get; set; }
        public string Solicita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DescripcionHeader { get; set; }
        public Guid? LineaColocacionId { get; set; }

        public Guid? SubrubroId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? ColocacionId { get; set; }
        public Guid? VidrioId { get; set; }

        public Guid? MonedaId { get; set; }
        public decimal Cotizacion { get; set; }

        // Footer
        public string DescripcionFooter { get; set; }
        public decimal ResumenCarpinteria { get; set; }
        public decimal ResumenTapajuntas { get; set; }
        public decimal ResumenVidrios { get; set; }
        public decimal ResumenColocacion { get; set; }
        public decimal ResumenSubtotal { get; set; }
        public decimal ResumenIva { get; set; }
        public decimal ResumenTotal { get; set; }
        public decimal ResumenVarios { get; set; }
        public string ConceptosVarios { get; set; }
      
        public virtual string NombreCliente { get; set; }
        public virtual string NombreObra { get; set; }
        public virtual string NombreConstructora { get; set; }

        public virtual string NombreSubrubro { get; set; }
        public virtual string NombreColor { get; set; }
        public virtual string NombreMoneda { get; set; }

        public virtual string NombreColocacion { get; set; }
        public virtual string NombreVidrio { get; set; }

        public virtual decimal PrecioColocacion { get; set; }
        public virtual decimal PrecioVidrio { get; set; }

        // Items
        public virtual IEnumerable<PresupuestoItemFormModel> Items { get; set; }

        // Seleccionables de cabecera
        public virtual IEnumerable<SelectListItem> Categorias { get; set; }
        public virtual IEnumerable<SelectListItem> Estados { get; set; }
        public virtual IEnumerable<SelectListItem> Vendedor { get; set; }
        public virtual IEnumerable<SelectListItem> Lineas { get; set; }
        public virtual IEnumerable<SelectListItem> Subrubros { get; set; }
        public virtual IEnumerable<SelectListItem> Colores { get; set; }
        public virtual IEnumerable<SelectListItem> Monedas { get; set; }
        public virtual IEnumerable<SelectListItem> PresupuestoColocaciones { get; set; }
        public virtual IEnumerable<SelectListItem> PresupuestoVidrios { get; set; }

        // Seleccionables de items
        public virtual IEnumerable<VWPrecioProducto> Productos { get; set; }
        public virtual IEnumerable<VWPrecioProducto> Colocaciones { get; set; }
        public virtual IEnumerable<VWPrecioProducto> Vidrios { get; set; }
    }
}