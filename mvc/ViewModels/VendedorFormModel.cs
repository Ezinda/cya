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
    public class VendedorFormModel
    {
        private string sortBy;

        public VendedorFormModel()
        {
            this.sortBy = sortBy;
        }


        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<VendedorListViewModel> List { get; internal set; }
    }
}