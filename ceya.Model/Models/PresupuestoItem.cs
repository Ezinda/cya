namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PresupuestoItem
    {
        public System.Guid Id { get; set; }
        public System.Guid PresupuestoId { get; set; }
        public Nullable<System.Guid> ArchivoTipologiaId { get; set; }
        public int NumeroPosicion { get; set; }
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Nullable<System.Guid> VidriosId { get; set; }
        public decimal VidriosPrecio { get; set; }
        public Nullable<System.Guid> ColocacionId { get; set; }
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

        public Nullable<Guid> PresupuestoItemAnteriorId { get; set; }
        public virtual PresupuestoItem PresupuestoItemAnterior { get; set; }
        public Nullable<Guid> PresupuestoItemNuevoId { get; set; }
        public virtual PresupuestoItem PresupuestoItemNuevo { get; set; }

        public virtual Archivo Archivo { get; set; }
        public virtual Precio PrecioVidrio { get; set; }
        public virtual Precio PrecioColocacion { get; set; }
        public virtual Presupuesto Presupuesto { get; set; }
    }
}
