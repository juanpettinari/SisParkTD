using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Models
{
    public class Parcela
    {
        public int ParcelaId { get; set; }

        public int  NumeroParcela  { get; set; }

        public int TamañoId { get; set; }

        public bool Disponible { get; set; }

        public virtual Tamaño Tamaño { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Abono> Abonos { get; set; }

    }
}