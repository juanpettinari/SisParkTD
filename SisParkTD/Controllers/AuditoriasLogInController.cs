using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class AuditoriasLogInController : Controller
    {
        private readonly SpContext _db = new SpContext();

        abstract class Auditoria
        {
            private readonly SpContext _db = new SpContext();

            public void Template(Usuario usuario)
            {
                var logAuditoria = Create();
                //var logAuditoriaCargado = CargarDatos(logAuditoria, usuario);

            }


            private AuditoriaLogIn Create()
            {
                var logAuditoria = _db.AuditoriasLogIn.Create();
                return logAuditoria;
            }

            private void CargarDatos()
            {

            }

            public void GuardarEnBd()
            {
            }



        }
        // GET: AuditoriasLogIn
        public ActionResult Index()
        {
            return View(_db.AuditoriasLogIn.ToList());
        }

        public void AuditoriaLogIn(Usuario usuario)
        {
            var logAuditoria = _db.AuditoriasLogIn.Create();
            logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
            logAuditoria.Accion = "Log In";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.AuditoriasLogIn.Add(logAuditoria);
            _db.SaveChanges();
        }

        public void AuditoriaLogOut(string userName)
        {
            var logAuditoria = _db.AuditoriasLogIn.Create();
            logAuditoria.NombreDeUsuario = userName;
            logAuditoria.Accion = "Log Out";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.AuditoriasLogIn.Add(logAuditoria);
            _db.SaveChanges();
        }

        public void AuditoriaUsuarioNoExistente(CuentaUsuarioViewModel cuentaUsuario)
        {
            var logAuditoria = _db.AuditoriasLogIn.Create();
            logAuditoria.NombreDeUsuario = cuentaUsuario.NombreDeUsuario;
            logAuditoria.Accion = "Usuario no existente";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            _db.AuditoriasLogIn.Add(logAuditoria);
            _db.SaveChanges();
        }


        public void AuditoriaContraseniaErronea(Usuario usuario)
        {
            var logAuditoria = _db.AuditoriasLogIn.Create();
            logAuditoria.NombreDeUsuario = usuario.NombreDeUsuario;
            logAuditoria.Accion = "Contraseña errónea";
            logAuditoria.FechaYHora = DateTime.Now;
            logAuditoria.Ip = "404";

            _db.AuditoriasLogIn.Add(logAuditoria);
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


    abstract class Auditoria
    {
        public virtual void Create()
        {
        }

    }
}
