using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IPrecioService
    {
        IPagedList<Precio> GetPreciosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        Precio GetPrecio(Guid id);
        void Add(Precio precio);
        void Update(Precio precio);
        void Delete(Guid id);
        void Save();
    }
}
