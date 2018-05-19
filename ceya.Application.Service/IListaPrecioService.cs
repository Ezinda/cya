using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IListaPrecioService
    {
        IPagedList<ListaPrecio> GetListaPreciosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        IEnumerable<ListaPrecio> GetListaPrecios();
        IEnumerable<Precio> GetPrecioProductosRelacionados(Guid id);
        ListaPrecio GetListaPrecio(Guid id);
        void Add(ListaPrecio listaPrecio);
        void Update(ListaPrecio listaPrecio);
        void Delete(Guid id);
        void Save();
        bool GetListaPrecioAny(Guid id);
    }
}
