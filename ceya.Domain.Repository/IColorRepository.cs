using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IColorRepository : IRepository<Color>
    {
        int MaxCodigo();
        IPagedList<Color> GetColoresByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
    }
}
