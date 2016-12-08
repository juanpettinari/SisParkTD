using System.ComponentModel.DataAnnotations;

namespace SisParkTD.Models.ViewModels
{
    public class AniosViewModel
    {
        [Required]
        public int Anio { get; set; }
    }
}