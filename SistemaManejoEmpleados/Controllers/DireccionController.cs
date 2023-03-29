using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using SistemaManejoEmpleados.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaManejoEmpleados.Controllers
{
    public class DireccionController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7101/api/Direccion/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direcciones = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse);
                return View(direcciones);
            }
        }

        [Route("Direccion/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7101/api/Direccion/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse);
                return View(direccion);
            }
        }
        public IActionResult Create()
        {           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(string nombreDireccion, string descripcionDireccion, int DPIdirector)
        {

            SistemamanejoEmpleadosModel.Direccion direccion = new SistemamanejoEmpleadosModel.Direccion
            {
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7101/api/Direccion/", direccion);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("Direccion/Delete/{idDireccion}")]
        public async Task<ActionResult> Delete(int idDireccion)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7101/api/Direccion/{idDireccion}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [Route("Direccion/Edit/{idDireccion}")]
        public async Task<ActionResult> Edit(int idDireccion)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7101/api/Direccion/{idDireccion}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse);
                return View(direccion);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int idDireccion, string nombreDireccion, string descripcionDireccion, int DPIdirector)
        {
            SistemamanejoEmpleadosModel.Direccion direccion = new SistemamanejoEmpleadosModel.Direccion
            {
                IdDireccion = idDireccion,
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion,
                Dpidirector = DPIdirector
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7101/api/Direccion/{idDireccion}", direccion);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
    }
}
