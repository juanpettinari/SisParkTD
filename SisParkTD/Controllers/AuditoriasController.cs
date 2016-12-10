using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;
using SisParkTD.Managers;

namespace SisParkTD.Controllers
{
    public class AuditoriasController : Controller
    {
        private readonly SpContext _db = new SpContext();

        //abstract class Auditoria
        //{
        //    private readonly SpContext _db = new SpContext();

        //    public void Template(Usuario usuario)
        //    {
        //        //var logAuditoria = Create();
        //        //var logAuditoriaCargado = CargarDatos(logAuditoria, usuario);

        //    }


        //    //private Auditoria Create()
        //    //{
        //    //    var logAuditoria = _db.Auditorias.Create();
        //    //    return logAuditoria;
        //    //}

        //    private void CargarDatos()
        //    {

        //    }

        //    public void GuardarEnBd()
        //    {
        //    }



        //}
        // GET: AuditoriasLogIn
        public ActionResult Index()
        {
            return View(_db.Auditorias.ToList());
        }


        #region Template Method Implementation

        /* *** Implementacion del Template Manager *** */

        public ActionResult AuditLogIn()
        {
            //Template Method Class
            var manager = new AuditoriaLogInManager();
            var logAuditoria = manager.Create(null, System.Web.HttpContext.Current);
            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
            return RedirectToAction("IngresarVehiculo", "Tickets");
        }

        public void AuditLogOut(HttpContext contexto)
        {
            //Template Method Class
            var manager = new AuditoriaLogOutManager();
            var logAuditoria = manager.Create(null, contexto);
            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }

        public void AuditTicket(Ticket ticket)
        {
            //Template Method Class
            var manager = new AuditoriaTicketManager();
            //TODO enviar el ticket
            var logAuditoria = manager.Create(ticket, System.Web.HttpContext.Current);
            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }

        #endregion


        public void AuditoriaLogIn(Usuario usuario)
        {
            var logAuditoria = _db.Auditorias.Create();
            logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
            logAuditoria.Accion = "Log In";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }

        public void AuditoriaLogOut(string userName)
        {
            var logAuditoria = _db.Auditorias.Create();
            logAuditoria.NombreDeUsuario = userName;
            logAuditoria.Accion = "Log Out";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }

        public void AuditoriaUsuarioNoExistente(CuentaUsuarioViewModel cuentaUsuario)
        {
            var logAuditoria = _db.Auditorias.Create();
            logAuditoria.NombreDeUsuario = cuentaUsuario.NombreDeUsuario;
            logAuditoria.Accion = "Usuario no existente";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }


        public void AuditoriaContraseniaErronea(Usuario usuario)
        {
            var logAuditoria = _db.Auditorias.Create();
            logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
            logAuditoria.Accion = "Contraseña errónea";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.Auditorias.Add(logAuditoria);
            _db.SaveChanges();
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
