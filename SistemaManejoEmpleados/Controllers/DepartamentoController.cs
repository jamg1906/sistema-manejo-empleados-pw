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
    public class DepartamentoController : Controller
    {
        #region Reads
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Departamento/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var departamentos = JsonConvert.DeserializeObject<IEnumerable<Models.Departamento>>(apiResponse);
                foreach (var departamento in departamentos)
                {
                    using (var httpClient2 = new HttpClient())
                    {
                        var response2 = await httpClient2.GetAsync($"{APIServices.baseurl}Direccion/{departamento.IdDireccion}");
                        string apiResponse2 = await response2.Content.ReadAsStringAsync();
                        var laDireccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse2);
                        departamento.IdDireccionNavigation = laDireccion;
                    }
                }
                return View(departamentos);
            }
        }

        [Route("Departamento/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Departamento/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var departamento = JsonConvert.DeserializeObject<Models.Departamento>(apiResponse);
                return View(departamento);
            }
        }
        #endregion

        public async static Task<string> DireccionName(int idDireccion)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Direccion/{idDireccion}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direccion = JsonConvert.DeserializeObject<Models.Direccion>(apiResponse);
                return direccion.NombreDireccion + " | " + direccion.DescripcionDireccion;
            }
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Direccion/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var direcciones = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse);
                ViewBag.direccion = (from direccion in direcciones
                               select new SelectListItem
                               {
                                   Value = Convert.ToString(direccion.IdDireccion),
                                   Text = DireccionName(direccion.IdDireccion).Result
                               }).ToList();
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(int idDireccion, string nombreDepartamento, string descripcionDepartamento)
        {

            SistemamanejoEmpleadosModel.Departamento departamento = new SistemamanejoEmpleadosModel.Departamento
            {
                IdDireccion = idDireccion,
                NombreDepartamento = nombreDepartamento,
                DescripcionDepartamento = descripcionDepartamento,
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Departamento/", departamento);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }

        #endregion

        #region Update
        [HttpGet]
        [Route("Departamento/Edit/{idDepartamento}")]
        public async Task<ActionResult> Edit(int idDepartamento)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Departamento/{idDepartamento}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var departamento = JsonConvert.DeserializeObject<Models.Departamento>(apiResponse);

                var response2 = await httpClient.GetAsync(APIServices.baseurl + "Direccion/");
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var direccions = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse2);
                ViewBag.direcciones = (from direccion in direccions
                               select new SelectListItem
                               {
                                   Value = Convert.ToString(direccion.IdDireccion),
                                   Text = DireccionName(direccion.IdDireccion).Result
                               }).ToList();

                return View(departamento);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int idDepartamento, int idDireccion, string nombreDepartamento, string descripcionDepartamento)
        {
            SistemamanejoEmpleadosModel.Departamento departamento = new SistemamanejoEmpleadosModel.Departamento
            {
                IdDepartamento = idDepartamento,
                IdDireccion = idDireccion,
                NombreDepartamento = nombreDepartamento,
                DescripcionDepartamento = descripcionDepartamento
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Departamento/{idDepartamento}", departamento);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete

        [HttpGet]
        [Route("Departamento/Delete/{idDepartamento}")]
        public async Task<ActionResult> Delete(int idDepartamento)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Departamento/{idDepartamento}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
