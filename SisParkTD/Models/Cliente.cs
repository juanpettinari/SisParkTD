using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }
    }
}