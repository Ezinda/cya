using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ContrasitaService : IConstructoraService
    {
        private readonly IConstructoraRepository constructoraRepository;

        private readonly IUnitOfWork unitOfWork;

        public ContrasitaService(IConstructoraRepository constructoraRepository, IUnitOfWork unitOfWork)
        {
            this.constructoraRepository = constructoraRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Constructora constructora)
        {
            constructoraRepository.Add(constructora);
            Save();
        }

        public void Delete(Guid id)
        {
            var constructora = constructoraRepository.GetById(id);
            constructoraRepository.Delete(constructora);
            Save();
        }

        public IEnumerable<Constructora> GetConstructoras()
        {
            var constructoras = constructoraRepository.GetAll().OrderBy(x => x.Codigo);
            return constructoras;
        }

        public IEnumerable<Constructora> GetConstructorasFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var constructoras = constructoraRepository
                    .GetMany(x => x.Documento.Contains(search) ||
                        x.RazonSocial.Contains(search) ||
                        x.Apellido.Contains(search) ||
                        x.Nombre.Contains(search))
                        .OrderBy(x => x.Codigo);

                return constructoras;
            }
            return GetConstructoras();
        }

        public Constructora GetConstructora(Guid id)
        {
            var constructora = constructoraRepository
              .GetAll()
              .Where(x => x.Id == id)
              .FirstOrDefault();

            return constructora;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Constructora constructora)
        {
            constructoraRepository.Update(constructora);
            Save();
        }

        public bool GetConstructoraAny(Guid id)
        {
            var constructora = constructoraRepository.Get(x => x.Id == id);
            if (constructora.Presupuesto.Count > 0)
            {
                return true;
            }
            return false;
        }

        public IPagedList<Constructora> GetConstructorasByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return this.constructoraRepository.GetConstructorasByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString);
        }
    }
}
