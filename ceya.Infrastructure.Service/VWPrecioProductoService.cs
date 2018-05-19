using ceya.Domain.Repository;
using ceya.Domain.Service;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Infrastructure.Service
{
    public class VWPrecioProductoService : IVWPrecioProductoService
    {
        private readonly IVWPrecioProductoRepository vwPrecioProductoRepository;
        private readonly IUnitOfWork unitOfWork;
        public VWPrecioProductoService(IVWPrecioProductoRepository vwPrecioProductoRepository, IUnitOfWork unitOfWork)
        {
            this.vwPrecioProductoRepository = vwPrecioProductoRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<VWPrecioProducto> GetColocaciones()
        {
            return this.vwPrecioProductoRepository.GetMany(x => x.RubroCodigo == 4).OrderBy(x=>x.ProductoDescripcion);
        }

        public VWPrecioProducto GetColocacion(Guid precioColocacionId)
        {
            return this.vwPrecioProductoRepository.Get(x => x.RubroCodigo == 4 && x.PrecioId == precioColocacionId);
        }

        public IEnumerable<VWPrecioProducto> GetVidrios()
        {
            return this.vwPrecioProductoRepository.GetMany(x => x.RubroCodigo == 3).OrderBy(x => x.ProductoDescripcion);
        }

        public VWPrecioProducto GetVidrio(Guid precioVidrioId)
        {
            return this.vwPrecioProductoRepository.Get(x => x.RubroCodigo == 3 && x.PrecioId == precioVidrioId);
        }

        public IEnumerable<VWPrecioProducto> GetLineasPresupuesto()
        {
            // return new List<VWPrecioProducto>();
            return this.vwPrecioProductoRepository.GetAll();
            //return this.vwPrecioProductoRepository.GetMany(x =>
            //    x.RubroSistema == true
            //    && (x.RubroDescripcion == "MATERIA PRIMA ALUMINIO" || x.RubroDescripcion == "MATERIA PRIMA PVC")
            //).OrderBy(x => x.ProductoCodigoCompuesto);
        }

        public IEnumerable<VWPrecioProducto> GetProductos()
        {
            return this.vwPrecioProductoRepository.GetMany(x => x.RubroCodigo != 4 && x.RubroCodigo != 3).OrderBy(x => x.ProductoDescripcion);
        }
    }
}
