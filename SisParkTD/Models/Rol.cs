using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }

    }
}