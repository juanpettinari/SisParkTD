namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ticket", "TiempoTotal", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "TiempoTotal", c => c.Time(precision: 7));
        }
    }
}
