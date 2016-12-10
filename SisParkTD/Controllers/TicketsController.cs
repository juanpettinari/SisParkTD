using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SisParkTD.DAL;
using SisParkTD.Models;
using static System.String;

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
            if (patente == Empty)
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
                    var ticket = tickets.Single(t => t.EstadoDeTicket == EstadoDeTicket.Inactivo && t.TipoDeTicket == TipoDeTicket.Abono);

                    ticket.EstadoDeTicket = EstadoDeTicket.Activo;
                    var movimientoDeVehiculo = new MovimientoDeVehiculo
                    {
                        TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Entrada,
                        Fecha = DateTime.Now
                    };

                    ticket.MovimientosDeVehiculo.Add(movimientoDeVehiculo);
                    _db.SaveChanges();
                    var auditoriaController = new AuditoriasController();
                    auditoriaController.AuditTicket(ticket);
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
                            var auditoriaController = new AuditoriasController();
                            auditoriaController.AuditTicket(ticket);
                        }

                        if (ticket.TipoDeTicket == TipoDeTicket.Ocasional)
                        {
                            ViewBag.infoParcela = "Indicar al conductor que se dirija a la parcela: " +
                                                  ticket.Parcela.NumeroParcela;
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
                                   tickets.Where(
                                       t =>
                                           t.VehiculoId == vehiculo.VehiculoId &&
                                           t.EstadoDeTicket == EstadoDeTicket.Activo)
                                       .Select(t => t.Parcela.NumeroParcela)
                                       .Single();
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
                        var auditoriaController = new AuditoriasController();
                        auditoriaController.AuditTicket(ticket);
                        return RedirectToAction("IngresarVehiculo");
                    }
                case TipoDeTicket.Ocasional:
                    {
                        //Logica Liberar Parcela
                        var parcela = _db.Parcelas.Find(ticket.ParcelaId);
                        parcela.Disponible = true;
                        _db.Entry(parcela).State = EntityState.Modified;

                        ticket.EstadoDeTicket = EstadoDeTicket.Inactivo;
                        ticket.Pagado = true;


                        var movimientoDeVehiculo = new MovimientoDeVehiculo
                        {
                            Fecha = DateTime.Now,
                            TipoDeMovimientoDeVehiculo = TipoDeMovimientoDeVehiculo.Salida,
                            TicketId = ticketId
                        };
                        _db.MovimientosDeVehiculo.Add(movimientoDeVehiculo);


                        var timeSpan = DateTime.Now.Subtract(ticket.MovimientosDeVehiculo.Where(mdv => mdv.TipoDeMovimientoDeVehiculo == TipoDeMovimientoDeVehiculo.Entrada).Select(mdv => mdv.Fecha).Single());
                        ticket.TiempoTotal = (int)timeSpan.TotalSeconds;

                        var tiempoEnHoras = timeSpan.TotalHours;
                        //Si está menos de 5 minutos, no se cobra

                        if (tiempoEnHoras < 0.0833)
                            ticket.PrecioTotalDecimal = 0;
                        else if (tiempoEnHoras < 0.25)
                            ticket.PrecioTotalDecimal = ticket.Vehiculo.TipoDeVehiculo.Tarifa15MDecimal;
                        else if (tiempoEnHoras < 0.50)
                            ticket.PrecioTotalDecimal = ticket.Vehiculo.TipoDeVehiculo.Tarifa30MDecimal;
                        else if (tiempoEnHoras < 1)
                            ticket.PrecioTotalDecimal = ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal;
                        else if (tiempoEnHoras >= 1)
                            ticket.PrecioTotalDecimal = ticket.Vehiculo.TipoDeVehiculo.TarifaOcasionalDecimal *
                                                        (decimal)tiempoEnHoras;

                        // TODO MODIFICAR LO DE ABAJO PARA QUE QUEDE ADENTRO DEL OTRO BLOQUE DE CODIGO,
                        // Y NO TENER QUE HACER ESTE IF DE ABAJO
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
                        var auditoriaController = new AuditoriasController();
                        auditoriaController.AuditTicket(ticket);

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
        public ActionResult Index(int? page, string ddTipoDeTicket, string ddPatente, string ddEstadoDeTicket, string ddPagado)
        {
            ViewBag.TipoDeTicket = ddTipoDeTicket;
            ViewBag.Patente = ddPatente;
            ViewBag.EstadoDeTicket = ddEstadoDeTicket;
            ViewBag.Pagado = ddPagado;

            var listaTipoDeTicket = new List<string>();
            var consultaTipodeTicket = from m in _db.Tickets
                                       orderby m.TipoDeTicket
                                       select m.TipoDeTicket.ToString();
            listaTipoDeTicket.AddRange(consultaTipodeTicket.Distinct());
            ViewBag.ddTipoDeTicket = new SelectList(listaTipoDeTicket);

            var listaPatente = new List<string>();
            var consultaPatente = from m in _db.Tickets
                                  orderby m.Vehiculo.Patente
                                  select m.Vehiculo.Patente;
            listaPatente.AddRange(consultaPatente.Distinct());
            ViewBag.ddPatente = new SelectList(listaPatente);

            var listaEstadoDeTicket = new List<string>();
            var consultaEstadoDeTicket = from m in _db.Tickets
                                         orderby m.EstadoDeTicket
                                         select m.EstadoDeTicket.ToString();
            listaEstadoDeTicket.AddRange(consultaEstadoDeTicket.Distinct());
            ViewBag.ddEstadoDeTicket = new SelectList(listaEstadoDeTicket);

            var listaPagado = new List<string>();
            var consultaPagado = from m in _db.Tickets
                                 orderby m.Pagado
                                 select m.Pagado.ToString();
            listaPagado.AddRange(consultaPagado.Distinct());
            ViewBag.ddPagado = new SelectList(listaPagado);

            var tickets = from t in _db.Tickets select t;

            if (!IsNullOrEmpty(ddTipoDeTicket))
            {
                tickets = tickets.Where(m => m.TipoDeTicket.ToString() == ddTipoDeTicket);
            }

            if (!IsNullOrEmpty(ddPatente))
            {
                tickets = tickets.Where(m => m.Vehiculo.Patente == ddPatente);
            }

            if (!IsNullOrEmpty(ddEstadoDeTicket))
            {
                tickets = tickets.Where(m => m.EstadoDeTicket.ToString() == ddEstadoDeTicket);
            }

            if (!IsNullOrEmpty(ddPagado))
            {
                tickets = tickets.Where(m => m.Pagado.ToString() == ddPagado);
            }

            const int pageSize = 9;
            var pageNumber = page ?? 1;
            tickets = tickets.OrderBy(t => t.FechaYHoraCreacionTicket);

            return View(tickets.ToPagedList(pageNumber, pageSize));
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
