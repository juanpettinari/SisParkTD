using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TicketsController : Controller
    {
        private readonly sisparkdbEntities _db = new sisparkdbEntities();

        //GET
        public ActionResult RetirarVehiculo()
        {
            var listadoTicketsIngresados = _db.Tickets.Where(item => item.EstadosDeTicket.NombreEstadoDeTicket == "Ingresado");

            return View(listadoTicketsIngresados);
        }

        //return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new { patente = patente});

        public ActionResult ConfirmarEgreso(int idTicket)
        {
            var ticket = _db.Tickets.Find(idTicket);

            

            ticket.EstadosDeTicket = _db.EstadosDeTicket.FirstOrDefault(item => item.NombreEstadoDeTicket == "Finalizado");
            ticket.HorarioDeSalida = DateTime.Now;
            var tiempoTotal = ticket.HorarioDeSalida.Value.Subtract(ticket.HorarioDeLlegada);
            var horas = tiempoTotal.TotalHours;
            
            ticket.TiempoTotal = Convert.ToDecimal(horas);
            ticket.PrecioTotal = ticket.TiempoTotal * ticket.Vehiculos.TiposDeVehiculo.Tarifa;


            
            _db.Entry(ticket).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("ImprimirTicket", new { idticket = ticket.IDTicket });

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
                return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new {patente });
        }

        public ActionResult NoHayParcelas(Vehiculos vehiculo)
        {
            var tipodevehiculo = _db.TiposDeVehiculo.Find(vehiculo.IDTipoDeVehiculo).NombreTipoDeVehiculo;
            ViewBag.TipoDeVehiculo = tipodevehiculo;
            return View();
        }

        public ActionResult ConfirmarIngreso(int idParcela, int idVehiculo)
        {
            var ticket = new Tickets();

            var vehiculo = _db.Vehiculos.Find(idVehiculo);
            ticket.Vehiculos = vehiculo;
            ticket.EstadosDeTicket = _db.EstadosDeTicket.FirstOrDefault(item => item.NombreEstadoDeTicket == "Ingresado");
            ticket.Parcelas = _db.Parcelas.Find(idParcela);
            ticket.HorarioDeLlegada = DateTime.Now;
            if (ModelState.IsValid)
            {
                _db.Tickets.Add(ticket);
                _db.SaveChanges();
            }
            return RedirectToAction("ImprimirTicket", new { idticket = ticket.IDTicket });
            
            
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
                return RedirectToAction("ImprimirTicket", new {idticket = ticket.IDTicket});
            ViewBag.errorNoExisteTicket = "El ticket ingresado no existe";
            return View();
        }


        public ActionResult ImprimirTicket(int idticket)
        {
            var ticket = _db.Tickets.Find(idticket);
            if (Request.UrlReferrer == null)
                return View(ticket);
            var urlDeReferencia = Request.UrlReferrer.Segments.Skip(2).Take(1).SingleOrDefault();
            if (urlDeReferencia != null && (ticket.HorarioDeSalida == null && urlDeReferencia.Trim('/') == "IngresarVehiculo"))
            {
                ViewBag.infoParcela = "Indicar al conductor que se dirija a la parcela: " + ticket.Parcelas.NumeroParcela;
            }
            return View(ticket);
        }

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = _db.Tickets.Include(t => t.EstadosDeTicket).Include(t => t.Parcelas).Include(t => t.Vehiculos);
            return View(tickets.ToList());
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
