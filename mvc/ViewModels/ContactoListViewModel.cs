using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ContactoListViewModel
    {
        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Constructora { get; set; }
        
    }
}