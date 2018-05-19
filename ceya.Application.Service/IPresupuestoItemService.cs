using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Service
{
    public interface IPresupuestoItemService
    {
        IEnumerable<PresupuestoItem> GetItems(Guid presupuestoId);
    }
}
