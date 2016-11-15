using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using PagedList;
using static System.String;


namespace SisParkTD.Controllers
{
    public class MovimientosDeVehiculoController : Controller
    {
        private readonly SpContext _db = new SpContext();

        // GET: MovimientosDeVehiculo
        public ActionResult Index(int? page, string ddPatente, string ddTipoMovimiento, string fechaDesde, string fechaHasta)
        {
            ViewBag.Patente = ddPatente;
            ViewBag.TipoMovimiento = ddTipoMovimiento;
            ViewBag.fechaDesde = fechaDesde;
            ViewBag.fechaHasta = fechaHasta;



            var listaTipoMovimiento = new List<string>();

            var consultaTipoMovimiento = from m in _db.MovimientosDeVehiculo
                                         orderby m.TipoDeMovimientoDeVehiculo.ToString()
                                         select m.TipoDeMovimientoDeVehiculo.ToString();
            listaTipoMovimiento.AddRange(consultaTipoMovimiento.Distinct());
            ViewBag.ddTipoMovimiento = new SelectList(listaTipoMovimiento);

            var listaPatente = new List<string>();

            var consultaPatente = from m in _db.MovimientosDeVehiculo
                                         orderby m.Ticket.Vehiculo.Patente
                                         select m.Ticket.Vehiculo.Patente;
            listaPatente.AddRange(consultaPatente.Distinct());
            ViewBag.ddPatente = new SelectList(listaPatente);

            var movimientos = from m in _db.MovimientosDeVehiculo
                select m;

            if (!IsNullOrEmpty(fechaDesde))
            {
                var fecha1 = Convert.ToDateTime(fechaDesde);
                movimientos = movimientos.Where(m => m.Fecha >= fecha1);
            }
            if (!IsNullOrEmpty(fechaHasta))
            {
                var fecha2 = Convert.ToDateTime(fechaHasta);
                movimientos = movimientos.Where(m => m.Fecha <= fecha2);
            }

            if (!IsNullOrEmpty(ddPatente))
            {
                movimientos = movimientos.Where(m => m.Ticket.Vehiculo.Patente == ddPatente);
            }

            if (!IsNullOrEmpty(ddTipoMovimiento))
            {
                movimientos = movimientos.Where(m => m.TipoDeMovimientoDeVehiculo.ToString() == ddTipoMovimiento);
            }

            const int pageSize = 9;
            var pageNumber = page ?? 1;
            movimientos = movimientos.OrderBy(m => m.Fecha);
            return View(movimientos.ToPagedList(pageNumber, pageSize));
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
