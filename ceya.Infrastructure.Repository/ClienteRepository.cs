using System;
using ceya.Domain.Repository;
using ceya.Infrastructure.DataAccess;
using ceya.Model.Models;
using System.Linq;
using X.PagedList;

namespace ceya.Infrastructure.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public long MaxCodigo()
        {
            return (dbset.Max(x => (long?)x.Codigo).GetValueOrDefault() + 1);
        }

        public IPagedList<Cliente> GetClientesByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString)
        {
            IQueryable<Cliente> clientes;

            clientes = from p in this.dbset select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(x => x.Codigo.ToString().Contains(searchString)
                                       || x.Documento.Contains(searchString)
                                       || x.RazonSocial.Contains(searchString)
                                       || x.Apellido.Contains(searchString)
                                       || x.Nombre.Contains(searchString));
            }

            if (direction.Equals("asc"))
            {
                switch (sortBy)
                {
                    case "Codigo":
                        clientes = clientes.OrderBy(x => x.Codigo);
                        break;
                    case "Cliente":
                        clientes = clientes.OrderBy(s => s.Apellido);
                        break;
                    default:
                        clientes = clientes.OrderBy(x => x.Apellido);
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Codigo":
                        clientes = clientes.OrderByDescending(x => x.Codigo);
                        break;
                    case "Cliente":
                        clientes = clientes.OrderByDescending(s => s.Apellido);
                        break;
                    default:
                        clientes = clientes.OrderByDescending(x => x.Apellido);
                        break;
                }
            }

            return clientes.ToPagedList(currentPage, noOfRecords);
        }
    }
}
