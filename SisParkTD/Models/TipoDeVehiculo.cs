using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Models
{
    public class TipoDeVehiculo
    {
        public int TipoDeVehiculoId { get; set; }

        public int TamañoId { get; set; }

        public string NombreDeTipoDeVehiculo { get; set; }

        public decimal TarifaOcasionalDecimal { get; set; }

        public decimal TarifaMensualDecimal { get; set; }
    }
}