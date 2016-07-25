using System;
using System.Collections.Generic;

namespace SisParkTD.Models
{
    public class Abono
    {
        public int AbonoId { get; set; }

        public int CuentaId { get; set; }

        public int ParcelaId { get; set; }

        public DateTime FechaMesInicio { get; set; }

        public DateTime? FechaMesFin { get; set; }

        public virtual Parcela Parcela { get; set; }

        public virtual Cuenta Cuenta { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}