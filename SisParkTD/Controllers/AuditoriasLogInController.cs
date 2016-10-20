using System;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class AuditoriasLogInController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: AuditoriasLogIn
        public ActionResult Index()
        {
            return View(_db.AuditoriasLogIn.ToList());
        }

        public static void AuditoriaLogIn(Usuario usuario)
        {
            using (var db = new SpContext())
            {
                var logAuditoria = db.AuditoriasLogIn.Create();
                logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
                logAuditoria.Accion = "Log In";
                logAuditoria.FechaYHora = DateTime.Now;
                logAuditoria.Ip = "404";

                db.AuditoriasLogIn.Add(logAuditoria);
                db.SaveChanges();

            }
        }

        public static void AuditoriaLogOut(string userName)
        {
            using (var db = new SpContext())
            {
                var logAuditoria = db.AuditoriasLogIn.Create();
                logAuditoria.NombreDeUsuario = userName;
                logAuditoria.Accion = "Log Out";
                logAuditoria.FechaYHora = DateTime.Now;
                logAuditoria.Ip = "404";

                db.AuditoriasLogIn.Add(logAuditoria);
                db.SaveChanges();

            }
        }

        public static void AuditoriaUsuarioNoExistente(CuentaUsuarioViewModel cuentaUsuario)
        {
            using (var db = new SpContext())
            {
                var logAuditoria = db.AuditoriasLogIn.Create();
                logAuditoria.NombreDeUsuario = cuentaUsuario.NombreDeUsuario;
                logAuditoria.Accion = "Usuario no existente";
                logAuditoria.FechaYHora = DateTime.Now;
                logAuditoria.Ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                db.AuditoriasLogIn.Add(logAuditoria);
                db.SaveChanges();

            }
        }


        public static void AuditoriaContraseniaErronea(Usuario usuario)
        {
            using (var db = new SpContext())
            {
                var logAuditoria = db.AuditoriasLogIn.Create();
                logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
                logAuditoria.Accion = "Contraseña errónea";
                logAuditoria.FechaYHora = DateTime.Now;
                logAuditoria.Ip = "404";

                db.AuditoriasLogIn.Add(logAuditoria);
                db.SaveChanges();

            }
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
