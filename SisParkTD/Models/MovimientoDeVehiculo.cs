using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class MovimientoDeVehiculo
    {
        [Key]
        public int MovimientoDeVehiculoId { get; set; }

        public DateTime Fecha { get; set; }
        [DisplayName("Tipo")]
        public TipoDeMovimientoDeVehiculo TipoDeMovimientoDeVehiculo { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}