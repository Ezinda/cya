using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface ISubrubroService
    {
        IEnumerable<Subrubro> GetSubrubros();
        IEnumerable<Subrubro> GetSubrubroFilter(string search);
        IEnumerable<Color> GetColoresRelacionados(Guid id);
        IPagedList<Subrubro> GetSubrubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        Subrubro GetSubrubro(Guid id);
        void Add(Subrubro subrubro);
        void Update(Subrubro subrubro);
        void Delete(Guid id);
        void Save();
        bool GetSubrubroAny(Guid id);
    }
}
