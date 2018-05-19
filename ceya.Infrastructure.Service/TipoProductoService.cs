using System;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class TipoProductoService : ITipoProductoService
    {
        private readonly ITipoProductoRepository tipoProductoRepository;

        private readonly IUnitOfWork unitOfWork;

        public TipoProductoService(ITipoProductoRepository tipoProductoRepository, IUnitOfWork unitOfWork)
        {
            this.tipoProductoRepository = tipoProductoRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(TipoProducto tipoProducto)
        {
            tipoProductoRepository.Add(tipoProducto);
            Save();
        }

        public void Update(TipoProducto tipoProducto)
        {
            tipoProductoRepository.Update(tipoProducto);
            Save();
        }

        public void Delete(Guid id)
        {
            var tipoProducto = tipoProductoRepository.GetById(id);
            tipoProductoRepository.Delete(tipoProducto);
            Save();
        }

        public IEnumerable<TipoProducto> GetTipoProductos()
        {
            var tipoProductos = tipoProductoRepository.GetAll()
                .OrderBy(x => x.Codigo);

            return tipoProductos;
        }

        public TipoProducto GetTipoProducto(Guid id)
        {
            var tipoProducto = tipoProductoRepository.GetById(id);

            return tipoProducto;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<TipoProducto> GetTipoProductoFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var tipoProductos = tipoProductoRepository
                    .GetMany(x => x.Codigo.ToString().Contains(search) ||
                        x.Descripcion.Contains(search))
                        .OrderBy(x => x.Codigo);

                return tipoProductos;
            }
            return GetTipoProductos();
        }

        public IPagedList<TipoProducto> GetTipoProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.tipoProductoRepository.GetTipoProductosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public bool GetTipoProductoAny(Guid id)
        {
            var tipo = tipoProductoRepository.Get(x => x.Id == id);
            if (tipo.Producto.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
