namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abono",
                c => new
                    {
                        AbonoId = c.Int(nullable: false, identity: true),
                        FechaMesInicio = c.DateTime(nullable: false),
                        FechaMesFin = c.DateTime(),
                    })
                .PrimaryKey(t => t.AbonoId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        VehiculoId = c.Int(nullable: false),
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
                        TicketId = c.Int(nullable: false),
                        TipoDeMovimientoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovimientoId)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .ForeignKey("dbo.TipoDeMovimiento", t => t.TipoDeMovimientoId)
                .Index(t => t.TicketId)
                .Index(t => t.TipoDeMovimientoId);
            
            CreateTable(
                "dbo.TipoDeMovimiento",
                c => new
                    {
                        TipoDeMovimientoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Signo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoDeMovimientoId);
            
            CreateTable(
                "dbo.Parcela",
                c => new
                    {
                        ParcelaId = c.Int(nullable: false, identity: true),
                        NumeroParcela = c.Int(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                        TipoDeVehiculoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParcelaId)
                .ForeignKey("dbo.TipoDeVehiculo", t => t.TipoDeVehiculoId)
                .Index(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.TipoDeVehiculo",
                c => new
                    {
                        TipoDeVehiculoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        TamanioVehiculo = c.Int(nullable: false),
                        TarifaOcasionalDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TarifaMensualDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.Vehiculo",
                c => new
                    {
                        VehiculoId = c.Int(nullable: false, identity: true),
                        Patente = c.String(nullable: false),
                        TipoDeVehiculoId = c.Int(nullable: false),
                        DescripcionDeVehiculo = c.String(),
                    })
                .PrimaryKey(t => t.VehiculoId)
                .ForeignKey("dbo.TipoDeVehiculo", t => t.TipoDeVehiculoId)
                .Index(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Dni = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        SaldoDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Vehiculo", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehiculo", "TipoDeVehiculoId", "dbo.TipoDeVehiculo");
            DropForeignKey("dbo.Ticket", "VehiculoId", "dbo.Vehiculo");
            DropForeignKey("dbo.Cliente", "ClienteId", "dbo.Vehiculo");
            DropForeignKey("dbo.Parcela", "TipoDeVehiculoId", "dbo.TipoDeVehiculo");
            DropForeignKey("dbo.Ticket", "ParcelaId", "dbo.Parcela");
            DropForeignKey("dbo.Movimiento", "TipoDeMovimientoId", "dbo.TipoDeMovimiento");
            DropForeignKey("dbo.Movimiento", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Ticket", "AbonoId", "dbo.Abono");
            DropIndex("dbo.Cliente", new[] { "ClienteId" });
            DropIndex("dbo.Vehiculo", new[] { "TipoDeVehiculoId" });
            DropIndex("dbo.Parcela", new[] { "TipoDeVehiculoId" });
            DropIndex("dbo.Movimiento", new[] { "TipoDeMovimientoId" });
            DropIndex("dbo.Movimiento", new[] { "TicketId" });
            DropIndex("dbo.Ticket", new[] { "ParcelaId" });
            DropIndex("dbo.Ticket", new[] { "AbonoId" });
            DropIndex("dbo.Ticket", new[] { "VehiculoId" });
            DropTable("dbo.Cliente");
            DropTable("dbo.Vehiculo");
            DropTable("dbo.TipoDeVehiculo");
            DropTable("dbo.Parcela");
            DropTable("dbo.TipoDeMovimiento");
            DropTable("dbo.Movimiento");
            DropTable("dbo.Ticket");
            DropTable("dbo.Abono");
        }
    }
}
