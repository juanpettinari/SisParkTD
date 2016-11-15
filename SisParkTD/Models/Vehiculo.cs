using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }
        [StringLength(7, MinimumLength = 6, ErrorMessage = "Debe ingresar una {0} de {2} a {1} caracteres")]
        [Required]
        public string Patente { get; set; }

        
        [Required]
        public int TipoDeVehiculoId { get; set; }
        [DisplayName("Descripción de Vehículo")]
        public string DescripcionDeVehiculo { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual TipoDeVehiculo TipoDeVehiculo { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}