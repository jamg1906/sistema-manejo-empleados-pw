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
    public class PuestoController : Controller
    {
        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombrePuesto, string descripcionPuesto)
        {
            SistemamanejoEmpleadosModel.Puesto puesto = new SistemamanejoEmpleadosModel.Puesto
            {
                nombrePuesto = nombrePuesto,
                descripcionPuesto = descripcionPuesto
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Puesto/", puesto);
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
                var response = await httpClient.GetAsync(APIServices.baseurl + "Puesto/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var puestos = JsonConvert.DeserializeObject<IEnumerable<Models.Puesto>>(apiResponse);
                return View(puestos);
            }
        }

        [Route("Puesto/Details/{idPuesto}")]
        public async Task<ActionResult> Details(int idPuesto)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Puesto/{idPuesto}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var unPuesto = JsonConvert.DeserializeObject<Models.Puesto>(apiResponse);
                return View(unPuesto);
            }
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Puesto/Edit/{idPuesto}")]
        public async Task<ActionResult> Edit(int idPuesto)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Puesto/{idPuesto}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var puesto = JsonConvert.DeserializeObject<Models.Puesto>(apiResponse);

                return View(puesto);
            }
        }



        [HttpPost]
        [Route("Puesto/Edit/{idPuesto}")]
        public async Task<IActionResult> Editar(int idPuesto, string nombrePuesto, string descripcionPuesto)
        {
            SistemamanejoEmpleadosModel.Puesto puesto = new SistemamanejoEmpleadosModel.Puesto
            {
                idPuesto = idPuesto,
                nombrePuesto = nombrePuesto,
                descripcionPuesto = descripcionPuesto
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Puesto/{idPuesto}", puesto);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        [HttpGet]
        [Route("Puesto/Delete/{idPuesto}")]
        public async Task<ActionResult> Delete(int idPuesto)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Puesto/{idPuesto}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
