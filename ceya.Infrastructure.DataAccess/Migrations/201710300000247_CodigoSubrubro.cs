namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoSubrubro : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rubro", "Codigo", c => c.Int(nullable: false));
            AlterColumn("dbo.Subrubro", "Codigo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subrubro", "Codigo", c => c.String());
            AlterColumn("dbo.Rubro", "Codigo", c => c.String());
        }
    }
}
