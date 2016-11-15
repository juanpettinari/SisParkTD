namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MovimientoFinanciero", new[] { "CuentaId" });
            AlterColumn("dbo.MovimientoFinanciero", "CuentaId", c => c.Int());
            CreateIndex("dbo.MovimientoFinanciero", "CuentaId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MovimientoFinanciero", new[] { "CuentaId" });
            AlterColumn("dbo.MovimientoFinanciero", "CuentaId", c => c.Int(false));
            CreateIndex("dbo.MovimientoFinanciero", "CuentaId");
        }
    }
}
