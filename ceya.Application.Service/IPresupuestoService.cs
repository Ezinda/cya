using ceya.Core.Common;
using ceya.Domain.Model.DTOs;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IPresupuestoService
    {
        Presupuesto GetPresupuesto(Guid Id);
        long GetNuevoCodigo();
        IPagedList<Presupuesto> GetPresupuestosByPage(int currentPage, int noOfRecords, string sortBy,string direction, string filterBy, string searchString,Guid? estadoId);
        void EditPresupuesto(Presupuesto presupuesto);
        IEnumerable<ValidationResult> CanAddPresupuesto(Presupuesto presupuestoPorEditar);
        IEnumerable<ValidationResult> CanAddNewRevisionPresupuesto(Presupuesto presupuestoPorEditar);
        void CreatePresupuesto(Presupuesto presupuesto);
        void UpdateEstado(PresupuestoSeguimiento seguimiento);
        IEnumerable<PrecioActualizado> ObtenerPreciosActualizados(Guid presupuestoId);
    }
}
