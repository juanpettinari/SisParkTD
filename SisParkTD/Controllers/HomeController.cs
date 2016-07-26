using System;
using System.Data.Entity;
using System.Web.Mvc;
using SisParkTD.DAL;

namespace SisParkTD.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpContext _db = new SpContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(int? id)
        {
            ViewBag.Message = "Your application description page.";
            if (id != null)
            {
                var ticket = _db.Tickets.Find(id);
                ticket.FechaYHoraDeEntrada = ticket.FechaYHoraDeEntrada.AddHours(-1);
                _db.Entry(ticket).State = EntityState.Modified;
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