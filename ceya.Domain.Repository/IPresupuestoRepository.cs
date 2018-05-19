using System;
using System.Collections.Generic;
using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IPresupuestoRepository : IRepository<Presupuesto>
    {
        long MaxCodigo();
        IPagedList<Presupuesto> GetPresupuestosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString, Guid? estadoId);
    }
}
