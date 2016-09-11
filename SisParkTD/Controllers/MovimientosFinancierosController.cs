using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class MovimientosFinancierosController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: MovimientoFinancieros
        public ActionResult Index(int? page)
        {
            var movimientosFinancieros = _db.MovimientosFinancieros.Include(m => m.Cuenta).Include(m => m.Ticket).Include(m => m.TipoDeMovimientoFinanciero).OrderBy(m => m.Fecha);
            const int pageSize = 9;
            var pageNumber = page ?? 1;
            return View(movimientosFinancieros.ToPagedList(pageNumber, pageSize));
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
