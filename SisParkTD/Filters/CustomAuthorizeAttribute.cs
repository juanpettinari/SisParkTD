using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisParkTD.DAL;

namespace SisParkTD.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (var db = new SpContext())
            {
                var controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];
                var action = (string) httpContext.Request.RequestContext.RouteData.Values["action"];
                var accion = db.Acciones.FirstOrDefault(a => a.Descripcion == action && a.Pagina.Descripcion == controller);
                var username = httpContext.User.Identity.Name;
                var user = db.Usuarios.FirstOrDefault(u => u.NombreDeUsuario == username);
                if (user != null)
                {
                    if (accion != null)
                    {
                        var p = db.Permisos.Find(controller, accion);
                        if (p != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

    }
}