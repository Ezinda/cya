using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;

namespace ceya.Infrastructure.Repository
{
    public class PresupuestoItemRepository : RepositoryBase<PresupuestoItem>, IPresupuestoItemRepository
    {
        public PresupuestoItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
