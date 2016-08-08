namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class q : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientoFinanciero", "Fecha", c => c.DateTime(false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientoFinanciero", "Fecha");
        }
    }
}
