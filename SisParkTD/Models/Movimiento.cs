using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Movimiento
    {
        [Key]
        public int MovimientoId { get; set; }
        [Required]
        public TipoDeMovimiento TipoDeMovimiento { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }

    public enum TipoDeMovimiento
    {
        Credito,
        Debito,
        PagoOcasional
    }
}