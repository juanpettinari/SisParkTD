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
                        AbonoId = c.Int(false, true),
                        FechaMesInicio = c.DateTime(false),
                        FechaMesFin = c.DateTime(),
                    })
                .PrimaryKey(t => t.AbonoId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(false, true),
                        VehiculoId = c.Int(false),
                        AbonoId = c.Int(),
                        ParcelaId = c.Int(false),
                        EstadoDeTicket = c.Int(false),
                        FechaYHoraDeEntrada = c.DateTime(false),
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
                        MovimientoId = c.Int(false, true),
                        TicketId = c.Int(false),
                        TipoDeMovimientoId = c.Int(false),
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
                        TipoDeMovimientoId = c.Int(false, true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Signo = c.Int(false),
                    })
                .PrimaryKey(t => t.TipoDeMovimientoId);
            
            CreateTable(
                "dbo.Parcela",
                c => new
                    {
                        ParcelaId = c.Int(false, true),
                        NumeroParcela = c.Int(false),
                        Disponible = c.Boolean(false),
                        TipoDeVehiculoId = c.Int(false),
                    })
                .PrimaryKey(t => t.ParcelaId)
                .ForeignKey("dbo.TipoDeVehiculo", t => t.TipoDeVehiculoId)
                .Index(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.TipoDeVehiculo",
                c => new
                    {
                        TipoDeVehiculoId = c.Int(false, true),
                        Nombre = c.String(false),
                        TamanioVehiculo = c.Int(false),
                        TarifaOcasionalDecimal = c.Decimal(false, 18, 2),
                        TarifaMensualDecimal = c.Decimal(false, 18, 2),
                    })
                .PrimaryKey(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.Vehiculo",
                c => new
                    {
                        VehiculoId = c.Int(false, true),
                        Patente = c.String(false),
                        TipoDeVehiculoId = c.Int(false),
                        DescripcionDeVehiculo = c.String(),
                    })
                .PrimaryKey(t => t.VehiculoId)
                .ForeignKey("dbo.TipoDeVehiculo", t => t.TipoDeVehiculoId)
                .Index(t => t.TipoDeVehiculoId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(false),
                        Nombre = c.String(false),
                        Apellido = c.String(false),
                        Dni = c.String(false),
                        Telefono = c.String(false),
                        SaldoDecimal = c.Decimal(false, 18, 2),
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
