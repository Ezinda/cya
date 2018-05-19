using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ProductoListViewModel
    {
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoCompuesto { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string Rubro { get; set; }
        public string Subrubro { get; set; }
    }
}