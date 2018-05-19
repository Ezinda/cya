using System;
using ceya.Domain.Repository;
using ceya.Model.Models;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class PrecioService : IPrecioService
    {
        private readonly IPrecioRepository precioRepository;
        private readonly IUnitOfWork unitOfWork;
        public PrecioService(IPrecioRepository precioRepository, IUnitOfWork unitOfWork)
        {
            this.precioRepository = precioRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Precio precio)
        {
            precioRepository.Add(precio);
            Save();
        }

        public void Delete(Guid id)
        {
            var precio = precioRepository.GetById(id);
            precioRepository.Delete(precio);
            Save();
        }

        public Precio GetPrecio(Guid id)
        {
            var precio = precioRepository.GetById(id);

            return precio;
        }

        public IPagedList<Precio> GetPreciosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.precioRepository.GetPreciosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Precio precio)
        {
            precioRepository.Update(precio);
            Save();
        }
    }
}