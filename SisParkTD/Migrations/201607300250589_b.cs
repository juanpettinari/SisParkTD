namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehiculo", "Patente", c => c.String(false, 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehiculo", "Patente", c => c.String(false));
        }
    }
}
