namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubrubro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subrubro",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descripcion = c.String(),
                        RubroId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubro", t => t.RubroId, cascadeDelete: true)
                .Index(t => t.RubroId);
            
            AddColumn("dbo.Producto", "SubrubroId", c => c.Guid());
            CreateIndex("dbo.Producto", "SubrubroId");
            AddForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subrubro", "RubroId", "dbo.Rubro");
            DropForeignKey("dbo.Producto", "SubrubroId", "dbo.Subrubro");
            DropIndex("dbo.Subrubro", new[] { "RubroId" });
            DropIndex("dbo.Producto", new[] { "SubrubroId" });
            DropColumn("dbo.Producto", "SubrubroId");
            DropTable("dbo.Subrubro");
        }
    }
}
