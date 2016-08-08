namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "TipoDocumento", c => c.Int(false));
            AddColumn("dbo.Cliente", "NumeroDocumento", c => c.Int(false));
            DropColumn("dbo.Cliente", "DniCuit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "DniCuit", c => c.String(false));
            DropColumn("dbo.Cliente", "NumeroDocumento");
            DropColumn("dbo.Cliente", "TipoDocumento");
        }
    }
}
