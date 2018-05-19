using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class ProductoFormModel
    {
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoCompuesto { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Tipo")]
        public Nullable<System.Guid> TipoProductoId { get; set; }
        [Display(Name = "Unidad de Medida")]
        public Nullable<System.Guid> UnidadMedidaId { get; set; }
        [Display(Name = "Rubro Primario")]
        public Nullable<System.Guid> RubroMaestroId { get; set; }
        [Display(Name = "Rubro Secundario")]
        public Nullable<System.Guid> RubroId { get; set; }
        [Display(Name = "Subrubro")]
        public Nullable<System.Guid> SubrubroId { get; set; }

        public virtual string NombreTipoProducto { get; set; }
        public virtual string NombreUnidad { get; set; }
        public virtual string NombreRubroMaestro { get; set; }
        public virtual string NombreRubro { get; set; }
        public virtual string NombreSubrubro { get; set; }

        public IEnumerable<SelectListItem> TipoProductos { get; set; }
        public IEnumerable<SelectListItem> UnidadMedidas { get; set; }
        public IEnumerable<SelectListItem> RubroMaestros { get; set; }
        public IEnumerable<SelectListItem> Rubros { get; set; }
        public IEnumerable<SelectListItem> Subrubros { get; set; }

    }
}