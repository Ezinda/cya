using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class ArchivoConfiguration : EntityTypeConfiguration<Archivo>
    {
        public ArchivoConfiguration()
        {
            //base.HasKey(x => x.Id);
            //Property(x => x.GoalName).HasMaxLength(50);
            //Property(x => x.Description).HasMaxLength(100);
            //Property(x => x.StartDate).IsRequired();
            //Property(x => x.EndDate).IsRequired();
            //Property(x => x.GoalStatusId).IsRequired();
            //Property(x => x.GroupUserId).IsRequired();
            HasMany(x => x.PresupuestoItem).WithOptional(x => x.Archivo);
        }
    }
}
