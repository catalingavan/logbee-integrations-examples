using KissLog.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KissLogExample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello world from {Controller}", "Home");

            _logger.LogAsFile(JsonSerializer.Serialize(new { Prop = "Value" }), "File.json");

            return Content("HomeController Index() action");
        }
    }
}
