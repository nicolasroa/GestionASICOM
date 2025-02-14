using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades.GPS
{
    public class ResultadoGPS
    {
        public bool ResultadoGeneral { get; set; }
        public string Mensaje { get; set; }
        public List<SolicitudGPS> lstSolicitudes { get; set; }
        public List<ParticipantesGPS> lstParticipantes { get; set; }
        public List<SegurosGPS> lstSeguros { get; set; }
        public List<ObservacionesGPS> lstObservaciones { get; set; }
        public List<GastosOperacionalesGPS> lstGastosOperacionales { get; set; }
        public List<ControlEtapasGPS> lstControlEtapas { get; set; }


        public ResultadoGPS()
        {
            ResultadoGeneral = true;
            Mensaje = "OK";
            lstSolicitudes = new List<SolicitudGPS>();
            lstParticipantes = new List<ParticipantesGPS>();
            lstSeguros = new List<SegurosGPS>();
            lstObservaciones = new List<ObservacionesGPS>();
            lstGastosOperacionales = new List<GastosOperacionalesGPS>();
            lstControlEtapas = new List<ControlEtapasGPS>();

        }

    }
    public class SolicitudGPS
    {
        public int NumeroSolicitud { get; set; }
        public int Rut { get; set; }
        public string DescripcionEstado { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string NombreEjecutivoComercial { get; set; }
        public string MailEjecutivoComercial { get; set; }
        public string EjecutivoFormalizacion { get; set; }
        public string MailEjecutivoFormalizacion { get; set; }
        public string DescripcionProducto { get; set; }
        public string DescripcionDestino { get; set; }
        public decimal MontoPropiedad { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal MontoContado { get; set; }
        public decimal MontoSubsidio { get; set; }
        public string DescripcionSubsidio { get; set; }
    }

    public class ParticipantesGPS
    {
        public int NumeroSolicitud { get; set; }
        public string RutParticipante { get; set; }
        public string NombreParticipante { get; set; }
        public string DescripcionTipoParticipacion { get; set; }
    }

    public class SegurosGPS
    {
        public int NumeroSolicitud { get; set; }
        public string Poliza { get; set; }
        public string DescripcionSeguro { get; set; }
        public string DescripcionCompañia { get; set; }
        public string DescripcionTipo { get; set; }
        public string DescripcionGrupo { get; set; }
    }

    public class ObservacionesGPS
    {
        public int NumeroSolicitud { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionTipo { get; set; }
    }

    public class GastosOperacionalesGPS
    {
        public int NumeroSolicitud { get; set; }
        public string Descripcion { get; set; }
        public int NumeroBoletaFactura { get; set; }
        public decimal? ValorPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Valor { get; set; }

    }

    public class ControlEtapasGPS
    {
        public int NumeroSolicitud { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaEsperada { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string Estado { get; set; }

    }
}
