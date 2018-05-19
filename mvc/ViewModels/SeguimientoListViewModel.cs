using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class SeguimientoListViewModel
    {
        public Guid Id { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
        public string FechaAlerta { get; set; }
        public string Observacion { get; set; }
    }
}