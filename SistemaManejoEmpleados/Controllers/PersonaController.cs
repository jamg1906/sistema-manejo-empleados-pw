using Microsoft.AspNetCore.Mvc;
using SistemaManejoEmpleados.Models;
using System.Xml.Linq;
using SistemamanejoEmpleadosModel;
using Newtonsoft.Json;

namespace SistemaManejoEmpleados.Controllers
{
    public class PersonaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7101/api/Persona/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var personas = JsonConvert.DeserializeObject<IEnumerable<Models.Persona>>(apiResponse);
                return View(personas);
            }
        }

        [Route("Persona/Details/{Dpi}")]
        public async Task<ActionResult> Details(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7101/api/Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonConvert.DeserializeObject<Models.Persona>(apiResponse);
                return View(persona);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            //clic derecho en iactionresult, razor view create template create utilizar model class, utilizar layout.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int dpi, string nombre, string primerapellido,
            string segundoapellido, string email, string direccion, int telefono,
            int celular, DateTime fechaNacimiento, decimal salario, string contraseña,
            int estado, sbyte esadmin)
        {
            SistemamanejoEmpleadosModel.Persona person = new SistemamanejoEmpleadosModel.Persona
            {
                Dpi = dpi,
                Nombre = nombre,
                PrimerApellido = primerapellido,
                SegundoApellido = segundoapellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Celular = celular,
                FechaNacimiento = fechaNacimiento,
                FechaContratación = DateTime.Now,
                Salario = salario,
                Contraseña = contraseña,
                Estado = estado,
                EsAdmin = esadmin
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7101/api/Persona/", person);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("Persona/Delete/{Dpi}")]
        public async Task<ActionResult> Delete(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7101/api/Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }

        [Route("Persona/Edit/{Dpi}")]
        public async Task<ActionResult> Update(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7101/api/Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonConvert.DeserializeObject<Models.Persona>(apiResponse);
                return View(persona);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int dpi, string nombre, string primerapellido,
        string segundoapellido, string email, string direccion, int telefono,
        int celular, DateTime fechaNacimiento, decimal salario, string contraseña,
        int estado, sbyte esadmin)
        {
            SistemamanejoEmpleadosModel.Persona person = new SistemamanejoEmpleadosModel.Persona
            {
                Dpi = dpi,
                Nombre = nombre,
                PrimerApellido = primerapellido,
                SegundoApellido = segundoapellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Celular = celular,
                FechaNacimiento = fechaNacimiento,
                FechaContratación = DateTime.Now,
                Salario = salario,
                Contraseña = contraseña,
                Estado = estado,
                EsAdmin = esadmin
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7101/api/Persona/{dpi}", person);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }

    }
}
