namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Rubro
    {
        public Rubro()
        {
            this.Producto = new HashSet<Producto>();
            this.Subrubro = new HashSet<Subrubro>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool? Sistema { get; set; }
        public Nullable<System.Guid> RubroMaestroId { get; set; }

        public virtual RubroMaestro RubroMaestro { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Subrubro> Subrubro { get; set; }

    }
}
