using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{

    public class TipoActivoBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoPersona_Id  {get;set;}
    }
    public class TipoActivoInfo : TipoActivoBase
    {
        public string DescripcionTipoPersona { get; set; }
        public string DescripcionEstado { get; set; }
    }

    public class ActivosClienteBase : Base
    {
        public int RutCliente { get; set; }
        public int TipoActivo_Id { get; set; }
        public int Moneda_Id { get; set; }
        public decimal MontoTotal { get; set; }
        public string Observacion { get; set; }
        public int UsuarioCreacion_Id { get; set; }
        public int UsuarioModificacion_Id { get; set; }
    }

    public class ActivosClienteInfo : ActivosClienteBase
    {
        public string DescripcionActivo { get; set; }
        public string DescripcionMoneda { get; set; }
        public decimal TotalUF { get; set; }
        public decimal TotalPesos { get; set; }
    }


    public class ActivosSolicitudInfo
    {
        public int Solicitud_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal ActivosDeudor { get; set; }
        public decimal ActivosCodeudor { get; set; }
        public decimal ActivosAval { get; set; }

    }
}
