using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class PresupuestoConfiguration : EntityTypeConfiguration<Presupuesto>
    {
        public PresupuestoConfiguration()
        {
            // this.HasOptional(x => x.PresupuestoNuevo).WithOptionalPrincipal(x => x.PresupuestoAnterior);
            // this.HasOptional(x => x.PresupuestoAnterior).WithOptionalDependent(x => x.PresupuestoNuevo);
            // Aqui WithMany() no tiene sentido pero es la unica forma para setear la Foreign Key
            this.HasOptional(e => e.PresupuestoNuevo)
                .WithMany()
                .HasForeignKey(p => p.PresupuestoNuevoId);
            this.HasOptional(e => e.PresupuestoAnterior)
               .WithMany()
               .HasForeignKey(p => p.PresupuestoAnteriorId);
        }
    }
}
