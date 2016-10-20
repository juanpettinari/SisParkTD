using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        [DisplayName("Rol")]
        [Required]
        public string Descripcion { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }

    }
}