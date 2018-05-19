using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;

namespace ceya.Infrastructure.Repository
{
    public class MonedaRepository : RepositoryBase<Moneda>, IMonedaRepository
    {
        public MonedaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
