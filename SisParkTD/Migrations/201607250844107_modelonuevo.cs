namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelonuevo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abono",
                c => new
                    {
                        AbonoId = c.Int(nullable: false, identity: true),
                        CuentaId = c.Int(nullable: false),
                        ParcelaId = c.Int(nullable: false),
                        FechaMesInicio = c.DateTime(nullable: false),
                        FechaMesFin = c.DateTime(),
                    })
                .PrimaryKey(t => t.AbonoId)
                .ForeignKey("dbo.Parcela", t => t.ParcelaId)
                .ForeignKey("dbo.Cuenta", t => t.CuentaId)
                .Index(t => t.CuentaId)
                .Index(t => t.ParcelaId);
            
            CreateTable(
                "dbo.Cuenta",
                c => new
                    {
                        CuentaId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        SaldoDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CuentaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Dni = c.String(),
                        Telefono = c.String(),
                        Vehiculo_VehiculoId = c.Int(),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Vehiculo", t => t.Vehiculo_VehiculoId)
                .Index(t => t.Vehiculo_VehiculoId);
            
            CreateTable(
                "dbo.Vehiculo",
                c => new
                    {
                        VehiculoId = c.Int(nullable: false, identity: true),
                        Patente = c.String(),
                        ClienteId = c.Int(),
                        TipoDeVehiculoId = c.Int(nullable: false),
                        DescripcionDeVehiculo = c.String(),
                    })
                .PrimaryKey(t => t.VehiculoId)
                .ForeignKey("dbo.TipoDeVehiculo", t => t.TipoDeVehiculoId)
                .Index(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        VehiculoId = c.Int(),
                        AbonoId = c.Int(),
                        ParcelaId = c.Int(nullable: false),
                        EstadoDeTicket = c.Int(nullable: false),
                        FechaYHoraDeEntrada = c.DateTime(nullable: false),
                        FechaYHoraDeSalida = c.DateTime(),
                        TiempoTotal = c.Time(precision: 7),
                        PrecioTotalDecimal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Abono", t => t.AbonoId)
                .ForeignKey("dbo.Parcela", t => t.ParcelaId)
                .ForeignKey("dbo.Vehiculo", t => t.VehiculoId)
                .Index(t => t.VehiculoId)
                .Index(t => t.AbonoId)
                .Index(t => t.ParcelaId);
            
            CreateTable(
                "dbo.Movimiento",
                c => new
                    {
                        MovimientoId = c.Int(nullable: false, identity: true),
                        TipoDeMovimiento = c.Int(nullable: false),
                        CuentaId = c.Int(),
                        TicketId = c.Int(),
                    })
                .PrimaryKey(t => t.MovimientoId)
                .ForeignKey("dbo.Cuenta", t => t.CuentaId)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.CuentaId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Parcela",
                c => new
                    {
                        ParcelaId = c.Int(nullable: false, identity: true),
                        NumeroParcela = c.Int(nullable: false),
                        TamañoId = c.Int(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ParcelaId)
                .ForeignKey("dbo.Tamaño", t => t.TamañoId)
                .Index(t => t.TamañoId);
            
            CreateTable(
                "dbo.Tamaño",
                c => new
                    {
                        TamañoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        ValorTamaño = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TamañoId);
            
            CreateTable(
                "dbo.TipoDeVehiculo",
                c => new
                    {
                        TipoDeVehiculoId = c.Int(nullable: false, identity: true),
                        TamañoId = c.Int(nullable: false),
                        NombreDeTipoDeVehiculo = c.String(),
                        TarifaOcasionalDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TarifaMensualDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TipoDeVehiculoId)
                .ForeignKey("dbo.Tamaño", t => t.TamañoId)
                .Index(t => t.TamañoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Abono", "CuentaId", "dbo.Cuenta");
            DropForeignKey("dbo.Cuenta", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "Vehiculo_VehiculoId", "dbo.Vehiculo");
            DropForeignKey("dbo.Ticket", "VehiculoId", "dbo.Vehiculo");
            DropForeignKey("dbo.Ticket", "ParcelaId", "dbo.Parcela");
            DropForeignKey("dbo.Vehiculo", "TipoDeVehiculoId", "dbo.TipoDeVehiculo");
            DropForeignKey("dbo.TipoDeVehiculo", "TamañoId", "dbo.Tamaño");
            DropForeignKey("dbo.Parcela", "TamañoId", "dbo.Tamaño");
            DropForeignKey("dbo.Abono", "ParcelaId", "dbo.Parcela");
            DropForeignKey("dbo.Movimiento", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Movimiento", "CuentaId", "dbo.Cuenta");
            DropForeignKey("dbo.Ticket", "AbonoId", "dbo.Abono");
            DropIndex("dbo.TipoDeVehiculo", new[] { "TamañoId" });
            DropIndex("dbo.Parcela", new[] { "TamañoId" });
            DropIndex("dbo.Movimiento", new[] { "TicketId" });
            DropIndex("dbo.Movimiento", new[] { "CuentaId" });
            DropIndex("dbo.Ticket", new[] { "ParcelaId" });
            DropIndex("dbo.Ticket", new[] { "AbonoId" });
            DropIndex("dbo.Ticket", new[] { "VehiculoId" });
            DropIndex("dbo.Vehiculo", new[] { "TipoDeVehiculoId" });
            DropIndex("dbo.Cliente", new[] { "Vehiculo_VehiculoId" });
            DropIndex("dbo.Cuenta", new[] { "ClienteId" });
            DropIndex("dbo.Abono", new[] { "ParcelaId" });
            DropIndex("dbo.Abono", new[] { "CuentaId" });
            DropTable("dbo.TipoDeVehiculo");
            DropTable("dbo.Tamaño");
            DropTable("dbo.Parcela");
            DropTable("dbo.Movimiento");
            DropTable("dbo.Ticket");
            DropTable("dbo.Vehiculo");
            DropTable("dbo.Cliente");
            DropTable("dbo.Cuenta");
            DropTable("dbo.Abono");
        }
    }
}
