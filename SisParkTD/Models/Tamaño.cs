using System.Collections.Generic;

namespace SisParkTD.Models
{
    public class Tamaño
    {
        public int TamañoId { get; set; }

        public string Descripcion { get; set; }

        public int ValorTamaño { get; set; }

        public virtual ICollection<Parcela> Parcelas { get; set; }

        public virtual ICollection<TipoDeVehiculo> TiposDeVehiculo { get; set; }
    }
}