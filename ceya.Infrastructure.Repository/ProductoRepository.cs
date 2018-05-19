using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<Producto> GetProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<Producto> productos;

            productos = from u in dbset
                     select u;

            switch (filterBy)
            {
                default:
                    productos = from s in this.dbset
                                select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos
                    .Where(x => x.Codigo.ToString().Contains(searchString) ||
                    x.CodigoCompuesto.Contains(searchString) ||
                    x.CodigoProveedor.Contains(searchString) ||
                    x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    productos = productos.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    productos = productos.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    productos = productos.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    productos = productos.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    productos = productos.OrderBy(x => x.Codigo);
                    break;
            }

            // Usando X.PagedList
            return productos.ToPagedList(currentPage, noOfRecords);
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;

        }
    }
}
