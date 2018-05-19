using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IRubroMaestroService
    {
        IEnumerable<RubroMaestro> GetRubroMaestros();
        IEnumerable<RubroMaestro> GetRubroMaestroFilter(string search);
        IEnumerable<Rubro> GetRubroMaestrosRelacionados(Guid id);
        IPagedList<RubroMaestro> GetRubroMaestrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        RubroMaestro GetRubroMaestro(Guid id);
        void Add(RubroMaestro RubroMaestro);
        void Update(RubroMaestro RubroMaestro);
        void Delete(Guid id);
        void Save();
        bool GetRubroAny(Guid id);
    }
}
