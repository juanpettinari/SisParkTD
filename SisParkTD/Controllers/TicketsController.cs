using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TicketsController : Controller
    {
        private readonly SpContext _db = new SpContext();


        public ActionResult RetirarVehiculo()
        {
            var listadoTicketsIngresados = _db.Tickets.Where(lti => lti.EstadoDeTicket == EstadoDeTicket.Ingresado);

            return View(listadoTicketsIngresados);
        }

        //return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new { patente = patente});

        public ActionResult ConfirmarEgreso(int idTicket)
        {
            var ticket = _db.Tickets.Find(idTicket);



            ticket.EstadoDeTicket = EstadoDeTicket.Retirado;
            ticket.FechaYHoraDeSalida = DateTime.Now;
            var tiempoTotal = ticket.FechaYHoraDeSalida.Value.Subtract(ticket.FechaYHoraDeEntrada);

            var horas = tiempoTotal.TotalHours;

            ticket.TiempoTotal = Convert.ToDecimal(horas);
            ticket.PrecioTotalDecimal = ticket.TiempoTotal * ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;



            _db.Entry(ticket).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("ImprimirTicket", new { idticket = ticket.TicketId });

        }


        public ActionResult IngresarVehiculo(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View();
        }

        [ActionName("IngresarVehiculo")]
        [HttpPost]
        public ActionResult IngresarVehiculo_Post(string patente)
        {
            if (patente == "")
            {
                ViewBag.errorMessage = "Error: Ingrese una patente";
                return View();
            }
            else
                return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new { patente });
        }

        public ActionResult NoHayParcelas(Vehiculo vehiculo)
        {
            var tipodevehiculo = _db.TiposDeVehiculo.Find(vehiculo.TipoDeVehiculoId).NombreDeTipoDeVehiculo;
            ViewBag.TipoDeVehiculo = tipodevehiculo;
            return View();
        }

        public ActionResult ConfirmarIngreso(int parcelaId, int vehiculoId)
        {
            var ticket = new Ticket();

            var vehiculo = _db.Vehiculos.Find(vehiculoId);
            ticket.Vehiculo = vehiculo;
            ticket.EstadoDeTicket = EstadoDeTicket.Ingresado;
            ticket.Parcela = _db.Parcelas.Find(parcelaId);
            ticket.FechaYHoraDeEntrada = DateTime.Now;
            if (ModelState.IsValid)
            {
                _db.Tickets.Add(ticket);
                _db.SaveChanges();
            }
            return RedirectToAction("ImprimirTicket", new { idticket = ticket.TicketId });


        }
        public ActionResult ReImprimirTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReImprimirTicket(int idTicket)
        {
            var ticket = _db.Tickets.Find(idTicket);
            if (ticket != null)
                return RedirectToAction("ImprimirTicket", new { idticket = ticket.TicketId });
            ViewBag.errorNoExisteTicket = "El ticket ingresado no existe";
            return View();
        }


        public ActionResult ImprimirTicket(int ticketId)
        {
            var ticket = _db.Tickets.Find(ticketId);
            if (Request.UrlReferrer == null)
                return View(ticket);
            var urlDeReferencia = Request.UrlReferrer.Segments.Skip(2).Take(1).SingleOrDefault();
            if (urlDeReferencia != null && ticket.FechaYHoraDeSalida == null && urlDeReferencia.Trim('/') == "IngresarVehiculo")
            {
                ViewBag.infoParcela = "Indicar al conductor que se dirija a la parcela: " + ticket.Parcela.NumeroParcela;
            }
            return View(ticket);
        }





        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = _db.Tickets.Include(t => t.Abono).Include(t => t.Parcela).Include(t => t.Vehiculo);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AbonoId = new SelectList(_db.Abonos, "AbonoId", "AbonoId");
            ViewBag.ParcelaId = new SelectList(_db.Parcelas, "ParcelaId", "ParcelaId");
            ViewBag.VehiculoId = new SelectList(_db.Vehiculos, "VehiculoId", "Patente");
            return View();
        }

        // POST: Tickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,VehiculoId,AbonoId,ParcelaId,EstadoDeTicket,FechaYHoraDeEntrada,FechaYHoraDeSalida,TiempoTotal,PrecioTotalDecimal")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _db.Tickets.Add(ticket);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AbonoId = new SelectList(_db.Abonos, "AbonoId", "AbonoId", ticket.AbonoId);
            ViewBag.ParcelaId = new SelectList(_db.Parcelas, "ParcelaId", "ParcelaId", ticket.ParcelaId);
            ViewBag.VehiculoId = new SelectList(_db.Vehiculos, "VehiculoId", "Patente", ticket.VehiculoId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AbonoId = new SelectList(_db.Abonos, "AbonoId", "AbonoId", ticket.AbonoId);
            ViewBag.ParcelaId = new SelectList(_db.Parcelas, "ParcelaId", "ParcelaId", ticket.ParcelaId);
            ViewBag.VehiculoId = new SelectList(_db.Vehiculos, "VehiculoId", "Patente", ticket.VehiculoId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,VehiculoId,AbonoId,ParcelaId,EstadoDeTicket,FechaYHoraDeEntrada,FechaYHoraDeSalida,TiempoTotal,PrecioTotalDecimal")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(ticket).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AbonoId = new SelectList(_db.Abonos, "AbonoId", "AbonoId", ticket.AbonoId);
            ViewBag.ParcelaId = new SelectList(_db.Parcelas, "ParcelaId", "ParcelaId", ticket.ParcelaId);
            ViewBag.VehiculoId = new SelectList(_db.Vehiculos, "VehiculoId", "Patente", ticket.VehiculoId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = _db.Tickets.Find(id);
            _db.Tickets.Remove(ticket);
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
