namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PresupuestoTrackingFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presupuesto", "FechaCreacion", c => c.DateTime(nullable: false));
            AddColumn("dbo.Presupuesto", "PresupuestoAnteriorId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "PresupuestoNuevoId", c => c.Guid());
            CreateIndex("dbo.Presupuesto", "PresupuestoAnteriorId");
            CreateIndex("dbo.Presupuesto", "PresupuestoNuevoId");
            AddForeignKey("dbo.Presupuesto", "PresupuestoAnteriorId", "dbo.Presupuesto", "Id");
            AddForeignKey("dbo.Presupuesto", "PresupuestoNuevoId", "dbo.Presupuesto", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Presupuesto", "PresupuestoNuevoId", "dbo.Presupuesto");
            DropForeignKey("dbo.Presupuesto", "PresupuestoAnteriorId", "dbo.Presupuesto");
            DropIndex("dbo.Presupuesto", new[] { "PresupuestoNuevoId" });
            DropIndex("dbo.Presupuesto", new[] { "PresupuestoAnteriorId" });
            DropColumn("dbo.Presupuesto", "PresupuestoNuevoId");
            DropColumn("dbo.Presupuesto", "PresupuestoAnteriorId");
            DropColumn("dbo.Presupuesto", "FechaCreacion");
        }
    }
}
