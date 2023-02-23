using Microsoft.AspNetCore.Mvc;
using SistemaManejoEmpleados.Models;
using System.Xml.Linq;
using SistemamanejoEmpleadosModel;

namespace SistemaManejoEmpleados.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            return View(_sistemaempleadosContext.Personas.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            //clic derecho en iactionresult, razor view create template create utilizar model class, utilizar layout.
            return View();
        }
        [HttpPost]
        public IActionResult Create(int dpi, string nombre, string primerapellido,
            string segundoapellido, string email, string direccion, int telefono,
            int celular, DateTime fechaNacimiento, decimal salario, string contraseña,
            int estado, sbyte esadmin)
        {
            //clic derecho en iactionresult, razor view create template create utilizar model class, utilizar layout.
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            Models.Persona persona = new Models.Persona
            {
                Dpi = dpi,
                Nombre = nombre,
                PrimerApellido = primerapellido,
                SegundoApellido = segundoapellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Celular = celular,
                FechaNacimiento = fechaNacimiento,
                FechaContratación = DateTime.Now,
                Salario = salario,
                Contraseña = contraseña,
                Estado = estado,
                EsAdmin = esadmin
            };
            _sistemaempleadosContext.Personas.Add(persona);
            _sistemaempleadosContext.SaveChanges();
            //en el formulario el form hay que ponerle method="post"
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("Persona/Delete/{Dpi}")]
        public ActionResult Delete(int Dpi)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            _sistemaempleadosContext.Personas.Remove(_sistemaempleadosContext.Personas.Find(Dpi));
            _sistemaempleadosContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("Persona/Edit/{Dpi}")]
        public ActionResult Update(int Dpi)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();

            return View(_sistemaempleadosContext.Personas.Find(Dpi));
        }


        [HttpPost]
        public IActionResult Edit(int dpi, string nombre, string primerapellido,
        string segundoapellido, string email, string direccion, int telefono,
        int celular, DateTime fechaNacimiento, decimal salario, string contraseña,
        int estado, sbyte esadmin)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            Models.Persona persona = new Models.Persona
            {
                Dpi = dpi,
                Nombre = nombre,
                PrimerApellido = primerapellido,
                SegundoApellido = segundoapellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Celular = celular,
                FechaNacimiento = fechaNacimiento,
                FechaContratación = DateTime.Now,
                Salario = salario,
                Contraseña = contraseña,
                Estado = estado,
                EsAdmin = esadmin
            };
            _sistemaempleadosContext.Personas.Update(persona);
            _sistemaempleadosContext.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
