using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly sisparkdbEntities _db = new sisparkdbEntities();

        // GET: Vehiculos
        public ActionResult Index()
        {
            var vehiculos = _db.Vehiculos.Include(v => v.TiposDeVehiculo);
            return View(vehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculos = _db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }
        //GET
        public ActionResult BuscarExistenciaVehiculo(string patente)
        {

            var vehiculo = _db.Vehiculos.FirstOrDefault(i => i.Patente == patente);

            var urlDeReferencia = Request.UrlReferrer?.Segments.Skip(2).Take(1).SingleOrDefault();
            if (urlDeReferencia != null)
            {
                var value = urlDeReferencia.Trim('/');


                switch (value)
                {
                    case "IngresarVehiculo":
                        if (vehiculo == null)
                        {
                            return RedirectToAction("Create", new {patente });
                        }
                        else if (_db.Tickets.FirstOrDefault(item => item.IDVehiculo == vehiculo.IDVehiculo && item.EstadosDeTicket.NombreEstadoDeTicket == "Ingresado") == null)
                        {
                            return RedirectToAction("BuscarParcela", "Parcelas", vehiculo); 
                        }
                        else
                        {
                            return RedirectToAction("IngresarVehiculo", "Tickets", new { errorMessage = "Ya está ingresado un vehiculo con patente: " + vehiculo.Patente });
                        }
                    case "RetirarVehiculo":
                        return RedirectToAction("LiberarParcela", "Parcelas", vehiculo);
                    default:
                        return RedirectToAction("RetirarVehiculo", "Tickets", null);
                }
            }
            return RedirectToAction("RetirarVehiculo", "Tickets", null);
        }

        // GET: Vehiculos/Create
        [HttpGet]
        public ActionResult Create(string patente)
        {
            ViewBag.IDTipoDeVehiculo = new SelectList(_db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo");
            ViewBag.patente = patente;
            return View();
        }
        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.IDTipoDeVehiculo = new SelectList(_db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo");
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
                _db.Vehiculos.Add(vehiculos);
                _db.SaveChanges();
                if (Request.UrlReferrer != null && Request.UrlReferrer.Query != "")
                {
                    return RedirectToAction("BuscarParcela", "Parcelas", vehiculos);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDTipoDeVehiculo = new SelectList(_db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = _db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTipoDeVehiculo = new SelectList(_db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
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
                _db.Entry(vehiculos).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTipoDeVehiculo = new SelectList(_db.TiposDeVehiculo, "IDTipoDeVehiculo", "NombreTipoDeVehiculo", vehiculos.IDTipoDeVehiculo);
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = _db.Vehiculos.Find(id);
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
            Vehiculos vehiculos = _db.Vehiculos.Find(id);
            _db.Vehiculos.Remove(vehiculos);
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
