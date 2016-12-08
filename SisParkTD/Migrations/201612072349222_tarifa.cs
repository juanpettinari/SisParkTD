namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tarifa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoDeVehiculo", "Tarifa15MDecimal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TipoDeVehiculo", "Tarifa30MDecimal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoDeVehiculo", "Tarifa30MDecimal");
            DropColumn("dbo.TipoDeVehiculo", "Tarifa15MDecimal");
        }
    }
}
