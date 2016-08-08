namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ticket", "MovimientoDeVehiculoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ticket", "MovimientoDeVehiculoId", c => c.Int(false));
        }
    }
}
