using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SisParkTD.DAL;
using SisParkTD.Models;

namespace SisParkTD.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpContext _db = new SpContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(int? id, int h)
        {
            ViewBag.Message = "Your application description page.";
            if (id != null)
            {
                var ticket = _db.Tickets.Find(id);
                var movimiento =
                    _db.MovimientosDeVehiculo.Single(mdv => mdv.TicketId == ticket.TicketId 
                    && mdv.TipoDeMovimientoDeVehiculo == TipoDeMovimientoDeVehiculo.Entrada);
                movimiento.Fecha = movimiento.Fecha.AddHours(-h);
                    //ticket.FechaYHoraDeEntrada.AddHours(-h);
                _db.Entry(movimiento).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return RedirectToAction("RetirarVehiculo", "Tickets");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}