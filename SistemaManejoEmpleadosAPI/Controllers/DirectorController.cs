using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public DirectorController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> PostDirector(SistemamanejoEmpleadosModel.Director newDirector)
        {
            Models.Director director = new Models.Director
            {
                Dpi = newDirector.Dpi,
                Bonos = newDirector.Bonos
            };
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        // GET: api/Director
        //Este es el get general para mi index.
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Director>> GetDirectores()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Director> directors =
                  await _context.Directors.Select(s =>
                  new SistemamanejoEmpleadosModel.Director
                  {
                      Dpi = s.Dpi,
                      Bonos = s.Bonos,
                  }).ToListAsync();

            return directors;
        }
        // GET: api/Director/5
        //este sería el read
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Director>> GetDirector(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Director> directors =
                  await _context.Directors.Select(s =>
                  new SistemamanejoEmpleadosModel.Director
                  {
                      Dpi = s.Dpi,
                      Bonos= s.Bonos,
                  }).ToListAsync();

            return directors.Where(s => s.Dpi == id).FirstOrDefault();
        }
        #endregion

        #region Update
        // PUT: api/Director/5
        //este sería el update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(SistemamanejoEmpleadosModel.Director editedDirector)
        {
            try
            {
                Models.Director director = new Models.Director
                {
                    Dpi = editedDirector.Dpi,
                    Bonos = editedDirector.Bonos,
                };
                _context.Directors.Update(director);
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

        [HttpDelete("{Dpi}")]
        public async Task<IActionResult> DeleteDirector(long Dpi)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Director> directors =
                  await _context.Directors.Select(s =>
                  new SistemamanejoEmpleadosModel.Director
                  {
                      Dpi = s.Dpi,
                      Bonos = s.Bonos,
                  }).ToListAsync();

            var elegida = directors.Where(s => s.Dpi == Dpi).FirstOrDefault();

            Models.Director director = new Models.Director
            {
                Dpi = elegida.Dpi,
                Bonos = elegida.Bonos,
            };
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion
    }
}
