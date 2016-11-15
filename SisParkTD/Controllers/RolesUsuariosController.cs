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
        public ActionResult MostrarRolesParaUsuario(int usuarioId)
        {
            var usuario = _db.Usuarios.Find(usuarioId);
            ViewBag.usuario = usuario.NombreDeUsuario;
            var listaRolUsuarioVM = new List<RolUsuarioViewModel>();
            foreach (var rol in _db.Roles)
            {
                var rolUsuario = new RolUsuarioViewModel
                {
                    Usuario = usuario,
                    Rol = rol,
                    EstaEnElRol = _db.RolesUsuarios.Find(rol.RolId, usuario.UsuarioId) != null
                };
                listaRolUsuarioVM.Add(rolUsuario);
            }


            return View(listaRolUsuarioVM);
        }

        [HttpPost]
        public ActionResult MostrarRolesParaUsuario(List<RolUsuarioViewModel> listaRolUsuarioViewModel)
        {
            foreach (var rolUsuarioNuevo in listaRolUsuarioViewModel)
            {
                var rolUsuarioViejo = _db.RolesUsuarios.Find(rolUsuarioNuevo.Rol.RolId, rolUsuarioNuevo.Usuario.UsuarioId);
                if (rolUsuarioViejo == null) // ANTES NO EXISTÍA EL PAR ROL USUARIO
                {
                    if (rolUsuarioNuevo.EstaEnElRol)
                    {
                        var rolUsuario = new RolUsuario
                        {
                            RolId = rolUsuarioNuevo.Rol.RolId,
                            UsuarioId = rolUsuarioNuevo.Usuario.UsuarioId
                        };
                        _db.RolesUsuarios.Add(rolUsuario);
                        
                    }
                }
                else
                {
                    if (!rolUsuarioNuevo.EstaEnElRol)
                    {
                        _db.RolesUsuarios.Remove(rolUsuarioViejo);
                    }
                }
            }
            _db.SaveChanges();
            var usuarioId = listaRolUsuarioViewModel.FirstOrDefault()?.Usuario.UsuarioId;
            return RedirectToAction("MostrarRolesParaUsuario", new {usuarioId } );
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
            return RedirectToAction("MostrarRolesParaUsuario");
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
