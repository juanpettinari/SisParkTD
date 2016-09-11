using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class Cliente
    {
        [Key, ForeignKey("Vehiculo")]
        public int ClienteId { get; set; }
        [DisplayName("Razón Social")]
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public TipoDocumento? TipoDocumento { get; set; }
        [StringLength(11, MinimumLength = 7, ErrorMessage = "Ingrese un {0} que tenga entre {2} y {1} numeros")]
        [Required]
        public string NumeroDocumento { get; set; }
        [Required]
        public string Telefono { get; set; }

        [Required]
        public virtual Vehiculo Vehiculo { get; set; }

        public virtual Cuenta Cuenta { get; set; }
    }
}