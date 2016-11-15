using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Abono
    {
        [Key]
        public int AbonoId { get; set; }
        [DisplayName("Fecha de Inicio")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Fecha de Finalización")]
        public DateTime? FechaFin { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}