using System;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class RubroService : IRubroService
    {
        private readonly IRubroRepository rubroRepository;

        private readonly IRubroMaestroService rubroMaestroService;

        private readonly IUnitOfWork unitOfWork;

        public RubroService(IRubroRepository rubroRepository,
            IRubroMaestroService rubroMaestroService,
            IUnitOfWork unitOfWork)
        {
            this.rubroRepository = rubroRepository;
            this.rubroMaestroService = rubroMaestroService;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Rubro rubro)
        {
            rubroRepository.Add(rubro);
            Save();
        }

        public void Update(Rubro rubro)
        {
            rubroRepository.Update(rubro);
            Save();
        }

        public void Delete(Guid id)
        {
            var rubro = rubroRepository.GetById(id);
            rubroRepository.Delete(rubro);
            Save();
        }

        public IEnumerable<Rubro> GetRubros()
        {
            var rubros = rubroRepository.GetAll()
                .OrderBy(x => x.Codigo);
            return rubros;
        }

        public Rubro GetRubro(Guid id)
        {
            var rubro = rubroRepository.GetById(id);

            return rubro;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Rubro> GetRubroFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var rubros = rubroRepository
                    .GetMany(x => x.Codigo.ToString().Contains(search) ||
                        x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return rubros;
            }
            return GetRubros();
        }

        public IPagedList<Rubro> GetRubrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.rubroRepository.GetRubrosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public IEnumerable<Subrubro> GetSubrubrosRelacionados(Guid id)
        {
            Rubro rubro = GetRubro(id);

            IEnumerable<Subrubro> subrubros = null;

            if (rubro.Subrubro != null)
            {
                subrubros = rubro.Subrubro;
            }
            return subrubros;
        }

        public IEnumerable<Rubro> GetRubrosRelacionados(Guid id)
        {
            RubroMaestro rubro = rubroMaestroService.GetRubroMaestro(id);

            IEnumerable<Rubro> rubros = null;

            if (rubro.Rubros != null)
            {
                rubros = rubro.Rubros;
            }
            return rubros;
        }

        public bool GetRubroAny(Guid id)
        {
            var rubro = rubroRepository.Get(x => x.Id == id);
            if (rubro.RubroMaestro != null)
            {
                return true;
            }
            return false;
        }

    }
}
