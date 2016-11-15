using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TiposDeMovimientoFinancieroController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: TiposDeMovimientoFinanciero
        public async Task<ActionResult> Index()
        {
            return View(await _db.TiposDeMovimientoFinanciero.ToListAsync());
        }

        // GET: TiposDeMovimientoFinanciero/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipoDeMovimientoFinanciero = await _db.TiposDeMovimientoFinanciero.FindAsync(id);
            if (tipoDeMovimientoFinanciero == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMovimientoFinanciero);
        }

        // GET: TiposDeMovimientoFinanciero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposDeMovimientoFinanciero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoDeMovimientoFinancieroId,Nombre,Descripcion,Signo")] TipoDeMovimientoFinanciero tipoDeMovimientoFinanciero)
        {
            if (ModelState.IsValid)
            {
                _db.TiposDeMovimientoFinanciero.Add(tipoDeMovimientoFinanciero);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoDeMovimientoFinanciero);
        }

        // GET: TiposDeMovimientoFinanciero/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipoDeMovimientoFinanciero = await _db.TiposDeMovimientoFinanciero.FindAsync(id);
            if (tipoDeMovimientoFinanciero == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMovimientoFinanciero);
        }

        // POST: TiposDeMovimientoFinanciero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoDeMovimientoFinancieroId,Nombre,Descripcion,Signo")] TipoDeMovimientoFinanciero tipoDeMovimientoFinanciero)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(tipoDeMovimientoFinanciero).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoDeMovimientoFinanciero);
        }

        // GET: TiposDeMovimientoFinanciero/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipoDeMovimientoFinanciero = await _db.TiposDeMovimientoFinanciero.FindAsync(id);
            if (tipoDeMovimientoFinanciero == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMovimientoFinanciero);
        }

        // POST: TiposDeMovimientoFinanciero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var tipoDeMovimientoFinanciero = await _db.TiposDeMovimientoFinanciero.FindAsync(id);
            _db.TiposDeMovimientoFinanciero.Remove(tipoDeMovimientoFinanciero);
            await _db.SaveChangesAsync();
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
