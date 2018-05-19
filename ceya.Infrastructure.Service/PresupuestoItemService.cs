using ceya.Domain.Repository;
using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ceya.Domain.Service
{
    public class PresupuestoItemService : IPresupuestoItemService
    {
        private readonly IPresupuestoItemRepository presupuestoItemRepository;
        private readonly IUnitOfWork unitOfWork;
        public PresupuestoItemService(IPresupuestoItemRepository presupuestoItemRepository, IUnitOfWork unitOfWork)
        {
            this.presupuestoItemRepository = presupuestoItemRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PresupuestoItem> GetItems(Guid presupuestoId)
        {
            return this.presupuestoItemRepository
                .GetMany(x => x.PresupuestoId == presupuestoId) // No haría falta un campo DeBaja?
                .OrderBy(x => x.NumeroPosicion).ToList(); // No haría falta un campo de numero de orden?
        }

        public void SaveItem()
        {
            this.unitOfWork.Commit();
        }
    }
}
