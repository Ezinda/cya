using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ClienteListViewModel
    {
        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string Cliente { get; set; }
        public string Documento { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}