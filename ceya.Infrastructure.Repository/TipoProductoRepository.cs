using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class TipoProductoRepository : RepositoryBase<TipoProducto>, ITipoProductoRepository
    {
        public TipoProductoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<TipoProducto> GetTipoProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<TipoProducto> tipoProductos;

            tipoProductos = from u in dbset
                     select u;

            switch (filterBy)
            {
                default:
                    tipoProductos = from s in this.dbset
                             select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                tipoProductos = tipoProductos.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    tipoProductos = tipoProductos.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    tipoProductos = tipoProductos.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    tipoProductos = tipoProductos.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    tipoProductos = tipoProductos.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    tipoProductos = tipoProductos.OrderBy(x => x.Codigo);
                    break;
            }

            // Usando X.PagedList
            return tipoProductos.ToPagedList(currentPage, noOfRecords);
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;
        }
    }
}
