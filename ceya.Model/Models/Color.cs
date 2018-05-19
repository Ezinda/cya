namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Color
    {

        public Color()
        {
            this.Clases = new HashSet<Clase>();
        }

        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Clase> Clases { get; set; }
    }
}
