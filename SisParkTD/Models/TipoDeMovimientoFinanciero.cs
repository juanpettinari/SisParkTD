using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models
{
    public class TipoDeMovimientoFinanciero
    {
        [Key]
        public int TipoDeMovimientoFinancieroId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public Signo Signo { get; set; }

        public virtual ICollection<MovimientoFinanciero> MovimientosFinancieros { get; set; }

    }
}