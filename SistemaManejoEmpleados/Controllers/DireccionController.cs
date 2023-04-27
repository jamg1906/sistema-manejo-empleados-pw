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
    public class DireccionController : Microsoft.AspNetCore.Mvc.Controller
    {
        #region Reads
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl+ "Direccion/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direcciones = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse);
                foreach (var direccion in direcciones)
                {
                    using (var httpClient2 = new HttpClient())
                    {
                        var response2 = await httpClient2.GetAsync($"{APIServices.baseurl}Persona/{direccion.Dpidirector}");
                        string apiResponse2 = await response2.Content.ReadAsStringAsync();
                        var elDirector = JsonConvert.DeserializeObject<Models.Persona>(apiResponse2);
                        direccion.DpidirectorNavigation = new Director();
                        direccion.DpidirectorNavigation.DpiNavigation = elDirector;

                    }
                }
                    return View(direcciones);
            }
        }

        [Route("Direccion/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Direccion/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse);
                return View(direccion);
            }
        }
        #endregion
        public async static Task<string> DirectorName(int Dpi)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Persona/{Dpi}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var persona = JsonConvert.DeserializeObject<Models.Persona>(apiResponse);
                return persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            }
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Director/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var directors = JsonConvert.DeserializeObject<IEnumerable<Models.Director>>(apiResponse);
                ViewBag.Dpi = (from director in directors
                               select new SelectListItem
                               {
                                   Value = Convert.ToString(director.Dpi),
                                   Text = DirectorName(director.Dpi).Result + " - " + director.Dpi,
                               }).ToList();
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(string nombreDireccion, string descripcionDireccion, int DPIdirector)
        {

            SistemamanejoEmpleadosModel.Direccion direccion = new SistemamanejoEmpleadosModel.Direccion
            {
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion,
                Dpidirector = DPIdirector
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Direccion/", direccion);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }

        #endregion

        #region Delete

        [HttpGet]
        [Route("Direccion/Delete/{idDireccion}")]
        public async Task<ActionResult> Delete(int idDireccion)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Direccion/{idDireccion}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Direccion/Edit/{idDireccion}")]
        public async Task<ActionResult> Edit(int idDireccion)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Direccion/{idDireccion}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse);

                var response2 = await httpClient.GetAsync(APIServices.baseurl + "Director/");
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var directors = JsonConvert.DeserializeObject<IEnumerable<Models.Director>>(apiResponse2);
                ViewBag.Dpi = (from director in directors
                               select new SelectListItem
                               {
                                   Value = Convert.ToString(director.Dpi),
                                   Text = DirectorName(director.Dpi).Result + " - " + director.Dpi,
                               }).ToList();

                return View(direccion);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int idDireccion, string nombreDireccion, string descripcionDireccion, int Dpidirector)
        {
            SistemamanejoEmpleadosModel.Direccion direccion = new SistemamanejoEmpleadosModel.Direccion
            {
                IdDireccion = idDireccion,
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion,
                Dpidirector = Dpidirector
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Direccion/{idDireccion}", direccion);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
        #endregion
    }
}
