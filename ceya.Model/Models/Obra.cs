namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Obra
    {
        public Obra()
        {
            this.Presupuesto = new HashSet<Presupuesto>();
        }
    
        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string CodigoObra { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public System.Guid ClienteId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Presupuesto> Presupuesto { get; set; }
    }
}
