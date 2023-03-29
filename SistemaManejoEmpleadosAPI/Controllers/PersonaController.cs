using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleadosAPI.Models;
using SistemamanejoEmpleadosModel;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaManejoEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly SistemaempleadosContext _context;

        public PersonaController(SistemaempleadosContext context)
        {
            _context = context;
        }

        #region Create
            // POST: api/Persona
            //este sería el create
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<IActionResult> PostPersona(SistemamanejoEmpleadosModel.Persona newPerson)
            {
                Models.Persona persona = new Models.Persona
                {
                    Dpi = newPerson.Dpi,
                    Nombre = newPerson.Nombre,
                    PrimerApellido = newPerson.PrimerApellido,
                    SegundoApellido = newPerson.SegundoApellido,
                    Email = newPerson.Email,
                    Direccion = newPerson.Direccion,
                    Telefono = newPerson.Telefono,
                    Celular = newPerson.Celular,
                    FechaNacimiento = newPerson.FechaNacimiento,
                    FechaContratación = newPerson.FechaContratación,
                    Salario = newPerson.Salario,
                    Contraseña = newPerson.Contraseña,
                    Estado = newPerson.Estado,
                    EsAdmin = newPerson.EsAdmin
                };
                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();
                return Ok();
            }
            #endregion

        #region Reads
            // GET: api/Persona
            //Este es el get general para mi index.
            [HttpGet]
        public async Task<IEnumerable<SistemamanejoEmpleadosModel.Persona>> GetPersonas()
        {
            IEnumerable<SistemamanejoEmpleadosModel.Persona> personas =
                  await _context.Personas.Select(s =>
                  new SistemamanejoEmpleadosModel.Persona
                  {
                      Dpi = s.Dpi,
                      Nombre = s.Nombre,
                      PrimerApellido = s.PrimerApellido,
                      SegundoApellido = s.SegundoApellido,
                      Email = s.Email,
                      Direccion = s.Direccion,
                      Telefono = s.Telefono,
                      Celular = s.Celular,
                      FechaNacimiento = s.FechaNacimiento,
                      FechaContratación = s.FechaContratación,
                      Salario = s.Salario,
                      Contraseña = s.Contraseña,
                      Estado = s.Estado,
                      EsAdmin = s.EsAdmin
                  }).ToListAsync();

            return personas;
        }

        // GET: api/Persona/5
        //este sería el read
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemamanejoEmpleadosModel.Persona>> GetPersona(long id)
        {

            IEnumerable<SistemamanejoEmpleadosModel.Persona> personas =
                  await _context.Personas.Select(s =>
                  new SistemamanejoEmpleadosModel.Persona
                  {
                      Dpi = s.Dpi,
                      Nombre = s.Nombre,
                      PrimerApellido = s.PrimerApellido,
                      SegundoApellido = s.SegundoApellido,
                      Email = s.Email,
                      Direccion = s.Direccion,
                      Telefono = s.Telefono,
                      Celular = s.Celular,
                      FechaNacimiento = s.FechaNacimiento,
                      FechaContratación = s.FechaContratación,
                      Salario = s.Salario,
                      Contraseña = s.Contraseña,
                      Estado = s.Estado,
                      EsAdmin = s.EsAdmin
                  }).ToListAsync();

            return personas.Where(s => s.Dpi == id).FirstOrDefault();
        }
        #endregion

        #region Update
        // PUT: api/Persona/5
        //este sería el update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(SistemamanejoEmpleadosModel.Persona editedPerson)
        {
            try
            {
                Models.Persona person = new Models.Persona
                {
                    Dpi = editedPerson.Dpi,
                    Nombre = editedPerson.Nombre,
                    PrimerApellido = editedPerson.PrimerApellido,
                    SegundoApellido = editedPerson.SegundoApellido,
                    Email = editedPerson.Email,
                    Direccion = editedPerson.Direccion,
                    Telefono = editedPerson.Telefono,
                    Celular = editedPerson.Celular,
                    FechaNacimiento = editedPerson.FechaNacimiento,
                    FechaContratación = editedPerson.FechaContratación,
                    Salario = editedPerson.Salario,
                    Contraseña = editedPerson.Contraseña,
                    Estado = editedPerson.Estado,
                    EsAdmin = editedPerson.EsAdmin
                };
                _context.Personas.Update(person);
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
        // DELETE: api/Persona/5
        //este sería el delete
        [HttpDelete("{Dpi}")]
        public async Task<IActionResult> DeletePerson(long Dpi)
        {
            IEnumerable<SistemamanejoEmpleadosModel.Persona> personas =
                  await _context.Personas.Select(s =>
                  new SistemamanejoEmpleadosModel.Persona
                  {
                      Dpi = s.Dpi,
                      Nombre = s.Nombre,
                      PrimerApellido = s.PrimerApellido,
                      SegundoApellido = s.SegundoApellido,
                      Email = s.Email,
                      Direccion = s.Direccion,
                      Telefono = s.Telefono,
                      Celular = s.Celular,
                      FechaNacimiento = s.FechaNacimiento,
                      FechaContratación = s.FechaContratación,
                      Salario = s.Salario,
                      Contraseña = s.Contraseña,
                      Estado = s.Estado,
                      EsAdmin = s.EsAdmin
                  }).ToListAsync();

            var elegida = personas.Where(s => s.Dpi == Dpi).FirstOrDefault();

            Models.Persona persona = new Models.Persona
            {
                Dpi = elegida.Dpi,
                Nombre = elegida.Nombre,
                PrimerApellido = elegida.PrimerApellido,
                SegundoApellido = elegida.SegundoApellido,
                Email = elegida.Email,
                Direccion = elegida.Direccion,
                Telefono = elegida.Telefono,
                Celular = elegida.Celular,
                FechaNacimiento = elegida.FechaNacimiento,
                FechaContratación = elegida.FechaContratación,
                Salario = elegida.Salario,
                Contraseña = elegida.Contraseña,
                Estado = elegida.Estado,
                EsAdmin = elegida.EsAdmin
            };
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();;
            return Ok();
        }
        #endregion

    }
}
