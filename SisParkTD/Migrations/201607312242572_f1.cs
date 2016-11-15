namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class f1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "Pagado", c => c.Boolean(false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "Pagado");
        }
    }
}
