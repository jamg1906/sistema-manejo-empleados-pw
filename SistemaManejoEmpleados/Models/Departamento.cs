using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public int IdDireccion { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public string? DescripcionDepartamento { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();

    public virtual Direccion IdDireccionNavigation { get; set; } = null!;
}
