using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class PrecioRepository : RepositoryBase<Precio>, IPrecioRepository
    {
        public PrecioRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<Precio> GetPreciosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<Precio> precios;

            precios = from u in dbset
                        select u;

            switch (filterBy)
            {
                default:
                    precios = from s in this.dbset
                                select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                //precios = precios
                //    .Where(x => x.Codigo.ToString().Contains(searchString) ||
                //    x.CodigoCompuesto.Contains(searchString) ||
                //    x.CodigoProveedor.Contains(searchString) ||
                //    x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            //switch (sortBy)
            //{
                //case "Codigo":
                //    precios = precios.OrderBy(x => x.Codigo);
                //    break;
                //case "Codigo_desc":
                //    precios = precios.OrderByDescending(x => x.Codigo);
                //    break;
                //case "Descripcion":
                //    precios = precios.OrderBy(s => s.Descripcion);
                //    break;
                //case "Descripcion_desc":
                //    precios = precios.OrderByDescending(s => s.Descripcion);
                //    break;
                //default:
                //    precios = precios.OrderBy(x => x.Codigo);
                //    break;
            //}

            // Usando X.PagedList
            return precios.ToPagedList(currentPage, noOfRecords);

        }
    }
}
