namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            this.Producto = new HashSet<Producto>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
       
        public virtual ICollection<Producto> Producto { get; set; }
      
    }
}
