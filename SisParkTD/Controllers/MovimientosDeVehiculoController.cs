using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using PagedList;


namespace SisParkTD.Controllers
{
    public class MovimientosDeVehiculoController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: MovimientosDeVehiculo
        public ActionResult Index(int? page)
        {
            var movimientosDeVehiculo = _db.MovimientosDeVehiculo.Include(m => m.Ticket).OrderByDescending(m => m.Fecha);
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return View(movimientosDeVehiculo.ToPagedList(pageNumber,pageSize));
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
