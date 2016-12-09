using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string NombreDeUsuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }
        [Required]
        public int RolId { get; set; }

        public Rol Rol { get; set; }
    }
}