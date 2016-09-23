using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Accion
    {
        [Key]
        public int AccionId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int PaginaId { get; set; }

        public virtual Pagina Pagina { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}