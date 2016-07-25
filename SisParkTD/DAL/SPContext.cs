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
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Tamaño> Tamaños { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TipoDeVehiculo> TiposDeVehiculo { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}