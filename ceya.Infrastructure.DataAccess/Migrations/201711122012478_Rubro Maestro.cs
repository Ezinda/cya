namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RubroMaestro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RubroMaestro",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Rubro", "RubroMaestroId", c => c.Guid());
            CreateIndex("dbo.Rubro", "RubroMaestroId");
            AddForeignKey("dbo.Rubro", "RubroMaestroId", "dbo.RubroMaestro", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rubro", "RubroMaestroId", "dbo.RubroMaestro");
            DropIndex("dbo.Rubro", new[] { "RubroMaestroId" });
            DropColumn("dbo.Rubro", "RubroMaestroId");
            DropTable("dbo.RubroMaestro");
        }
    }
}
