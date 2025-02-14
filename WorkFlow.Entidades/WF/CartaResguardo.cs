using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class CartaResguardoBase : Base
    {
        public string Contacto { get; set; }
        public string EmailContacto { get; set; }
        public string FonoContacto { get; set; }
        public string DireccionContacto { get; set; }
    }

    public class CartaResguardoInfo : CartaResguardoBase
    {
    }

    public class ApoderadoBase : Base
    {
        public string Apoderado { get; set; }
    }

    public class ApoderadoInfo : ApoderadoBase
    {

    }

    public class ApoderadoTasacionBase : Base
    {
        public int Tasacion_Id { get; set; }
        public int Apoderado_Id { get; set; }
        public DateTime? FechaFirma { get; set; }

    }

    public class ApoderadoTasacionInfo : ApoderadoTasacionBase
    {
        public string NombreInstitucionAlzamientoHipoteca { get; set; }
        public string DireccionCompleta { get; set; }
        public string Apoderado { get; set; }

    }

    public class DocumentosCartaResguardoBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Obligatorio { get; set; }

    }
    public class DocumentosCartaResguardoInfo : DocumentosCartaResguardoBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class RegistroDocumentosCartaResguardoBase : Base
    {
        public int Estado_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public int Tasacion_Id { get; set; }
        public int Documento_Id { get; set; }
    }
    public class RegistroDocumentosCartaResguardoInfo : RegistroDocumentosCartaResguardoBase
    {
        public string DescripcionDocumento { get; set; }
        public bool Validado { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool Obligatorio { get; set; }

    }

    public class ReporteCartaResguardo
    {
        public string ParrafoInmuebles { get; set; }
        public ClientesInfo oCliente { get; set; }
        public UsuarioInfo oEjecutivo { get; set; }
        public SolicitudInfo oSolicitud { get; set; }
        public List<TasacionInfo> lstTasacion { get; set; }
        public List<ParticipanteInfo> lstParticipante { get; set; }
        public ReporteCartaResguardo()
        {
        }
    }

}
