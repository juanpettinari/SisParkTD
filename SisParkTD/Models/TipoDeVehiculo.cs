using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class TipoDeVehiculo
    {
        [Key]
        public int TipoDeVehiculoId { get; set; }
        [DisplayName("Tipo de Vehículo")]
        [Required]
        public string Nombre { get; set; }
        [DisplayName("Tamaño de Vehiculo")]
        public TamanioVehiculo TamanioVehiculo { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal TarifaOcasionalDecimal { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal TarifaMensualDecimal { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }

        public virtual ICollection<Parcela> Parcelas { get; set; }

    }
}