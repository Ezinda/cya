namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCyclicFkSubrubro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro");
            DropIndex("dbo.Producto", new[] { "SubrubroId" });
            AlterColumn("dbo.Producto", "SubrubroId", c => c.Guid());
            CreateIndex("dbo.Producto", "SubrubroId");
            AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro");
            DropIndex("dbo.Producto", new[] { "SubrubroId" });
            // AlterColumn("dbo.Producto", "SubrubroId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Producto", "SubrubroId", c => c.Guid(nullable: true));
            CreateIndex("dbo.Producto", "SubrubroId");
            // AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id");
        }
    }
}
