using System;

namespace WorkFlow.Entidades
{
    public class DocumentosPersonalesBase : Base
    {
        public int Estado_Id { get; set; }
        public int TipoParticipacion_Id { get; set; }
        public int TipoActividad { get; set; }
        public string Descripcion { get; set; }
        public bool? Obligatorio { get; set; }

    }
    public class DocumentosPersonalesInfo : DocumentosPersonalesBase
    {
        public string DescripcionEstado { get; set; }
        public string DescripciontipoParticipante { get; set; }
        public string DescripcionTipoActividad { get; set; }
    }


    public class RegistroDocumentosPersonalesBase : Base
    {
        public int Estado_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public int RutCliente { get; set; }
        public int Documento_Id { get; set; }
        
    }
    public class RegistroDocumentosPersonalesInfo : RegistroDocumentosPersonalesBase
    {
        public string DescripcionDocumento { get; set; }
        public string DescripcionEstado { get; set; }
        public bool Validado { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool Obligatorio { get; set; }
        public int TipoPersona_Id { get; set; }
    }




    public class RegistroDocumentosOriginacionComercialBase : Base
    {
        public int Estado_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public int Documento_Id { get; set; }
        public int EstadoRevision_Id { get; set; }
        public string Observacion { get; set; }

    }
    public class RegistroDocumentosOriginacionComercialInfo : RegistroDocumentosOriginacionComercialBase
    {
        public string DescripcionDocumento { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionEstadoRevision { get; set; }
        public bool Validado { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool Obligatorio { get; set; }
        
    }




}
