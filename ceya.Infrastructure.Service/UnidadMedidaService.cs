using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnidadMedidaRepository unidadMedidaRepository;

        private readonly IUnitOfWork unitOfWork;

        public UnidadMedidaService(IUnidadMedidaRepository unidadMedidaRepository, IUnitOfWork unitOfWork)
        {
            this.unidadMedidaRepository = unidadMedidaRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UnidadMedida> GetUnidadMedidas()
        {
            var unidadMedidas = unidadMedidaRepository.GetAll().OrderBy(x => x.Codigo);
            return unidadMedidas;
        }

        public IEnumerable<UnidadMedida> GetUnidadMedidaFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var unidades = unidadMedidaRepository
                    .GetMany(x => x.Abreviacion.Contains(search) ||
                        x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return unidades;
            }
            return GetUnidadMedidas();
        }

        public IPagedList<UnidadMedida> GetUnidadMedidasByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.unidadMedidaRepository.GetUnidadMedidasByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public UnidadMedida GetUnidadMedida(Guid id)
        {
            var unidad = unidadMedidaRepository.GetById(id);
              
            return unidad;
        }

        public void Add(UnidadMedida unidadMedida)
        {
            unidadMedidaRepository.Add(unidadMedida);
            Save();
        }

        public void Update(UnidadMedida unidadMedida)
        {
            unidadMedidaRepository.Update(unidadMedida);
            Save();
        }

        public void Delete(Guid id)
        {
            var unidadMedida = unidadMedidaRepository.GetById(id);
            unidadMedidaRepository.Delete(unidadMedida);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetUnidadMedidaAny(Guid id)
        {
            var unidad = unidadMedidaRepository.Get(x => x.Id == id);
            if (unidad.Producto.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
