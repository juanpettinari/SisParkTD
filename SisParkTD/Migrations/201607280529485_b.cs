namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Cliente");
            AddColumn("dbo.Cliente", "VehiculoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cliente", "ClienteId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Cliente", "ClienteId");
            CreateIndex("dbo.Cliente", "ClienteId");
            AddForeignKey("dbo.Cliente", "ClienteId", "dbo.Vehiculo", "VehiculoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "ClienteId", "dbo.Vehiculo");
            DropIndex("dbo.Cliente", new[] { "ClienteId" });
            DropPrimaryKey("dbo.Cliente");
            AlterColumn("dbo.Cliente", "ClienteId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Cliente", "VehiculoId");
            AddPrimaryKey("dbo.Cliente", "ClienteId");
        }
    }
}
