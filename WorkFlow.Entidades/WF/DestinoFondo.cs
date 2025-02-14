using System;

namespace WorkFlow.Entidades
{
    public class TipoDestinoFondoInfo : Base
    {

        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public string SpObtenerBeneficiarios { get; set; }
        public int Solicitud_Id { get; set; }
        public string DescripcionEstado { get; set; }

    }

    public class DestinoFondoInfo : Base
    {
        public int Estado_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public int TipoDestinoFondo_Id { get; set; }
        public int Beneficiario_Id { get; set; }
        public string NombreBeneficiario { get; set; }
        public int FormaPago_Id { get; set; }
        public DateTime? FechaValorizacion { get; set; }
        public decimal MontoUF { get; set; }
        public decimal MontoPesos { get; set; }

        public string DescripcionTipoDestinoFondo { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionFormaPago { get; set; }

    }
}
