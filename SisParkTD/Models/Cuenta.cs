using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisParkTD.Models
{
    public class Cuenta
    {
        [Key, ForeignKey("Cliente")]
        public int CuentaId { get; set; }

        [DataType(DataType.Currency)]
        public decimal SaldoDecimal { get; set; }

        public virtual ICollection<MovimientoFinanciero> MovimientosFinancieros { get; set; }
        [Required]
        public virtual  Cliente Cliente { get; set; }


    }
}