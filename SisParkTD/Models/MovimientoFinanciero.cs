using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class MovimientoFinanciero
    {
        [Key]
        public int MovimientoFinancieroId { get; set; }

        public int TicketId { get; set; }

        public int TipoDeMovimientoFinancieroId { get; set; }

        public virtual TipoDeMovimientoFinanciero TipoDeMovimientoFinanciero { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}