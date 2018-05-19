namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Archivo
    {
        public Archivo()
        {
            this.PresupuestoItem = new HashSet<PresupuestoItem>();
        }
    
        public System.Guid Id { get; set; }
        public string NombreOriginal { get; set; }
        public string MimeType { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public System.DateTime FechaHoraSubida { get; set; }
        public System.Guid TransaccionId { get; set; }
        public bool TransaccionCompletada { get; set; }
    
        public virtual ICollection<PresupuestoItem> PresupuestoItem { get; set; }
    }
}
