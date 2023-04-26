using Microsoft.AspNetCore.Mvc;
using SistemaManejoEmpleados.Models;
using System.Xml.Linq;
using SistemamanejoEmpleadosModel;
using Newtonsoft.Json;
using SistemaManejoEmpleados.Functions;

namespace SistemaManejoEmpleados.Controllers
{
    public class PersonaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Persona/");
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
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Persona/{Dpi}");
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
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Persona/", person);
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
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }

        [Route("Persona/Edit/{Dpi}")]
        public async Task<ActionResult> Edit(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonConvert.DeserializeObject<Models.Persona>(apiResponse);
                return View(persona);
            }
        }



        [HttpPost]
        [Route("Persona/Edit/{Dpi}")]
        public async Task<IActionResult> Edit(int Dpi, string nombre, string primerapellido,
        string segundoapellido, string email, string direccion, int telefono,
        int celular, DateTime fechaNacimiento, decimal salario, string contraseña,
        int estado, sbyte esadmin)
        {
            SistemamanejoEmpleadosModel.Persona person = new SistemamanejoEmpleadosModel.Persona
            {
                Dpi = Dpi,
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
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Persona/{Dpi}", person);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }

    }
}
