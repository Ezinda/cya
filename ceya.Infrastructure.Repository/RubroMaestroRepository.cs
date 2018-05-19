using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using X.PagedList;
using System.Linq;
using System;

namespace ceya.Infrastructure.Repository
{
    public class RubroMaestroRepository : RepositoryBase<RubroMaestro>, IRubroMaestroRepository
    {
        public RubroMaestroRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<RubroMaestro> GetRubroMaestrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            IQueryable<RubroMaestro> RubroMaestros;

            RubroMaestros = from u in dbset
                        select u;

            switch (filterBy)
            {
                default:
                    RubroMaestros = from s in this.dbset
                                select s;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                RubroMaestros = RubroMaestros.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Descripcion.Contains(searchString));
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Codigo":
                    RubroMaestros = RubroMaestros.OrderBy(x => x.Codigo);
                    break;
                case "Codigo_desc":
                    RubroMaestros = RubroMaestros.OrderByDescending(x => x.Codigo);
                    break;
                case "Descripcion":
                    RubroMaestros = RubroMaestros.OrderBy(s => s.Descripcion);
                    break;
                case "Descripcion_desc":
                    RubroMaestros = RubroMaestros.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    RubroMaestros = RubroMaestros.OrderBy(x => x.Codigo);
                    break;
            }

            // Usando X.PagedList
            return RubroMaestros.ToPagedList(currentPage, noOfRecords);
        }

        public int MaxCodigo()
        {
            var codigo = dbset.Count() > 0 ? dbset.Max(x => x.Codigo) + 1 : 1;
            return codigo;

        }
    }
}
