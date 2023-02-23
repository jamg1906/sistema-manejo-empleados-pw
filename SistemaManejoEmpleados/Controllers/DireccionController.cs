using Microsoft.AspNetCore.Mvc;
using SistemaManejoEmpleados.Models;

namespace SistemaManejoEmpleados.Controllers
{
    public class DireccionController : Controller
    {
        public IActionResult Index()
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            return View(_sistemaempleadosContext.Direccions.ToList());
        }

        public IActionResult Create()
        {           
            return View();
        }

        [HttpGet]
        [Route("Direccion/Delete/{idDireccion}")]
        public ActionResult Delete(int idDireccion)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            _sistemaempleadosContext.Direccions.Remove(_sistemaempleadosContext.Direccions.Find(idDireccion));
            _sistemaempleadosContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(string nombreDireccion, string descripcionDireccion, int DPIdirector)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();

            Models.Direccion direccion = new Models.Direccion
            {
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion

            };
            _sistemaempleadosContext.Direccions.Add(direccion);
            _sistemaempleadosContext.SaveChanges();

            return RedirectToAction("Index");

        }


        [HttpGet]
        [Route("Direccion/Edit/{idDireccion}")]
        public ActionResult Edit(int idDireccion)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();
            return View(_sistemaempleadosContext.Direccions.Find(idDireccion));
        }


        [HttpPost]
        public IActionResult Edit(int idDireccion, string nombreDireccion, string descripcionDireccion, int DPIdirector)
        {
            SistemaempleadosContext _sistemaempleadosContext = new SistemaempleadosContext();

            Models.Direccion direccion = new Models.Direccion
            {
                IdDireccion = idDireccion,
                NombreDireccion = nombreDireccion,
                DescripcionDireccion = descripcionDireccion
            };
            _sistemaempleadosContext.Direccions.Update(direccion);
            _sistemaempleadosContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
