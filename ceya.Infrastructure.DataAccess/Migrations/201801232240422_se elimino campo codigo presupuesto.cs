namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seeliminocampocodigopresupuesto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PresupuestoSeguimiento", "CodigoPresupuesto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PresupuestoSeguimiento", "CodigoPresupuesto", c => c.Long(nullable: false));
        }
    }
}
