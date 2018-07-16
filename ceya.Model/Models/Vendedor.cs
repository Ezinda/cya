namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Vendedor
    {
        public Vendedor()
        { }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
