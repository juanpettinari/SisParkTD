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
    public class ParcelasController : Controller
    {
        private sisparkdbEntities db = new sisparkdbEntities();


        //GET
        public ActionResult BuscarParcela(Vehiculos vehiculo)
        {
            var ParcelaMasGrande = db.Parcelas.Max(item => item.Tamaños.Valor);

            for (int i = 0; i == ParcelaMasGrande; i++)
            {
                var parcela = db.Parcelas.Where(item => item.Tamaños.Valor == vehiculo.TiposDeVehiculo.Tamaños.Valor && item.Disponible == true).FirstOrDefault();

                if (parcela != null)
                {
                    parcela.Disponible = false;
                    db.Entry(parcela).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ConfirmarIngreso", "Tickets", new { Parcelas = parcela , Vehiculos = vehiculo});
                }
            }

            return RedirectToAction("NoHayParcelas", "Tickets", new { vehiculo});







        }






        //POST



        // GET: Parcelas
        public ActionResult Index()
        {
            var parcelas = db.Parcelas.Include(p => p.Tamaños);
            return View(parcelas.ToList());
        }

        // GET: Parcelas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = db.Parcelas.Find(id);
            if (parcelas == null)
            {
                return HttpNotFound();
            }
            return View(parcelas);
        }

        // GET: Parcelas/Create
        public ActionResult Create()
        {
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño");
            return View();
        }

        // POST: Parcelas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDParcela,NumeroParcela,Disponible,IDTamaño")] Parcelas parcelas)
        {
            if (ModelState.IsValid)
            {
                db.Parcelas.Add(parcelas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
            return View(parcelas);
        }

        // GET: Parcelas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = db.Parcelas.Find(id);
            if (parcelas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
            return View(parcelas);
        }

        // POST: Parcelas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDParcela,NumeroParcela,Disponible,IDTamaño")] Parcelas parcelas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parcelas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTamaño = new SelectList(db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
            return View(parcelas);
        }

        // GET: Parcelas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = db.Parcelas.Find(id);
            if (parcelas == null)
            {
                return HttpNotFound();
            }
            return View(parcelas);
        }

        // POST: Parcelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parcelas parcelas = db.Parcelas.Find(id);
            db.Parcelas.Remove(parcelas);
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
