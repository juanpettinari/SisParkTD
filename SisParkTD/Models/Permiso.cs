using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class Permiso
    {
        [Key, Column(Order = 0)]
        [Required]
        public int RolId { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int AccionId { get; set; }

        public virtual Rol Rol { get; set; }

        public virtual Accion Accion { get; set; }
    }
}