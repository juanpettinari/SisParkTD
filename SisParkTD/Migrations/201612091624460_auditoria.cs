namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auditoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        AuditoriaId = c.Int(nullable: false, identity: true),
                        NombreDeUsuario = c.String(nullable: false),
                        Accion = c.String(nullable: false),
                        FechaYHora = c.DateTime(nullable: false),
                        Ip = c.String(nullable: false),
                        Patente = c.String(),
                        Parcela = c.String(),
                        TipoDeTicket = c.Int(),
                        TiempoTotal = c.String(),
                        Precio = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AuditoriaId);
            
            DropTable("dbo.AuditoriaLogIn");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuditoriaLogIn",
                c => new
                    {
                        AuditoriaLogId = c.Int(nullable: false, identity: true),
                        NombreDeUsuario = c.String(nullable: false),
                        Accion = c.String(nullable: false),
                        FechaYHora = c.DateTime(nullable: false),
                        Ip = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuditoriaLogId);
            
            DropTable("dbo.Auditoria");
        }
    }
}
