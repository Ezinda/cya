using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class UnidadMedidaListViewModel
    {
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Abreviacion { get; set; }
        public string Descripcion { get; set; }

    }
}