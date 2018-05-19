namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conceptosvarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presupuesto", "ConceptosVarios", c => c.String());
            AddColumn("dbo.Presupuesto", "ResumenVarios", c => c.Decimal(nullable: false, precision: 22, scale: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presupuesto", "ResumenVarios");
            DropColumn("dbo.Presupuesto", "ConceptosVarios");
        }
    }
}
