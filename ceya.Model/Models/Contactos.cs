namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Contacto
    {
        public Contacto()
        { }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Guid ConstructoraId { get; set; }
        public Constructora Constructora { get; set; }
    }
}
