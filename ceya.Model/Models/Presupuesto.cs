namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Presupuesto
    {
        public Presupuesto()
        {
            this.PresupuestoItem = new HashSet<PresupuestoItem>();
            this.PresupuestoSeguimiento = new HashSet<PresupuestoSeguimiento>();
        }

        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public System.Guid ArchivoTransaccionId { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<System.Guid> ClienteId { get; set; }
        public Nullable<System.Guid> ObraId { get; set; }
        public Nullable<System.Guid> PresupuestoCategoriaId { get; set; }
        public Nullable<System.Guid> ConstructoraId { get; set; }
        public string Solicita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DescripcionHeader { get; set; }
        public string DescripcionFooter { get; set; }
        public decimal ResumenCarpinteria { get; set; }
        public decimal ResumenTapajuntas { get; set; }
        public decimal ResumenVidrios { get; set; }
        public decimal ResumenColocacion { get; set; }
        public string  ConceptosVarios { get; set; }
        public decimal ResumenVarios { get; set; }
        public decimal ResumenSubtotal { get; set; }
        public decimal ResumenIva { get; set; }
        public decimal ResumenTotal { get; set; }
        public System.Guid PresupuestoEstadoId { get; set; }

        public Nullable<System.Guid> MonedaId { get; set; }
        public decimal Cotizacion { get; set; }

        public Nullable<System.Guid> SubrubroId { get; set; }
        public Nullable<System.Guid> SubrubroNombre { get; set; }
        public Nullable<System.Guid> ColorId { get; set; }
        public Nullable<System.Guid> ColorNombre { get; set; }
        public Nullable<System.Guid> VidrioId { get; set; }
        public Nullable<System.Guid> VidrioNombre { get; set; }
        public Nullable<System.Guid> ColocacionId { get; set; }
        public Nullable<System.Guid> ColocacionNombre { get; set; }

        public DateTime FechaCreacion { get; set; }
        public Nullable<Guid> PresupuestoAnteriorId { get; set; }
        public virtual Presupuesto PresupuestoAnterior { get; set; }
        public Nullable<Guid> PresupuestoNuevoId { get; set; }
        public virtual Presupuesto PresupuestoNuevo { get; set; }

        public virtual Obra Obra { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual Constructora Constructora { get; set; }
        public virtual PresupuestoCategoria PresupuestoCategoria { get; set; }
        public virtual PresupuestoEstado PresupuestoEstado { get; set; }
        public virtual ICollection<PresupuestoItem> PresupuestoItem { get; set; }
        public virtual ICollection<PresupuestoSeguimiento> PresupuestoSeguimiento { get; set; }
    }
}
