namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuenta",
                c => new
                    {
                        CuentaId = c.Int(false),
                        SaldoDecimal = c.Decimal(false, 18, 2),
                    })
                .PrimaryKey(t => t.CuentaId)
                .ForeignKey("dbo.Cliente", t => t.CuentaId)
                .Index(t => t.CuentaId);
            
            AddColumn("dbo.MovimientoFinanciero", "CuentaId", c => c.Int(false));
            AddColumn("dbo.Cliente", "RazonSocial", c => c.String(false));
            CreateIndex("dbo.MovimientoFinanciero", "CuentaId");
            AddForeignKey("dbo.MovimientoFinanciero", "CuentaId", "dbo.Cuenta", "CuentaId");
            DropColumn("dbo.Cliente", "Nombre");
            DropColumn("dbo.Cliente", "Apellido");
            DropColumn("dbo.Cliente", "SaldoDecimal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "SaldoDecimal", c => c.Decimal(false, 18, 2));
            AddColumn("dbo.Cliente", "Apellido", c => c.String(false));
            AddColumn("dbo.Cliente", "Nombre", c => c.String(false));
            DropForeignKey("dbo.MovimientoFinanciero", "CuentaId", "dbo.Cuenta");
            DropForeignKey("dbo.Cuenta", "CuentaId", "dbo.Cliente");
            DropIndex("dbo.Cuenta", new[] { "CuentaId" });
            DropIndex("dbo.MovimientoFinanciero", new[] { "CuentaId" });
            DropColumn("dbo.Cliente", "RazonSocial");
            DropColumn("dbo.MovimientoFinanciero", "CuentaId");
            DropTable("dbo.Cuenta");
        }
    }
}
