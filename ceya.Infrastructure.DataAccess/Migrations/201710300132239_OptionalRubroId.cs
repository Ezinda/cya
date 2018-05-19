namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalRubroId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subrubro", "RubroId", "dbo.Rubro");
            DropIndex("dbo.Subrubro", new[] { "RubroId" });
            AlterColumn("dbo.Subrubro", "RubroId", c => c.Guid());
            CreateIndex("dbo.Subrubro", "RubroId");
            AddForeignKey("dbo.Subrubro", "RubroId", "dbo.Rubro", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subrubro", "RubroId", "dbo.Rubro");
            DropIndex("dbo.Subrubro", new[] { "RubroId" });
            AlterColumn("dbo.Subrubro", "RubroId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Subrubro", "RubroId");
            AddForeignKey("dbo.Subrubro", "RubroId", "dbo.Rubro", "Id", cascadeDelete: true);
        }
    }
}
