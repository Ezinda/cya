using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ListaPrecioService : IListaPrecioService
    {
        private readonly IListaPrecioRepository listaPrecioRepository;

        private readonly IUnitOfWork unitOfWork;

        public ListaPrecioService(IListaPrecioRepository listaPrecioRepository, IUnitOfWork unitOfWork)
        {
            this.listaPrecioRepository = listaPrecioRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(ListaPrecio listaPrecio)
        {
            listaPrecioRepository.Add(listaPrecio);
            Save();
        }

        public void Delete(Guid id)
        {
            var listaPrecio = listaPrecioRepository.GetById(id);
            listaPrecioRepository.Delete(listaPrecio);
            Save();
        }
        
        public ListaPrecio GetListaPrecio(Guid id)
        {
            var listaPrecio = listaPrecioRepository.GetById(id);

            return listaPrecio;
        }

        public bool GetListaPrecioAny(Guid id)
        {
            var lista = listaPrecioRepository.Get(x => x.Id == id);
            if (lista.Precio.Count > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ListaPrecio> GetListaPrecios()
        {
            var listaPrecios = listaPrecioRepository.GetAll().OrderBy(x => x.Codigo);
            return listaPrecios;

        }

        public IPagedList<ListaPrecio> GetListaPreciosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return this.listaPrecioRepository.GetListaPreciosByPage(currentPage, noOfRecords, sortBy, direction,filterBy, searchString);

        }

        public IEnumerable<Precio> GetPrecioProductosRelacionados(Guid id)
        {
            ListaPrecio lista = GetListaPrecio(id);

            IEnumerable<Precio> precios = null;

            if (lista.Precio != null)
            {
                precios = lista.Precio;
            }
            return precios;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(ListaPrecio listaPrecio)
        {
            listaPrecioRepository.Update(listaPrecio);
            Save();
        }
    }
}