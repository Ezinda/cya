namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PresupuestoCategoria
    {
        public PresupuestoCategoria()
        {
            this.Presupuesto = new HashSet<Presupuesto>();
        }
    
        public System.Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Presupuesto> Presupuesto { get; set; }
    }
}
