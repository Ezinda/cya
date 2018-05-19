using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class SubrubroRepository : RepositoryBase<Subrubro>, ISubrubroRepository
    {
        public SubrubroRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<Subrubro> GetSubrubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<Subrubro> subrubros;

            subrubros = from u in dbset
                     select u;
            
            switch (filterBy)
            {
                default:
                    subrubros = from s in this.dbset
                              select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                subrubros = subrubros.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    subrubros = subrubros.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    subrubros = subrubros.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    subrubros = subrubros.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    subrubros = subrubros.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    subrubros = subrubros.OrderBy(x => x.Codigo);
                    break;
            }

            // Usando X.PagedList
            return subrubros.ToPagedList(currentPage, noOfRecords);
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;

        }
    }
}
