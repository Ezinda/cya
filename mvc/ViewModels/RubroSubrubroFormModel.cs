using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class RubroSubrubroFormModel
    {
        public System.Guid Id { get; set; }
        public System.Guid[] SubrubrosId { get; set; }
        public Nullable<System.Guid> SubrubroId { get; set; }

    }
}