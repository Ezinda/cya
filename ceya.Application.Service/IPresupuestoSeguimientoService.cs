using ceya.Core.Common;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IPresupuestoSeguimientoService
    {
        PresupuestoSeguimiento GetUltimoSeguimiento(Guid Id);
        void Add(PresupuestoSeguimiento presupuestoSeguimiento);
        void UpdateActivo(Guid PresupuestoId);
        IPagedList<PresupuestoSeguimiento> GetSeguimientosByPage(int currentPage, int noOfRecords,
            string sortBy, string filterBy, Guid PresupuestoId);
    }
}
