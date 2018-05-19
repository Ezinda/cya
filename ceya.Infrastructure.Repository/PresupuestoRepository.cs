using ceya.Model.Models;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using System.Linq;
using System.Collections.Generic;
using System;
using X.PagedList;

namespace ceya.Infrastructure.Repository
{
    public class PresupuestoRepository : RepositoryBase<Presupuesto>, IPresupuestoRepository
    {
        public PresupuestoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public long MaxCodigo()
        {
            long codigo;

            try
            {
                codigo = dbset.Max(x => x.Codigo);
            }
            catch
            {
                codigo = 0;
            }

            return codigo;
        }

        public IPagedList<Presupuesto> GetPresupuestosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString, Guid? estadoId)
        {
            // NO ejecutar this.GetAll() o this.GetMany(...) esto es un gran inpacto a la base de datos
            // var presupuestos = this.GetAll();

            // Usando IQueryable
            IQueryable<Presupuesto> presupuestos;

            switch (filterBy)
            {
                case "Presupuestos Finalizados":
                    presupuestos = from p in this.dbset
                                   where p.PresupuestoNuevoId == null && p.PresupuestoEstado.Descripcion == "Finalizado"
                                   select p;
                    break;
                default:
                    presupuestos = from p in this.dbset
                                   where p.PresupuestoNuevoId == null
                                   select p;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                presupuestos = presupuestos.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Obra.Nombre.Contains(searchString)
                                       || x.Cliente.Nombre.Contains(searchString)
                                       || x.Solicita.Contains(searchString));
            }

            if (estadoId != null)
            {
                presupuestos = presupuestos.Where(x => x.PresupuestoEstadoId == estadoId);
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Codigo":
                        presupuestos = presupuestos.OrderBy(x => x.Codigo);
                        break;
                    case "Fecha":
                        presupuestos = presupuestos.OrderBy(s => s.Fecha);
                        break;
                    case "Solicita":
                        presupuestos = presupuestos.OrderBy(s => s.Solicita);
                        break;
                    default:
                        presupuestos = presupuestos.OrderBy(x => x.Codigo);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Codigo":
                        presupuestos = presupuestos.OrderByDescending(x => x.Codigo);
                        break;
                    case "Fecha":
                        presupuestos = presupuestos.OrderByDescending(s => s.Fecha);
                        break;
                    case "Solicita":
                        presupuestos = presupuestos.OrderByDescending(s => s.Solicita);
                        break;
                    default:
                        presupuestos = presupuestos.OrderByDescending(x => x.Codigo);
                        break;
                }
            }
            // NO usar EF
            // var skip = noOfRecords * (currentPage - 1);
            // presupuestos = presupuestos.Skip(skip).Take(noOfRecords);
            // return presupuestos.ToList();

            // Usando X.PagedList
            return presupuestos.ToPagedList(currentPage, noOfRecords);
        }
    }
}
