using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class TipoPasivoBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoPersona_Id { get; set; }
    }
    public class TipoPasivoInfo : TipoPasivoBase
    {
        public string DescripcionTipoPersona { get; set; }
        public string DescripcionEstado { get; set; }
    }
    public class PasivosClienteBase : Base
    {
        public int RutCliente { get; set; }
        public int TipoPasivo_Id { get; set; }
        public int Moneda_Id { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal CuotaMensual { get; set; }
        public int Institucion_Id { get; set; }
        public string Observacion { get; set; }
        public int UsuarioCreacion_Id { get; set; }
        public int UsuarioModificacion_Id { get; set; }
    }

    public class PasivosClienteInfo : PasivosClienteBase
    {

        public string DescripcionPasivo { get; set; }
        public string DescripcionMoneda { get; set; }
        public string DescripcionInstitucion { get; set; }
        public decimal TotalUF { get; set; }
        public decimal TotalPesos { get; set; }
        public decimal TotalCuotaUF { get; set; }
        public decimal TotalCuotaPesos { get; set; }

        public decimal TotalDeudaHipotecariaCuotaUF { get; set; }
        public decimal TotalDeudaHipotecariaCuotaPesos { get; set; }
    }



    public class PasivosSolicitudInfo
    {
        public int Solicitud_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal PasivosDeudor { get; set; }
        public decimal PasivosCodeudor { get; set; }
        public decimal PasivosAval { get; set; }

    }
}
