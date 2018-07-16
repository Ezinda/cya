using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Application.Service
{
    public interface IVendedorService
    {
        void Add(Vendedor vendedor);
        void Update(Vendedor vendedor);
        void Delete(Guid id);
        void Save();
        bool Get(Guid id);
        IEnumerable<Vendedor> GetVendedor();
        IEnumerable<Vendedor> GetVendedorFilter(string search);
        IPagedList<Vendedor> GetVendedorByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        Vendedor GetVendedor(Guid id);
    }

}
