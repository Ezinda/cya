using ceya.Domain.Repository;
using ceya.Model.Models;
using System;
using System.Collections.Generic;

namespace ceya.Domain.Service
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository archivoRepository;
        private readonly IUnitOfWork unitOfWork;
        public ArchivoService(IArchivoRepository archivoRepository, IUnitOfWork unitOfWork)
        {
            this.archivoRepository = archivoRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Archivo> GetArchivosPorTransaccion(Guid transaccionId)
        {

            return this.archivoRepository.GetMany(x => x.TransaccionId == transaccionId && !x.Nombre.Contains(".xls") && x.TransaccionCompletada == true);
        }

        public void Eliminar(Guid id)
        {
            var archivoParaEliminar = archivoRepository.GetById(id);
            if (archivoParaEliminar != null)
            {
                archivoRepository.Delete(archivoParaEliminar);
                unitOfWork.Commit();
            }
            else
            {
                throw new Exception("Intentó eliminar un archivo que no existe");
            }
        }
    }
}