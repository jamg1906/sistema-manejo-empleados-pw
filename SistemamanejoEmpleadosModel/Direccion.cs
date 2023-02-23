using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemamanejoEmpleadosModel
{
    public class Direccion
    {
        public int IdDireccion { get; set; }

        public string NombreDireccion { get; set; } = null!;

        public string DescripcionDireccion { get; set; } = null!;

        public int? Dpidirector { get; set; }
    }
}
