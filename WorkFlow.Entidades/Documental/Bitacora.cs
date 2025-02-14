using System;

namespace WorkFlow.Entidades.Documental
{
    public class Bitacora : Base
    {
        //public int Id { get; set; }
        public int IdArchivo { get; set; }
        public int Rut { get; set; }
        public string NumeroSolicitud { get; set; }
        public int IdGrupoDocumento { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Descripcion { get; set; }
        public string NombreArchivo { get; set; }
        public byte[] ArchivoVB { get; set; }
        public string ExtensionArchivo { get; set; }
        public string SizeKB { get; set; }
        public string ContentType { get; set; }
        public int IdTipoEvento { get; set; }
        public DateTime FechaHoraEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
    }
    public class BitacoraInfo : Bitacora
    {
        public string TipoDocumento { get; set; }
        public string Usuario { get; set; }
        public string strRutParticipantes { get; set; }
        public string SistemaWF { get; set; }
    }
}
