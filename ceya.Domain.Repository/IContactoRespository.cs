using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface _contactoRepository : IRepository<Contacto>
    {
        IPagedList<Contacto> GetByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        IPagedList<Contacto> GetContactoByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);  
    }
}
