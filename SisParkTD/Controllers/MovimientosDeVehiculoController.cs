﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
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
            const int pageSize = 9;
            var pageNumber = page ?? 1;
            return View(movimientosDeVehiculo.ToPagedList(pageNumber, pageSize));
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
