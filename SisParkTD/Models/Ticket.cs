using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int? VehiculoId { get; set; }

        public int? AbonoId { get; set; }

        public int ParcelaId { get; set; }

        public EstadoDeTicket EstadoDeTicket { get; set; }

        public DateTime FechaYHoraDeEntrada { get; set; }
        
        public DateTime? FechaYHoraDeSalida { get; set; }

        public TimeSpan? TiempoTotal { get; set; }
        [DataType(DataType.Currency)]
        public decimal? PrecioTotalDecimal { get; set; }

        public virtual Abono Abono { get; set; }

        public virtual Parcela Parcela { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }

        public virtual ICollection<Movimiento> Movimientos { get; set; }

    }

    public enum EstadoDeTicket
    {
        Ingresado,
        Retirado
    }
}