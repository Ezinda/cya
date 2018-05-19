namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConstructoraColumnInPresupuesto : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Presupuesto", name: "Constructora_Id", newName: "ConstructoraId");
            RenameIndex(table: "dbo.Presupuesto", name: "IX_Constructora_Id", newName: "IX_ConstructoraId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Presupuesto", name: "IX_ConstructoraId", newName: "IX_Constructora_Id");
            RenameColumn(table: "dbo.Presupuesto", name: "ConstructoraId", newName: "Constructora_Id");
        }
    }
}
