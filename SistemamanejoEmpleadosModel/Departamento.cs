using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemamanejoEmpleadosModel
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        public int IdDireccion { get; set; }

        public string NombreDepartamento { get; set; } = null!;

        public string? DescripcionDepartamento { get; set; }
    }
}
