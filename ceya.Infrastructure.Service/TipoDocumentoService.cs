using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;

namespace ceya.Domain.Service
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository tipoDocumentoRepository;

        private readonly IUnitOfWork unitOfWork;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository, IUnitOfWork unitOfWork)
        {
            this.tipoDocumentoRepository = tipoDocumentoRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<TipoDocumento> GetTipoDocumentos()
        {
            var tiposDocumento = tipoDocumentoRepository.GetAll().OrderBy(x => x.Codigo);
            return tiposDocumento;
        }

        public TipoDocumento GetById(Guid id)
        {
            var tiposDocumento = tipoDocumentoRepository
              .GetAll()
              .Where(x => x.Id == id)
              .FirstOrDefault();

            return tiposDocumento;
        }
    }
}
