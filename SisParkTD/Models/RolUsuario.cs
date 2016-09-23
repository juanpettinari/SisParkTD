using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class RolUsuario
    {
        [Key, Column(Order = 0)]
        public int RolId { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int UsuarioId { get; set; }
    }
}