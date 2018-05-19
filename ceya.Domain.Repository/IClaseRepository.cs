using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IClaseRepository : IRepository<Clase>
    {
        int MaxCodigo();
        IPagedList<Clase> GetClasesByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string search);
    }
}
