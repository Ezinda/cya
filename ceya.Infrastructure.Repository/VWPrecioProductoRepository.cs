using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using ceya.Model.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ceya.Infrastructure.Repository
{
    public class VWPrecioProductoRepository : VWRepositoryBase<VWPrecioProducto>, IVWPrecioProductoRepository
    {
        public VWPrecioProductoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public virtual VWPrecioProducto GetById(Guid id)
        {
            return dbset.Where(x => x.PrecioId == id).SingleOrDefault();
        }

        public virtual VWPrecioProducto GetById(string id)
        {
            return this.GetById(Guid.Parse(id));
        }
    }
}
