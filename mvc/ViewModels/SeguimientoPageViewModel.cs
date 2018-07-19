using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class SeguimientoPageViewModel
    {
        public IEnumerable<SeguimientoListViewModel> List { get; set; }

        public IEnumerable<SelectListItem> FilterBy { get; set; }
        public string FilterByString { get; set; }
        public Guid? PresupuestoId { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string Constructora { get; set; }
        public string Email { get; set; }
        public string Solicita { get; set; }
        public string Vendedor { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
        public IEnumerable<SelectListItem> SortBy { get; set; }
        public string SortByString { get; set; }
        public bool IsSortAsc { get; set; }


    }
}