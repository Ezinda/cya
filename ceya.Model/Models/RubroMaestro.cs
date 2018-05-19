namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RubroMaestro
    {
        public RubroMaestro()
        {
            this.Producto = new HashSet<Producto>();
            this.Rubros = new HashSet<Rubro>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Rubro> Rubros { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
