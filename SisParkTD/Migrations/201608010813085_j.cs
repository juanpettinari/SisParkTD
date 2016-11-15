namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class j : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "DniCuit", c => c.String(false));
            DropColumn("dbo.Cliente", "Dni");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "Dni", c => c.String(false));
            DropColumn("dbo.Cliente", "DniCuit");
        }
    }
}
