using System.Collections.Generic;

namespace SisParkTD.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }

        public string Patente { get; set; }

        public int? ClienteId { get; set; }

        public int TipoDeVehiculoId { get; set; }
        
        public string DescripcionDeVehiculo { get; set; }

        public virtual TipoDeVehiculo TipoDeVehiculo { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}