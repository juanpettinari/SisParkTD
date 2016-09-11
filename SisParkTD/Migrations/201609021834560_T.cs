namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class T : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abono", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Abono", "Activo");
        }
    }
}
