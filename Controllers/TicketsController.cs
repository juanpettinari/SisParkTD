using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class TicketsController : Controller
    {
        private sisparkdbEntities db = new sisparkdbEntities();

        //GET
        public ActionResult RetirarVehiculo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetirarVehiculo(string patente)
        {
            return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new { patente = patente });
        }

        public ActionResult ConfirmarEgreso(Vehiculos vehiculo)
        {
            Tickets ticket = db.Tickets.Where(item => item.IDVehiculo == vehiculo.IDVehiculo && item.EstadosDeTicket.NombreEstadoDeTicket == "Ingresado" ).FirstOrDefault();

            ticket.IDEstadoDeTicket = db.EstadosDeTicket.Where(item => item.NombreEstadoDeTicket == "Finalizado").Select(item => item.IDEstadoDeTicket).FirstOrDefault();

            ticket.HorarioDeSalida = DateTime.Now;
            TimeSpan TiempoTotal = ticket.HorarioDeSalida.Value.Subtract(ticket.HorarioDeLlegada);
            double Horas = TiempoTotal.TotalHours;
            ticket.TiempoTotal = Convert.ToDecimal(Horas);
            ticket.PrecioTotal = ticket.TiempoTotal * ticket.Vehiculos.TiposDeVehiculo.Tarifa;




            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ImprimirTicket", ticket);



        }

        //GET 
        public ActionResult IngresarVehiculo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngresarVehiculo(string patente)
        {
            return RedirectToAction("BuscarExistenciaVehiculo", "Vehiculos", new { patente = patente });
        }

        public ActionResult NoHayParcelas(Vehiculos vehiculo)
        {
            ViewBag.TipoDeVehiculo = vehiculo.TiposDeVehiculo.NombreTipoDeVehiculo;
            return View();
        }

        public ActionResult ConfirmarIngreso(int IDParcela, int IDVehiculo)
        {
            Tickets ticket = new Tickets();

            Vehiculos vehiculo = db.Vehiculos.Find(IDVehiculo);
            ticket.Vehiculos = vehiculo;
            ticket.Vehiculos.Patente = vehiculo.Patente;
            var estadoTicket = db.EstadosDeTicket.Where(item => item.NombreEstadoDeTicket == "Ingresado").Select(item => item.IDEstadoDeTicket).FirstOrDefault();
            ticket.IDEstadoDeTicket = estadoTicket;
            ticket.IDParcela = IDParcela;
            ticket.HorarioDeLlegada = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
            return RedirectToAction("ImprimirTicket",  ticket);
            
        }


        public ActionResult ImprimirTicket(Tickets ticket)
        {
            ticket = db.Tickets.Find(ticket.IDTicket);
            return View(ticket);
        }

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.EstadosDeTicket).Include(t => t.Parcelas).Include(t => t.Vehiculos);
            return View(tickets.ToList());
        }

        //// GET: Tickets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tickets tickets = db.Tickets.Find(id);
        //    if (tickets == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tickets);
        //}

        //// GET: Tickets/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IDEstadoDeTicket = new SelectList(db.EstadosDeTicket, "IDEstadoDeTicket", "NombreEstadoDeTicket");
        //    ViewBag.IDParcela = new SelectList(db.Parcelas, "IDParcela", "IDParcela");
        //    ViewBag.IDVehiculo = new SelectList(db.Vehiculos, "IDVehiculo", "Patente");
        //    return View();
        //}

        //// POST: Tickets/Create
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDTicket,IDVehiculo,IDEstadoDeTicket,IDParcela,HorarioDeLlegada,HorarioDeSalida,TiempoTotal,PrecioTotal")] Tickets tickets)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tickets.Add(tickets);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDEstadoDeTicket = new SelectList(db.EstadosDeTicket, "IDEstadoDeTicket", "NombreEstadoDeTicket", tickets.IDEstadoDeTicket);
        //    ViewBag.IDParcela = new SelectList(db.Parcelas, "IDParcela", "IDParcela", tickets.IDParcela);
        //    ViewBag.IDVehiculo = new SelectList(db.Vehiculos, "IDVehiculo", "Patente", tickets.IDVehiculo);
        //    return View(tickets);
        //}

        //// GET: Tickets/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tickets tickets = db.Tickets.Find(id);
        //    if (tickets == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDEstadoDeTicket = new SelectList(db.EstadosDeTicket, "IDEstadoDeTicket", "NombreEstadoDeTicket", tickets.IDEstadoDeTicket);
        //    ViewBag.IDParcela = new SelectList(db.Parcelas, "IDParcela", "IDParcela", tickets.IDParcela);
        //    ViewBag.IDVehiculo = new SelectList(db.Vehiculos, "IDVehiculo", "Patente", tickets.IDVehiculo);
        //    return View(tickets);
        //}

        //// POST: Tickets/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IDTicket,IDVehiculo,IDEstadoDeTicket,IDParcela,HorarioDeLlegada,HorarioDeSalida,TiempoTotal,PrecioTotal")] Tickets tickets)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tickets).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IDEstadoDeTicket = new SelectList(db.EstadosDeTicket, "IDEstadoDeTicket", "NombreEstadoDeTicket", tickets.IDEstadoDeTicket);
        //    ViewBag.IDParcela = new SelectList(db.Parcelas, "IDParcela", "IDParcela", tickets.IDParcela);
        //    ViewBag.IDVehiculo = new SelectList(db.Vehiculos, "IDVehiculo", "Patente", tickets.IDVehiculo);
        //    return View(tickets);
        //}

        //// GET: Tickets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tickets tickets = db.Tickets.Find(id);
        //    if (tickets == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tickets);
        //}

        //// POST: Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tickets tickets = db.Tickets.Find(id);
        //    db.Tickets.Remove(tickets);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
