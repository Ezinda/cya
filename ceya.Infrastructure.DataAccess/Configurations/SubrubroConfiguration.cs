using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class SubrubroConfiguration : EntityTypeConfiguration<Subrubro>
    {  
        public SubrubroConfiguration()
        {
            Property(x => x.RubroId).IsOptional();
        }
    }
}
