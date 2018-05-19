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
    public class ClienteFormModel
    {
        private GestionComercialWebEntities db = new GestionComercialWebEntities();

        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Tipo de documento")]
        public Nullable<System.Guid> TipoDocumentoId { get; set; }
        public string Documento { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<SelectListItem> TipoDocumentos { get; set; }


    }
}