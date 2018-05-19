using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        long MaxCodigo();
        IPagedList<Cliente> GetClientesByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
    }
}
