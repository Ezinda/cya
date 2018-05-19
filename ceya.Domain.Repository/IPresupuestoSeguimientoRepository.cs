using System;
using System.Collections.Generic;
using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IPresupuestoSeguimientoRepository : IRepository<PresupuestoSeguimiento>
    {
        IPagedList<PresupuestoSeguimiento> GetSeguimientoByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, Guid presupuestoId);
    }
}
