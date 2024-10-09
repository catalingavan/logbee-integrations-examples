using KissLog;
using System.Web.Mvc;

namespace KissLogExample.AspNet.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IKLogger _logger;
        public HomeController()
        {
            _logger = Logger.Factory.Get();
        }

        public ActionResult Index()
        {
            _logger.Debug("Hello World! from ASP.NET MVC ");
            return View();
        }
    }
}