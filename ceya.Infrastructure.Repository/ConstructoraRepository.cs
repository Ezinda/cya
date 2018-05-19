using System;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Infrastructure.Repository
{
    public class ConstructoraRepository : RepositoryBase<Constructora>, IConstructoraRepository
    {
        public ConstructoraRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<Constructora> GetConstructorasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Constructora> constructoras;

            constructoras = from p in this.dbset select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                constructoras = constructoras.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Documento.Contains(searchString)
                                       || x.RazonSocial.Contains(searchString)
                                       || x.Apellido.Contains(searchString)
                                       || x.Nombre.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Codigo":
                        constructoras = constructoras.OrderBy(x => x.Codigo);
                        break;
                    case "Cliente":
                        constructoras = constructoras.OrderBy(s => s.Apellido);
                        break;
                    default:
                        constructoras = constructoras.OrderBy(x => x.Apellido);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Codigo":
                        constructoras = constructoras.OrderByDescending(x => x.Codigo);
                        break;
                    case "Cliente":
                        constructoras = constructoras.OrderByDescending(s => s.Apellido);
                        break;
                    default:
                        constructoras = constructoras.OrderByDescending(x => x.Apellido);
                        break;
                }
            }

            return constructoras.ToPagedList(currentPage, noOfRecords);

        }

        public long MaxCodigo()
        {
            long codigo = dbset.Count() == 0 ? 1 : dbset.Max(x => x.Codigo) + 1;
            return codigo;
        }
    }
}
