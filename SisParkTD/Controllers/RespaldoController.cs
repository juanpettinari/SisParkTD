using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class RespaldoController : Controller
    {
        [HttpGet]
        public ActionResult LoginBackUp()
        {
            if (Session["loginbackup"] != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public ActionResult LoginBackUp(CuentaUsuarioViewModel cuentaUsuarioViewModel)
        {
            if (cuentaUsuarioViewModel.NombreDeUsuario == "admin" && cuentaUsuarioViewModel.Contrasenia == "admin")
            {
                Session["loginbackup"] = "yes";
            }
            else
            {
                ViewBag.ErrorMessage = "Error en el usuario y/o contraseña";
            }
            return RedirectToAction("Index");
        }


        // GET: Respaldo
        public ActionResult Index()
        {
            if (Session["loginbackup"] != null)
            {
                string[] extensions = { ".bak" };
                var files = Directory.EnumerateFiles(@"D:\Juani\Drive\PROYECTOS\SisParkTD\back-up").Where(s => extensions.Any(ext => ext == Path.GetExtension(s)));
                // TODO AGREGAR LA FECHA DE ULTIMA MODIFICACIÓN DE ARCHIVO.
                return View(files);
            }
            return RedirectToAction("LoginBackUp");
        }


        public ActionResult RealizarBackUp()
        {
            if (Session["loginbackup"] != null)
            {
                var now = DateTime.Now;
                var dbPath = @"D:\Juani\Drive\PROYECTOS\SisParkTD\back-up\backup" + "_" + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".bak";
                using (var db = new SpContext())
                {

                    var cmd = $"BACKUP DATABASE SPContext TO DISK=\'{dbPath}\' WITH FORMAT, MEDIANAME=\'DbBackUps\', MEDIADESCRIPTION=\'Media set for {dbPath} database\';";
                    db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginBackUp");
        }

        public ActionResult RealizarRestore(string filePath, string asd)
        {
            if (Session["loginbackup"] != null)
            {
                using (var db = new SpContext())
                {
                    var cmd = $"USE master restore DATABASE SPContext from DISK=\'{filePath}\' WITH REPLACE;";
                    db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("LoginBackUp");
        }


    }
}