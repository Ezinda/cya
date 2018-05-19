using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IRubroMaestroRepository : IRepository<RubroMaestro>
    {
        int MaxCodigo();
        IPagedList<RubroMaestro> GetRubroMaestrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);

    }
}
