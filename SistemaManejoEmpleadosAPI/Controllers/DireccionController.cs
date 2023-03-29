using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public DireccionController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        // POST: api/Direccion
        //este sería el create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDireccion(SistemamanejoEmpleadosModel.Direccion newDireccion)
        {
            Models.Direccion direccion = new Models.Direccion
            {
                IdDireccion = newDireccion.IdDireccion,
                NombreDireccion = newDireccion.NombreDireccion,
                DescripcionDireccion = newDireccion.DescripcionDireccion
            };
            _context.Direccions.Add(direccion);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        // GET: api/Direccion
        //Este es el get general para mi index.
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Direccion>> GetDirecciones()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Direccion> direccions =
                  await _context.Direccions.Select(s =>
                  new SistemamanejoEmpleadosModel.Direccion
                  {
                      IdDireccion = s.IdDireccion,
                      NombreDireccion = s.NombreDireccion,
                      DescripcionDireccion = s.DescripcionDireccion
                  }).ToListAsync();

            return direccions;
        }

        // GET: api/Direccion/5
        //este sería el read
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Direccion>> GetDireccion(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Direccion> direcciones =
                  await _context.Direccions.Select(s =>
                  new SistemamanejoEmpleadosModel.Direccion
                  {
                      IdDireccion = s.IdDireccion,
                      NombreDireccion = s.NombreDireccion,
                      DescripcionDireccion = s.DescripcionDireccion
                  }).ToListAsync();

            return direcciones.Where(s => s.IdDireccion == id).FirstOrDefault();
        }
        #endregion

        #region Update
        // PUT: api/Persona/5
        //este sería el update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion(SistemamanejoEmpleadosModel.Direccion editedDireccion)
        {
            try
            {
                Models.Direccion direccion = new Models.Direccion
                {
                    IdDireccion = editedDireccion.IdDireccion,
                    NombreDireccion = editedDireccion.NombreDireccion,
                    DescripcionDireccion = editedDireccion.DescripcionDireccion,
                };
                _context.Direccions.Update(direccion);
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
        // DELETE: api/Direccion/5
        //este sería el delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Direccion> direcciones =
                  await _context.Direccions.Select(s =>
                  new SistemamanejoEmpleadosModel.Direccion
                  {
                        IdDireccion = s.IdDireccion,
                        NombreDireccion = s.NombreDireccion,
                        DescripcionDireccion = s.NombreDireccion,
                        Dpidirector = s.Dpidirector
                  }).ToListAsync();

            var elegida = direcciones.Where(s => s.IdDireccion == id).FirstOrDefault();

            Models.Direccion direccion = new Models.Direccion
            {
                IdDireccion = elegida.IdDireccion,
                NombreDireccion = elegida.NombreDireccion,
                DescripcionDireccion = elegida.NombreDireccion,
                Dpidirector = elegida.Dpidirector
            };
            _context.Direccions.Remove(direccion);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion

    }
}
