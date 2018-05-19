namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoCompuestoProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "CodigoCompuesto", c => c.String());
            AlterColumn("dbo.Producto", "Codigo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producto", "Codigo", c => c.String());
            DropColumn("dbo.Producto", "CodigoCompuesto");
        }
    }
}
