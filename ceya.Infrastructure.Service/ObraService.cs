using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository obraRepository;

        private readonly IUnitOfWork unitOfWork;

        public ObraService(IObraRepository obraRepository, IUnitOfWork unitOfWork)
        {
            this.obraRepository = obraRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Obra> GetObras()
        {
            var obras = obraRepository.GetAll().OrderBy(x => x.Codigo);
            return obras;
        }

        public IEnumerable<Obra> GetObraFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var obras = obraRepository
                    .GetMany(x => x.CodigoObra.Contains(search) ||
                        x.Nombre.Contains(search) ||
                        x.Cliente.RazonSocial.Contains(search) ||
                        x.Cliente.Apellido.Contains(search) ||
                        x.Cliente.Nombre.Contains(search) ||
                        x.Cliente.Domicilio.Contains(search))
                        .OrderBy(x => x.Codigo);

                return obras;
            }
            return GetObras();
        }

        public IEnumerable<Obra> GetObraFilterWithCliente(string search, Guid? clienteId)
        {
            if (!string.IsNullOrEmpty(search) || clienteId != null)
            {
                var obras = obraRepository
                    .GetMany(x => x.CodigoObra.Contains(search) ||
                        x.Nombre.Contains(search))
                        .Where(x => x.ClienteId == clienteId)
                        .OrderBy(x => x.Codigo);

                return obras;
            }
            return GetObras();
        }

        public Obra GetObra(Guid id)
        {
            var cliente = obraRepository.GetById(id);

            return cliente;
        }

        public void Add(Obra obra)
        {
            obraRepository.Add(obra);
            Save();
        }

        public void Update(Obra obra)
        {
            obraRepository.Update(obra);
            Save();
        }

        public void Delete(Guid id)
        {
            var obra = obraRepository.GetById(id);
            obraRepository.Delete(obra);
            Save();
        }
        
        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetObraAny(Guid id)
        {
            var obra = obraRepository.Get(x => x.Id == id);

            if (obra.Presupuesto.Count > 0)
            {
                return true;
            }
            return false;
        }

        public IPagedList<Obra> GetObrasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return this.obraRepository.GetObrasByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString);
        }
    }
}