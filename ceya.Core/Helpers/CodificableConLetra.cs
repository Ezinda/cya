using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ceya.Core.Helpers
{
    public class CodificableConLetra
    {
        public static string GenerateCodigo(string letraDeCodigo, string codigo)
        {
            var result = String.Empty;

            if (letraDeCodigo == null || letraDeCodigo == String.Empty)
            {
                throw new InvalidCastException("El código necesita al menos una letra para identificarlo.");
            }

            if (codigo.Length < Constants.CODE_PADDING_LENGHT)
            {
                result = codigo.PadLeft(Constants.CODE_PADDING_LENGHT, '0');
            }

            result = letraDeCodigo + result;

            return result;
        }

        public static string GenerateStringForSearch(string letraDeCodigo, string codigo, params string[] nombres)
        {
            var mynombres = new List<string>();
            var codigoParseado = GenerateCodigo(letraDeCodigo, codigo);
            var flagEsPrimerNombre = true;

            var result = codigoParseado;
            
            foreach (var nombre in nombres)
            {
                if (nombre != null && nombre.Trim() != String.Empty)
                {
                    mynombres.Add(nombre.Trim());
                }
            }

            foreach (var nombre in mynombres)
            {
                if (flagEsPrimerNombre)
                {
                    result += " - " + nombre;
                    flagEsPrimerNombre = false;
                }
                else {
                    result += ", " + nombre;
                }
            }

            return result;
        }
    }
}