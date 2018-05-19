using ceya.Model.Models;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ceya.Application.Service
{
    public interface IContactoService
    {
        void Add(Contacto contacto);
        void Update(Contacto contacto);
        void Delete(Guid id);
        void Save();
        bool Get(Guid id);
        IEnumerable<Contacto> GetContactos();
        IEnumerable<Contacto> GetContactoFilter(string search);
        IEnumerable<Contacto> GetContactoFilterWithConstructora(string search, Guid? constructoraId);
        IPagedList<Contacto> GetContactoByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString);
    }

}
