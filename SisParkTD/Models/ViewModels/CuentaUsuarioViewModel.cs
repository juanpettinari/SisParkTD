using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisParkTD.Models.ViewModels
{
    public class CuentaUsuarioViewModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }
    }

}