using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class PrecioViewModel
    {
        public System.Guid Id { get; set; }
        public string Producto { get; set; }
        public decimal PrecioProducto { get; set; }
       
    }
}