namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Precio
    {
        public Precio()
        {
            this.PresupuestoItemColocacion = new HashSet<PresupuestoItem>();
            this.PresupuestoItemVidrios = new HashSet<PresupuestoItem>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public decimal PrecioProducto { get; set; }
        public System.Guid ProductoId { get; set; }
        public System.Guid ListaPrecioId { get; set; }
    
        public virtual ListaPrecio ListaPrecio { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual ICollection<PresupuestoItem> PresupuestoItemVidrios { get; set; }
        public virtual ICollection<PresupuestoItem> PresupuestoItemColocacion { get; set; }
    }
}
