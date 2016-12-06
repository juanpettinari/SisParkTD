using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using SisParkTD.Common;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SpContext _db = new SpContext();

        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("IngresarVehiculo", "Tickets");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CuentaUsuarioViewModel cuentaUsuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                //The ".FirstOrDefault()" method will return either the first matched
                //result or null
                var miUsuario = _db.Usuarios.
                    FirstOrDefault(u => u.NombreDeUsuario == cuentaUsuarioViewModel.NombreDeUsuario);
                // TODO AGREGAR "ISACTIVE" AL USUARIO
                if (miUsuario != null) //Usuario encontrado
                {
                    if (!PasswordHash.ValidatePassword(cuentaUsuarioViewModel.Contrasenia, miUsuario.Contrasenia))
                    {
                        // TODO ADMIN RESET PASSWORD POR SI NO RECUERDA EL USER LA PASS.
                        ModelState.AddModelError("", "La contraseña es incorrecta, vuelva a escribirla.");
                        AuditoriasLogInController.AuditoriaContraseniaErronea(miUsuario);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(cuentaUsuarioViewModel.NombreDeUsuario, false);
                        AuditoriasLogInController.AuditoriaLogIn(miUsuario);
                        return RedirectToAction("IngresarVehiculo", "Tickets");
                    }
                }
                else // el usuario no fue encontrado
                { 
                    ModelState.AddModelError("", "No existe el usuario: " + cuentaUsuarioViewModel.NombreDeUsuario);
                    AuditoriasLogInController.AuditoriaUsuarioNoExistente(cuentaUsuarioViewModel);
                }
            }
            else
                ModelState.AddModelError("", "Ocurrió un error.");
            return View(cuentaUsuarioViewModel);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            AuditoriasLogInController.AuditoriaLogOut(User.Identity.Name);
            return RedirectToAction("LogIn");
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(_db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Registration()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "UsuarioId,Nombre,Apellido,Telefono,Email,Dni,NombreDeUsuario,Contrasenia")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                if (usuario.Contrasenia != null)
                {
                    if (!_db.Usuarios.Any(u => u.NombreDeUsuario == usuario.NombreDeUsuario))
                    {
                        usuario.Contrasenia = PasswordHash.CreateHash(usuario.Contrasenia);
                        _db.Usuarios.Add(usuario);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "El nombre de usuario ya existe.");
                        return View(usuario);
                    }
                    
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,Nombre,Apellido,Telefono,Email,Dni,NombreDeUsuario,Contrasenia")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(usuario).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = _db.Usuarios.Find(id);
            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
