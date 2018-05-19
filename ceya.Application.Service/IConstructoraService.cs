using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IConstructoraService
    {
        IEnumerable<Constructora> GetConstructoras();
        IEnumerable<Constructora> GetConstructorasFilter(string search);
        IPagedList<Constructora> GetConstructorasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        Constructora GetConstructora(Guid id);
        void Add(Constructora constructora);
        void Update(Constructora constructora);
        void Delete(Guid id);
        void Save();
        bool GetConstructoraAny(Guid id);
    }
}
