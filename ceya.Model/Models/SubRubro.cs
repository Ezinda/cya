namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subrubro
    {
        public Subrubro()
        {
            this.Producto = new HashSet<Producto>();
        }
    
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.Guid> RubroId { get; set; }
        public Nullable<System.Guid> ClaseId { get; set; }

        public virtual Rubro Rubro { get; set; }
        public virtual Clase Clase { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
