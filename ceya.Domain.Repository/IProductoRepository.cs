using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        int MaxCodigo();
        IPagedList<Producto> GetProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
    }
}
