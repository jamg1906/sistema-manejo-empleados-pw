using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Director
{
    public int Dpi { get; set; }
    public int? Bonos { get; set; }
    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();
    public virtual Persona DpiNavigation { get; set; } = null!;
    public string NombreCompletoDirector { get { return DpiNavigation.Nombre + " " + DpiNavigation.PrimerApellido + " " + DpiNavigation.SegundoApellido; } }

}
