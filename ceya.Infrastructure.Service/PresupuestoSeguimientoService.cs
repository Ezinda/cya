using ceya.Domain.Repository;
using ceya.Domain.Service;
using ceya.Model.Models;
using System.Collections.Generic;
using System;
using X.PagedList;

namespace ceya.Domain.Service
{
    public class PresupuestoSeguimientoService : IPresupuestoSeguimientoService
    {
        private readonly IPresupuestoEstadoRepository _presupuestoEstadoRepository;
        private readonly IPresupuestoSeguimientoRepository _presupuestoSeguimientoRepository;
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PresupuestoSeguimientoService(IPresupuestoEstadoRepository presupuestoEstadoRepository,
            IPresupuestoSeguimientoRepository presupuestoSeguimientoRepository,
            IPresupuestoRepository presupuestoRepository,
            IUnitOfWork unitOfWork)
        {
            _presupuestoEstadoRepository = presupuestoEstadoRepository;
            _presupuestoSeguimientoRepository = presupuestoSeguimientoRepository;
            _presupuestoRepository = presupuestoRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(PresupuestoSeguimiento presupuestoSeguimiento)
        {
            _presupuestoSeguimientoRepository.Add(presupuestoSeguimiento);
            Save();
        }

        public IEnumerable<PresupuestoEstado> GetEstados()
        {
            return _presupuestoEstadoRepository.GetAll();
        }
        
        public PresupuestoSeguimiento GetUltimoSeguimiento(Guid Id)
        {
            return _presupuestoSeguimientoRepository.Get(x => x.Activo == true && x.PresupuestoId == Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateActivo(Guid PresupuestoId)
        {
            var seguimientos = _presupuestoSeguimientoRepository
                .GetMany(x => x.PresupuestoId == PresupuestoId);

            foreach (var item in seguimientos)
            {
                var seguimiento = _presupuestoSeguimientoRepository.Get(x=>x.Id == item.Id);
                seguimiento.Activo = false;
                _presupuestoSeguimientoRepository.Update(seguimiento);
                Save();
            }
        }

        public IPagedList<PresupuestoSeguimiento> GetSeguimientosByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, Guid PresupuestoId)
        {
            return this._presupuestoSeguimientoRepository.GetSeguimientoByPage(currentPage, noOfRecords, sortBy, filterBy, PresupuestoId);
        }
    }
}
