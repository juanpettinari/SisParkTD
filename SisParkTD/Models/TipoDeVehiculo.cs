using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class TipoDeVehiculo
    {
        [Key]
        public int TipoDeVehiculoId { get; set; }
        [Required]
        public int TamañoId { get; set; }
        [Required]
        public string NombreDeTipoDeVehiculo { get; set; }
        [Required]
        public decimal TarifaOcasionalDecimal { get; set; }
        [Required]
        public decimal TarifaMensualDecimal { get; set; }

        public virtual Tamaño   Tamaño { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}