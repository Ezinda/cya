namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archivo",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    NombreOriginal = c.String(),
                    MimeType = c.String(),
                    Nombre = c.String(),
                    Ubicacion = c.String(),
                    FechaHoraSubida = c.DateTime(nullable: false),
                    TransaccionId = c.Guid(nullable: false),
                    TransaccionCompletada = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PresupuestoItem",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    PresupuestoId = c.Guid(nullable: false),
                    ArchivoTipologiaId = c.Guid(),
                    Posicion = c.String(),
                    Descripcion = c.String(),
                    Unidades = c.Int(nullable: false),
                    PrecioUnitario = c.Decimal(nullable: false, precision: 22, scale: 10),
                    VidriosId = c.Guid(),
                    VidriosPrecio = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ColocacionId = c.Guid(),
                    ColocacionPrecio = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Ancho = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Alto = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Carpinteria = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Tapajuntas = c.Decimal(nullable: false, precision: 22, scale: 10),
                    VidriosCalculado = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ColocacionCalculado = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Detalle = c.String(),
                    Importe = c.Decimal(nullable: false, precision: 22, scale: 10),
                    Estado = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Precio", t => t.ColocacionId)
                .ForeignKey("dbo.Precio", t => t.VidriosId)
                .ForeignKey("dbo.Presupuesto", t => t.PresupuestoId, cascadeDelete: true)
                .ForeignKey("dbo.Archivo", t => t.ArchivoTipologiaId)
                .Index(t => t.PresupuestoId)
                .Index(t => t.ArchivoTipologiaId)
                .Index(t => t.VidriosId)
                .Index(t => t.ColocacionId);

            CreateTable(
                "dbo.Precio",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    FechaDesde = c.DateTime(nullable: false),
                    FechaHasta = c.DateTime(nullable: false),
                    PrecioProducto = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ProductoId = c.Guid(nullable: false),
                    ListaPrecioId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListaPrecio", t => t.ListaPrecioId, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.ListaPrecioId);

            CreateTable(
                "dbo.ListaPrecio",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Nombre = c.String(),
                    Activo = c.Boolean(nullable: false),
                    Predefinida = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Producto",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    CodigoProveedor = c.String(),
                    Descripcion = c.String(),
                    UnidadMedidaId = c.Guid(nullable: false),
                    RubroId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubro", t => t.RubroId, cascadeDelete: true)
                .ForeignKey("dbo.UnidadMedida", t => t.UnidadMedidaId, cascadeDelete: true)
                .Index(t => t.UnidadMedidaId)
                .Index(t => t.RubroId);

            CreateTable(
                "dbo.Rubro",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UnidadMedida",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Presupuesto",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    ArchivoTransaccionId = c.Guid(nullable: false),
                    Fecha = c.DateTime(nullable: false),
                    ClienteId = c.Guid(),
                    ObraId = c.Guid(),
                    PresupuestoCategoriaId = c.Guid(),
                    Solicita = c.String(),
                    Telefono = c.String(),
                    Email = c.String(),
                    DescripcionHeader = c.String(),
                    DescripcionFooter = c.String(),
                    ResumenCarpinteria = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenTapajuntas = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenVidrios = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenColocacion = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenSubtotal = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenIva = c.Decimal(nullable: false, precision: 22, scale: 10),
                    ResumenTotal = c.Decimal(nullable: false, precision: 22, scale: 10),
                    PresupuestoEstadoId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Obra", t => t.ObraId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.PresupuestoCategoria", t => t.PresupuestoCategoriaId)
                .ForeignKey("dbo.PresupuestoEstado", t => t.PresupuestoEstadoId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.ObraId)
                .Index(t => t.PresupuestoCategoriaId)
                .Index(t => t.PresupuestoEstadoId);

            CreateTable(
                "dbo.Cliente",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    RazonSocial = c.String(),
                    Apellido = c.String(),
                    Nombre = c.String(),
                    Documento = c.String(),
                    TipoDocumentoId = c.Guid(nullable: false),
                    Domicilio = c.String(),
                    Telefono = c.String(),
                    Celular = c.String(),
                    Email = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDocumento", t => t.TipoDocumentoId, cascadeDelete: true)
                .Index(t => t.TipoDocumentoId);

            CreateTable(
                "dbo.Obra",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Nombre = c.String(),
                    Domicilio = c.String(),
                    ClienteId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);

            CreateTable(
                "dbo.TipoDocumento",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Nombre = c.String(),
                    Orden = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PresupuestoCategoria",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PresupuestoEstado",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Codigo = c.String(),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresupuestoItem", "ArchivoTipologiaId", "dbo.Archivo");
            DropForeignKey("dbo.PresupuestoItem", "PresupuestoId", "dbo.Presupuesto");
            DropForeignKey("dbo.Presupuesto", "PresupuestoEstadoId", "dbo.PresupuestoEstado");
            DropForeignKey("dbo.Presupuesto", "PresupuestoCategoriaId", "dbo.PresupuestoCategoria");
            DropForeignKey("dbo.Cliente", "TipoDocumentoId", "dbo.TipoDocumento");
            DropForeignKey("dbo.Presupuesto", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Presupuesto", "ObraId", "dbo.Obra");
            DropForeignKey("dbo.Obra", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Producto", "UnidadMedidaId", "dbo.UnidadMedida");
            DropForeignKey("dbo.Producto", "RubroId", "dbo.Rubro");
            DropForeignKey("dbo.Precio", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.PresupuestoItem", "VidriosId", "dbo.Precio");
            DropForeignKey("dbo.PresupuestoItem", "ColocacionId", "dbo.Precio");
            DropForeignKey("dbo.Precio", "ListaPrecioId", "dbo.ListaPrecio");
            DropIndex("dbo.Obra", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "TipoDocumentoId" });
            DropIndex("dbo.Presupuesto", new[] { "PresupuestoEstadoId" });
            DropIndex("dbo.Presupuesto", new[] { "PresupuestoCategoriaId" });
            DropIndex("dbo.Presupuesto", new[] { "ObraId" });
            DropIndex("dbo.Presupuesto", new[] { "ClienteId" });
            DropIndex("dbo.Producto", new[] { "RubroId" });
            DropIndex("dbo.Producto", new[] { "UnidadMedidaId" });
            DropIndex("dbo.Precio", new[] { "ListaPrecioId" });
            DropIndex("dbo.Precio", new[] { "ProductoId" });
            DropIndex("dbo.PresupuestoItem", new[] { "ColocacionId" });
            DropIndex("dbo.PresupuestoItem", new[] { "VidriosId" });
            DropIndex("dbo.PresupuestoItem", new[] { "ArchivoTipologiaId" });
            DropIndex("dbo.PresupuestoItem", new[] { "PresupuestoId" });
            DropTable("dbo.PresupuestoEstado");
            DropTable("dbo.PresupuestoCategoria");
            DropTable("dbo.TipoDocumento");
            DropTable("dbo.Obra");
            DropTable("dbo.Cliente");
            DropTable("dbo.Presupuesto");
            DropTable("dbo.UnidadMedida");
            DropTable("dbo.Rubro");
            DropTable("dbo.Producto");
            DropTable("dbo.ListaPrecio");
            DropTable("dbo.Precio");
            DropTable("dbo.PresupuestoItem");
            DropTable("dbo.Archivo");
        }
    }
}
