namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagetypecodigopresupuesto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Presupuesto", "Codigo", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Presupuesto", "Codigo", c => c.String());
        }
    }
}
