using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IConstructoraRepository : IRepository<Constructora>
    {
        long MaxCodigo();
        IPagedList<Constructora> GetConstructorasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
    }
}
