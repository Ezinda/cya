using ceya.Model.Models;
using System;
using System.Collections.Generic;

namespace ceya.Domain.Service
{
    public interface IVWPrecioProductoService
    {
        IEnumerable<VWPrecioProducto> GetColocaciones();
        VWPrecioProducto GetColocacion(Guid precioColocacionId);
        IEnumerable<VWPrecioProducto> GetVidrios();
        VWPrecioProducto GetVidrio(Guid precioVidrioId);
        IEnumerable<VWPrecioProducto> GetLineasPresupuesto();
        IEnumerable<VWPrecioProducto> GetProductos();
    }
}
