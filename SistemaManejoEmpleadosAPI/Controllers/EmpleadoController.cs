using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public EmpleadoController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> PostEmpleado(SistemamanejoEmpleadosModel.Empleado newEmpleado)
        {
            Models.Empleado empleado = new Models.Empleado
            {
                Dpiempleado = newEmpleado.Dpiempleado,
                IdDepartamento = newEmpleado.IdDepartamento,
                IdDireccion = newEmpleado.IdDireccion,
                IdPuesto = newEmpleado.IdPuesto,
            };
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Empleado>> GetEmpleados()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Empleado> empleados =
                  await _context.Empleados.Select(s =>
                  new SistemamanejoEmpleadosModel.Empleado
                  {
                        Dpiempleado = s.Dpiempleado,
                        IdDepartamento = s.IdDepartamento,
                        IdDireccion = s.IdDireccion,
                        IdPuesto = s.IdPuesto,
                  }).ToListAsync();

            return empleados;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Empleado>> GetEmpleado(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Empleado> empleado =
                  await _context.Empleados.Select(s =>
                  new SistemamanejoEmpleadosModel.Empleado
                  {
                      Dpiempleado = s.Dpiempleado,
                      IdDepartamento = s.IdDepartamento,
                      IdDireccion = s.IdDireccion,
                      IdPuesto = s.IdPuesto,
                  }).ToListAsync();

            return empleado.Where(s => s.Dpiempleado == id).FirstOrDefault();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(SistemamanejoEmpleadosModel.Empleado editedEmpleado)
        {
            try
            {
                Models.Empleado empleado = new Models.Empleado
                {
                    Dpiempleado = editedEmpleado.Dpiempleado,
                    IdDepartamento = editedEmpleado.IdDepartamento,
                    IdDireccion = editedEmpleado.IdDireccion,
                    IdPuesto = editedEmpleado.IdPuesto,
                };
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Empleado> empleados =
                  await _context.Empleados.Select(s =>
                  new SistemamanejoEmpleadosModel.Empleado
                  {
                      Dpiempleado = s.Dpiempleado,
                      IdDepartamento = s.IdDepartamento,
                      IdDireccion = s.IdDireccion,
                      IdPuesto = s.IdPuesto,
                  }).ToListAsync();

            var elegida = empleados.Where(s => s.Dpiempleado == id).FirstOrDefault();

            Models.Empleado empleado = new Models.Empleado
            {
                Dpiempleado = elegida.Dpiempleado,
                IdDepartamento = elegida.IdDepartamento,
                IdDireccion = elegida.IdDireccion,
                IdPuesto = elegida.IdPuesto,
            };
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion

    }
}
