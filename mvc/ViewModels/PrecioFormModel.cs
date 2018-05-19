using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class PrecioFormModel
    {
        public System.Guid Id { get; set; }
        public System.Guid ProductoId { get; set; }
        public System.Guid ListaPrecioId { get; set; }
        public string Producto { get; set; }
        public DateTime Desde { get; set; }

        public DateTime Hasta { get; set; }

        public Decimal Precio { get; set; }
    }
}