namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolactivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PresupuestoSeguimiento", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PresupuestoSeguimiento", "Activo");
        }
    }
}
