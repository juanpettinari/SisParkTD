using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class ParcelasController : Controller
    {
        private readonly SpContext _db = new SpContext();

        public ActionResult BuscarParcela(Vehiculo vehiculo)
        {
            var parcelaMasGrande = _db.Parcelas.Max(item => item.Tamaño.ValorTamaño);
            var tipoDeVehiculo = _db.TiposDeVehiculo.Find(vehiculo.TipoDeVehiculoId);
            for (int i = tipoDeVehiculo.Tamaño.ValorTamaño; i <= parcelaMasGrande + 1; i++)
            {

                var parcela = _db.Parcelas.FirstOrDefault(p => p.Tamaño.ValorTamaño == i && p.Disponible);

                if (parcela != null)
                {
                    parcela.Disponible = false;
                    _db.Entry(parcela).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("ConfirmarIngreso", "Tickets", new { parcela.ParcelaId, vehiculo.VehiculoId });
                }
            }
            return RedirectToAction("NoHayParcelas", "Tickets", vehiculo);
        }

        public ActionResult LiberarParcela(int idticket)
        {
            var ticket = _db.Tickets.Find(idticket);
            var parcela = ticket.Parcela;

            parcela.Disponible = true;

            _db.Entry(parcela).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ConfirmarEgreso", "Tickets", new { idticket });

        }


        // GET: Parcelas
        public ActionResult Index()
        {
            var parcelas = _db.Parcelas.Include(p => p.Tamaño);
            return View(parcelas.ToList());
        }

        // GET: Parcelas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcela parcela = _db.Parcelas.Find(id);
            if (parcela == null)
            {
                return HttpNotFound();
            }
            return View(parcela);
        }

        // GET: Parcelas/Create
        public ActionResult Create()
        {
            ViewBag.TamañoId = new SelectList(_db.Tamaños, "TamañoId", "Descripcion");
            return View();
        }

        // POST: Parcelas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParcelaId,NumeroParcela,TamañoId,Disponible")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _db.Parcelas.Add(parcela);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TamañoId = new SelectList(_db.Tamaños, "TamañoId", "Descripcion", parcela.TamañoId);
            return View(parcela);
        }

        // GET: Parcelas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcela parcela = _db.Parcelas.Find(id);
            if (parcela == null)
            {
                return HttpNotFound();
            }
            ViewBag.TamañoId = new SelectList(_db.Tamaños, "TamañoId", "Descripcion", parcela.TamañoId);
            return View(parcela);
        }

        // POST: Parcelas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParcelaId,NumeroParcela,TamañoId,Disponible")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(parcela).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TamañoId = new SelectList(_db.Tamaños, "TamañoId", "Descripcion", parcela.TamañoId);
            return View(parcela);
        }

        // GET: Parcelas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcela parcela = _db.Parcelas.Find(id);
            if (parcela == null)
            {
                return HttpNotFound();
            }
            return View(parcela);
        }

        // POST: Parcelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parcela parcela = _db.Parcelas.Find(id);
            _db.Parcelas.Remove(parcela);
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
