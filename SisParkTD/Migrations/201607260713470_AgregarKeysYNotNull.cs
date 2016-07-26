namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarKeysYNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Apellido", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Dni", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Telefono", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculo", "Patente", c => c.String(nullable: false));
            AlterColumn("dbo.Tamaño", "Descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.TipoDeVehiculo", "NombreDeTipoDeVehiculo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipoDeVehiculo", "NombreDeTipoDeVehiculo", c => c.String());
            AlterColumn("dbo.Tamaño", "Descripcion", c => c.String());
            AlterColumn("dbo.Vehiculo", "Patente", c => c.String());
            AlterColumn("dbo.Cliente", "Telefono", c => c.String());
            AlterColumn("dbo.Cliente", "Dni", c => c.String());
            AlterColumn("dbo.Cliente", "Apellido", c => c.String());
            AlterColumn("dbo.Cliente", "Nombre", c => c.String());
        }
    }
}
