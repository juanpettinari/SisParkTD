using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Tamaño
    {
        [Key]
        public int TamañoId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int ValorTamaño { get; set; }

        public virtual ICollection<Parcela> Parcelas { get; set; }

        public virtual ICollection<TipoDeVehiculo> TiposDeVehiculo { get; set; }
    }
}