using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Service
{
    public interface IPresupuestoEstadoService
    {
        IEnumerable<PresupuestoEstado> GetEstados();
        PresupuestoEstado GetEstado(Guid Id);
        IEnumerable<PresupuestoEstado> GetEstados(string nombre);
    }
}
