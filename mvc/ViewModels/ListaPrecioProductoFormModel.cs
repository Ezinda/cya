using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ListaPrecioProductoFormModel
    {
        public System.Guid Id { get; set; }
        public System.Guid[] ProductosId { get; set; }
        public Nullable<System.Guid> ProductoId { get; set; }
    }
}