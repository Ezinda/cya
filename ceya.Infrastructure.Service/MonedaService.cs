using System;
using System.Collections.Generic;
using ceya.Domain.Repository;
using ceya.Model.Models;
using System.Linq;

namespace ceya.Domain.Service
{
    public class MonedaService : IMonedaService
    {
        private readonly IMonedaRepository monedaRepository;

        private readonly IUnitOfWork unitOfWork;

        public MonedaService(IMonedaRepository monedaRepository, IUnitOfWork unitOfWork)
        {
            this.monedaRepository = monedaRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Moneda> GetMonedas()
        {
            var monedas = monedaRepository.GetAll().OrderBy(x => x.Nombre);
            return monedas;
        }

        public IEnumerable<Moneda> GetMonedaFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var Monedas = monedaRepository
                    .GetMany(x => x.Nombre.Contains(search));

                return Monedas;
            }
            return GetMonedas();
        }
             
        public Moneda GetMoneda(Guid id)
        {
            var moneda = monedaRepository.GetById(id);

            return moneda;
        }

        public void Add(Moneda Moneda)
        {
            monedaRepository.Add(Moneda);
            Save();
        }

        public void Update(Moneda Moneda)
        {
            monedaRepository.Update(Moneda);
            Save();
        }

        public void Delete(Guid id)
        {
            var Moneda = monedaRepository.GetById(id);
            monedaRepository.Delete(Moneda);
            Save();
        }
        
        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool GetMonedaAny(Guid id)
        {
            var Moneda = monedaRepository.Get(x => x.Id == id);

            if (Moneda.Presupuestos.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}