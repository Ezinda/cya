using ceya.Model.Models;
using System;
using System.Collections.Generic;

namespace ceya.Domain.Service
{
    public interface ITipoDocumentoService
    {
        IEnumerable<TipoDocumento> GetTipoDocumentos();
        TipoDocumento GetById(Guid id);
    }
}
