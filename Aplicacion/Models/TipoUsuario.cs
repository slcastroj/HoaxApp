using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Models.TipoUsuario
{
    public class Get
    {
        public Int32 Id_Tipo { get; set; }
        public String Nombre { get; set; }

        public override String ToString() => Nombre;
    }
}
