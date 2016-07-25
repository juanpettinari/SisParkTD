using System.Web.Mvc;
using SisParkTD.DAL;

namespace SisParkTD.Controllers
{
    public class HomeController : Controller
    {
        private SpContext _db = new SpContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}