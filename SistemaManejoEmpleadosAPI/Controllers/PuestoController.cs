using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public PuestoController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> PostPuesto(SistemamanejoEmpleadosModel.Puesto newPuesto)
        {
            Models.Puesto puesto = new Models.Puesto
            {
                IdPuesto = newPuesto.idPuesto,
                NombrePuesto = newPuesto.nombrePuesto,
                DescripcionPuesto = newPuesto.descripcionPuesto
            };
            _context.Puestos.Add(puesto);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Puesto>> GetPuestos()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Puesto> puestos =
                  await _context.Puestos.Select(s =>
                  new SistemamanejoEmpleadosModel.Puesto
                  {
                        idPuesto = s.IdPuesto,
                        nombrePuesto = s.NombrePuesto,
                        descripcionPuesto = s.DescripcionPuesto
                  }).ToListAsync();

            return puestos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Puesto>> GetPuesto(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Puesto> puestos =
                  await _context.Puestos.Select(s =>
                  new SistemamanejoEmpleadosModel.Puesto
                  {
                      idPuesto = s.IdPuesto,
                      nombrePuesto = s.NombrePuesto,
                      descripcionPuesto = s.DescripcionPuesto
                  }).ToListAsync();

            return puestos.Where(s => s.idPuesto == id).FirstOrDefault();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuesto(SistemamanejoEmpleadosModel.Puesto editedPuesto)
        {
            try
            {
                Models.Puesto puesto = new Models.Puesto
                {
                    IdPuesto = editedPuesto.idPuesto,
                    NombrePuesto = editedPuesto.nombrePuesto,
                    DescripcionPuesto = editedPuesto.descripcionPuesto
                };
                _context.Puestos.Update(puesto);
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
        public async Task<IActionResult> DeletePuesto(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Puesto> puestos =
                  await _context.Puestos.Select(s =>
                  new SistemamanejoEmpleadosModel.Puesto
                  {
                        idPuesto = s.IdPuesto,
                        nombrePuesto = s.NombrePuesto,
                        descripcionPuesto = s.DescripcionPuesto
                  }).ToListAsync();

            var elegida = puestos.Where(s => s.idPuesto == id).FirstOrDefault();

            Models.Puesto elPuesto = new Models.Puesto
            {
                IdPuesto = elegida.idPuesto,
                NombrePuesto = elegida.nombrePuesto,
                DescripcionPuesto = elegida.descripcionPuesto
            };
            _context.Puestos.Remove(elPuesto);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion
    }
}
