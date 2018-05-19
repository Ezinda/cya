using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using X.PagedList;
using System;

namespace ceya.Infrastructure.Repository
{
    public class UnidadMedidaRepository : RepositoryBase<UnidadMedida>, IUnidadMedidaRepository
    {
        public UnidadMedidaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int MaxCodigo()
        {
            return dbset.Max(x => x.Codigo) + 1;
        }

        public IPagedList<UnidadMedida> GetUnidadMedidasByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<UnidadMedida> unidades;

            unidades = from u in this.dbset
                       select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                unidades = unidades
                    .Where(x => x.Codigo.ToString().Contains(searchString) ||
                        x.Abreviacion.Contains(searchString) ||
                        x.Descripcion.Contains(searchString));
            }

            switch (sortBy)
            {
                case "Codigo":
                    unidades = unidades.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    unidades = unidades.OrderByDescending(x => x.Codigo);
                    break;
                case "Abreviacion":
                    unidades = unidades.OrderBy(x => x.Abreviacion);
                    break;
                case "Abreviacion_desc":
                    unidades = unidades.OrderByDescending(x => x.Abreviacion);
                    break;
                case "Descripcion":
                    unidades = unidades.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    unidades = unidades.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    unidades = unidades.OrderBy(x => x.Codigo);
                    break;
            }

            return unidades.ToPagedList(currentPage, noOfRecords);
        }

    }
}
