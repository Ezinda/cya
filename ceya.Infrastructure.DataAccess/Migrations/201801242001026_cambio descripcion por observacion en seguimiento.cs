namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiodescripcionporobservacionenseguimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PresupuestoSeguimiento", "Observacion", c => c.String());
            DropColumn("dbo.PresupuestoSeguimiento", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PresupuestoSeguimiento", "Descripcion", c => c.String());
            DropColumn("dbo.PresupuestoSeguimiento", "Observacion");
        }
    }
}
