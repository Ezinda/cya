using ceya.Model.Models;
using mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ceya.Infrastructure.DataAccess;
using ceya.Core.Helpers;

namespace mvc.ViewModels
{
    public class PresupuestoViewModel
    {
        private GestionComercialWebEntities db = new GestionComercialWebEntities();

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public Guid ArchivoTransaccionId { get; set; }
        public DateTime Fecha { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ObraId { get; set; }
        public Guid PresupuestoCategoriaId { get; set; }
        public string Solicita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DescripcionHeader { get; set; }
        public List<PresupuestoItemViewModel> Items { get; set; }
        public string DescripcionFooter { get; set; }
        public decimal ResumenCarpinteria { get; set; }
        public decimal ResumenTapajuntas { get; set; }
        public decimal ResumenVidrios { get; set; }
        public decimal ResumenColocacion { get; set; }
        public decimal ResumenSubtotal { get; set; }
        public decimal ResumenIva { get; set; }
        public decimal ResumenTotal { get; set; }

        
        public List<PresupuestoCategoria> Categorias;
        public List<Precio> Colocaciones;
        public List<Precio> Vidrios;

        private void Initialize()
        {
            this.Codigo = GenerarCodigoNuevo();
            this.ArchivoTransaccionId = Guid.NewGuid();
            this.Fecha = DateTime.Now;
            this.Categorias = this.db.PresupuestoCategoria.OrderBy(x => x.Codigo).ToList();
            this.Colocaciones = this.db.Precio
                .Include(x => x.ListaPrecio)
                .Include(x => x.Producto)
                .Where(x => x.ListaPrecio.Codigo == Constants.ListaDePreciosDelSistema.ColocacionCodigo)
                .OrderBy(x => x.Producto.Descripcion)
                .ToList();
            this.Vidrios = this.db.Precio
                .Include(x => x.ListaPrecio)
                .Include(x => x.Producto)
                .Where(x => x.ListaPrecio.Codigo == Constants.ListaDePreciosDelSistema.VidriosCodigo)
                .OrderBy(x => x.Producto.Descripcion)
                .ToList();
        }

        private string GenerarCodigoNuevo()
        {
            var ultimo = ObtenerNuevoNumeroDeCodigo();

            var nuevo = (ultimo + 1).ToString();

            return Codificable.GenerateCodigo(nuevo);
        }

        private long ObtenerNuevoNumeroDeCodigo()
        {
            var ultimo = db.Presupuesto.Max(x => x.Codigo);

            return Convert.ToInt64(ultimo);
        }

        public PresupuestoViewModel Load()
        {
            Initialize();
            return this;
        }
    }
}