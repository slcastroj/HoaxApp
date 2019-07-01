using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Models.Usuario
{
    public class Get
    {
        public String Rut { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public DateTime FechaNac { get; set; }
        public Int32 IdTipo { get; set; }
    }

    public class GetSingle
    {
        public String Rut { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public DateTime FechaNac { get; set; }
        public Int32 IdTipo { get; set; }
    }
}
