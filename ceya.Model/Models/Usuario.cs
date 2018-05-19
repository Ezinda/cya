namespace ceya.Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Usuario
    {
        public System.Guid Id { get; set; }
        public System.Guid RolId { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }

        public bool NuevoCliente { get; set; }
        public bool ModificarCliente { get; set; }
        public bool EliminarCliente { get; set; }

        public bool NuevaConstructora { get; set; }
        public bool ModificarConstructora { get; set; }
        public bool EliminarConstructora { get; set; }

        public bool NuevaObra { get; set; }
        public bool ModificarObra { get; set; }
        public bool EliminarObra { get; set; }

        public bool NuevoPresupuesto { get; set; }
        public bool ModificarPresupuesto { get; set; }
        public bool EliminarPresupuesto { get; set; }
        public bool ImprimirPresupuesto { get; set; }
        public bool SeguimientoPresupuesto { get; set; }
        public bool AnularPresupuesto { get; set; }
        
        public virtual Rol Rol { get; set; }
        
    }
}
