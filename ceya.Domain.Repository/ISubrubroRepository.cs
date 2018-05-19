using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface ISubrubroRepository : IRepository<Subrubro>
    {
        int MaxCodigo();
        IPagedList<Subrubro> GetSubrubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
    }
}
