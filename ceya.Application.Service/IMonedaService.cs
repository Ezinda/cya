using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Service
{
    public interface IMonedaService
    {
        IEnumerable<Moneda> GetMonedas();
        IEnumerable<Moneda> GetMonedaFilter(string search);
        Moneda GetMoneda(Guid id);
        void Add(Moneda Moneda);
        void Update(Moneda Moneda);
        void Delete(Guid id);
        void Save();
        bool GetMonedaAny(Guid id);
    }
}
