using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IListaPrecioRepository : IRepository<ListaPrecio>
    {
        IPagedList<ListaPrecio> GetListaPreciosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
    }
}
