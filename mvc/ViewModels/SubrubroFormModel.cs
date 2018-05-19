using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class SubrubroFormModel
    {
        public System.Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.Guid> ClaseId { get; set; }
        public string Clase { get; set; }
        public Nullable<System.Guid> RubroId { get; set; }
    }
}