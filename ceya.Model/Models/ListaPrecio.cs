namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ListaPrecio
    {
        public ListaPrecio()
        {
            this.Precio = new HashSet<Precio>();
        }
    
        public System.Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public bool Predefinida { get; set; }
    
        public virtual ICollection<Precio> Precio { get; set; }
    }
}
