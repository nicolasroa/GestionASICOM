using System;

namespace WorkFlow.Entidades
{
    public class DesembolsoBase : Base
    {
        public string NombreCliente { get; set; }
        public string DescripcionProducto { get; set; }
        public string DescripcionEvento { get; set; }
        public DateTime FechaTermino { get; set; }
        public int DiasTranscurridos { get; set; }
        public string RutCompleto { get; set; }
        public int Solicitud_Id { get; set; }
    }

    public class DesembolsoInfo : DesembolsoBase
    {

    }


    public class DesembolsoFiltro
    {
        public int Solicitud_Id { get; set; }
        public int Rut { get; set; }
        public int Usuario_Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }

        public DesembolsoFiltro()
        {
            this.Solicitud_Id = -1;
            this.Rut = -1;
            this.Usuario_Id = -1;
            this.Inmobiliaria_Id = -1;
            this.Proyecto_Id = -1;
        }

    }
}