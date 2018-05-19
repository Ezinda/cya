using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ceya.Core.Helpers
{
    public class Codificable
    {
        public static string GenerateCodigo(string codigo)
        {
            var result = String.Empty;

            if (codigo.Length < Constants.CODE_PADDING_LENGHT)
            {
                result = codigo.PadLeft(Constants.CODE_PADDING_LENGHT, '0');
            }

            return result;
        }

        public static string ParseCodigo(string codigo)
        {
            return codigo.TrimStart(new Char[] { '0' });
        }

        public static string GenerateStringForSearch(string codigo, params string[] nombres)
        {
            var mynombres = new List<string>();
            var codigoParseado = GenerateCodigo(codigo);
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

        public static string GenerateStringForSearchWithoutParseCode(string codigo, params string[] nombres)
        {
            var mynombres = new List<string>();
            var flagEsPrimerNombre = true;

            var result = (codigo ?? String.Empty).Trim();

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
                else
                {
                    result += ", " + nombre;
                }
            }

            return result;
        }
    }
}