using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceya.Application.Service;
using ceya.Model.Models;
using ceya.Domain.Repository;
using X.PagedList;

namespace ceya.Infrastructure.Service
{
    public class vendedorService : IVendedorService
    {
        private readonly _vendedorRepository vendedorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public vendedorService(_vendedorRepository vendedorRepository, IUnitOfWork unitOfWork)
        {
            this.vendedorRepository = vendedorRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(Vendedor vendedor)
        {
            vendedorRepository.Add(vendedor);
            Save();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Vendedor vendedor)
        {
            vendedorRepository.Update(vendedor);
            Save();
        }


        public IEnumerable<Vendedor> GetVendedor()
        {
            var vendedor = vendedorRepository.GetAll().OrderBy(x => x.Nombre);
            return vendedor;
        }


        IEnumerable<Vendedor> IVendedorService.GetVendedorFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var vendedor = vendedorRepository.GetMany(x => x.Nombre.Contains(search) ||
                        x.Domicilio.Contains(search) ||
                        x.Telefono.Contains(search) ||
                        x.Email.Contains(search))
                        .OrderBy(x => x.Nombre);

                return vendedor;
            }
            return GetVendedor();
        }

        public object GetVendedorByPage(int page, int pageSize, string sortBy, string direction, string filterBy, string searchString)
        {
            throw new NotImplementedException();
        }

        IPagedList<Vendedor> IVendedorService.GetVendedorByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return vendedorRepository.GetVendedorByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString);
        }

        public IEnumerable<Vendedor> GetVendedorFilter(string search)
        {

            if (!string.IsNullOrEmpty(search))
            {
                var vendedor = vendedorRepository.GetMany(x => x.Nombre.Contains(search) ||
                        x.Domicilio.Contains(search) ||
                        x.Telefono.Contains(search) ||
                        x.Email.Contains(search))
                        .OrderBy(x => x.Nombre);

                return vendedor;
            }
            return GetVendedor();

        }

        public Vendedor GetVendedor(Guid id)
        {
            var cliente = vendedorRepository.GetById(id);

            return cliente;
        }
    }
}
