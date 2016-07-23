using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class ParcelasController : Controller
    {
        private readonly sisparkdbEntities _db = new sisparkdbEntities();


        //GET
        [Authorize]
        public ActionResult BuscarParcela(Vehiculos vehiculo)
        {

            var parcelaMasGrande = _db.Parcelas.Max(item => item.Tamaños.Valor);
            var tipodevehiculo = _db.TiposDeVehiculo.Find(vehiculo.IDTipoDeVehiculo);
            for (int i = tipodevehiculo.Tamaños.Valor; i <= parcelaMasGrande+1; i++)
            {
                
                var parcela = _db.Parcelas.FirstOrDefault(item => item.Tamaños.Valor == i && item.Disponible);

                if (parcela != null)
                {
                    parcela.Disponible = false;
                    _db.Entry(parcela).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("ConfirmarIngreso", "Tickets",  new {parcela.IDParcela,vehiculo.IDVehiculo});
                }
            }
            return RedirectToAction("NoHayParcelas", "Tickets", vehiculo);
        }

        public ActionResult LiberarParcela (int idticket)
        {
            var ticket = _db.Tickets.Find(idticket);
            var parcela = ticket.Parcelas;

            parcela.Disponible = true;

            _db.Entry(parcela).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ConfirmarEgreso", "Tickets",new {idticket });

        }

        // GET: Parcelas
        public ActionResult Index()
        {
            var parcelas = _db.Parcelas.Include(p => p.Tamaños);
            return View(parcelas.ToList());
        }

        // GET: Parcelas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = _db.Parcelas.Find(id);
            if (parcelas == null)
            {
                return HttpNotFound();
            }
            return View(parcelas);
        }

        // GET: Parcelas/Create
        public ActionResult Create()
        {
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño");
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
                _db.Parcelas.Add(parcelas);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
            return View(parcelas);
        }

        // GET: Parcelas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = _db.Parcelas.Find(id);
            if (parcelas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
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
                _db.Entry(parcelas).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTamaño = new SelectList(_db.Tamaños, "IDTamaño", "NombreTamaño", parcelas.IDTamaño);
            return View(parcelas);
        }

        // GET: Parcelas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcelas parcelas = _db.Parcelas.Find(id);
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
            Parcelas parcelas = _db.Parcelas.Find(id);
            _db.Parcelas.Remove(parcelas);
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
