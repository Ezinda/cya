using ceya.Domain.Repository;
using ceya.Domain.Service;
using ceya.Model.Models;
using System.Collections.Generic;

namespace ceya.Infrastructure.Service
{
    public class PresupuestoCategoriaService : IPresupuestoCategoriaService
    {
        private readonly IPresupuestoCategoriaRepository presupuestoCategoriaRepository;
        private readonly IUnitOfWork unitOfWork;
        public PresupuestoCategoriaService(IPresupuestoCategoriaRepository presupuestoCategoriaRepository, IUnitOfWork unitOfWork)
        {
            this.presupuestoCategoriaRepository = presupuestoCategoriaRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PresupuestoCategoria> GetCategorias()
        {
            return presupuestoCategoriaRepository.GetAll();
        }
    }
}
