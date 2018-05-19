namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregarrelacionseguimientoestado : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PresupuestoSeguimiento", "PresupuestoEstadoId");
            AddForeignKey("dbo.PresupuestoSeguimiento", "PresupuestoEstadoId", "dbo.PresupuestoEstado", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresupuestoSeguimiento", "PresupuestoEstadoId", "dbo.PresupuestoEstado");
            DropIndex("dbo.PresupuestoSeguimiento", new[] { "PresupuestoEstadoId" });
        }
    }
}
