using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Models.Inspeccion
{
    public class Get
    {
        public int Id_inspeccion { get; set; }
        public DateTime Fecha_visita { get; set; }
        public String Observaciones { get; set; }
        public int Monto { get; set; }
        public int id_Solicitud { get; set; }
    }
}
