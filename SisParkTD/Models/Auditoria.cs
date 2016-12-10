using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisParkTD.Models
{
    public class Auditoria
    {
        [Key]
        public int AuditoriaId { get; set; }
        [Required]
        public string NombreDeUsuario { get; set; }
        [Required]
        public string Accion { get; set; }
        [Required]
        public DateTime FechaYHora { get; set; }
        [Required]
        public string Ip { get; set; }

        public string Patente { get; set; }

        public string Parcela { get; set; }
        
        public TipoDeTicket? TipoDeTicket { get; set; }

        public string TiempoTotal { get; set; }

        public decimal? Precio { get; set; }
    }
}