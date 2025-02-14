using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class InversionistaBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int Contrato_Id { get; set; }
    }

    public class InversionistaInfo : InversionistaBase
    {

    }


    public class SolicitudInvercionistasInfo : Base
    {
        public int Estado_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public bool? Asignado { get; set; }
        public int Inversionista_Id { get; set; }
        public decimal TasaEndoso { get; set; }
        public string DescripcionInversionista { get; set; }
        public string DescripcionEstado { get; set; }

    }
}
