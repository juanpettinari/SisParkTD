using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class EstadosDeTicketsController : Controller
    {
        private readonly sisparkdbEntities _db = new sisparkdbEntities();

        // GET: EstadosDeTickets
        public ActionResult Index()
        {
            return View(_db.EstadosDeTicket.ToList());
        }

        // GET: EstadosDeTickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadosDeTicket estadosDeTicket = _db.EstadosDeTicket.Find(id);
            if (estadosDeTicket == null)
            {
                return HttpNotFound();
            }
            return View(estadosDeTicket);
        }

        // GET: EstadosDeTickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosDeTickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDEstadoDeTicket,NombreEstadoDeTicket,DescripcionEstadoDeTicket")] EstadosDeTicket estadosDeTicket)
        {
            if (ModelState.IsValid)
            {
                _db.EstadosDeTicket.Add(estadosDeTicket);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadosDeTicket);
        }

        // GET: EstadosDeTickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadosDeTicket estadosDeTicket = _db.EstadosDeTicket.Find(id);
            if (estadosDeTicket == null)
            {
                return HttpNotFound();
            }
            return View(estadosDeTicket);
        }

        // POST: EstadosDeTickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDEstadoDeTicket,NombreEstadoDeTicket,DescripcionEstadoDeTicket")] EstadosDeTicket estadosDeTicket)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(estadosDeTicket).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadosDeTicket);
        }

        // GET: EstadosDeTickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadosDeTicket estadosDeTicket = _db.EstadosDeTicket.Find(id);
            if (estadosDeTicket == null)
            {
                return HttpNotFound();
            }
            return View(estadosDeTicket);
        }

        // POST: EstadosDeTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadosDeTicket estadosDeTicket = _db.EstadosDeTicket.Find(id);
            _db.EstadosDeTicket.Remove(estadosDeTicket);
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
