using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ceya.Web.Core.Extensions
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<VWPrecioProducto> precioproductos, Guid selectedId)
        {
            return precioproductos.OrderBy(x => x.ProductoCodigoCompuesto)
                .Select(x => new SelectListItem
                {
                    Selected = (x.PrecioId == selectedId),
                    // Text = String.Format("{0} - {1}", x.ProductoCodigoCompuesto, x.ProductoDescripcionCompuesto),
                    Text = String.Format("{0}", x.ProductoDescripcion),
                    Value = x.PrecioId.ToString()
                });
        }

        public static IEnumerable<SelectListItem> ToSelectListItemsColocaciones(
              this IEnumerable<VWPrecioProducto> precioproductos, Guid selectedId)
        {
            return precioproductos.OrderBy(x => x.ProductoCodigoCompuesto)
                .Select(x => new SelectListItem
                {
                    Selected = (x.PrecioId == selectedId),
                    Text = String.Format("{0} - {1}, {2}, {3}", x.ProductoCodigoCompuesto, x.ProductoDescripcion, x.SubrubroDescripcion, x.ColorDescripcion),
                    Value = x.PrecioId.ToString()
                });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<PresupuestoCategoria> categorias, Guid selectedId)
        {
            return categorias.OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Selected = (x.Id == selectedId),
                    Text = String.Format("{0} - {1}", x.Codigo, x.Descripcion),
                    Value = x.Id.ToString()
                });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<PresupuestoEstado> estados, Guid selectedId)
        {
            return estados.OrderBy(x => x.Codigo)
                .Select(x => new SelectListItem
                {
                    Selected = (x.Id == selectedId),
                    Text = String.Format("{0}", x.Descripcion),
                    Value = x.Id.ToString()
                });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
             this IEnumerable<TipoDocumento> tipoDocumentos, Guid selectedId)
        {
            return

                tipoDocumentos.OrderBy(x => x.Orden)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Codigo,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
           this IEnumerable<TipoProducto> tipoProductos, Guid selectedId)
        {
            return

                tipoProductos.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Descripcion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
             this IEnumerable<UnidadMedida> unidadMedidas, Guid selectedId)
        {
            return

                unidadMedidas.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Abreviacion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
        this IEnumerable<RubroMaestro> rubroMaestros, Guid selectedId)
        {
            return

                rubroMaestros.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Descripcion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
           this IEnumerable<Rubro> rubros, Guid selectedId)
        {
            return

                rubros.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Descripcion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
           this IEnumerable<Subrubro> subrubros, Guid selectedId)
        {
            return

                subrubros.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Descripcion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
         this IEnumerable<Color> colores, Guid selectedId)
        {
            return

                colores.OrderBy(x => x.Codigo)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Descripcion,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
        this IEnumerable<Vendedor> vendedor, Guid selectedId)
        {
            return

                vendedor.OrderBy(x => x.Nombre)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Nombre,
                              Value = x.Id.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Moneda> monedas, Guid selectedId)
        {
            return

                monedas.OrderBy(x => x.Nombre)
                      .Select(x =>
                          new SelectListItem
                          {
                              Selected = (x.Id == selectedId),
                              Text = x.Nombre,
                              Value = x.Id.ToString()
                          });
        }
    }
}
