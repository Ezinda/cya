using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using X.PagedList;

namespace ceya.Infrastructure.Repository
{
    public class ObraRepository : RepositoryBase<Obra>, IObraRepository
    {
        public ObraRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public long MaxCodigo()
        {
            return (dbset.Max(x => (long?)x.Codigo).GetValueOrDefault() + 1);
        }

        public IPagedList<Obra> GetObrasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Obra> obras;

            obras = from p in this.dbset select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                obras = obras.Where(x => x.CodigoObra.ToString().Contains(searchString)
                                       || x.Cliente.Nombre.Contains(searchString)
                                       || x.Domicilio.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "CodigoObra":
                        obras = obras.OrderBy(x => x.CodigoObra);
                        break;
                    case "Cliente":
                        obras = obras.OrderBy(s => s.Cliente);
                        break;
                    default:
                        obras = obras.OrderBy(x => x.CodigoObra);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "CodigoObra":
                        obras = obras.OrderByDescending(x => x.CodigoObra);
                        break;
                    case "Cliente":
                        obras = obras.OrderByDescending(s => s.Cliente);
                        break;
                    default:
                        obras = obras.OrderByDescending(x => x.CodigoObra);
                        break;
                }
            }

            return obras.ToPagedList(currentPage, noOfRecords);
        }
    }
}
