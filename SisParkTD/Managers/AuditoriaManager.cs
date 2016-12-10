using System;
using System.Web;
using SisParkTD.Common;
using SisParkTD.Models;

namespace SisParkTD.Managers
{
    public interface IAuditoriaManager
    {
        Auditoria Create(Ticket tkt, HttpContext contexto);
    }

    public class AuditoriaTicketManager : IAuditoriaManager
    {
        public Auditoria Create(Ticket tkt, HttpContext contexto)
        {
            var model = new Auditoria
            {
                NombreDeUsuario = contexto.User.Identity.Name,
                FechaYHora = DateTime.Now,
                Ip = contexto.Request.UserHostAddress,
                Patente = tkt.Vehiculo.Patente,
                Parcela = tkt.Parcela.NumeroParcela.ToString(),
                TipoDeTicket = tkt.TipoDeTicket
            };

            if (tkt.TipoDeTicket == TipoDeTicket.Ocasional)
            {
                if (tkt.EstadoDeTicket == EstadoDeTicket.Activo)
                {
                    model.Accion = "Ingreso Ticket Ocasional";
                }
                else
                {
                    model.Accion = "Egreso Ticket Ocasional";
                    model.TiempoTotal = tkt.TiempoTotal.ToString();
                    model.Precio = tkt.PrecioTotalDecimal;
                }
            }
            else
            {
                model.Accion = tkt.EstadoDeTicket == EstadoDeTicket.Activo ? "Ingreso Abonado" : "Egreso Abonado";
            }
            return model;
        }
    }

    public class AuditoriaLogInManager : IAuditoriaManager
    {
        public Auditoria Create(Ticket tkt, HttpContext contexto)
        {
            var model = new Auditoria
            {
                NombreDeUsuario = contexto.User.Identity.Name,
                Accion = "LogIn",
                FechaYHora = DateTime.Now,
                Ip = contexto.Request.UserHostAddress
            };

            return model;
        }
    }
    public class AuditoriaLogOutManager : IAuditoriaManager
    {
        public Auditoria Create(Ticket tkt, HttpContext contexto)
        {
            var model = new Auditoria
            {
                NombreDeUsuario = contexto.User.Identity.Name,
                Accion = "LogOut",
                FechaYHora = DateTime.Now,
                Ip = contexto.Request.UserHostAddress
            };


            return model;
        }
    }

}