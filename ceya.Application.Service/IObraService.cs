using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IObraService
    {
        IEnumerable<Obra> GetObras();
        IEnumerable<Obra> GetObraFilter(string search);
        IEnumerable<Obra> GetObraFilterWithCliente(string search, Guid? clienteId);
        IPagedList<Obra> GetObrasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        Obra GetObra(Guid id);
        void Add(Obra obra);
        void Update(Obra obra);
        void Delete(Guid id);
        void Save();
        bool GetObraAny(Guid id);
    }
}
