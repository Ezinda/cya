namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Constructora : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Constructora",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Long(nullable: false),
                        RazonSocial = c.String(),
                        Apellido = c.String(),
                        Nombre = c.String(),
                        Documento = c.String(),
                        TipoDocumentoId = c.Guid(nullable: false),
                        Domicilio = c.String(),
                        Telefono = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDocumento", t => t.TipoDocumentoId, cascadeDelete: true)
                .Index(t => t.TipoDocumentoId);
            
            AddColumn("dbo.Presupuesto", "Constructora_Id", c => c.Guid());
            AddColumn("dbo.Obra", "Constructora_Id", c => c.Guid());
            CreateIndex("dbo.Presupuesto", "Constructora_Id");
            CreateIndex("dbo.Obra", "Constructora_Id");
            AddForeignKey("dbo.Obra", "Constructora_Id", "dbo.Constructora", "Id");
            AddForeignKey("dbo.Presupuesto", "Constructora_Id", "dbo.Constructora", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Constructora", "TipoDocumentoId", "dbo.TipoDocumento");
            DropForeignKey("dbo.Presupuesto", "Constructora_Id", "dbo.Constructora");
            DropForeignKey("dbo.Obra", "Constructora_Id", "dbo.Constructora");
            DropIndex("dbo.Constructora", new[] { "TipoDocumentoId" });
            DropIndex("dbo.Obra", new[] { "Constructora_Id" });
            DropIndex("dbo.Presupuesto", new[] { "Constructora_Id" });
            DropColumn("dbo.Obra", "Constructora_Id");
            DropColumn("dbo.Presupuesto", "Constructora_Id");
            DropTable("dbo.Constructora");
        }
    }
}
