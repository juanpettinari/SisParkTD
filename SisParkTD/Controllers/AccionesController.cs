using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class AccionesController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: Acciones
        public ActionResult Index()
        {
            var acciones = _db.Acciones.Include(a => a.Pagina);
            return View(acciones.ToList());
        }

        // GET: Acciones/Create
        public ActionResult Create()
        {
            ViewBag.ListaDePaginas = new SelectList(_db.Paginas, "PaginaId", "Descripcion");
            return View();
        }

        // POST: Acciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccionId,Descripcion,PaginaId")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                _db.Acciones.Add(accion);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaginaId = new SelectList(_db.Paginas, "PaginaId", "Descripcion", accion.PaginaId);
            return View(accion);
        }

        // GET: Acciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accion = _db.Acciones.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaginaId = new SelectList(_db.Paginas, "PaginaId", "Descripcion", accion.PaginaId);
            return View(accion);
        }

        // POST: Acciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccionId,Descripcion,PaginaId")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(accion).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaginaId = new SelectList(_db.Paginas, "PaginaId", "Descripcion", accion.PaginaId);
            return View(accion);
        }

        // GET: Acciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accion = _db.Acciones.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // POST: Acciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var accion = _db.Acciones.Find(id);
            _db.Acciones.Remove(accion);
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
