using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ProductoViewModel
    {
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Descripcion { get; set; }
        public System.Guid UnidadMedidaId { get; set; }
        public string Unidad { get; set; }
        public System.Guid RubroId { get; set; }
        public string Rubro { get; set; }

    }
}