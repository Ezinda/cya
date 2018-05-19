namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clase
    {

        public Clase()
        {
            this.Colores = new HashSet<Color>();
            this.Subrubros = new HashSet<Subrubro>();
        }

        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Subrubro> Subrubros { get; set; }
        public virtual ICollection<Color> Colores { get; set; }
    }
}
