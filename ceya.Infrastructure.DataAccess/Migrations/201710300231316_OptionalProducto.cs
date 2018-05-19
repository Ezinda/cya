namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalProducto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Producto", "RubroId", "dbo.Rubro");
            DropForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro");
            DropForeignKey("dbo.Producto", "UnidadMedidaId", "dbo.UnidadMedida");
            DropIndex("dbo.Producto", new[] { "UnidadMedidaId" });
            DropIndex("dbo.Producto", new[] { "RubroId" });
            DropIndex("dbo.Producto", new[] { "SubrubroId" });
            AlterColumn("dbo.Producto", "UnidadMedidaId", c => c.Guid());
            AlterColumn("dbo.Producto", "RubroId", c => c.Guid());
            AlterColumn("dbo.Producto", "SubrubroId", c => c.Guid());
            CreateIndex("dbo.Producto", "UnidadMedidaId");
            CreateIndex("dbo.Producto", "RubroId");
            CreateIndex("dbo.Producto", "SubrubroId");
            AddForeignKey("dbo.Producto", "RubroId", "dbo.Rubro", "Id");
            AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id");
            AddForeignKey("dbo.Producto", "UnidadMedidaId", "dbo.UnidadMedida", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "UnidadMedidaId", "dbo.UnidadMedida");
            DropForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro");
            DropForeignKey("dbo.Producto", "RubroId", "dbo.Rubro");
            DropIndex("dbo.Producto", new[] { "SubrubroId" });
            DropIndex("dbo.Producto", new[] { "RubroId" });
            DropIndex("dbo.Producto", new[] { "UnidadMedidaId" });
            AlterColumn("dbo.Producto", "SubrubroId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Producto", "RubroId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Producto", "UnidadMedidaId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Producto", "SubrubroId");
            CreateIndex("dbo.Producto", "RubroId");
            CreateIndex("dbo.Producto", "UnidadMedidaId");
            AddForeignKey("dbo.Producto", "UnidadMedidaId", "dbo.UnidadMedida", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Producto", "RubroId", "dbo.Rubro", "Id", cascadeDelete: true);
        }
    }
}
