using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IUnidadMedidaService
    {
        IEnumerable<UnidadMedida> GetUnidadMedidas();
        IEnumerable<UnidadMedida> GetUnidadMedidaFilter(string search);
        IPagedList<UnidadMedida> GetUnidadMedidasByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string search);
        UnidadMedida GetUnidadMedida(Guid Id);
        void Add(UnidadMedida unidadMedida);
        void Update(UnidadMedida unidadMedida);
        void Delete(Guid id);
        void Save();
        bool GetUnidadMedidaAny(Guid id);
    }
}
