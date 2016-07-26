using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Parcela
    {
        [Key]
        public int ParcelaId { get; set; }
        [Required]
        public int  NumeroParcela  { get; set; }
        [Required]
        public int TamañoId { get; set; }
        [Required]
        public bool Disponible { get; set; }

        public virtual Tamaño Tamaño { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Abono> Abonos { get; set; }

    }
}