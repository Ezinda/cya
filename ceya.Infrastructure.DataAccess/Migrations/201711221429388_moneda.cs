namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moneda",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Presupuesto", "MonedaId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "Cotizacion", c => c.Decimal(nullable: false, precision: 22, scale: 10));
            CreateIndex("dbo.Presupuesto", "MonedaId");
            AddForeignKey("dbo.Presupuesto", "MonedaId", "dbo.Moneda", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Presupuesto", "MonedaId", "dbo.Moneda");
            DropIndex("dbo.Presupuesto", new[] { "MonedaId" });
            DropColumn("dbo.Presupuesto", "Cotizacion");
            DropColumn("dbo.Presupuesto", "MonedaId");
            DropTable("dbo.Moneda");
        }
    }
}
