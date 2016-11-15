namespace SisParkTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accion",
                c => new
                    {
                        AccionId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        PaginaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccionId)
                .ForeignKey("dbo.Pagina", t => t.PaginaId)
                .Index(t => t.PaginaId);
            
            CreateTable(
                "dbo.Pagina",
                c => new
                    {
                        PaginaId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PaginaId);
            
            CreateTable(
                "dbo.Permiso",
                c => new
                    {
                        RolId = c.Int(nullable: false),
                        AccionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RolId, t.AccionId })
                .ForeignKey("dbo.Accion", t => t.AccionId)
                .ForeignKey("dbo.Rol", t => t.RolId)
                .Index(t => t.RolId)
                .Index(t => t.AccionId);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.RolUsuario",
                c => new
                    {
                        RolId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RolId, t.UsuarioId });
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Dni = c.String(nullable: false),
                        NombreDeUsuario = c.String(nullable: false),
                        Contrasenia = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permiso", "RolId", "dbo.Rol");
            DropForeignKey("dbo.Permiso", "AccionId", "dbo.Accion");
            DropForeignKey("dbo.Accion", "PaginaId", "dbo.Pagina");
            DropIndex("dbo.Permiso", new[] { "AccionId" });
            DropIndex("dbo.Permiso", new[] { "RolId" });
            DropIndex("dbo.Accion", new[] { "PaginaId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.RolUsuario");
            DropTable("dbo.Rol");
            DropTable("dbo.Permiso");
            DropTable("dbo.Pagina");
            DropTable("dbo.Accion");
        }
    }
}
