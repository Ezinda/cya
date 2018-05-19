namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoProducto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoProducto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Producto", "TipoProductoId", c => c.Guid());
            CreateIndex("dbo.Producto", "TipoProductoId");
            AddForeignKey("dbo.Producto", "TipoProductoId", "dbo.TipoProducto", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "TipoProductoId", "dbo.TipoProducto");
            DropIndex("dbo.Producto", new[] { "TipoProductoId" });
            DropColumn("dbo.Producto", "TipoProductoId");
            DropTable("dbo.TipoProducto");
        }
    }
}
