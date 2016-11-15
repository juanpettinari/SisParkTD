using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models.ViewModels
{
    public class AbonoViewModel
    {
        [StringLength(7, MinimumLength = 6, ErrorMessage = "Debe ingresar una patente de {0} a {1} caracteres")]
        [Required]
        public string Patente { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public TipoDocumento? TipoDocumento { get; set; }
        [StringLength(11, MinimumLength = 7, ErrorMessage = "Ingrese un {0} que tenga entre {2} y {1} numeros")]
        [Required]
        public string NumeroDocumento { get; set; }
        [Required]
        public string Telefono { get; set; }
        [DisplayName("Mes")]
        [Required]
        public Mes? MesFin { get; set; }
        [DisplayName("Año")]
        [Required]
        [Range(2016,2030)]
        public int AnioFin { get; set; }




    }

}