using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Collections.Generic;
using System;

namespace ceya.Domain.Service
{
    public class PresupuestoEstadoService : IPresupuestoEstadoService
    {
        private readonly IPresupuestoEstadoRepository presupuestoEstadoRepository;
        private readonly IUnitOfWork unitOfWork;
        public PresupuestoEstadoService(IPresupuestoEstadoRepository presupuestoEstadoRepository, IUnitOfWork unitOfWork)
        {
            this.presupuestoEstadoRepository = presupuestoEstadoRepository;
            this.unitOfWork = unitOfWork;
        }

        public PresupuestoEstado GetEstado(Guid Id)
        {
            return presupuestoEstadoRepository.GetById(Id);
        }

        public IEnumerable<PresupuestoEstado> GetEstados()
        {
            return presupuestoEstadoRepository.GetAll();
        }

        public IEnumerable<PresupuestoEstado> GetEstados(string nombre)
        {
            if (nombre == "REGISTRADO")
            {
                return presupuestoEstadoRepository.GetMany(x => x.Descripcion == "ENTREGADO"
                || x.Descripcion == "ANULADO"
                || x.Descripcion == "REGISTRADO");
            }
            else if (nombre == "ENTREGADO")
            {
                return presupuestoEstadoRepository.GetMany(x => x.Descripcion == "EN SEGUIMIENTO"
                || x.Descripcion == "ANULADO"
                || x.Descripcion == "ENTREGADO");
            }
            else if (nombre == "EN SEGUIMIENTO")
            {
                return presupuestoEstadoRepository.GetMany(x => x.Descripcion == "APROBADO"
                || x.Descripcion == "RECHAZADO"
                || x.Descripcion == "EN SEGUIMIENTO");
            }
            else if (nombre == "APROBADO")
            {
                return presupuestoEstadoRepository.GetMany(x => x.Descripcion == "POSTVENTA");
            }
            else
            {
                return presupuestoEstadoRepository.GetMany(x => x.Descripcion == "EN SEGUIMIENTO");
            }
        }
    }
}
