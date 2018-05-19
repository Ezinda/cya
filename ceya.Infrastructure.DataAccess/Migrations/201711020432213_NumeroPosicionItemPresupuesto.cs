namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumeroPosicionItemPresupuesto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PresupuestoItem", "NumeroPosicion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PresupuestoItem", "NumeroPosicion");
        }
    }
}
