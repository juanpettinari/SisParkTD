namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class k : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abono", "FechaInicio", c => c.DateTime(false));
            AddColumn("dbo.Abono", "FechaFin", c => c.DateTime());
            DropColumn("dbo.Abono", "FechaMesInicio");
            DropColumn("dbo.Abono", "FechaMesFin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abono", "FechaMesFin", c => c.DateTime());
            AddColumn("dbo.Abono", "FechaMesInicio", c => c.DateTime(false));
            DropColumn("dbo.Abono", "FechaFin");
            DropColumn("dbo.Abono", "FechaInicio");
        }
    }
}
