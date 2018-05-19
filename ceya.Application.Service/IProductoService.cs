using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IProductoService
    {
        IPagedList<Producto> GetProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        Producto GetProducto(Guid id);
        void Add(Producto producto);
        void Update(Producto producto);
        void Delete(Guid id);
        void Save();
        bool Exists(Guid id);
        string GetCodigoCompuesto(int codigo, Guid rubroId, Guid? subrubroId);
    }
}
