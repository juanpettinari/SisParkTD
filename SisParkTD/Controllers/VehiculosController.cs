using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class VehiculosController : Controller
    {
        private sisparkdbEntities db = new sisparkdbEntities();

        // GET: Vehiculos
        public ActionResult Index()
        {
            var vehiculos = db.Vehiculos.Include(v => v.TiposDeVehiculo);
            return View(vehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }
        //GET
        public ActionResult BuscarExistenciaVehiculo(string patente)
        {

            Vehiculos vehiculo = db.Vehiculos.Where(i => i.Patente == patente).FirstOrDefault();

            var value = Request.UrlReferrer.Segments.Skip(2).Take(1).SingleOrDefault().Trim('/');

            switch (value)
            {
                case "IngresarVehiculo":
                    if (vehiculo == null)
                    {
                        return RedirectToAction("Create", new { patente = patente });
                    }
                    else
                    {
                        return RedirectToAction("BuscarParcela", "Parcelas", vehiculo);
                    }
                case "RetirarVehiculo":
                    return RedirectToAction("LiberarParcela", "Parcelas", vehiculo);
            }
            return View();
        }

        // GET: Vehiculos/Create
        [HttpGet]
        public ActionResult Create(string patente)
        {
            ViewBag.IDTipoDeVehiculo = new SelectList(db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo");
            ViewBag.patente = patente;
            return View();
        }
        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.IDTipoDeVehiculo = new SelectList(db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo");
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVehiculo,IDTipoDeVehiculo,Patente,DescripcionDeVehiculo")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Vehiculos.Add(vehiculos);
                db.SaveChanges();
                if (Request.UrlReferrer.Query != "")
                {
                    return RedirectToAction("BuscarParcela", "Parcelas", vehiculos);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDTipoDeVehiculo = new SelectList(db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTipoDeVehiculo = new SelectList(db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVehiculo,IDTipoDeVehiculo,Patente,DescripcionDeVehiculo")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTipoDeVehiculo = new SelectList(db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
