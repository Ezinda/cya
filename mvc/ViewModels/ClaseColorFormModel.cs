using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class ClaseColorFormModel
    {
        public System.Guid Id { get; set; }
        public System.Guid[] ColoresId { get; set; }
        public Nullable<System.Guid> ColorId { get; set; }
    }
}