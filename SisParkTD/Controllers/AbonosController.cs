using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class AbonosController : Controller
    {
        private readonly SpContext _db = new SpContext();


        //Get
        public ActionResult IngresarAbono(string patente)
        {
            ViewBag.patente = patente;
            return View();
        }

        [ActionName("IngresarAbono")]
        [HttpPost]
        public async Task<ActionResult> IngresarAbono_Post(string patente)
        {

            //Revisa si existe el vehiculo, sino existe, lo crea y retorna al metodo con la patente
            if (await _db.Vehiculos.FirstOrDefaultAsync(v => v.Patente == patente) == null)
            {
                TempData["urlPrevia"] = Request.RequestContext.RouteData.Values["action"];
                return RedirectToAction("Create", "Vehiculos", new { patente });
            }

            if (await _db.Tickets.FirstOrDefaultAsync(t => t.TipoDeTicket == TipoDeTicket.Abono
                                                && t.Vehiculo.Patente == patente &&
                                                t.EstadoDeTicket != EstadoDeTicket.Historico) != null)
            {
                ViewBag.errorMessage = "Ya existe un abono para este vehiculo";
                return View();
            }
            return RedirectToAction("ConfirmarAbono", new { patente });
        }




        //GET
        public ActionResult ConfirmarAbono(string patente)
        {
            // Verificar que haya parcelas
            if (patente == null)
            {
                return RedirectToAction("IngresarAbono");
            }
            var vehiculo = _db.Vehiculos.FirstOrDefault(v => v.Patente == patente);
            if (
                _db.Parcelas.FirstOrDefault(
                    p => p.Disponible && p.TipoDeVehiculo.TamanioVehiculo == vehiculo.TipoDeVehiculo.TamanioVehiculo) ==
                null)
            {
                if (vehiculo != null)
                    return RedirectToAction("NoHayParcelas", "Tickets", new { vehiculo.TipoDeVehiculoId });
            }
            
            ViewBag.patente = patente;
            if (vehiculo?.Cliente == null) return View();
            var abonoViewModel = new AbonoViewModel
            {
                Patente = patente,
                TipoDocumento = vehiculo.Cliente.TipoDocumento,
                NumeroDocumento = vehiculo.Cliente.NumeroDocumento,
                Telefono = vehiculo.Cliente.Telefono,
                RazonSocial = vehiculo.Cliente.RazonSocial,
                AnioFin = DateTime.Now.Year,
                MesFin = (Mes)DateTime.Now.Month
                
            };
            return View(abonoViewModel);
        }

        [HttpPost]
        public ActionResult ConfirmarAbono(AbonoViewModel model)
        {
            var fechafin = DateTime.Parse("1/" + model.MesFin + "/" + model.AnioFin + " 00:00:00");
            if (fechafin < DateTime.Now)
            {
                ViewBag.errorFecha = "La fecha de fin de abono es previa al día de hoy";
                return View();
            }

            if (ModelState.IsValid)
            {
                //vehiculo>cliente>cuenta>Abono>ticket>MovimientoFinanciero
                var vehiculo = _db.Vehiculos.FirstOrDefault(v => v.Patente == model.Patente);


                // Crear Abono
                var abono = new Abono
                {
                    Activo = true,
                    FechaInicio = DateTime.Now,
                    FechaFin = fechafin
                };
                _db.Abonos.Add(abono);
                //Logica calcular precio del ticket de este mes.
                var parcela = _db.Parcelas.FirstOrDefault(p =>
                                p.Disponible &&
                                p.TipoDeVehiculo.TamanioVehiculo == vehiculo.TipoDeVehiculo.TamanioVehiculo);


                var ultimoDiaDelMes = DateTime.DaysInMonth(abono.FechaInicio.Year, abono.FechaInicio.Month);
                var ultimaFechaDelMes =
                    DateTime.Parse(ultimoDiaDelMes + "/" + abono.FechaInicio.Month + "/" + abono.FechaInicio.Year + " 23:59:59");
                var restoDelMes = ultimaFechaDelMes.Subtract(abono.FechaInicio).TotalDays / ultimoDiaDelMes;
                if (vehiculo == null) return View();

                var precioRestoDelMes = (decimal)restoDelMes * vehiculo.TipoDeVehiculo.TarifaMensualDecimal;

                //Crear Ticket
                var ticket = new Ticket
                {
                    Vehiculo = vehiculo,
                    Abono = abono,
                    Parcela = parcela,
                    EstadoDeTicket = EstadoDeTicket.Inactivo,
                    TipoDeTicket = TipoDeTicket.Abono,
                    FechaYHoraCreacionTicket = abono.FechaInicio,
                    PrecioTotalDecimal = precioRestoDelMes,
                    Pagado = false
                };
                _db.Tickets.Add(ticket);

                // Bloquear parcela
                if (parcela != null)
                {
                    parcela.Disponible = false;
                    _db.Entry(parcela).State = EntityState.Modified;
                }


                if (vehiculo.Cliente == null)
                //Crear cliente y cuenta cuando no existen
                {
                    var cliente = new Cliente
                    {
                        Vehiculo = vehiculo,
                        RazonSocial = model.RazonSocial,
                        TipoDocumento = model.TipoDocumento,
                        NumeroDocumento = model.NumeroDocumento,
                        Telefono = model.Telefono
                    };
                    _db.Clientes.Add(cliente);

                    var cuenta = new Cuenta
                    {
                        Cliente = cliente,
                        SaldoDecimal = 0
                    };
                    _db.Cuentas.Add(cuenta);
                }


                var movimientoFinanciero = new MovimientoFinanciero
                {
                    Ticket = ticket,
                    TipoDeMovimientoFinancieroId = (int)TipoDeMovimientoFinancieroenum.FacturacionAbono,
                    Cuenta = vehiculo.Cliente?.Cuenta,
                    Fecha = DateTime.Now
                };
                _db.MovimientosFinancieros.Add(movimientoFinanciero);

                if (ticket.PrecioTotalDecimal != null)
                    if (vehiculo.Cliente != null)
                        vehiculo.Cliente.Cuenta.SaldoDecimal =
                            vehiculo.Cliente.Cuenta.SaldoDecimal - (decimal)ticket.PrecioTotalDecimal;
                _db.SaveChanges();
                _db.Entry(vehiculo.Cliente?.Cuenta).State = EntityState.Modified;
                _db.SaveChanges();


                if (vehiculo.Cliente != null)
                    return RedirectToAction("VerMovimientos", "Cuentas", new { id = vehiculo.Cliente.Cuenta.CuentaId });
            }
            return View();
        }





        // GET: Abonos
        public async Task<ActionResult> Index()
        {
            return View(await _db.Abonos.ToListAsync());
        }

        public ActionResult VerAbonosActuales()
        {

            var abonosActuales = _db.Abonos.Where(aa => aa.FechaFin > DateTime.Now);
            return View(abonosActuales);
        }

        public async Task<ActionResult> VerAbonosHistoricos()
        {

            return View(await _db.Abonos.ToListAsync());
        }




        // GET: Abonos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var abono = await _db.Abonos.FindAsync(id);
            if (abono == null)
            {
                return HttpNotFound();
            }
            return View(abono);
        }


        // GET: Abonos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var abono = await _db.Abonos.FindAsync(id);
            if (abono == null)
            {
                return HttpNotFound();
            }
            return View(abono);
        }

        // POST: Abonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AbonoId,FechaMesInicio,FechaMesFin")] Abono abono)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(abono).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(abono);
        }

        // GET: Abonos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var abono = await _db.Abonos.FindAsync(id);
            if (abono == null)
            {
                return HttpNotFound();
            }
            return View(abono);
        }

        // POST: Abonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var abono = await _db.Abonos.FindAsync(id);
            _db.Abonos.Remove(abono);
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
