using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Persona
{
    public int Dpi { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Telefono { get; set; }

    public int? Celular { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaContratación { get; set; }

    public decimal Salario { get; set; }

    public string Contraseña { get; set; } = null!;

    public int Estado { get; set; }

    public sbyte EsAdmin { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Empleado? Empleado { get; set; }
}
