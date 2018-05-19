using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class PresupuestoListViewModel
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public string CodigoObra { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string Solicita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CodigoCategoria { get; set; }
        public string Estado { get; set; }
        public string FechaAlerta { get; set; }
    }
}