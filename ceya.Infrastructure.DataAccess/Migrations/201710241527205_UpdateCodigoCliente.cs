namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCodigoCliente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Codigo", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "Codigo", c => c.String());
        }
    }
}
