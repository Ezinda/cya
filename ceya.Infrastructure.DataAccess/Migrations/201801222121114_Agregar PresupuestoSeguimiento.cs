namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarPresupuestoSeguimiento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PresupuestoSeguimiento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PresupuestoId = c.Guid(nullable: false),
                        PresupuestoEstadoId = c.Guid(nullable: false),
                        CodigoPresupuesto = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        FechaAlerta = c.DateTime(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PresupuestoSeguimiento");
        }
    }
}
