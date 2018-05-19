using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ceya.Infrastructure.DataAccess;

namespace mvc.ViewModels
{
    public class ObraFormModel
    {
        public System.Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public System.Guid ClienteId { get; set; }
        public string Cliente { get; set; }
    }
}