using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Models.ViewModels
{
    public class RolUsuarioViewModel
    {
        public Usuario Usuario { get; set; }

        public Rol Rol { get; set; }

        public bool EstaEnElRol { get; set; }
    }
}