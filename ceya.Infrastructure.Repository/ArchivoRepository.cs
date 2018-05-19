using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using ceya.Model.Models;

namespace ceya.Infrastructure.Repository
{
    public class ArchivoRepository : RepositoryBase<Archivo>, IArchivoRepository
    {
        public ArchivoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
