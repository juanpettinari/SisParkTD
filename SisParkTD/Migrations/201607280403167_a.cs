namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movimiento", "CuentaId", "dbo.Cuenta");
            DropForeignKey("dbo.Parcela", "TamañoId", "dbo.Tamaño");
            DropForeignKey("dbo.TipoDeVehiculo", "TamañoId", "dbo.Tamaño");
            DropForeignKey("dbo.Cliente", "Vehiculo_VehiculoId", "dbo.Vehiculo");
            DropForeignKey("dbo.Cuenta", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Abono", "CuentaId", "dbo.Cuenta");
            DropIndex("dbo.Abono", new[] { "CuentaId" });
            DropIndex("dbo.Cuenta", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "Vehiculo_VehiculoId" });
            DropIndex("dbo.Movimiento", new[] { "CuentaId" });
            DropIndex("dbo.Movimiento", new[] { "TicketId" });
            DropIndex("dbo.Parcela", new[] { "TamañoId" });
            DropIndex("dbo.TipoDeVehiculo", new[] { "TamañoId" });
            AddColumn("dbo.Cliente", "SaldoDecimal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Parcela", "TipoDeVehiculoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Movimiento", "TicketId", c => c.Int(nullable: false));
            CreateIndex("dbo.Parcela", "TipoDeVehiculoId");
            CreateIndex("dbo.Movimiento", "TicketId");
            AddForeignKey("dbo.Parcela", "TipoDeVehiculoId", "dbo.TipoDeVehiculo", "TipoDeVehiculoId");
            DropColumn("dbo.Cliente", "Vehiculo_VehiculoId");
            DropColumn("dbo.Vehiculo", "ClienteId");
            DropColumn("dbo.Movimiento", "CuentaId");
            DropColumn("dbo.TipoDeVehiculo", "TamañoId");
            DropTable("dbo.Cuenta");
            DropTable("dbo.Tamaño");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tamaño",
                c => new
                    {
                        TamañoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        ValorTamaño = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TamañoId);
            
            CreateTable(
                "dbo.Cuenta",
                c => new
                    {
                        CuentaId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        SaldoDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CuentaId);
            
            AddColumn("dbo.TipoDeVehiculo", "TamañoId", c => c.Int(nullable: false));
            AddColumn("dbo.Movimiento", "CuentaId", c => c.Int());
            AddColumn("dbo.Vehiculo", "ClienteId", c => c.Int());
            AddColumn("dbo.Cliente", "Vehiculo_VehiculoId", c => c.Int());
            DropForeignKey("dbo.Parcela", "TipoDeVehiculoId", "dbo.TipoDeVehiculo");
            DropIndex("dbo.Movimiento", new[] { "TicketId" });
            DropIndex("dbo.Parcela", new[] { "TipoDeVehiculoId" });
            AlterColumn("dbo.Movimiento", "TicketId", c => c.Int());
            DropColumn("dbo.Parcela", "TipoDeVehiculoId");
            DropColumn("dbo.Cliente", "SaldoDecimal");
            CreateIndex("dbo.TipoDeVehiculo", "TamañoId");
            CreateIndex("dbo.Parcela", "TamañoId");
            CreateIndex("dbo.Movimiento", "TicketId");
            CreateIndex("dbo.Movimiento", "CuentaId");
            CreateIndex("dbo.Cliente", "Vehiculo_VehiculoId");
            CreateIndex("dbo.Cuenta", "ClienteId");
            CreateIndex("dbo.Abono", "CuentaId");
            AddForeignKey("dbo.Abono", "CuentaId", "dbo.Cuenta", "CuentaId");
            AddForeignKey("dbo.Cuenta", "ClienteId", "dbo.Cliente", "ClienteId");
            AddForeignKey("dbo.Cliente", "Vehiculo_VehiculoId", "dbo.Vehiculo", "VehiculoId");
            AddForeignKey("dbo.TipoDeVehiculo", "TamañoId", "dbo.Tamaño", "TamañoId");
            AddForeignKey("dbo.Parcela", "TamañoId", "dbo.Tamaño", "TamañoId");
            AddForeignKey("dbo.Movimiento", "CuentaId", "dbo.Cuenta", "CuentaId");
        }
    }
}
