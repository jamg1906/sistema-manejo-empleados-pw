using Microsoft.AspNetCore.Mvc;
using SistemaManejoEmpleados.Models;
using System.Diagnostics;

namespace SistemaManejoEmpleados.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
            //Scaffold-DbContext "server=127.0.0.1;userid=root;password=Zangetsu;database=sistemaempleados;TreatTinyAsBoolean=False" MySql.EntityFrameworkCore -OutputDir Models -f

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}