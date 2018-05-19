using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using X.PagedList;
using System;

namespace ceya.Infrastructure.Repository
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;
        }

        public IPagedList<Color> GetColoresByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            // Usando IQueryable
            IQueryable<Color> colores;

            switch (filterBy)
            {
               default:
                    colores = from c in this.dbset
                                   select c;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                colores = colores.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    colores = colores.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    colores = colores.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    colores = colores.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    colores = colores.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    colores = colores.OrderBy(x => x.Codigo);
                    break;
            }
            
            // Usando X.PagedList
            return colores.ToPagedList(currentPage, noOfRecords);

        }
    }
}
