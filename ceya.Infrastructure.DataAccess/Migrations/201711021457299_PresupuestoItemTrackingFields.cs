namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PresupuestoItemTrackingFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PresupuestoItem", "PresupuestoItemAnteriorId", c => c.Guid());
            AddColumn("dbo.PresupuestoItem", "PresupuestoItemNuevoId", c => c.Guid());
            CreateIndex("dbo.PresupuestoItem", "PresupuestoItemAnteriorId");
            CreateIndex("dbo.PresupuestoItem", "PresupuestoItemNuevoId");
            AddForeignKey("dbo.PresupuestoItem", "PresupuestoItemAnteriorId", "dbo.PresupuestoItem", "Id");
            AddForeignKey("dbo.PresupuestoItem", "PresupuestoItemNuevoId", "dbo.PresupuestoItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresupuestoItem", "PresupuestoItemNuevoId", "dbo.PresupuestoItem");
            DropForeignKey("dbo.PresupuestoItem", "PresupuestoItemAnteriorId", "dbo.PresupuestoItem");
            DropIndex("dbo.PresupuestoItem", new[] { "PresupuestoItemNuevoId" });
            DropIndex("dbo.PresupuestoItem", new[] { "PresupuestoItemAnteriorId" });
            DropColumn("dbo.PresupuestoItem", "PresupuestoItemNuevoId");
            DropColumn("dbo.PresupuestoItem", "PresupuestoItemAnteriorId");
        }
    }
}
