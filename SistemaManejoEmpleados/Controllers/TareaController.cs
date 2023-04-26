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
using static System.Net.Mime.MediaTypeNames;

namespace SistemaManejoEmpleados.Controllers
{
    public class TareaController : Controller
    {
        #region Reads
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Tarea/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var tareas = JsonConvert.DeserializeObject<IEnumerable<Models.Tarea>>(apiResponse);
                return View(tareas);
            }
        }

        [Route("Tarea/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Tarea/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var tarea = JsonConvert.DeserializeObject<Models.Tarea>(apiResponse);
                return View(tarea);
            }
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Empleado/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var empleados = JsonConvert.DeserializeObject<IEnumerable<Models.Empleado>>(apiResponse);
                foreach (var empleado in empleados)
                {
                    using (var httpClient2 = new HttpClient())
                    {
                        var response2 = await httpClient2.GetAsync($"{APIServices.baseurl}Persona/{empleado.Dpiempleado}");
                        string apiResponse2 = await response2.Content.ReadAsStringAsync();
                        var elempleado = JsonConvert.DeserializeObject<Models.Persona>(apiResponse2);
                        empleado.DpiempleadoNavigation = elempleado;
                    }
                }
                ViewBag.empleados = (from empleado in empleados
                                     select new SelectListItem
                                     {
                                         Value = Convert.ToString(empleado.Dpiempleado),
                                         Text = empleado.DpiempleadoNavigation.Nombre + " " + empleado.DpiempleadoNavigation.PrimerApellido + " " + empleado.DpiempleadoNavigation.SegundoApellido + " | " + empleado.Dpiempleado,
                                     }).ToList();
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(int DpiempleadoAsignado, string nombreTarea, string requerimientosTarea,
            DateTime fechaLimite, string estado, string comentarios)
        {

            SistemamanejoEmpleadosModel.Tarea tarea = new SistemamanejoEmpleadosModel.Tarea
            {
                DpiempleadoAsignado = DpiempleadoAsignado,
                NombreTarea = nombreTarea,
                RequerimientosTarea = requerimientosTarea,
                FechaCreacion = DateTime.Now,
                FechaLimite = fechaLimite,
                Estado = estado,
                Comentarios = comentarios,
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Tarea/", tarea);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Update
        [HttpGet]
        [Route("Tarea/Edit/{idTarea}")]
        public async Task<ActionResult> Edit(int idTarea)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Tarea/{idTarea}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var departamento = JsonConvert.DeserializeObject<Models.Tarea>(apiResponse);

                var response2 = await httpClient.GetAsync(APIServices.baseurl + "Empleado/");
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var empleados = JsonConvert.DeserializeObject<IEnumerable<Models.Empleado>>(apiResponse2);
                foreach (var empleado in empleados)
                {
                    using (var httpClient3 = new HttpClient())
                    {
                        var response3 = await httpClient3.GetAsync($"{APIServices.baseurl}Persona/{empleado.Dpiempleado}");
                        string apiResponse3 = await response3.Content.ReadAsStringAsync();
                        var elempleado = JsonConvert.DeserializeObject<Models.Persona>(apiResponse3);
                        empleado.DpiempleadoNavigation = elempleado;
                    }
                }
                ViewBag.empleados = (from empleado in empleados
                                       select new SelectListItem
                                       {
                                           Value = Convert.ToString(empleado.Dpiempleado),
                                           Text = empleado.DpiempleadoNavigation.Nombre + " " + empleado.DpiempleadoNavigation.PrimerApellido + " " + empleado.DpiempleadoNavigation.SegundoApellido + " | " + empleado.Dpiempleado,

                                       }).ToList();

                return View(departamento);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int idTarea, int DpiempleadoAsignado, string nombreTarea, string requerimientosTarea,
            DateTime fechaLimite, string estado, string comentarios)
        {
            SistemamanejoEmpleadosModel.Tarea tarea = new SistemamanejoEmpleadosModel.Tarea
            {
                Idtarea = idTarea,
                DpiempleadoAsignado = DpiempleadoAsignado,
                NombreTarea = nombreTarea,
                FechaCreacion = DateTime.Now,
                RequerimientosTarea = requerimientosTarea,
                FechaLimite = fechaLimite,
                Estado = estado,
                Comentarios = comentarios,
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Tarea/{idTarea}", tarea);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete

        [HttpGet]
        [Route("Tarea/Delete/{idTarea}")]
        public async Task<ActionResult> Delete(int idTarea)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Tarea/{idTarea}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
