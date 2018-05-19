using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ceya.Infrastructure.DataAccess;

namespace mvc.ViewModels
{
    public class ContactosFormModel
    {
        private string sortBy;

        public ContactosFormModel(string constructora)
        {
            Constructora = constructora;
        }

        public ContactosFormModel()
        {
        }

        public ContactosFormModel(string constructora, string sortBy) : this(constructora)
        {
            this.sortBy = sortBy;
        }

        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public System.Guid ConstructoraId { get; set; }
        public string Constructora { get; set; }
        public List<ContactoListViewModel> List { get; internal set; }
    }
}