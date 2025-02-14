using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{

    public class AccionesBase : Base
    {
        public string Descripcion { get; set; }
        public int Estado_Id { get; set; }
        public int Sentido_Id { get; set; }
        public int EstadoEvento_Id { get; set; }
        public int EstadoSolicitud_Id { get; set; }

    }
    public class AccionesInfo : AccionesBase
    {
        public string DescripcionSentido { get; set; }
        public string DescripcionEstado { get; set; }
        public int Evento_Id { get; set; }
    }
}
