using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public DepartamentoController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> PostDepartamento(SistemamanejoEmpleadosModel.Departamento newDepartamento)
        {
            Models.Departamento departamento = new Models.Departamento
            {
                IdDepartamento = newDepartamento.IdDepartamento,
                IdDireccion = newDepartamento.IdDireccion,
                NombreDepartamento = newDepartamento.NombreDepartamento,
                DescripcionDepartamento = newDepartamento.DescripcionDepartamento
            };
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Departamento>> GetDepartamentos()
        {
            var departamentos2 = await _context.Departamentos.Include(e => e.IdDireccionNavigation).FirstOrDefaultAsync(); ;
            IEnumerable<SistemamanejoEmpleadosModel.Departamento> departamentos =
                  await _context.Departamentos.Select(s =>
                  new SistemamanejoEmpleadosModel.Departamento
                  {
                      IdDepartamento = s.IdDepartamento,
                      IdDireccion = s.IdDireccion,
                      NombreDepartamento = s.NombreDepartamento,
                      DescripcionDepartamento = s.DescripcionDepartamento                  
                  }).ToListAsync();

            return departamentos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Departamento>> GetDepartamento(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Departamento> departamentos =
                  await _context.Departamentos.Select(s =>
                  new SistemamanejoEmpleadosModel.Departamento
                  {
                      IdDepartamento = s.IdDepartamento,
                      IdDireccion = s.IdDireccion,
                      NombreDepartamento = s.NombreDepartamento,
                      DescripcionDepartamento = s.DescripcionDepartamento
                  }).ToListAsync();

            return departamentos.Where(s => s.IdDepartamento == id).FirstOrDefault();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(SistemamanejoEmpleadosModel.Departamento editedDepartamento)
        {
            try
            {
                Models.Departamento departamento = new Models.Departamento
                {
                    IdDepartamento = editedDepartamento.IdDepartamento,
                    IdDireccion = editedDepartamento.IdDireccion,
                    NombreDepartamento = editedDepartamento.NombreDepartamento,
                    DescripcionDepartamento = editedDepartamento.DescripcionDepartamento
                };
                _context.Departamentos.Update(departamento);
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
        public async Task<IActionResult> DeleteDepartamento(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Departamento> departamentos =
                  await _context.Departamentos.Select(s =>
                  new SistemamanejoEmpleadosModel.Departamento
                  {
                      IdDepartamento = s.IdDepartamento,
                      IdDireccion = s.IdDireccion,
                      NombreDepartamento = s.NombreDepartamento,
                      DescripcionDepartamento = s.DescripcionDepartamento
                  }).ToListAsync();

            var elegida = departamentos.Where(s => s.IdDepartamento == id).FirstOrDefault();

            Models.Departamento departamento = new Models.Departamento
            {
                IdDepartamento = elegida.IdDepartamento,
                IdDireccion = elegida.IdDireccion,
                NombreDepartamento = elegida.NombreDepartamento,
                DescripcionDepartamento = elegida.DescripcionDepartamento
            };
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion
    }
}
