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
    public class TamañosController : Controller
    {
        private sisparkdbEntities db = new sisparkdbEntities();

        // GET: Tamaños
        public ActionResult Index()
        {
            return View(db.Tamaños.ToList());
        }

        // GET: Tamaños/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamaños tamaños = db.Tamaños.Find(id);
            if (tamaños == null)
            {
                return HttpNotFound();
            }
            return View(tamaños);
        }

        // GET: Tamaños/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tamaños/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTamaño,NombreTamaño,Valor")] Tamaños tamaños)
        {
            if (ModelState.IsValid)
            {
                db.Tamaños.Add(tamaños);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tamaños);
        }

        // GET: Tamaños/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamaños tamaños = db.Tamaños.Find(id);
            if (tamaños == null)
            {
                return HttpNotFound();
            }
            return View(tamaños);
        }

        // POST: Tamaños/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTamaño,NombreTamaño,Valor")] Tamaños tamaños)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tamaños).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tamaños);
        }

        // GET: Tamaños/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamaños tamaños = db.Tamaños.Find(id);
            if (tamaños == null)
            {
                return HttpNotFound();
            }
            return View(tamaños);
        }

        // POST: Tamaños/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tamaños tamaños = db.Tamaños.Find(id);
            db.Tamaños.Remove(tamaños);
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
