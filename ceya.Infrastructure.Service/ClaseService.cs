using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ClaseService : IClaseService
    {
        private readonly IClaseRepository claseRepository;

        private readonly IUnitOfWork unitOfWork;

        public ClaseService(IClaseRepository claseRepository, IUnitOfWork unitOfWork)
        {
            this.claseRepository = claseRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Clase> GetClases()
        {
            var Clases = claseRepository.GetAll().OrderBy(x => x.Codigo);
            return Clases;
        }

        public IEnumerable<Clase> GetClaseFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var clases = claseRepository
                    .GetMany(x => x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return clases;
            }
            return GetClases();
        }

        public IPagedList<Clase> GetClasesByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.claseRepository.GetClasesByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public Clase GetClase(Guid id)
        {
            var clase = claseRepository.GetById(id);
              
            return clase;
        }

        public void Add(Clase Clase)
        {
            claseRepository.Add(Clase);
            Save();
        }

        public void Update(Clase Clase)
        {
            claseRepository.Update(Clase);
            Save();
        }

        public void Delete(Guid id)
        {
            var Clase = claseRepository.GetById(id);
            claseRepository.Delete(Clase);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetClaseAny(Guid id)
        {
            var clase = claseRepository.Get(x => x.Id == id);
            if (clase.Subrubros != null)
            {
                return true;
            }
            return false;
        }
    }
}
