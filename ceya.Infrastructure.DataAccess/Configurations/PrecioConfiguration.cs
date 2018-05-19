using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class PrecioConfiguration : EntityTypeConfiguration<Precio>
    {
        public PrecioConfiguration()
        {
            HasMany(x => x.PresupuestoItemVidrios).WithOptional(x => x.PrecioVidrio).HasForeignKey(x => x.VidriosId);
            HasMany(x => x.PresupuestoItemColocacion).WithOptional(x => x.PrecioColocacion).HasForeignKey(x => x.ColocacionId);
        }
    }
}
