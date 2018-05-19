using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ListaPrecioListViewModel
    {
        public System.Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Activo { get; set; }
    }
}