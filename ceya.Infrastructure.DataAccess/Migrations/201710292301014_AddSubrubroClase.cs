namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubrubroClase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClaseSubrubro",
                c => new
                    {
                        Clase_Id = c.Guid(nullable: false),
                        Subrubro_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clase_Id, t.Subrubro_Id })
                .ForeignKey("dbo.Clase", t => t.Clase_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subrubro", t => t.Subrubro_Id, cascadeDelete: true)
                .Index(t => t.Clase_Id)
                .Index(t => t.Subrubro_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClaseSubrubro", "Subrubro_Id", "dbo.Subrubro");
            DropForeignKey("dbo.ClaseSubrubro", "Clase_Id", "dbo.Clase");
            DropIndex("dbo.ClaseSubrubro", new[] { "Subrubro_Id" });
            DropIndex("dbo.ClaseSubrubro", new[] { "Clase_Id" });
            DropTable("dbo.ClaseSubrubro");
            DropTable("dbo.Clase");
        }
    }
}
