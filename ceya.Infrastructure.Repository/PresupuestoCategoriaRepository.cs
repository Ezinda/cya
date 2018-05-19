using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;

namespace ceya.Infrastructure.Repository
{
    public class PresupuestoCategoriaRepository : RepositoryBase<PresupuestoCategoria>, IPresupuestoCategoriaRepository
    {
        public PresupuestoCategoriaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
