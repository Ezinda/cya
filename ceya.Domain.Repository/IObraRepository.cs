using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IObraRepository : IRepository<Obra>
    {
        long MaxCodigo();
        IPagedList<Obra> GetObrasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
    }
}
