using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class RubroRepository : RepositoryBase<Rubro>, IRubroRepository
    {
        public RubroRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<Rubro> GetRubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<Rubro> rubros;

            rubros = from u in dbset
                        select u;

            switch (filterBy)
            {
                default:
                    rubros = from s in this.dbset
                                select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                rubros = rubros.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    rubros = rubros.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    rubros = rubros.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    rubros = rubros.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    rubros = rubros.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    rubros = rubros.OrderBy(x => x.Codigo);
                    break;
            }

            // Usando X.PagedList
            return rubros.ToPagedList(currentPage, noOfRecords);
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;

        }
    }
}
