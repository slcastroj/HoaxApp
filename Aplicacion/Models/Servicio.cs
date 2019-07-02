using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Models.Servicio
{
    public class Get
    {
        public int Id_servicio { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public int Costo { get; set; }
    }
}
