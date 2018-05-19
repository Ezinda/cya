using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IRubroService
    {
        IEnumerable<Rubro> GetRubros();
        IEnumerable<Rubro> GetRubroFilter(string search);
        IEnumerable<Subrubro> GetSubrubrosRelacionados(Guid id);
        IEnumerable<Rubro> GetRubrosRelacionados(Guid id);
        IPagedList<Rubro> GetRubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        Rubro GetRubro(Guid id);
        void Add(Rubro rubro);
        void Update(Rubro rubro);
        void Delete(Guid id);
        void Save();
        bool GetRubroAny(Guid id);
        
    }
}
