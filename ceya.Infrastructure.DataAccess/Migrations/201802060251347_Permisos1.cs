namespace ceya.Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permisos1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "NuevoCliente", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "ModificarCliente", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "EliminarCliente", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NuevaConstructora", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "ModificarConstructora", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "EliminarConstructora", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NuevaObra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "ModificarObra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "EliminarObra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NuevoPresupuesto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "ModificarPresupuesto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "EliminarPresupuesto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "ImprimirPresupuesto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "SeguimientoPresupuesto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "AnularPresupuesto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "AnularPresupuesto");
            DropColumn("dbo.Usuario", "SeguimientoPresupuesto");
            DropColumn("dbo.Usuario", "ImprimirPresupuesto");
            DropColumn("dbo.Usuario", "EliminarPresupuesto");
            DropColumn("dbo.Usuario", "ModificarPresupuesto");
            DropColumn("dbo.Usuario", "NuevoPresupuesto");
            DropColumn("dbo.Usuario", "EliminarObra");
            DropColumn("dbo.Usuario", "ModificarObra");
            DropColumn("dbo.Usuario", "NuevaObra");
            DropColumn("dbo.Usuario", "EliminarConstructora");
            DropColumn("dbo.Usuario", "ModificarConstructora");
            DropColumn("dbo.Usuario", "NuevaConstructora");
            DropColumn("dbo.Usuario", "EliminarCliente");
            DropColumn("dbo.Usuario", "ModificarCliente");
            DropColumn("dbo.Usuario", "NuevoCliente");
        }
    }
}
