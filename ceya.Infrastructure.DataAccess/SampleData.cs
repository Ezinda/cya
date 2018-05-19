using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Infrastructure.DataAccess
{
    public class SampleData : CreateDatabaseIfNotExists<GestionComercialWebEntities>
    {
        protected override void Seed(GestionComercialWebEntities context)
        {
            new List<TipoDocumento>
            {

                new TipoDocumento { Id = Guid.Parse("D2C92E8F-EFEF-4E70-A99E-21BD3C2F209D"), Codigo = "C.U.I.L", Nombre = "Código Único de Identificación Laboral", Orden = 2 },
                new TipoDocumento { Id = Guid.Parse("739B2266-693F-4A96-80D7-350D8E017D78"), Codigo = "D.N.I", Nombre = "Documento Nacional de Identidad", Orden = 1 },
                new TipoDocumento { Id = Guid.Parse("3DF7AF11-60B4-402C-B644-9B311C039636"), Codigo = "C.U.I.T", Nombre = "Código Único de Identificación Tributaria", Orden = 3 }

            }.ForEach(x => context.TipoDocumento.Add(x));

            new List<PresupuestoCategoria>
            {

                new PresupuestoCategoria { Id = Guid.Parse("55AE44F1-A091-4F98-919C-0C73735B48E0"), Codigo = "S", Descripcion = "SERVICE" },
                new PresupuestoCategoria { Id = Guid.Parse("A749CB4D-4950-4A85-8E88-7DDF3335C180"), Codigo = "P", Descripcion = "ALUMINIO Y PVC PARCIALES Y/O AMPLIACIONES" },
                new PresupuestoCategoria { Id = Guid.Parse("56AED665-50E2-428D-88C2-C8CAC04FCB25"), Codigo = "E", Descripcion = "EDIFICIOS EN ALTURA" },
                new PresupuestoCategoria { Id = Guid.Parse("1E358070-26E1-4E78-82D5-FD99F7F94A9E"), Codigo = "C", Descripcion = "ALUMINIO Y PVC COMPLETAS" }

            }.ForEach(x => context.PresupuestoCategoria.Add(x));

            new List<PresupuestoEstado>
            {
                new PresupuestoEstado { Id = Guid.Parse("F1FAF1EC-D578-4901-9D85-2D7F069558F1"), Codigo = "1", Descripcion = "PRESUPUESTADO" },

            }.ForEach(x => context.PresupuestoEstado.Add(x));

            context.Commit();
        }
    }
}
