using ceya.EF.Configurations;
using ceya.Model.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ceya.Infrastructure.DataAccess
{
    public class GestionComercialWebEntities : DbContext
    {
        public GestionComercialWebEntities()
            : base(ConnectionStringHelper.GetConnectionString())
        {
        }

        public GestionComercialWebEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        // tablas
        public virtual DbSet<Archivo> Archivo { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Constructora> Constructora { get; set; }
        public virtual DbSet<ListaPrecio> ListaPrecio { get; set; }
        public virtual DbSet<Obra> Obra { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<Presupuesto> Presupuesto { get; set; }
        public virtual DbSet<PresupuestoCategoria> PresupuestoCategoria { get; set; }
        public virtual DbSet<PresupuestoEstado> PresupuestoEstado { get; set; }
        public virtual DbSet<PresupuestoItem> PresupuestoItem { get; set; }
        public virtual DbSet<PresupuestoSeguimiento> PresupuestoSeguimiento { get; set; }
        public virtual DbSet<RubroMaestro> RubroMaestro { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Subrubro> Subrubro { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }

        // vistas
        public virtual DbSet<VWPrecioProducto> VWPrecioProducto { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(22, 10));

            modelBuilder.Configurations.Add(new ArchivoConfiguration());
            modelBuilder.Configurations.Add(new ClienteUserConfiguration());
            modelBuilder.Configurations.Add(new ConstructoraConfiguration());
            modelBuilder.Configurations.Add(new ListaPrecioConfiguration());
            modelBuilder.Configurations.Add(new ObraConfiguration());
            modelBuilder.Configurations.Add(new PrecioConfiguration());
            modelBuilder.Configurations.Add(new PresupuestoConfiguration());
            modelBuilder.Configurations.Add(new PresupuestoCategoriaConfiguration());
            modelBuilder.Configurations.Add(new PresupuestoEstadoConfiguration());
            modelBuilder.Configurations.Add(new PresupuestoItemConfiguration());
            modelBuilder.Configurations.Add(new ProductoConfiguration());
            modelBuilder.Configurations.Add(new RubroMaestroConfiguration());
            modelBuilder.Configurations.Add(new RubroConfiguration());
            modelBuilder.Configurations.Add(new SubrubroConfiguration());
            modelBuilder.Configurations.Add(new TipoDocumentoConfiguration());
            modelBuilder.Configurations.Add(new UnidadMedidaConfiguration());
            modelBuilder.Configurations.Add(new TipoProductoConfiguration());
            modelBuilder.Configurations.Add(new MonedaConfiguration());

            modelBuilder.Entity<VWPrecioProducto>().HasKey(x => x.PrecioId);
        }
    }
}
