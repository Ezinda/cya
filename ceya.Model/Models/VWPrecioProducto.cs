using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Model.Models
{
    public class VWPrecioProducto
    {
        // PRECIO
        public Guid ListaId { get; set; }
        public string ListaCodigo { get; set; }
        public string ListaDescripcion { get; set; }
        public Guid PrecioId { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public decimal PrecioProducto { get; set; }
        // PRODUCTO
        public Guid ProductoId { get; set; }
        public int ProductoCodigo { get; set; }
        public string ProductoDescripcion { get; set; }
        public Guid? RubroId { get; set; }
        public int? RubroCodigo { get; set; }
        public string RubroDescripcion { get; set; }
        public bool RubroSistema { get; set; }
        public Guid? SubrubroId { get; set; }
        public int? SubrubroCodigo { get; set; }
        public string SubrubroDescripcion { get; set; }
        public Guid? ClaseId { get; set; }
        public int? ClaseCodigo { get; set; }
        public string ClaseDescripcion { get; set; }
        public Guid? ColorId { get; set; }
        public int? ColorCodigo { get; set; }
        public string ColorDescripcion { get; set; }
        public string ProductoCodigoCompuesto { get; set; }
        public string ProductoDescripcionCompuesto { get; set; }

    }
}
