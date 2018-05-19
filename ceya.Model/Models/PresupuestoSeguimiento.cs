using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Model.Models
{
    public class PresupuestoSeguimiento
    {
        public System.Guid Id { get; set; }
        public System.Guid PresupuestoId { get; set; }
        public System.Guid PresupuestoEstadoId { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime FechaAlerta { get; set; }
        public string Observacion { get; set; }
        public bool Activo { get; set; }
        public virtual Presupuesto Presupuesto { get; set; }
        public virtual PresupuestoEstado PresupuestoEstado { get; set; }
    }
}
