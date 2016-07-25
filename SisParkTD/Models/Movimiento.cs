namespace SisParkTD.Models
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }

        public TipoDeMovimiento TipoDeMovimiento { get; set; }

        public int? CuentaId { get; set; }

        public int? TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Cuenta Cuenta { get; set; }
    }

    public enum TipoDeMovimiento
    {
        Credito,
        Debito,
        Pago
    }
}