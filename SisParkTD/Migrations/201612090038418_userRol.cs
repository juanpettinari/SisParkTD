namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "RolId", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "RolId");
            AddForeignKey("dbo.Usuario", "RolId", "dbo.Rol", "RolId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "RolId", "dbo.Rol");
            DropIndex("dbo.Usuario", new[] { "RolId" });
            DropColumn("dbo.Usuario", "RolId");
        }
    }
}
