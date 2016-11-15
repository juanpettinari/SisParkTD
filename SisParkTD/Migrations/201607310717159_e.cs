namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class e : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movimiento", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Movimiento", "TipoDeMovimientoId", "dbo.TipoDeMovimiento");
            DropIndex("dbo.Movimiento", new[] { "TicketId" });
            DropIndex("dbo.Movimiento", new[] { "TipoDeMovimientoId" });
            CreateTable(
                "dbo.MovimientoDeVehiculo",
                c => new
                    {
                        MovimientoDeVehiculoId = c.Int(false, true),
                        Fecha = c.DateTime(false),
                        TipoDeMovimientoDeVehiculo = c.Int(false),
                        TicketId = c.Int(false),
                    })
                .PrimaryKey(t => t.MovimientoDeVehiculoId)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.MovimientoFinanciero",
                c => new
                    {
                        MovimientoFinancieroId = c.Int(false, true),
                        TicketId = c.Int(false),
                        TipoDeMovimientoFinancieroId = c.Int(false),
                    })
                .PrimaryKey(t => t.MovimientoFinancieroId)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .ForeignKey("dbo.TipoDeMovimientoFinanciero", t => t.TipoDeMovimientoFinancieroId)
                .Index(t => t.TicketId)
                .Index(t => t.TipoDeMovimientoFinancieroId);
            
            CreateTable(
                "dbo.TipoDeMovimientoFinanciero",
                c => new
                    {
                        TipoDeMovimientoFinancieroId = c.Int(false, true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Signo = c.Int(false),
                    })
                .PrimaryKey(t => t.TipoDeMovimientoFinancieroId);
            
            AddColumn("dbo.Ticket", "MovimientoDeVehiculoId", c => c.Int(false));
            AddColumn("dbo.Ticket", "TipoDeTicket", c => c.Int(false));
            AddColumn("dbo.Ticket", "FechaYHoraCreacionTicket", c => c.DateTime(false));
            DropColumn("dbo.Ticket", "FechaYHoraDeEntrada");
            DropColumn("dbo.Ticket", "FechaYHoraDeSalida");
            DropTable("dbo.Movimiento");
            DropTable("dbo.TipoDeMovimiento");
        }
        
        public override void Down()
        {
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
                "dbo.Movimiento",
                c => new
                    {
                        MovimientoId = c.Int(false, true),
                        TicketId = c.Int(false),
                        TipoDeMovimientoId = c.Int(false),
                    })
                .PrimaryKey(t => t.MovimientoId);
            
            AddColumn("dbo.Ticket", "FechaYHoraDeSalida", c => c.DateTime());
            AddColumn("dbo.Ticket", "FechaYHoraDeEntrada", c => c.DateTime(false));
            DropForeignKey("dbo.MovimientoFinanciero", "TipoDeMovimientoFinancieroId", "dbo.TipoDeMovimientoFinanciero");
            DropForeignKey("dbo.MovimientoFinanciero", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.MovimientoDeVehiculo", "TicketId", "dbo.Ticket");
            DropIndex("dbo.MovimientoFinanciero", new[] { "TipoDeMovimientoFinancieroId" });
            DropIndex("dbo.MovimientoFinanciero", new[] { "TicketId" });
            DropIndex("dbo.MovimientoDeVehiculo", new[] { "TicketId" });
            DropColumn("dbo.Ticket", "FechaYHoraCreacionTicket");
            DropColumn("dbo.Ticket", "TipoDeTicket");
            DropColumn("dbo.Ticket", "MovimientoDeVehiculoId");
            DropTable("dbo.TipoDeMovimientoFinanciero");
            DropTable("dbo.MovimientoFinanciero");
            DropTable("dbo.MovimientoDeVehiculo");
            CreateIndex("dbo.Movimiento", "TipoDeMovimientoId");
            CreateIndex("dbo.Movimiento", "TicketId");
            AddForeignKey("dbo.Movimiento", "TipoDeMovimientoId", "dbo.TipoDeMovimiento", "TipoDeMovimientoId");
            AddForeignKey("dbo.Movimiento", "TicketId", "dbo.Ticket", "TicketId");
        }
    }
}
