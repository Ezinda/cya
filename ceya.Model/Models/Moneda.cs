namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Moneda
    {

        public Moneda()
        {
            this.Presupuestos = new HashSet<Presupuesto>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Presupuesto> Presupuestos { get; set; }

    }
}
