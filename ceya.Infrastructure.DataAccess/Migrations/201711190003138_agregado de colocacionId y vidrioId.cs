namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregadodecolocacionIdyvidrioId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presupuesto", "VidrioId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "VidrioNombre", c => c.Guid());
            AddColumn("dbo.Presupuesto", "ColocacionId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "ColocacionNombre", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presupuesto", "ColocacionNombre");
            DropColumn("dbo.Presupuesto", "ColocacionId");
            DropColumn("dbo.Presupuesto", "VidrioNombre");
            DropColumn("dbo.Presupuesto", "VidrioId");
        }
    }
}
