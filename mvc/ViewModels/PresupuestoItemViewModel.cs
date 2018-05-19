using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class PresupuestoItemViewModel
    {
        public Guid Id { get; set; }
        public Guid TipologiaId { get; set; }
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Guid VidriosId { get; set; }
        public decimal VidriosPrecio { get; set; }
        public Guid ColocacionId { get; set; }
        public decimal ColocacionPrecio { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public decimal Carpinteria { get; set; }
        public decimal Tapajuntas { get; set; }
        public decimal VidriosCalculado { get; set; }
        public decimal ColocacionCalculado { get; set; }
        public string Detalle { get; set; }
        public decimal Importe { get; set; }

        public string Estado { get; set; }
    }
}