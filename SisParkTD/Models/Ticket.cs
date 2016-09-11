using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int VehiculoId { get; set; }
        [DisplayName("Abono")]
        public int? AbonoId { get; set; }
        [Required]
        public int ParcelaId { get; set; }
        [Required]
        public EstadoDeTicket EstadoDeTicket { get; set; }
        [DisplayName("Tipo de Ticket")]
        public TipoDeTicket TipoDeTicket { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraCreacionTicket { get; set; }

        public int? TiempoTotal { get; set; }
        [DisplayName("Precio")]
        [DataType(DataType.Currency)]
        public decimal? PrecioTotalDecimal { get; set; }

        public bool Pagado { get; set; }

        public virtual Abono Abono { get; set; }

        public virtual Parcela Parcela { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }

        public virtual ICollection<MovimientoFinanciero> MovimientosFinancieros { get; set; }

        public virtual ICollection<MovimientoDeVehiculo> MovimientosDeVehiculo { get; set; }

    }
}