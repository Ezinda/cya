using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface _vendedorRepository : IRepository<Vendedor>
    {
        IPagedList<Vendedor> GetByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        IPagedList<Vendedor> GetVendedorByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);  
    }
}
