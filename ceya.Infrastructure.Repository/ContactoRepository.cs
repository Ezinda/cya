using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceya.Infrastructure.Repository;
using ceya.Domain.Repository;
using ceya.Core;
using ceya.Model.Models;
using System.Linq.Expressions;
using X.PagedList;
using ceya.Infrastructure.DataAccess;

namespace ceya.Infrastructure.Repository
{
    public class ContactoRepository : RepositoryBase<Contacto>, _contactoRepository
    {
        public ContactoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IPagedList<Contacto> GetByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Contacto> contactos;

            contactos = from p in this.dbset select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                contactos = contactos.Where(x => x.Nombre.ToString().Contains(searchString)
                                       || x.Domicilio.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Nombre":
                        contactos = contactos.OrderBy(c => c.Nombre);
                        break;
                    case "Domicilio":
                        contactos = contactos.OrderBy(c => c.Domicilio);
                        break;
                    case "Constructora":
                        contactos = contactos.OrderBy(c => c.Constructora);
                        break;
                    default:
                        contactos = contactos.OrderBy(x => x.Nombre);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {

                    case "Nombre":
                        contactos = contactos.OrderByDescending(c => c.Nombre);
                        break;
                    case "Domicilio":
                        contactos = contactos.OrderByDescending(c => c.Domicilio);
                        break;
                    case "Constructora":
                        contactos = contactos.OrderByDescending(c => c.Constructora);
                        break;
                    default:
                        contactos = contactos.OrderByDescending(x => x.Nombre);
                        break;
                }
            }
            return contactos.ToPagedList(currentPage, noOfRecords);
        }

        public IPagedList<Contacto> GetContactoByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Contacto> contactos;

            //contactos = from p in this.dbset select p;

             contactos = DataContext.Contacto.Include("Constructora");

            if (!string.IsNullOrEmpty(searchString))
            {
                contactos = contactos.Where(x => x.Nombre.ToString().Contains(searchString)
                                       || x.Constructora.Nombre.Contains(searchString)
                                       || x.Domicilio.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                   
                    case "Nombre":
                        contactos = contactos.OrderBy(s => s.Nombre);
                        break;
                    default:
                        contactos = contactos.OrderBy(x => x.Nombre);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                  
                    case "Nombre":
                        contactos = contactos.OrderByDescending(s => s.Nombre);
                        break;
                    default:
                        contactos = contactos.OrderByDescending(x => x.Nombre);
                        break;
                }
            }

            return contactos.ToPagedList(currentPage, noOfRecords);
        }
    }
}
