using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleadosAPI.Models;

public partial class Tarea
{
    public int Idtarea { get; set; }

    public int DpiempleadoAsignado { get; set; }

    public string NombreTarea { get; set; } = null!;

    public string RequerimientosTarea { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaLimite { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public virtual Empleado DpiempleadoAsignadoNavigation { get; set; } = null!;
}
