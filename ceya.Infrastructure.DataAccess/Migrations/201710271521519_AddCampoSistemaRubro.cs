namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoSistemaRubro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rubro", "Sistema", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rubro", "Sistema");
        }
    }
}
