using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetClientes();
        IEnumerable<Cliente> GetClientesFilter(string search);
        IPagedList<Cliente> GetClientesByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
        Cliente GetCliente(Guid id);
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Guid id);
        void Save();
        bool GetClienteAny(Guid id);
    }
}
