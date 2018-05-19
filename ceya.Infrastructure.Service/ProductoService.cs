using ceya.Domain.Repository;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository productoRepository;

        private readonly IRubroMaestroService rubroMaestroService;

        private readonly ISubrubroService subrubroService;

        private readonly IUnitOfWork unitOfWork;

        public ProductoService(IProductoRepository productoRepository,
            IRubroMaestroService rubroMaestroService,
            ISubrubroService subrubroService,
            IUnitOfWork unitOfWork)
        {
            this.productoRepository = productoRepository;
            this.rubroMaestroService = rubroMaestroService;
            this.subrubroService = subrubroService;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Producto producto)
        {
            productoRepository.Add(producto);
            Save();
        }

        public void Delete(Guid id)
        {
            var producto = productoRepository.GetById(id);
            productoRepository.Delete(producto);
            Save();
        }

        public Producto GetProducto(Guid id)
        {
            var producto = productoRepository.GetById(id);

            return producto;
        }

        public IPagedList<Producto> GetProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString)
        {
            return this.productoRepository.GetProductosByPage(currentPage, noOfRecords, sortBy, filterBy, searchString);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Producto producto)
        {
            productoRepository.Update(producto);
            Save();
        }
        public bool Exists(Guid id)
        {
            return productoRepository.Exists(x => x.UnidadMedidaId == id);
        }

        public string GetCodigoCompuesto(int codigo, Guid rubroMaestroId, Guid? subrubroId)
        {
            string codigoCompuesto;
            string codigoRubro = string.Empty;
            string codigoSubrubro = string.Empty;

            if (rubroMaestroId != Guid.Empty || rubroMaestroId != null)
            {
                RubroMaestro rubro = rubroMaestroService.GetRubroMaestro(rubroMaestroId);
                codigoRubro = rubro != null ? rubro.Codigo.ToString() : string.Empty;

                if (subrubroId != null)
                {
                    Subrubro subrubro = subrubroService.GetSubrubro(subrubroId.Value);
                    codigoSubrubro = subrubro != null ? subrubro.Codigo.ToString() : string.Empty;
                }
            }

            codigoCompuesto = codigoRubro.PadLeft(2, '0') +
                codigoSubrubro.PadLeft(2, '0') + codigo.ToString().PadLeft(4, '0');

            return codigoCompuesto;
        }
    }
}
