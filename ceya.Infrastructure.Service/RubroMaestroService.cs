using System;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class RubroMaestroService : IRubroMaestroService
    {
        private readonly IRubroMaestroRepository RubroMaestroRepository;

        private readonly IUnitOfWork unitOfWork;

        public RubroMaestroService(IRubroMaestroRepository RubroMaestroRepository, IUnitOfWork unitOfWork)
        {
            this.RubroMaestroRepository = RubroMaestroRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(RubroMaestro RubroMaestro)
        {
            RubroMaestroRepository.Add(RubroMaestro);
            Save();
        }

        public void Update(RubroMaestro RubroMaestro)
        {
            RubroMaestroRepository.Update(RubroMaestro);
            Save();
        }

        public void Delete(Guid id)
        {
            var RubroMaestro = RubroMaestroRepository.GetById(id);
            RubroMaestroRepository.Delete(RubroMaestro);
            Save();
        }

        public IEnumerable<RubroMaestro> GetRubroMaestros()
        {
            var RubroMaestros = RubroMaestroRepository.GetAll()
                .OrderBy(x => x.Codigo);
            return RubroMaestros;
        }

        public RubroMaestro GetRubroMaestro(Guid id)
        {
            var RubroMaestro = RubroMaestroRepository.GetById(id);

            return RubroMaestro;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<RubroMaestro> GetRubroMaestroFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var RubroMaestros = RubroMaestroRepository
                    .GetMany(x => x.Codigo.ToString().Contains(search) ||
                        x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return RubroMaestros;
            }
            return GetRubroMaestros();
        }

        public IPagedList<RubroMaestro> GetRubroMaestrosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.RubroMaestroRepository.GetRubroMaestrosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public IEnumerable<Rubro> GetRubroMaestrosRelacionados(Guid id)
        {
            RubroMaestro RubroMaestro = GetRubroMaestro(id);

            IEnumerable<Rubro> rubros = null;

            if (RubroMaestro.Rubros != null)
            {
                rubros = RubroMaestro.Rubros;
            }
            return rubros;
        }

        public bool GetRubroAny(Guid id)
        {
            var rubro = RubroMaestroRepository.Get(x => x.Id == id);
            if (rubro.Producto.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
