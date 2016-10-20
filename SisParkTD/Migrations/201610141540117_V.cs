namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditoriaLogIn",
                c => new
                    {
                        AuditoriaLogId = c.Int(nullable: false, identity: true),
                        TipoDeUsuario = c.String(nullable: false),
                        NombreDeUsuario = c.String(nullable: false),
                        Accion = c.String(nullable: false),
                        FechaYHora = c.DateTime(nullable: false),
                        Ip = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuditoriaLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditoriaLogIn");
        }
    }
}
