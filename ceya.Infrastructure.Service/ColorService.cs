using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository colorRepository;

        private readonly IUnitOfWork unitOfWork;

        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            this.colorRepository = colorRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Color> GetColores()
        {
            var colores = colorRepository.GetAll().OrderBy(x => x.Codigo);
            return colores;
        }

        public IEnumerable<Color> GetColorFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var colores = colorRepository
                    .GetMany(x => x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return colores;
            }
            return GetColores();
        }

        public IPagedList<Color> GetColoresByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.colorRepository.GetColoresByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public Color GetColor(Guid id)
        {
            var color = colorRepository.GetById(id);
              
            return color;
        }

        public void Add(Color color)
        {
            colorRepository.Add(color);
            Save();
        }

        public void Update(Color color)
        {
            colorRepository.Update(color);
            Save();
        }

        public void Delete(Guid id)
        {
            var color = colorRepository.GetById(id);
            colorRepository.Delete(color);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetColorAny(Guid id)
        {
            var color = colorRepository.Get(x=>x.Id == id);
            if(color.Clases.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
