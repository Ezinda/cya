using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;

        private readonly IUnitOfWork unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            this.clienteRepository = clienteRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            var clientes = clienteRepository.GetAll().OrderBy(x => x.Codigo);
            return clientes;
        }

        public IEnumerable<Cliente> GetClientesFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var clientes = clienteRepository
                    .GetMany(x => x.Documento.Contains(search) ||
                        x.RazonSocial.Contains(search) ||
                        x.Apellido.Contains(search) ||
                        x.Nombre.Contains(search))
                        .OrderBy(x => x.Codigo);

                return clientes;
            }
            return GetClientes();
        }

        public Cliente GetCliente(Guid id)
        {
            var cliente = clienteRepository.GetById(id);

            return cliente;
        }

        public void Add(Cliente cliente)
        {
            clienteRepository.Add(cliente);
            Save();
        }

        public void Update(Cliente cliente)
        {
            clienteRepository.Update(cliente);
            Save();
        }

        public void Delete(Guid id)
        {
            var cliente = clienteRepository.GetById(id);
            clienteRepository.Delete(cliente);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetClienteAny(Guid id)
        {
            var cliente = clienteRepository.Get(x => x.Id == id);
            if (cliente.Obra.Count > 0)
            {
                return true;
            }
            return false;
        }

        public IPagedList<Cliente> GetClientesByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return this.clienteRepository.GetClientesByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString);
        }
    }
}
