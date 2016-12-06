using System;
using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models.ViewModels
{
    public class FechaViewModel
    {
        [DataType(DataType.Date)]
        public DateTime FechaDesde { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }


    }
}