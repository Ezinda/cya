using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class PrecioListViewModel
    {
        public System.Guid Id { get; set; }
        public string CodigoCompuesto { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string RubroMaestro { get; set; }
        public string Rubro { get; set; }
        public string Subrubro { get; set; }
        public string ListaPrecio { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public string Precio { get; set; }
    }
}