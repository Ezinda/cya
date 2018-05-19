namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rol
    {

        public Rol()
        {
            this.Usuario = new HashSet<Usuario>();
        }
        
        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
