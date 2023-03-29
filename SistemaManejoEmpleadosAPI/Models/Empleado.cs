using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleadosAPI.Models;

public partial class Empleado
{
    public int Dpiempleado { get; set; }

    public int? IdDireccion { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdPuesto { get; set; }

    public virtual Persona DpiempleadoNavigation { get; set; } = null!;

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Direccion? IdDireccionNavigation { get; set; }

    public virtual Puesto? IdPuestoNavigation { get; set; }

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();
}
