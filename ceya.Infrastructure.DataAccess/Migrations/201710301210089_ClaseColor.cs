namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClaseColor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClaseSubrubro", "Clase_Id", "dbo.Clase");
            DropForeignKey("dbo.ClaseSubrubro", "Subrubro_Id", "dbo.Subrubro");
            DropIndex("dbo.ClaseSubrubro", new[] { "Clase_Id" });
            DropIndex("dbo.ClaseSubrubro", new[] { "Subrubro_Id" });
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ColorClase",
                c => new
                    {
                        Color_Id = c.Guid(nullable: false),
                        Clase_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Color_Id, t.Clase_Id })
                .ForeignKey("dbo.Color", t => t.Color_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clase", t => t.Clase_Id, cascadeDelete: true)
                .Index(t => t.Color_Id)
                .Index(t => t.Clase_Id);
            
            AddColumn("dbo.Subrubro", "ClaseId", c => c.Guid());
            CreateIndex("dbo.Subrubro", "ClaseId");
            AddForeignKey("dbo.Subrubro", "ClaseId", "dbo.Clase", "Id");
            DropTable("dbo.ClaseSubrubro");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClaseSubrubro",
                c => new
                    {
                        Clase_Id = c.Guid(nullable: false),
                        Subrubro_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clase_Id, t.Subrubro_Id });
            
            DropForeignKey("dbo.Subrubro", "ClaseId", "dbo.Clase");
            DropForeignKey("dbo.ColorClase", "Clase_Id", "dbo.Clase");
            DropForeignKey("dbo.ColorClase", "Color_Id", "dbo.Color");
            DropIndex("dbo.ColorClase", new[] { "Clase_Id" });
            DropIndex("dbo.ColorClase", new[] { "Color_Id" });
            DropIndex("dbo.Subrubro", new[] { "ClaseId" });
            DropColumn("dbo.Subrubro", "ClaseId");
            DropTable("dbo.ColorClase");
            DropTable("dbo.Color");
            CreateIndex("dbo.ClaseSubrubro", "Subrubro_Id");
            CreateIndex("dbo.ClaseSubrubro", "Clase_Id");
            AddForeignKey("dbo.ClaseSubrubro", "Subrubro_Id", "dbo.Subrubro", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClaseSubrubro", "Clase_Id", "dbo.Clase", "Id", cascadeDelete: true);
        }
    }
}
