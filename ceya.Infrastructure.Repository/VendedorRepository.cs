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
    public class VendedorRepository : RepositoryBase<Vendedor>, _vendedorRepository
    {
        public VendedorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IPagedList<Vendedor> GetByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Vendedor> vendedor;

            vendedor = from p in this.dbset select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                vendedor = vendedor.Where(x => x.Nombre.ToString().Contains(searchString)
                                       || x.Domicilio.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Nombre":
                        vendedor = vendedor.OrderBy(c => c.Nombre);
                        break;
                    case "Domicilio":
                        vendedor = vendedor.OrderBy(c => c.Domicilio);
                        break;
                    case "Telefono":
                        vendedor = vendedor.OrderBy(c => c.Telefono);
                        break;
                    case "E-Mail":
                        vendedor = vendedor.OrderBy(c => c.Email);
                        break;
                    default:
                        vendedor = vendedor.OrderBy(x => x.Nombre);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {

                    case "Nombre":
                        vendedor = vendedor.OrderByDescending(c => c.Nombre);
                        break;
                    case "Domicilio":
                        vendedor = vendedor.OrderByDescending(c => c.Domicilio);
                        break;
                    case "Telefono":
                        vendedor = vendedor.OrderBy(c => c.Telefono);
                        break;
                    case "E-Mail":
                        vendedor = vendedor.OrderBy(c => c.Email);
                        break;
                    default:
                        vendedor = vendedor.OrderByDescending(x => x.Nombre);
                        break;
                }
            }
            return vendedor.ToPagedList(currentPage, noOfRecords);
        }


        public IPagedList<Vendedor> GetVendedorByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Vendedor> vendedor;

            switch (filterBy)
            {
                default:
                    vendedor = from c in this.dbset
                              select c;
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                vendedor = vendedor.Where(x => x.Nombre.ToString().Contains(searchString)
                                       || x.Domicilio.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {

                    case "Nombre":
                        vendedor = vendedor.OrderBy(s => s.Nombre);
                        break;
                    default:
                        vendedor = vendedor.OrderBy(x => x.Nombre);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {

                    case "Nombre":
                        vendedor = vendedor.OrderByDescending(s => s.Nombre);
                        break;
                    default:
                        vendedor = vendedor.OrderByDescending(x => x.Nombre);
                        break;
                }
            }

            return vendedor.ToPagedList(currentPage, noOfRecords);
        }
    }
}
