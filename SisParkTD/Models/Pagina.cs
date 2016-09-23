using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Pagina
    {
        [Key]
        public int PaginaId { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public virtual ICollection<Accion> Acciones { get; set; }
    }
}