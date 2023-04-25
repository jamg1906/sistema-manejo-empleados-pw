using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaManejoEmpleados.Functions;
using SistemaManejoEmpleados.Models;

namespace SistemaManejoEmpleados.Controllers
{
    public class DirectorController : Controller
    {
        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Persona/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var personas = JsonConvert.DeserializeObject<IEnumerable<Models.Persona>>(apiResponse);
                ViewBag.Dpi = (from persona in personas
                               select new SelectListItem
                               {
                                   Value = Convert.ToString(persona.Dpi),
                                   Text = persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido,
                               }).ToList();

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string dpi, int Bonos)
        {
            SistemamanejoEmpleadosModel.Director director = new SistemamanejoEmpleadosModel.Director
            {
                Dpi = int.Parse(dpi),
                Bonos = Bonos
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Director/", director);
                string apiResponse = await response.Content.ReadAsStringAsync();

            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Reads
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Director/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var directores = JsonConvert.DeserializeObject<IEnumerable<Models.Director>>(apiResponse);
                return View(directores);
            }
        }

        [Route("Director/Details/{Dpi}")]
        public async Task<ActionResult> Details(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Director/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var unDirector = JsonConvert.DeserializeObject<Models.Director>(apiResponse);
                return View(unDirector);
            }
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Director/Edit/{Dpi}")]
        public async Task<ActionResult> Edit(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Director/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var director = JsonConvert.DeserializeObject<Models.Director>(apiResponse);

                return View(director);
            }
        }



        [HttpPost]
        [Route("Director/Edit/{Dpi}")]
        public async Task<IActionResult> Editar(int Dpi, int Bonos)
        {
            SistemamanejoEmpleadosModel.Director director = new SistemamanejoEmpleadosModel.Director
            {
                Dpi = Dpi,
                Bonos = Bonos
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Director/{Dpi}", director);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        [HttpGet]
        [Route("Director/Delete/{Dpi}")]
        public async Task<ActionResult> Delete(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Director/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
