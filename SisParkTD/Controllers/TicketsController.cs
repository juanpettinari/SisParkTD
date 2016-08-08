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


        public ActionResult IngresarVehiculo(string patente, string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View();
        }

        [ActionName("IngresarVehiculo")]
        [HttpPost]
        public ActionResult IngresarVehiculo_Post(string patente)
        {
            if (patente == string.Empty)
            {
                ViewBag.errorMessage = "Escriba una patente";
                return View();
            }

            return RedirectToAction("ConfirmarIngreso", new { patente });
        }




        public ActionResult ConfirmarIngreso(string patente)
        {

            // Logica buscar vehiculo
            var vehiculo = _db.Vehiculos.FirstOrDefault(v => v.Patente == patente);

            if (vehiculo == null)
            {
                TempData["urlPrevia"] = Request.RequestContext.RouteData.Values["action"];
                return RedirectToAction("Create", "Vehiculos", new { patente });
            }
            //Logica buscar tickets
            var tickets = _db.Tickets.Where(t => t.VehiculoId == vehiculo.VehiculoId);

            if (!tickets.Any(t => t.EstadoDeTicket == EstadoDeTicket.Activo))
            {
                //Ingreso de un abonado
                if (tickets.Any(t => t.TipoDeTicket == TipoDeTicket.Abono && t.EstadoDeTicket == EstadoDeTicket.Inactivo))
                {
                    var ticket = tickets.Single(t => t.EstadoDeTicket == EstadoDeTicket.Inactivo);

                    ticket.EstadoDeTicket = EstadoDeTicket.Activo;
                    var movimientoDeVehiculo = new MovimientoDeVehiculo
                    {
                        TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Entrada,
                        Fecha = DateTime.Now
                    };

                    ticket.MovimientosDeVehiculo.Add(movimientoDeVehiculo);
                    _db.SaveChanges();
                    return RedirectToAction("IngresarVehiculo");
                }
                //Ingreso de un vehiculo ocasional.
                var parcelaMasGrande = (int)Enum.GetValues(typeof(TamanioVehiculo)).Cast<TamanioVehiculo>().Max();
                for (var i = (int)vehiculo.TipoDeVehiculo.TamanioVehiculo; i <= parcelaMasGrande; i++)
                {
                    var parcela =
                        _db.Parcelas.FirstOrDefault(p => (int)p.TipoDeVehiculo.TamanioVehiculo == i && p.Disponible);

                    if (parcela != null)
                    {
                        // Logica bloquear parcela

                        parcela.Disponible = false;
                        _db.Entry(parcela).State = EntityState.Modified;

                        // Logica Guardar Ticket

                        var movimientoDeVehiculo = new MovimientoDeVehiculo
                        {
                            TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Entrada,
                            Fecha = DateTime.Now
                        };

                        var ticket = new Ticket
                        {
                            Vehiculo = vehiculo,
                            EstadoDeTicket = EstadoDeTicket.Activo,
                            Parcela = parcela,
                            FechaYHoraCreacionTicket = DateTime.Now,
                            TipoDeTicket = TipoDeTicket.Ocasional,
                            Pagado = false
                        };


                        if (ModelState.IsValid)
                        {
                            _db.Tickets.Add(ticket);

                            movimientoDeVehiculo.TicketId = ticket.TicketId;
                            _db.MovimientosDeVehiculo.Add(movimientoDeVehiculo);
                            _db.SaveChanges();
                        }

                        if (ticket.TipoDeTicket == TipoDeTicket.Ocasional)
                        {
                            ViewBag.infoParcela = "Indicar al conductor que se dirija a la parcela: " + ticket.Parcela.NumeroParcela;
                            ViewBag.Tiempo =
                                TimeSpan.FromSeconds(Convert.ToDouble(ticket.TiempoTotal))
                                    .ToString(ticket.TiempoTotal >= 86400 ? "d'd 'h'h 'm'm 's's'" : "h'h 'm'm 's's'");
                        }
                        return View("ImprimirTicket", ticket);
                    }

                }
                return RedirectToAction("NoHayParcelas", new { vehiculo.TipoDeVehiculoId });
            }
            // Logica vehiculo patente xx ya ingresado en la parcela:yy

            ViewBag.errorMessage = "Ya está ingresado el vehiculo con patente: " + vehiculo.Patente + " en la parcela: " +
                tickets.Where(t => t.VehiculoId == vehiculo.VehiculoId && t.EstadoDeTicket == EstadoDeTicket.Activo).Select(t => t.Parcela.NumeroParcela).Single();
            return View("IngresarVehiculo");
        }

        public ActionResult RetirarVehiculo()
        {
            var listadoTicketsIngresados = _db.Tickets.Where(lti => lti.EstadoDeTicket == EstadoDeTicket.Activo);

            return View(listadoTicketsIngresados);
        }

        public ActionResult ConfirmarEgreso(int ticketId)
        {
            var ticket = _db.Tickets.Find(ticketId);

            // Si es abono
            switch (ticket.TipoDeTicket)
            {
                case TipoDeTicket.Abono:
                {
                    ticket.EstadoDeTicket = EstadoDeTicket.Inactivo;
                    var movimientoDeVehiculo = new MovimientoDeVehiculo
                    {
                        TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Salida,
                        Fecha = DateTime.Now
                    };
                    _db.Entry(ticket).State = EntityState.Modified;
                    ticket.MovimientosDeVehiculo.Add(movimientoDeVehiculo);
                    _db.SaveChanges();
                    return RedirectToAction("IngresarVehiculo");

                }
                case TipoDeTicket.Ocasional:
                {
                    //Logica Liberar Parcela
                    var parcela = _db.Parcelas.Find(ticket.ParcelaId);
                    parcela.Disponible = true;
                    _db.Entry(parcela).State = EntityState.Modified;

                    ticket.EstadoDeTicket = EstadoDeTicket.Inactivo;

                    var movimientoDeVehiculo = new MovimientoDeVehiculo
                    {
                        Fecha = DateTime.Now,
                        TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Salida,
                        TicketId = ticketId
                    };
                    _db.MovimientosDeVehiculo.Add(movimientoDeVehiculo);

                    var timeSpan = DateTime.Now.Subtract(ticket.MovimientosDeVehiculo.Where(mdv => mdv.TipoDeMovimientoDeVehiculo == TipoDeMovimientoDeVehiculo.Entrada).Select(mdv => mdv.Fecha).Single());
                    ticket.TiempoTotal = (int)timeSpan.TotalSeconds;

                    var fraccionesDeTiempo = timeSpan.TotalMinutes / 15;
                    //Si está menos de 5 minutos, no se cobra
                    if (fraccionesDeTiempo < 0.33)
                    {
                        ticket.PrecioTotalDecimal = 0;
                    }
                    else {
                        var fraccionesDeTiempoRedondeado = Convert.ToInt32(Math.Ceiling(fraccionesDeTiempo));

                        //Menor a 4hs se cobra la tarifa*fracciones.
                        //Entre 4 y 6, cobrar estadía de 6hs.
                        //Entre 6 y 8, cobrar estadía de 8hs.
                        //Entre 8 y 10, cobrar estadía de 10hs.
                        //Entre 10 y 12, cobrar estadía de 12hs.
                        //Mayor a 12Hs se cobra la estadía de 12 hs + precio por hora de esa estadía de las horas excedentes.

                        //<4hs
                        if (fraccionesDeTiempoRedondeado < 16)
                            ticket.PrecioTotalDecimal = fraccionesDeTiempoRedondeado *
                                                        ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        else if (fraccionesDeTiempoRedondeado >= 16 && fraccionesDeTiempoRedondeado < 24)
                            ticket.PrecioTotalDecimal = 16 * ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        //6hs>=x>8
                        else if (fraccionesDeTiempoRedondeado >= 24 && fraccionesDeTiempoRedondeado < 32)
                            ticket.PrecioTotalDecimal = 18 * ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        //8hs>=x>10
                        else if (fraccionesDeTiempoRedondeado >= 32 && fraccionesDeTiempoRedondeado < 40)
                            ticket.PrecioTotalDecimal = 20 * ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        //10hs>=x>12
                        else if (fraccionesDeTiempoRedondeado >= 40 && fraccionesDeTiempoRedondeado < 48)
                            ticket.PrecioTotalDecimal = 22 * ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        //x>12
                        else if (fraccionesDeTiempoRedondeado > 48)
                        {
                            ticket.PrecioTotalDecimal = fraccionesDeTiempoRedondeado *
                                                        Math.Round(ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal / 2.1818m);
                        }
                    }
                    if (ticket.PrecioTotalDecimal != 0)
                    {
                        var movimientoFinanciero = new MovimientoFinanciero
                        {
                            TipoDeMovimientoFinancieroId = (int)TipoDeMovimientoFinancieroenum.PagoOcasional,
                            Ticket = ticket,
                            Fecha = DateTime.Now
                        };
                        _db.MovimientosFinancieros.Add(movimientoFinanciero);
                    }


                    _db.Entry(ticket).State = EntityState.Modified;
                    _db.SaveChanges();

                    ViewBag.Tiempo =
                        TimeSpan.FromSeconds(Convert.ToDouble(ticket.TiempoTotal))
                            .ToString(ticket.TiempoTotal >= 86400 ? "d'd 'h'h 'm'm 's's'" : "h'h 'm'm 's's'");

                    return View("ImprimirTicket", ticket);
                }
                default:
                    ViewBag.errorMessage = "No existe el ticket ingresado.";
                    return View("IngresarVehiculo");
            }
        }

        public ActionResult ConfirmarEgresoConGrua(int ticketId)
        {
            var ticket = _db.Tickets.Find(ticketId);

            var parcela = ticket.Parcela;

            parcela.Disponible = true;
            ticket.EstadoDeTicket = EstadoDeTicket.Historico;

            var movimientoDeVehiculo = new MovimientoDeVehiculo
            {
                Fecha = DateTime.Now,
                TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Salida
            };


            _db.Entry(parcela).State = EntityState.Modified;
            _db.Entry(ticket).State = EntityState.Modified;
            ticket.MovimientosDeVehiculo.Add(movimientoDeVehiculo);
            _db.SaveChanges();


            return RedirectToAction("RetirarVehiculo");

        }


        public ActionResult NoHayParcelas(int tipoDeVehiculoId)
        {
            var tipodevehiculo = _db.TiposDeVehiculo.Find(tipoDeVehiculoId);
            if (tipodevehiculo.Nombre != null) ViewBag.TipoDeVehiculo = tipodevehiculo.Nombre;
            return View();
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
            var ticket = _db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = _db.Tickets.Find(id);
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
