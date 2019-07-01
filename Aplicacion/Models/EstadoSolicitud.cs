using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Models.EstadoSolicitud
{
    public class Get
    {
        public Int32 Id_Estado { get; set; }
        public String Nombre { get; set; }

        public override String ToString()
        {
            return Nombre;
        }
    }
}
