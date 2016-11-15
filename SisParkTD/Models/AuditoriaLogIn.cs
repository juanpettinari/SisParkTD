using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisParkTD.Models
{
    public class AuditoriaLogIn
    {
        [Key]
        public int AuditoriaLogId { get; set; }
        [Required]
        public string NombreDeUsuario { get; set; }
        [Required]
        public string Accion { get; set; }
        [Required]
        public DateTime FechaYHora { get; set; }
        [Required]
        public string Ip { get; set; }

        
    }
}