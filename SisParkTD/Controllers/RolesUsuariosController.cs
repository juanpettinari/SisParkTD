using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;
using static System.String;

namespace SisParkTD.Controllers
{
    public class RolesUsuariosController : Controller
    {
        private readonly SpContext _db = new SpContext();
        /*

            var movimientos = from m in _db.MovimientosDeVehiculo
                select m;
                
            if (!IsNullOrEmpty(ddPatente))
            {
                movimientos = movimientos.Where(m => m.Ticket.Vehiculo.Patente == ddPatente);
            }

            if (!IsNullOrEmpty(ddTipoMovimiento))
            {
                movimientos = movimientos.Where(m => m.TipoDeMovimientoDeVehiculo.ToString() == ddTipoMovimiento);
            }
            */

        // GET: RolesUsuarios
        public ActionResult Index(string ddUsuario, string ddRol)
        {
            var listaRolUsuarioVM = new List<RolUsuarioViewModel>();
            foreach (var rol in _db.Roles)
            {
                foreach (var usuario in _db.Usuarios)
                {
                    var usuarioRol = new RolUsuarioViewModel
                    {
                        Usuario = usuario,
                        Rol = rol,
                        EstaEnElRol = _db.RolesUsuarios.Find(rol.RolId, usuario.UsuarioId) != null
                    };

                    listaRolUsuarioVM.Add(usuarioRol);
                }
            }

            var listaUsuario = new List<string>();

            var consultaUsuario = from m in _db.Usuarios
                                  orderby m.NombreDeUsuario
                                  select m.NombreDeUsuario;
            listaUsuario.AddRange(consultaUsuario.Distinct());
            ViewBag.ddUsuario = new SelectList(listaUsuario);

            var listaRol = new List<string>();

            var consultaRol = from m in _db.Roles
                              orderby m.Descripcion
                              select m.Descripcion;
            listaRol.AddRange(consultaRol.Distinct());
            ViewBag.ddRol = new SelectList(listaRol);

            var usuariosRoles = from ur in listaRolUsuarioVM
                                select ur;

            if (!IsNullOrEmpty(ddUsuario))
            {
                usuariosRoles = usuariosRoles.Where(ur => ur.Usuario.NombreDeUsuario == ddUsuario);
            }

            if (!IsNullOrEmpty(ddRol))
            {
                usuariosRoles = usuariosRoles.Where(ur => ur.Rol.Descripcion == ddRol);
            }
            usuariosRoles = usuariosRoles.OrderByDescending(ur => ur.EstaEnElRol);
            return View(usuariosRoles);
        }


        // GET: RolesUsuarios/Delete/5
        public ActionResult CambiarRolUsuario(int usuarioId, int rolId)
        {
            var rolUsuario = _db.RolesUsuarios.Find(rolId, usuarioId);
            if (rolUsuario != null)
            {
                _db.RolesUsuarios.Remove(rolUsuario);

            }
            else
            {
                rolUsuario = new RolUsuario
                {
                    RolId = rolId,
                    UsuarioId = usuarioId
                };
                _db.RolesUsuarios.Add(rolUsuario);
            }
            _db.SaveChanges();
            var urlPrevia = Request.UrlReferrer?.ToString();
            return Redirect(urlPrevia);
        }

        // POST: RolesUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rolUsuario = _db.RolesUsuarios.Find(id);
            _db.RolesUsuarios.Remove(rolUsuario);
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
