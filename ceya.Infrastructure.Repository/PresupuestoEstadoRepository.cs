using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;

namespace ceya.Infrastructure.Repository
{
    public class PresupuestoEstadoRepository : RepositoryBase<PresupuestoEstado>, IPresupuestoEstadoRepository
    {
        public PresupuestoEstadoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
