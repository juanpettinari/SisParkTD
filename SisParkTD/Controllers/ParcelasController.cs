using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class ParcelasController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: Parcelas
        public ActionResult Index()
        {
            var parcelas = _db.Parcelas.Include(p => p.TipoDeVehiculo);
            return View(parcelas.ToList());
        }

        // GET: Parcelas/Create
        public ActionResult Create()
        {
            ViewBag.TipoDeVehiculoId = new SelectList(_db.TiposDeVehiculo, "TipoDeVehiculoId", "Nombre");
            return View();
        }

        // POST: Parcelas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParcelaId,NumeroParcela,Disponible,TipoDeVehiculoId")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _db.Parcelas.Add(parcela);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDeVehiculoId = new SelectList(_db.TiposDeVehiculo, "TipoDeVehiculoId", "Nombre", parcela.TipoDeVehiculoId);
            return View(parcela);
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
