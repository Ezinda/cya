namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulleabletipodocumento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cliente", "TipoDocumentoId", "dbo.TipoDocumento");
            DropForeignKey("dbo.Constructora", "TipoDocumentoId", "dbo.TipoDocumento");
            DropIndex("dbo.Cliente", new[] { "TipoDocumentoId" });
            DropIndex("dbo.Constructora", new[] { "TipoDocumentoId" });
            AlterColumn("dbo.Cliente", "TipoDocumentoId", c => c.Guid());
            AlterColumn("dbo.Constructora", "TipoDocumentoId", c => c.Guid());
            CreateIndex("dbo.Cliente", "TipoDocumentoId");
            CreateIndex("dbo.Constructora", "TipoDocumentoId");
            AddForeignKey("dbo.Cliente", "TipoDocumentoId", "dbo.TipoDocumento", "Id");
            AddForeignKey("dbo.Constructora", "TipoDocumentoId", "dbo.TipoDocumento", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Constructora", "TipoDocumentoId", "dbo.TipoDocumento");
            DropForeignKey("dbo.Cliente", "TipoDocumentoId", "dbo.TipoDocumento");
            DropIndex("dbo.Constructora", new[] { "TipoDocumentoId" });
            DropIndex("dbo.Cliente", new[] { "TipoDocumentoId" });
            AlterColumn("dbo.Constructora", "TipoDocumentoId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Cliente", "TipoDocumentoId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Constructora", "TipoDocumentoId");
            CreateIndex("dbo.Cliente", "TipoDocumentoId");
            AddForeignKey("dbo.Constructora", "TipoDocumentoId", "dbo.TipoDocumento", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cliente", "TipoDocumentoId", "dbo.TipoDocumento", "Id", cascadeDelete: true);
        }
    }
}
