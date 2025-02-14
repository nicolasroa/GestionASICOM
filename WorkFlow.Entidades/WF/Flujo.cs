using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
   public class FlujoBase : Base
    {
        public int EventoOrigen_Id { get; set; }
        public int Accion_Id { get; set; }
        public int EventoDestino_Id { get; set; }
        public int EstadoSolicitud_Id { get; set; }
        public int SubEstadoSolicitud_Id { get; set; }
    }
    public class FlujoInfo : FlujoBase
    {
        public string DescripcionEventoOrigen { get;set;}
        public string DescripcionAccion { get; set; }
        public string DescripcionEventoDestino { get; set; }
        public string DescripcionSentido { get; set; }
        public string DescripcionEstadoSolicitud { get; set; }
        public string DescripcionSubEstadoSolicitud { get; set; }

        public int Solicitud_Id { get; set; }
        public int Secuencia { get; set; }
        public int Evento_Id { get; set; }
        public int Bandeja_Id { get; set; }
      

        public int TipoFlujo_Id { get; set; }

        





    }
}
