namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVWPrecioProducto : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            Sql(@"IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VWPrecioProducto]'))
                DROP VIEW dbo.VWPrecioProducto");
            Sql(@"CREATE VIEW [dbo].[VWPrecioProducto]
                AS
                SELECT
                   tproducto.Id AS ProductoId,
                   tprecio.Id AS PrecioId, 
                   tproducto.Codigo AS Codigo,
                   tproducto.Descripcion AS Descripcion,
                   tprecio.PrecioProducto AS PrecioProducto,
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
