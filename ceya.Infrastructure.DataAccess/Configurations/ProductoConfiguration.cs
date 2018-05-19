using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF.Configurations
{
    public class ProductoConfiguration : EntityTypeConfiguration<Producto>
    {
        public ProductoConfiguration()
        {
            Property(x => x.RubroId).IsOptional();
            Property(x => x.SubrubroId).IsOptional();
            Property(x => x.UnidadMedidaId).IsOptional();
        }
    }
}
