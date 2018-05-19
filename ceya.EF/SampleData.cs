using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.EF
{
    public class SampleData : DropCreateDatabaseIfModelChanges<GestionComercialWebEntities>
    {
        protected override void Seed(GestionComercialWebEntities context)
        {
            new List<TipoDocumento>
            {

                new TipoDocumento { Id = Guid.Parse("D2C92E8F-EFEF-4E70-A99E-21BD3C2F209D"), Codigo = "C.U.I.L", Nombre = "Código Único de Identificación Laboral", Orden = 2 },
                new TipoDocumento { Id = Guid.Parse("739B2266-693F-4A96-80D7-350D8E017D78"), Codigo = "D.N.I", Nombre = "Documento Nacional de Identidad", Orden = 1 },
                new TipoDocumento { Id = Guid.Parse("3DF7AF11-60B4-402C-B644-9B311C039636"), Codigo = "C.U.I.T", Nombre = "Código Único de Identificación Tributaria", Orden = 3 }

            }.ForEach(x => context.TipoDocumento.Add(x));

            context.Commit();
        }
    }
}
