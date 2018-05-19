using System;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class SubrubroService : ISubrubroService
    {
        private readonly ISubrubroRepository subrubroRepository;

        private readonly IClaseService claseService;

        private readonly IUnitOfWork unitOfWork;

        public SubrubroService(ISubrubroRepository subrubroRepository, 
            IUnitOfWork unitOfWork,
            IClaseService claseService)
        {
            this.subrubroRepository = subrubroRepository;
            this.unitOfWork = unitOfWork;
            this.claseService = claseService;
        }

        public void Add(Subrubro subrubro)
        {
            subrubroRepository.Add(subrubro);
            Save();
        }

        public void Update(Subrubro subrubro)
        {
            subrubroRepository.Update(subrubro);
            Save();
        }

        public void Delete(Guid id)
        {
            var subrubro = subrubroRepository.GetById(id);
            subrubroRepository.Delete(subrubro);
            Save();
        }

        public IEnumerable<Subrubro> GetSubrubros()
        {
            var subrubros = subrubroRepository.GetAll().OrderBy(x => x.Codigo);
            return subrubros;
        }

        public Subrubro GetSubrubro(Guid id)
        {
            var subrubro = subrubroRepository.GetById(id);

            return subrubro;
        }

        public IEnumerable<Subrubro> GetSubrubroFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var subrubro = subrubroRepository
                    .GetMany(x => x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return subrubro;
            }
            return GetSubrubros();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public IPagedList<Subrubro> GetSubrubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.subrubroRepository.GetSubrubrosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public IEnumerable<Color> GetColoresRelacionados(Guid id)
        {
            Subrubro subrubro = GetSubrubro(id);

            IEnumerable<Color> colores = null;

            if (subrubro.Clase != null)
            {
                if (subrubro.Clase.Colores != null)
                {
                    colores = subrubro.Clase.Colores;
                }
            }
            return colores;
        }

        public bool GetSubrubroAny(Guid id)
        {
            var subrubro = subrubroRepository.Get(x => x.Id == id);
            if (subrubro.Rubro != null)
            {
                return true;
            }
            return false;
        }
    }
}
