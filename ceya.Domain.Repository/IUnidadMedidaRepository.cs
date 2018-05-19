using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IUnidadMedidaRepository : IRepository<UnidadMedida>
    {
        int MaxCodigo();
        IPagedList<UnidadMedida> GetUnidadMedidasByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
    }
}
