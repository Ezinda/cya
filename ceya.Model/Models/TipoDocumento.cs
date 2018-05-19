namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            this.Cliente = new HashSet<Cliente>();
        }
    
        public System.Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Orden { get; set; }
    
        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
