namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkpresupuesSeguimiento : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PresupuestoSeguimiento", "PresupuestoId");
            AddForeignKey("dbo.PresupuestoSeguimiento", "PresupuestoId", "dbo.Presupuesto", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresupuestoSeguimiento", "PresupuestoId", "dbo.Presupuesto");
            DropIndex("dbo.PresupuestoSeguimiento", new[] { "PresupuestoId" });
        }
    }
}
