using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using ceya.Infrastructure.Repository;
using ceya.Model.Models;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class ListaPrecioRepository : RepositoryBase<ListaPrecio>, IListaPrecioRepository
    {
        public ListaPrecioRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<ListaPrecio> GetListaPreciosByPage(int currentPage, int noOfRecords, string sortBy, string direction,string filterBy, string searchString)
        {
            IQueryable<ListaPrecio> listaprecio;

            listaprecio = from u in dbset
                        select u;

            switch (filterBy)
            {
                default:
                    listaprecio = from s in this.dbset
                                select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                listaprecio = listaprecio
                    .Where(x => x.Codigo.ToString().Contains(searchString) ||
                    x.Nombre.Contains(searchString));
            }

            //for sorting based on date and popularity
            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Codigo":
                        listaprecio = listaprecio.OrderBy(x => x.Codigo);
                        break;
                    case "Descripcion":
                        listaprecio = listaprecio.OrderBy(s => s.Nombre);
                        break;
                    default:
                        listaprecio = listaprecio.OrderBy(x => x.Codigo);
                        break;
                }
            }
            else if(direction.Equals("desc"))
            {
                switch (sortBy)
                {
                    case "Codigo":
                        listaprecio = listaprecio.OrderByDescending(x => x.Codigo);
                        break;
                    case "Descripcion":
                        listaprecio = listaprecio.OrderByDescending(s => s.Nombre);
                        break;
                    default:
                        listaprecio = listaprecio.OrderByDescending(x => x.Codigo);
                        break;
                }
            }
            // Usando X.PagedList
            return listaprecio.ToPagedList(currentPage, noOfRecords);
        }
    }
}
