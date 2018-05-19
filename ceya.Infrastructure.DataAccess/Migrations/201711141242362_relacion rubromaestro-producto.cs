namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relacionrubromaestroproducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "RubroMaestroId", c => c.Guid());
            CreateIndex("dbo.Producto", "RubroMaestroId");
            AddForeignKey("dbo.Producto", "RubroMaestroId", "dbo.RubroMaestro", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "RubroMaestroId", "dbo.RubroMaestro");
            DropIndex("dbo.Producto", new[] { "RubroMaestroId" });
            DropColumn("dbo.Producto", "RubroMaestroId");
        }
    }
}
