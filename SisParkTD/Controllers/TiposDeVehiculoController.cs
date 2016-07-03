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
    public class TiposDeVehiculoController : Controller
    {
        private sisparkdbEntities db = new sisparkdbEntities();

        // GET: TiposDeVehiculo
        public ActionResult Index()
        {
            var tiposDeVehiculo = db.TiposDeVehiculo.Include(t => t.Tamaños);
            return View(tiposDeVehiculo.ToList());
        }

        // GET: TiposDeVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = db.TiposDeVehiculo.Find(id);
            if (tiposDeVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Create
        public ActionResult Create()
        {
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño");
            return View();
        }

        // POST: TiposDeVehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoDeVehiculo,IDTamaño,NombreTipoDeVehiculo,Tarifa")] TiposDeVehiculo tiposDeVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.TiposDeVehiculo.Add(tiposDeVehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = db.TiposDeVehiculo.Find(id);
            if (tiposDeVehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
            return View(tiposDeVehiculo);
        }

        // POST: TiposDeVehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTipoDeVehiculo,IDTamaño,NombreTipoDeVehiculo,Tarifa")] TiposDeVehiculo tiposDeVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposDeVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = db.TiposDeVehiculo.Find(id);
            if (tiposDeVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tiposDeVehiculo);
        }

        // POST: TiposDeVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposDeVehiculo tiposDeVehiculo = db.TiposDeVehiculo.Find(id);
            db.TiposDeVehiculo.Remove(tiposDeVehiculo);
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
