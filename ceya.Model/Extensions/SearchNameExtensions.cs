using ceya.Core.Helpers;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Domain.Model.Extensions
{
    public static class SearchNameExtensions
    {
        public static string ToSearchNameString(
              this Cliente cliente)
        {
            return Codificable.GenerateStringForSearch(
                cliente.Codigo.ToString(),
                Persona.GenerateStringForSearch(
                    cliente.RazonSocial,
                    cliente.Apellido,
                    cliente.Nombre,
                    cliente.Documento));
        }

        public static string ToSearchNameString(
              this Constructora constructora)
        {
            return Codificable.GenerateStringForSearch(
                constructora.Codigo.ToString(),
                Persona.GenerateStringForSearch(
                    constructora.RazonSocial,
                    constructora.Apellido,
                    constructora.Nombre,
                    constructora.Documento));
        }

        public static string ToSearchNameString(
              this Obra obra)
        {
            return Codificable.GenerateStringForSearchWithoutParseCode(
                        obra.CodigoObra,
                        obra.Nombre,
                        obra.Domicilio);
        }
    }
}
