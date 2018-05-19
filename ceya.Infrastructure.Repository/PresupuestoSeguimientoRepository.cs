using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using System.Collections.Generic;
using System;
using X.PagedList;

namespace ceya.Infrastructure.Repository
{
    public class PresupuestoSeguimientoRepository : RepositoryBase<PresupuestoSeguimiento>, IPresupuestoSeguimientoRepository
    {
        public PresupuestoSeguimientoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IPagedList<PresupuestoSeguimiento> GetSeguimientoByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, Guid presupuestoId)
        {
            IQueryable<PresupuestoSeguimiento> seguimientos;

            seguimientos = from p in this.dbset
                           select p;

            if (presupuestoId != null)
            {
                seguimientos = seguimientos
                    .Where(x => x.PresupuestoId == presupuestoId);
            }

            //for sorting based on date and popularity
            switch (sortBy)
            {
                case "Fecha":
                    seguimientos = seguimientos.OrderBy(x => x.Fecha);
                    break;
                case "Fecha_desc":
                    seguimientos = seguimientos.OrderByDescending(x => x.Fecha);
                    break;
                default:
                    seguimientos = seguimientos.OrderByDescending(x => x.Fecha);
                    break;
            }

            // NO usar EF
            // var skip = noOfRecords * (currentPage - 1);
            // presupuestos = presupuestos.Skip(skip).Take(noOfRecords);
            // return presupuestos.ToList();

            // Usando X.PagedList
            return seguimientos.ToPagedList(currentPage, noOfRecords);
        }
    }
}
