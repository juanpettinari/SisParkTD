namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "NumeroDocumento", c => c.String(false, 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "NumeroDocumento", c => c.Int(false));
        }
    }
}
