using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;
using static System.String;

namespace SisParkTD.Controllers
{
    public class PermisosController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: Permisos
        public ActionResult Index(string ddRol, string ddPagina, string ddAccion)
        {

            var listaPermisosVM = new List<PermisosViewModel>();
            foreach (var rol in _db.Roles)
            {
                foreach (var accion in _db.Acciones)
                {
                    var permiso = new PermisosViewModel
                    {
                        Accion = accion,
                        Rol = rol,
                        Permiso = _db.Permisos.Find(rol.RolId, accion.AccionId) != null
                    };

                    listaPermisosVM.Add(permiso);
                }
            }

            var listaRol = new List<string>();

            var consultaRol = from m in _db.Roles
                              orderby m.Descripcion
                              select m.Descripcion;
            listaRol.AddRange(consultaRol.Distinct());
            ViewBag.ddRol = new SelectList(listaRol);

            var listaPagina = new List<string>();

            var consultaPagina = from p in _db.Paginas
                                 orderby p.Descripcion
                                 select p.Descripcion;
            listaPagina.AddRange(consultaPagina.Distinct());
            ViewBag.ddPagina = new SelectList(listaPagina);

            var listaAccion = new List<string>();

            var consultaAccion = from a in _db.Acciones
                                 orderby a.Descripcion
                                 select a.Descripcion;
            listaAccion.AddRange(consultaAccion.Distinct());
            ViewBag.ddAccion = new SelectList(listaAccion);


            var permisos = from p in listaPermisosVM
                           select p;

            if (!IsNullOrEmpty(ddRol))
            {
                permisos = permisos.Where(p => p.Rol.Descripcion == ddRol);
            }
            if (!IsNullOrEmpty(ddPagina))
            {
                permisos = permisos.Where(p => p.Accion.Pagina.Descripcion == ddPagina);
            }
            if (!IsNullOrEmpty(ddAccion))
            {
                permisos = permisos.Where(p => p.Accion.Descripcion == ddAccion);
            }

            permisos = permisos.OrderByDescending(p => p.Permiso);
            return View(permisos);
        }


        public ActionResult DarTodosLosPermisos(int rolId)
        {
            var listaPermisos = new List<Permiso>();

            foreach (var accion in _db.Acciones)
            {
                var permiso = new Permiso
                {
                    AccionId = accion.AccionId,
                    RolId = rolId
                };

                listaPermisos.Add(permiso);

                _db.SaveChanges();
            }
            _db.Permisos.AddRange(listaPermisos);
            _db.SaveChanges();

            // TODO CAMBIAR A "GESTIONAR PERMISOS, con REDIRECT CREO, CON EL ID DE ROL MNADAR"

            return View("Index");
        }

        //public ActionResult QuitarTodosLosPermisos(int rolId)
        //{
        //    return View();
        //}



        //public ActionResult GestionarPermiso(int rolId)
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult GestionarPermiso(int rolId)
        {
            return View();
        }


        public ActionResult CambiarPermiso(int rolId, int accionId)
        {
            var permiso = _db.Permisos.Find(rolId, accionId);
            if (permiso != null)
            {
                _db.Permisos.Remove(permiso);
            }
            else
            {
                permiso = new Permiso
                {
                    AccionId = accionId,
                    RolId = rolId
                };
                _db.Permisos.Add(permiso);
            }
            _db.SaveChanges();

            var urlPrevia = Request.UrlReferrer?.ToString();
            return Redirect(urlPrevia);
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
