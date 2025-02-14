using System;

namespace WorkFlow.Entidades
{
    public class DocumentosEstudioTituloBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public bool Obligatorio { get; set; }
    }
    public class DocumentosEstudioTituloInfo : DocumentosEstudioTituloBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class RegistroDocumentosEstudioTituloInfo : DocumentosEstudioTituloInfo
    {
        public int Solicitud_Id { get; set; }
        public int Tasacion_Id { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool Seleccionado { get; set; }
        public int Documento_Id { get; set; }
        public string DescripcionDocumento { get; set; }
       

    }
}
