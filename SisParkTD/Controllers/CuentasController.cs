using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class CuentasController : Controller
    {
        private readonly SpContext _db = new SpContext();

        public ActionResult FacturacionGenerada()
        {
            return View();
        }


        public ActionResult GenerarFacturacion()
        {
            var tickets =
                _db.Tickets.Where(
                    t => t.TipoDeTicket == TipoDeTicket.Abono && t.EstadoDeTicket != EstadoDeTicket.Historico);

            var fechaPrimerDiaDelMes = DateTime.Parse("1/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 00:00:00");
            var ticketsAbonoFinalizado = tickets.Where(t => t.Abono.FechaFin == fechaPrimerDiaDelMes);
            var ticketsAbonoNoFinalizado = tickets.Except(ticketsAbonoFinalizado);
            var ticketsRenovantes = new List<Ticket>();
            foreach (var ticket in ticketsAbonoNoFinalizado)
            {
                if (fechaPrimerDiaDelMes > ticket.FechaYHoraCreacionTicket)
                {
                    ticketsRenovantes.Add(ticket);
                }

            }



            //PARAR LA EJECUCIÓN ACA A VER SI FUNCA ESTO
            if (ticketsAbonoFinalizado.Any())
            {
                Session["msjAbonosFinalizados"] = new List<string>();
                foreach (var ticket in ticketsAbonoFinalizado)
                {
                    if (ticket.EstadoDeTicket == EstadoDeTicket.Activo)
                    {
                        var msjAbonosFinalizados = (List<string>)Session["msjAbonosFinalizados"];

                        msjAbonosFinalizados.Add("Se finalizó el abono del cliente " +
                                                 ticket.Vehiculo.Cliente.RazonSocial + ", llamar al teléfono " +
                                                 ticket.Vehiculo.Cliente.Telefono +
                                                 ", preguntar si quiere registrar otro abono por más tiempo o llamar grua si no contesta.");

                        Session["msjAbonosFinalizados"] = msjAbonosFinalizados;

                    }
                    else
                    {
                        ticket.Parcela.Disponible = true;
                        ticket.Abono.Activo = false;
                        ticket.EstadoDeTicket = EstadoDeTicket.Historico;
                        _db.Entry(ticket).State = EntityState.Modified;
                    }
                }
                _db.SaveChanges();
            }
            var movimientos = new List<MovimientoFinanciero>();
            if (ticketsRenovantes.Any())
            {
                foreach (var ticket in ticketsRenovantes)
                {

                    var ticketNuevo = new Ticket
                    {
                        EstadoDeTicket = ticket.EstadoDeTicket,
                        TipoDeTicket = ticket.TipoDeTicket,
                        FechaYHoraCreacionTicket = DateTime.Now,
                        PrecioTotalDecimal = ticket.Vehiculo.TipoDeVehiculo.TarifaMensualDecimal,
                        Pagado = false,
                        Abono = ticket.Abono,
                        Parcela = ticket.Parcela,
                        Vehiculo = ticket.Vehiculo
                    };
                    _db.Tickets.Add(ticketNuevo);

                    ticket.EstadoDeTicket = EstadoDeTicket.Historico;
                    _db.Entry(ticket).State = EntityState.Modified;
                    
                    if (ticketNuevo.PrecioTotalDecimal != null)
                        ticketNuevo.Vehiculo.Cliente.Cuenta.SaldoDecimal =
                            ticketNuevo.Vehiculo.Cliente.Cuenta.SaldoDecimal - (decimal)ticketNuevo.PrecioTotalDecimal;

                    
                    var movimientoFinanciero = new MovimientoFinanciero
                    {
                        Cuenta = ticketNuevo.Vehiculo.Cliente.Cuenta,
                        TipoDeMovimientoFinancieroId = (int)TipoDeMovimientoFinancieroenum.FacturacionAbono,
                        Ticket = ticketNuevo,
                        Fecha = DateTime.Now

                    };
                    _db.MovimientosFinancieros.Add(movimientoFinanciero);
                    movimientos.Add(movimientoFinanciero);
                    


                }
                _db.SaveChanges();

            }


            ViewBag.message = "Se generaron: " + movimientos.Count + " Facturas";

            return View("FacturacionGenerada", movimientos);
        }

        public async Task<ActionResult> PagarAbono(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);

            ticket.Pagado = true;

            if (ticket.PrecioTotalDecimal != null)
                ticket.Vehiculo.Cliente.Cuenta.SaldoDecimal = ticket.Vehiculo.Cliente.Cuenta.SaldoDecimal + (decimal)ticket.PrecioTotalDecimal;
            _db.Entry(ticket.Vehiculo.Cliente.Cuenta).State = EntityState.Modified;

            var movimientoFinanciero = new MovimientoFinanciero
            {
                Ticket = ticket,
                TipoDeMovimientoFinancieroId = (int)TipoDeMovimientoFinancieroenum.PagoAbono,
                Cuenta = ticket.Vehiculo.Cliente.Cuenta,
                Fecha = DateTime.Now
            };

            _db.MovimientosFinancieros.Add(movimientoFinanciero);

            _db.SaveChanges();

            return RedirectToAction("VerMovimientos", new { id = ticket.Vehiculo.Cliente.Cuenta.CuentaId });

        }

        public async Task<ActionResult> VerMovimientos(int id)
        {
            var cuenta = await _db.Cuentas.FindAsync(id);

            return View(cuenta);
        }


        // GET: Cuentas
        public async Task<ActionResult> Index()
        {
            var cuentas = _db.Cuentas.Include(c => c.Cliente);

            return View(await cuentas.ToListAsync());
        }

        // GET: Cuentas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cuenta = await _db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.CuentaId = new SelectList(_db.Clientes, "ClienteId", "RazonSocial");
            return View();
        }

        // POST: Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CuentaId,SaldoDecimal")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                _db.Cuentas.Add(cuenta);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CuentaId = new SelectList(_db.Clientes, "ClienteId", "RazonSocial", cuenta.CuentaId);
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cuenta = await _db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaId = new SelectList(_db.Clientes, "ClienteId", "RazonSocial", cuenta.CuentaId);
            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CuentaId,SaldoDecimal")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cuenta).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaId = new SelectList(_db.Clientes, "ClienteId", "RazonSocial", cuenta.CuentaId);
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cuenta = await _db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var cuenta = await _db.Cuentas.FindAsync(id);
            _db.Cuentas.Remove(cuenta);
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

