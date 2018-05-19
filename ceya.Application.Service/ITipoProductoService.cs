using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface ITipoProductoService
    {
        IEnumerable<TipoProducto> GetTipoProductos();
        IEnumerable<TipoProducto> GetTipoProductoFilter(string search);
        IPagedList<TipoProducto> GetTipoProductosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        TipoProducto GetTipoProducto(Guid id);
        void Add(TipoProducto TipoProducto);
        void Update(TipoProducto TipoProducto);
        void Delete(Guid id);
        void Save();
        bool GetTipoProductoAny(Guid id);
    }
}
