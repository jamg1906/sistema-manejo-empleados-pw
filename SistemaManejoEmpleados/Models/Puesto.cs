using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Puesto
{
    public int IdPuesto { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public string DescripcionPuesto { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
