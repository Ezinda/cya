namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Obra = new HashSet<Obra>();
            this.Presupuesto = new HashSet<Presupuesto>();
        }
    
        public System.Guid Id { get; set; }
        public long Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public Nullable<System.Guid> TipoDocumentoId { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<Obra> Obra { get; set; }
        public virtual ICollection<Presupuesto> Presupuesto { get; set; }
    }
}
