using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IPrecioRepository : IRepository<Precio>
    {
        IPagedList<Precio> GetPreciosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);

    }
}
