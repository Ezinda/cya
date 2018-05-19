namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updatevwproducto : DbMigration
    {
        public override void Up()
        {
            Sql(@"IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VWPrecioProducto]'))
                DROP VIEW dbo.VWPrecioProducto");
            Sql(@"CREATE VIEW [dbo].[VWPrecioProducto]
                AS
                        SELECT 
                  --Precios
                  ListaPrecio.Id AS ListaId, -- Nuevo
                  ListaPrecio.Codigo AS ListaCodigo,
                  ListaPrecio.Nombre AS ListaDescripcion,
                  Precio.Id AS PrecioId,
                  Precio.FechaDesde AS FechaDesde,
                  Precio.FechaHasta AS FechaHasta,
                  Precio.PrecioProducto AS PrecioProducto,
  
                  --producto
                  Producto.Id AS ProductoId,
                  Producto.Codigo AS ProductoCodigo,
                  Producto.Descripcion AS ProductoDescripcion,
                  Rubro.Id AS RubroId, --Nuevos...
                  Rubro.Codigo AS RubroCodigo,
                  Rubro.Descripcion AS RubroDescripcion,
                  CAST(COALESCE(Rubro.Sistema, 0) AS BIT) AS RubroSistema,
                  Subrubro.Id AS SubrubroId,
                  Subrubro.Codigo AS SubrubroCodigo,
                  Subrubro.Descripcion AS SubrubroDescripcion,
                  Clase.Id AS ClaseId,
                  Clase.Codigo AS ClaseCodigo,
                  Clase.Descripcion AS ClaseDescripcion,
                  Color.Id AS ColorId,
                  Color.Codigo AS ColorCodigo,
                  Color.Descripcion AS ColorDescripcion,
                  REPLACE(STR(RubroMaestro.Codigo, (SELECT MAX(x) FROM (VALUES (LEN(RubroMaestro.Codigo)),(2)) AS value(x))), ' ', '0')
	                + REPLACE(STR(COALESCE(Rubro.Codigo, 0), (SELECT MAX(x) FROM (VALUES (LEN(Rubro.Codigo)),(2)) AS value(x))), ' ', '0')
				    + REPLACE(STR(COALESCE(Subrubro.Codigo, 0), (SELECT MAX(x) FROM (VALUES (LEN(Subrubro.Codigo)),(2)) AS value(x))), ' ', '0')
	              + REPLACE(STR(Producto.Codigo, (SELECT MAX(x) FROM (VALUES (LEN(Producto.Codigo)),(4)) AS value(x))), ' ', '0') AS ProductoCodigoCompuesto,
				  RubroMaestro.Descripcion
	                + COALESCE(' - ' + Rubro.Descripcion, '')
				    + COALESCE(' - ' + Subrubro.Descripcion, '')
	                + COALESCE(' - ' + Color.Descripcion, '') 
					+ ' - ' + Producto.Descripcion AS ProductoDescripcionCompuesto
                  FROM ListaPrecio

                  INNER JOIN dbo.Precio Precio ON Precio.ListaPrecioId = ListaPrecio.Id
                  INNER JOIN dbo.Producto Producto ON Producto.Id = Precio.ProductoId
				
				  LEFT JOIN RubroMaestro on RubroMaestro.Id = Producto.RubroMaestroId
                  LEFT JOIN Rubro on Rubro.Id = Producto.RubroId
                  LEFT JOIN Subrubro on Subrubro.Id = Producto.SubrubroId
                  LEFT JOIN Clase on Clase.Id = Subrubro.ClaseId
                  LEFT JOIN ColorClase on ColorClase.Clase_Id = Clase.Id
                  LEFT JOIN Color on Color.Id = ColorClase.Color_Id
                  WHERE
                  ListaPrecio.Activo = 1");
        }

        public override void Down()
        {
            Sql(@"IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VWPrecioProducto]'))
                DROP VIEW dbo.VWPrecioProducto");
            Sql(@"CREATE VIEW [dbo].[VWPrecioProducto]
                AS
                SELECT
                   tproducto.Id AS ProductoId,
                   tprecio.Id AS PrecioId, 
                   tproducto.Codigo AS ProductoCodigo,
                   tproducto.Descripcion AS ProductoDescripcion,
                   tprecio.PrecioProducto AS PrecioProducto,
                   tlista.Codigo AS ListaCodigo,
                   tlista.Nombre AS ListaDescripcion,
                   tprecio.FechaDesde AS FechaDesde,
                   tprecio.FechaHasta AS FechaHasta
                FROM
	                dbo.ListaPrecio tlista
	                INNER JOIN dbo.Precio tprecio ON tprecio.ListaPrecioId = tlista.Id
	                INNER JOIN dbo.Producto tproducto ON tproducto.Id = tprecio.ProductoId
                WHERE
	                tlista.Activo = 1");
        }

    }
}
