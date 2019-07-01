using System;

namespace Aplicacion.Models.Solicitud
{
    public class Get
    {
        public Int32 Id_solicitud { get; set; }
        public String Direccion { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime? Fin { get; set; }
        public String Usuario { get; set; }
        public Int32 Id_estado { get; set; }
        public Int32 Id_servicio { get; set; }
        public Int32 Id_equipo { get; set; }
    }
}
