using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TiposDeVehiculoController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: TiposDeVehiculo
        public ActionResult Index()
        {
            return View(_db.TiposDeVehiculo.ToList());
        }

        // GET: TiposDeVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeVehiculo tipoDeVehiculo = _db.TiposDeVehiculo.Find(id);
            if (tipoDeVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeVehiculo);
        }

        // GET: TiposDeVehiculo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposDeVehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoDeVehiculoId,Nombre,TamanioVehiculo,TarifaOcasionalDecimal,TarifaMensualDecimal")] TipoDeVehiculo tipoDeVehiculo)
        {
            if (ModelState.IsValid)
            {
                _db.TiposDeVehiculo.Add(tipoDeVehiculo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDeVehiculo);
        }

        // GET: TiposDeVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeVehiculo tipoDeVehiculo = _db.TiposDeVehiculo.Find(id);
            if (tipoDeVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeVehiculo);
        }

        // POST: TiposDeVehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoDeVehiculoId,Nombre,TamanioVehiculo,TarifaOcasionalDecimal,TarifaMensualDecimal")] TipoDeVehiculo tipoDeVehiculo)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(tipoDeVehiculo).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDeVehiculo);
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
