namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        public Producto()
        {
            this.Precio = new HashSet<Precio>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoCompuesto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.Guid> UnidadMedidaId { get; set; }
        public Nullable<System.Guid> TipoProductoId { get; set; }
        public Nullable<System.Guid> RubroMaestroId { get; set; }
        public Nullable<System.Guid> RubroId { get; set; }
        public Nullable<System.Guid> SubrubroId { get; set; }

        public virtual ICollection<Precio> Precio { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
        public virtual RubroMaestro RubroMaestro { get; set; }
        public virtual Rubro Rubro { get; set; }
        public virtual Subrubro Subrubro { get; set; }
    }
}
