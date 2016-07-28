using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }
        [Required]
        public string Patente { get; set; }

        
        [Required]
        public int TipoDeVehiculoId { get; set; }
        
        public string DescripcionDeVehiculo { get; set; }


        public virtual Cliente Cliente { get; set; }

        public virtual TipoDeVehiculo TipoDeVehiculo { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}