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
    public class EmpleadoController : Controller
    {
        #region Reads
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Empleado/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var empleados = JsonConvert.DeserializeObject<IEnumerable<Models.Empleado>>(apiResponse);
                return View(empleados);
            }
        }

        [Route("Empleado/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{APIServices.baseurl}Empleado/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var Empleado = JsonConvert.DeserializeObject<Models.Empleado>(apiResponse);
                return View(Empleado);
            }
        }
        #endregion


        #region Create
        [HttpGet]
        [Route("Empleado/GetDepartamentosList")]
        public JsonResult GetDepartamentosList(int IdDireccion)
        {
            SistemaempleadosContext _context = new SistemaempleadosContext();
            var departamentos = _context.Departamentos.Where(x => x.IdDireccion == IdDireccion).ToList();
            return Json(departamentos);
        }
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(APIServices.baseurl + "Persona/");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var personas = JsonConvert.DeserializeObject<IEnumerable<Models.Persona>>(apiResponse);
                ViewBag.personas = (from persona in personas
                                     select new SelectListItem
                                     {
                                         Value = Convert.ToString(persona.Dpi),
                                         Text = persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + " | " + persona.Dpi,
                                     }).ToList();
                var response2 = await httpClient.GetAsync(APIServices.baseurl + "Puesto/");
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var puestos = JsonConvert.DeserializeObject<IEnumerable<Models.Puesto>>(apiResponse2);
                ViewBag.puestos = (from puesto in puestos
                                    select new SelectListItem
                                    {
                                        Value = Convert.ToString(puesto.IdPuesto),
                                        Text = puesto.NombrePuesto + " | " + puesto.DescripcionPuesto,
                                    }).ToList();
                var response3 = await httpClient.GetAsync(APIServices.baseurl + "Direccion/");
                string apiResponse3 = await response3.Content.ReadAsStringAsync();
                var direcciones = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse3);
                ViewBag.Direcciones = (from direccion in direcciones
                                       select new SelectListItem
                                     {
                                         Value = Convert.ToString(direccion.IdDireccion),
                                         Text = direccion.NombreDireccion
                                     }).ToList();
                SistemaempleadosContext _context = new SistemaempleadosContext();
                ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(int DpiEmpleado, string DropDownDireccion, string DropDownDepartamentos, int idPuesto)
        {
            SistemamanejoEmpleadosModel.Empleado empleado = new SistemamanejoEmpleadosModel.Empleado
            {
                Dpiempleado = DpiEmpleado,
                IdDireccion = Convert.ToInt32(DropDownDireccion),
                IdDepartamento = Convert.ToInt32(DropDownDepartamentos),
                IdPuesto = idPuesto,
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(APIServices.baseurl + "Empleado/", empleado);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        #endregion


        #region Delete

        [HttpGet]
        [Route("Empleado/Delete/{DpiEmpleado}")]
        public async Task<ActionResult> Delete(int DpiEmpleado)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{APIServices.baseurl}Empleado/{DpiEmpleado}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Update
        [HttpGet]
        [Route("Empleado/Edit/{DpiEmpleado}")]
        public async Task<ActionResult> Edit(int DpiEmpleado)
        {
            using (var httpClient = new HttpClient())
            {
                var response2 = await httpClient.GetAsync(APIServices.baseurl + "Puesto/");
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var puestos = JsonConvert.DeserializeObject<IEnumerable<Models.Puesto>>(apiResponse2);
                ViewBag.puestos = (from puesto in puestos
                                   select new SelectListItem
                                   {
                                       Value = Convert.ToString(puesto.IdPuesto),
                                       Text = puesto.NombrePuesto + " | " + puesto.DescripcionPuesto,
                                   }).ToList();
                var response3 = await httpClient.GetAsync(APIServices.baseurl + "Direccion/");
                string apiResponse3 = await response3.Content.ReadAsStringAsync();
                var direcciones = JsonConvert.DeserializeObject<IEnumerable<Models.Direccion>>(apiResponse3);
                ViewBag.Direcciones = (from direccion in direcciones
                                       select new SelectListItem
                                       {
                                           Value = Convert.ToString(direccion.IdDireccion),
                                           Text = direccion.NombreDireccion
                                       }).ToList();
                SistemaempleadosContext _context = new SistemaempleadosContext();
                ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento");
                return View();
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int DpiEmpleado, string DropDownDireccion, string DropDownDepartamentos, int idPuesto)
        {
            SistemamanejoEmpleadosModel.Empleado empleado = new SistemamanejoEmpleadosModel.Empleado
            {
                Dpiempleado = DpiEmpleado,
                IdDireccion = Convert.ToInt32(DropDownDireccion),
                IdDepartamento = Convert.ToInt32(DropDownDepartamentos),
                IdPuesto = idPuesto
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync($"{APIServices.baseurl}Empleado/{DpiEmpleado}", empleado);
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
