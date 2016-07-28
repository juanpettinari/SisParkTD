using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string Telefono { get; set; }
        [DataType(DataType.Currency)]
        public decimal SaldoDecimal { get; set; }

        public int VehiculoId { get; set; }
        [Required]
        public virtual Vehiculo Vehiculo { get; set; }

    }
}