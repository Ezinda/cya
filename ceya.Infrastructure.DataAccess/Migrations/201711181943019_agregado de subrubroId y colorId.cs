namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregadodesubrubroIdycolorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presupuesto", "SubrubroId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "SubrubroNombre", c => c.Guid());
            AddColumn("dbo.Presupuesto", "ColorId", c => c.Guid());
            AddColumn("dbo.Presupuesto", "ColorNombre", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presupuesto", "ColorNombre");
            DropColumn("dbo.Presupuesto", "ColorId");
            DropColumn("dbo.Presupuesto", "SubrubroNombre");
            DropColumn("dbo.Presupuesto", "SubrubroId");
        }
    }
}
