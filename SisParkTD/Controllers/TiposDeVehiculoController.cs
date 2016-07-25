using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TiposDeVehiculoController : Controller
    {
        private readonly sisparkdbEntities _db = new sisparkdbEntities();

        // GET: TiposDeVehiculo
        public ActionResult Index()
        {
            var tiposDeVehiculo = _db.TiposDeVehiculo.Include(t => t.Tamaños);
            return View(tiposDeVehiculo.ToList());
        }

        // GET: TiposDeVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = _db.TiposDeVehiculo.Find(id);
            if (tiposDeVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Create
        public ActionResult Create()
        {
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño");
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
                _db.TiposDeVehiculo.Add(tiposDeVehiculo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = _db.TiposDeVehiculo.Find(id);
            if (tiposDeVehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
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
                _db.Entry(tiposDeVehiculo).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", tiposDeVehiculo.IDTamaño);
            return View(tiposDeVehiculo);
        }

        // GET: TiposDeVehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDeVehiculo tiposDeVehiculo = _db.TiposDeVehiculo.Find(id);
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
            TiposDeVehiculo tiposDeVehiculo = _db.TiposDeVehiculo.Find(id);
            _db.TiposDeVehiculo.Remove(tiposDeVehiculo);
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
