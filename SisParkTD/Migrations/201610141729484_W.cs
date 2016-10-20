namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class W : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AuditoriaLogIn", "TipoDeUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditoriaLogIn", "TipoDeUsuario", c => c.String(nullable: false));
        }
    }
}
