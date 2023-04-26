using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public TareaController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> PostTarea(SistemamanejoEmpleadosModel.Tarea newTarea)
        {
            Models.Tarea tarea = new Models.Tarea
            {
                Idtarea = newTarea.Idtarea,
                DpiempleadoAsignado = newTarea.DpiempleadoAsignado,
                NombreTarea = newTarea.NombreTarea,
                RequerimientosTarea = newTarea.RequerimientosTarea,
                FechaCreacion = newTarea.FechaCreacion,
                FechaLimite = newTarea.FechaLimite,
                Estado = newTarea.Estado,
                Comentarios = newTarea.Comentarios
            };
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Reads
        [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Tarea>> GetTareas()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Tarea> tareas =
                  await _context.Tareas.Select(s =>
                  new SistemamanejoEmpleadosModel.Tarea
                  {
                      Idtarea = s.Idtarea,
                      DpiempleadoAsignado = s.DpiempleadoAsignado,
                      NombreTarea = s.NombreTarea,
                      RequerimientosTarea = s.RequerimientosTarea,
                      FechaCreacion = s.FechaCreacion,
                      FechaLimite = s.FechaLimite,
                      Estado = s.Estado,
                      Comentarios = s.Comentarios
                  }).ToListAsync();

            return tareas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Tarea>> GetTarea(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Tarea> tareas =
                  await _context.Tareas.Select(s =>
                  new SistemamanejoEmpleadosModel.Tarea
                  {
                      Idtarea = s.Idtarea,
                      DpiempleadoAsignado = s.DpiempleadoAsignado,
                      NombreTarea = s.NombreTarea,
                      RequerimientosTarea = s.RequerimientosTarea,
                      FechaCreacion = s.FechaCreacion,
                      FechaLimite = s.FechaLimite,
                      Estado = s.Estado,
                      Comentarios = s.Comentarios
                  }).ToListAsync();

            return tareas.Where(s => s.Idtarea == id).FirstOrDefault();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(SistemamanejoEmpleadosModel.Tarea editedTarea)
        {
            try
            {
                Models.Tarea tarea = new Models.Tarea
                {
                    Idtarea = editedTarea.Idtarea,
                    DpiempleadoAsignado = editedTarea.DpiempleadoAsignado,
                    NombreTarea = editedTarea.NombreTarea,
                    RequerimientosTarea = editedTarea.RequerimientosTarea,
                    FechaCreacion = editedTarea.FechaCreacion,
                    FechaLimite = editedTarea.FechaLimite,
                    Estado = editedTarea.Estado,
                    Comentarios = editedTarea.Comentarios
                };
                _context.Tareas.Update(tarea);
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
        public async Task<IActionResult> DeleteTarea(long id)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Tarea> tareas =
                  await _context.Tareas.Select(s =>
                  new SistemamanejoEmpleadosModel.Tarea
                  {
                      Idtarea = s.Idtarea,
                      DpiempleadoAsignado = s.DpiempleadoAsignado,
                      NombreTarea = s.NombreTarea,
                      RequerimientosTarea = s.RequerimientosTarea,
                      FechaCreacion = s.FechaCreacion,
                      FechaLimite = s.FechaLimite,
                      Estado = s.Estado,
                      Comentarios = s.Comentarios
                  }).ToListAsync();

            var elegida = tareas.Where(s => s.Idtarea == id).FirstOrDefault();

            Models.Tarea tarea = new Models.Tarea
            {
                Idtarea = elegida.Idtarea,
                DpiempleadoAsignado = elegida.DpiempleadoAsignado,
                NombreTarea = elegida.NombreTarea,
                RequerimientosTarea = elegida.RequerimientosTarea,
                FechaCreacion = elegida.FechaCreacion,
                FechaLimite = elegida.FechaLimite,
                Estado = elegida.Estado,
                Comentarios = elegida.Comentarios
            };
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync(); ;
            return Ok();
        }
        #endregion
    }
}
