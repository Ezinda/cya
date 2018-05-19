namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NONULLFECHAALERTA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PresupuestoSeguimiento", "FechaAlerta", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PresupuestoSeguimiento", "FechaAlerta", c => c.DateTime());
        }
    }
}
