using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleadosAPI.Models;

public partial class Director
{
    public int Dpi { get; set; }

    public int? Bonos { get; set; }

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Persona DpiNavigation { get; set; } = null!;
}
