namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            this.Producto = new HashSet<Producto>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Abreviacion { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
