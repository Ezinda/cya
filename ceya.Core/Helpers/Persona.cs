using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ceya.Core.Helpers
{
    public static class Persona
    {
        public static string GenerateStringForSearch(string razonSocial, string apellido, string nombre, string documento) {
            var result = String.Empty;

            razonSocial = razonSocial == null ? String.Empty : razonSocial.Trim();
            apellido = apellido == null ? String.Empty : apellido.Trim();
            nombre = nombre == null ? String.Empty : nombre.Trim();
            documento = documento == null ? String.Empty : documento.Trim();

            if (razonSocial != String.Empty)
            {
                result += razonSocial;
            }
            else
            {
                if (apellido != String.Empty && nombre != String.Empty)
                {
                    result += apellido + ", " + nombre;
                }
                else if (apellido != String.Empty)
                {
                    result += apellido;
                }
                else if (nombre != String.Empty)
                {
                    result += nombre;
                }
            }

            if (documento != String.Empty)
            {
                result += " (" + documento + ")";
            }

            return result;
        }
    }
}