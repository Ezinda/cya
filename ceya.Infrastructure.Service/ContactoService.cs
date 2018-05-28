using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceya.Application.Service;
using ceya.Model.Models;
using ceya.Domain.Repository;
using X.PagedList;

namespace ceya.Infrastructure.Service
{
    public class contactoService : IContactoService
    {
        private readonly _contactoRepository contactoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public contactoService(_contactoRepository contactoRepository, IUnitOfWork unitOfWork)
        {
            this.contactoRepository = contactoRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(Contacto contacto)
        { 
            contactoRepository.Add(contacto);
            Save();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Contacto contacto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Contacto> GetContactos()
        {
            var contactos = contactoRepository.GetAll().OrderBy(x => x.Nombre);
            return contactos;
        }


        IEnumerable<Contacto> IContactoService.GetContactoFilter(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var contactos = contactoRepository.GetMany(x => x.Nombre.Contains(search) ||
                        x.Domicilio.Contains(search) ||
                        x.Telefono.Contains(search) ||
                        x.Email.Contains(search))
                        .OrderBy(x => x.Nombre);

                return contactos;
            }
            return GetContactos();
        }

        public object GetContactoByPage(int page, int pageSize, string sortBy, string direction, string filterBy, string searchString)
        {
            throw new NotImplementedException();
        }

        IPagedList<Contacto> IContactoService.GetContactoByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            return contactoRepository.GetContactoByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString);
        }

        public IEnumerable<Contacto> GetContactoFilterWithConstructora(string search, Guid? constructoraId)
        {
            if (!string.IsNullOrEmpty(search) || constructoraId != null)
            {
                var contactos = contactoRepository
                    .GetMany(x => x.Nombre.Contains(search))
                        .Where(x => x.ConstructoraId == constructoraId)
                        .OrderBy(x => x.Nombre);
                var result = contactos.ToList();
                 return contactos;
            }
            return GetContactos();
        }

        public Contacto GetContactos(Guid id)
        {
            var cliente = contactoRepository.GetById(id);

            return cliente;
        }

    }
}
