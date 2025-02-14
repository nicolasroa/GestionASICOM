using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class TextoReportesBase : Base
    {
        public int Orden { get; set; }
        public string NombreReporte { get; set; }
        public string Parrafo { get; set; }
        

    }

    public class TextoReportesInfo : TextoReportesBase
    {

    }


    public class ReporteResguardo
    {
        public ClientesInfo oCliente { get; set; }
        public List<TasacionInfo> lstTasacion { get; set; }
        public UsuarioInfo oEjecutivo { get; set; }
        public List<TextoReportesInfo> lstParrafos { get; set; }
        public SolicitudInfo oSolicitud { get; set; }
        public string VigenciaDoc { get; set; }
        public ReporteResguardo()
        {
            this.VigenciaDoc = "30";
        }
    }

}
