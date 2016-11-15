using System;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class MovimientoFinanciero
    {
        [Key]
        public int MovimientoFinancieroId { get; set; }

        public int TicketId { get; set; }

        public int TipoDeMovimientoFinancieroId { get; set; }

        public int? CuentaId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public virtual Cuenta   Cuenta { get; set; }

        public virtual TipoDeMovimientoFinanciero TipoDeMovimientoFinanciero { get; set; }

        public virtual Ticket Ticket { get; set; }
    }

    public enum TipoDeMovimientoFinancieroenum
    {
        FacturacionAbono = 1,
        PagoAbono,
        PagoOcasional
    }
}