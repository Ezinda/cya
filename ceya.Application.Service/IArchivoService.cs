using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Service
{
    public interface IArchivoService
    {
        IEnumerable<Archivo> GetArchivosPorTransaccion(Guid transaccionId);
    }
}
