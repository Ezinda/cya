using ceya.Core.Helpers;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Model.Extensions
{
    public static class CodigoExtensions
    {
        public static string GetCodigoConFormato(
              this Presupuesto presupuesto)
        {
            return Codificable.GenerateCodigo(presupuesto.Codigo.ToString());
        }

        public static string GetCodigoConFormato(
              this PresupuestoCategoria presupuestoCategoria)
        {
            return Codificable.GenerateCodigo(presupuestoCategoria.Codigo);
        }

        public static string GetCodigoConFormato(
              this PresupuestoEstado presupuestoEstado)
        {
            return Codificable.GenerateCodigo(presupuestoEstado.Codigo);
        }
    }
}
