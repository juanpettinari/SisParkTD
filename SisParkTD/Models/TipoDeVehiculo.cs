using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class TipoDeVehiculo
    {
        [Key]
        public int TipoDeVehiculoId { get; set; }
        [Required]
        public string Nombre { get; set; }

        public TamanioVehiculo TamanioVehiculo { get; set; }
        [Required]
        public decimal TarifaOcasionalDecimal { get; set; }
        [Required]
        public decimal TarifaMensualDecimal { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }

        public virtual ICollection<Parcela> Parcelas { get; set; }

    }
}