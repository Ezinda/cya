namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoClase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clase", "Codigo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clase", "Codigo", c => c.String());
        }
    }
}
