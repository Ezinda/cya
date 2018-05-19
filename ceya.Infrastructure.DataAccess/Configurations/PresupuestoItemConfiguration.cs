using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class PresupuestoItemConfiguration : EntityTypeConfiguration<PresupuestoItem>
    {
        public PresupuestoItemConfiguration()
        {
            HasOptional(t => t.Archivo).WithMany(x => x.PresupuestoItem).HasForeignKey(t => t.ArchivoTipologiaId);
            HasOptional(t => t.PrecioVidrio).WithMany(x => x.PresupuestoItemVidrios).HasForeignKey(t => t.VidriosId);
            HasOptional(t => t.PrecioColocacion).WithMany(x => x.PresupuestoItemColocacion).HasForeignKey(t => t.ColocacionId);

            this.HasOptional(e => e.PresupuestoItemNuevo)
                .WithMany()
                .HasForeignKey(p => p.PresupuestoItemNuevoId);
            this.HasOptional(e => e.PresupuestoItemAnterior)
               .WithMany()
               .HasForeignKey(p => p.PresupuestoItemAnteriorId);
        }
    }
}
