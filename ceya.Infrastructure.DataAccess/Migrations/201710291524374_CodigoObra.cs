namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoObra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Obra", "CodigoObra", c => c.String());
            AlterColumn("dbo.Obra", "Codigo", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Obra", "Codigo", c => c.String());
            DropColumn("dbo.Obra", "CodigoObra");
        }
    }
}
