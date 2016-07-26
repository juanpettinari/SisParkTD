using System.ComponentModel.DataAnnotations;

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

        public virtual Vehiculo Vehiculo { get; set; }
    }
}