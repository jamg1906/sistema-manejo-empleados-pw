using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Direccion
{
    public int IdDireccion { get; set; }

    public string NombreDireccion { get; set; } = null!;

    public string DescripcionDireccion { get; set; } = null!;

    public int? Dpidirector { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual Director? DpidirectorNavigation { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
