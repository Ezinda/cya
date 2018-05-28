using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Model.DTOs
{
    public class PrecioActualizado
    {
        public decimal PrecioColocacion { get; set; }
        public decimal PrecioVidrio { get; set; }
        public Guid Id { get; set; }
    }
}
