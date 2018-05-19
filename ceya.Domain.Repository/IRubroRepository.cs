using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IRubroRepository : IRepository<Rubro>
    {
        int MaxCodigo();
        IPagedList<Rubro> GetRubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);

    }
}
