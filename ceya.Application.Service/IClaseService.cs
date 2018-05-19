using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IClaseService
    {
        IEnumerable<Clase> GetClases();
        IEnumerable<Clase> GetClaseFilter(string search);
        IPagedList<Clase> GetClasesByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string search);
        Clase GetClase(Guid Id);
        void Add(Clase clase);
        void Update(Clase clase);
        void Delete(Guid id);
        void Save();
        bool GetClaseAny(Guid id);
    }
}
