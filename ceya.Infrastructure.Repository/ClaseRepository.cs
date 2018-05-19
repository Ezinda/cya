using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using X.PagedList;
using System;

namespace ceya.Infrastructure.Repository
{
    public class ClaseRepository : RepositoryBase<Clase>, IClaseRepository
    {
        public ClaseRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;
        }

        public IPagedList<Clase> GetClasesByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<Clase> clases;

            switch (filterBy)
            {
                default:
                    clases = from c in this.dbset
                             select c;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                clases = clases.Where(x => x.Codigo.ToString().Contains(searchString)
                                   || x.Descripcion.Contains(searchString));
            }

            switch (sortBy)
            {
                case "Codigo":
                    clases = clases.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    clases = clases.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    clases = clases.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    clases = clases.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    clases = clases.OrderBy(x => x.Codigo);
                    break;
            }

            return clases.ToPagedList(currentPage, noOfRecords);
        }
    }
}
