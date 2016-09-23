using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SisParkTD.Models;

namespace SisParkTD.DAL
{
    public class SpContext : DbContext
    {
        public SpContext() : base("SPContext")
        {
        }

        

        public DbSet<Abono> Abonos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<MovimientoFinanciero> MovimientosFinancieros { get; set; }
        public DbSet<TipoDeMovimientoFinanciero> TiposDeMovimientoFinanciero { get; set; }
        
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<MovimientoDeVehiculo> MovimientosDeVehiculo { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<TipoDeVehiculo> TiposDeVehiculo { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Accion> Acciones { get; set; }
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolUsuario> RolesUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


        }
    }
}