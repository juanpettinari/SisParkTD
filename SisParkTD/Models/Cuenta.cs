using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }

        public int ClienteId { get; set; }

        [DataType(DataType.Currency)]
        public decimal SaldoDecimal { get; set; }

        public virtual ICollection<Movimiento> Movimientos { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}