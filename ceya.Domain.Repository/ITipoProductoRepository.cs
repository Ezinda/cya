using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface ITipoProductoRepository : IRepository<TipoProducto>
    {
        int MaxCodigo();
        IPagedList<TipoProducto> GetTipoProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);


    }
}
