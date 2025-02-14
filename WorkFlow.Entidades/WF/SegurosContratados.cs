using System;

namespace WorkFlow.Entidades
{
    public class SegurosContratadosBase : Base
    {
        public int Solicitud_Id { get; set; }
        public int TipoSeguro_Id { get; set; }
        public int Seguro_Id { get; set; }
        public int RutCliente { get; set; }
        public int Tasacion_Id { get; set; }
        public decimal MontoAsegurado { get; set; }
        public decimal TasaMensual { get; set; }
        public decimal PrimaMensual { get; set; }

    }
    public class SegurosContratadosInfo : SegurosContratadosBase
    {
        public string Poliza { get; set; }
        public decimal PrimaAnual { get; set; }
        public string DescripcionSeguro { get; set; }
        public string DescripcionCompañia { get; set; }
        public string DescripcionTipoSeguro { get; set; }
        public string Beneficiario { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaTerminoVigencia { get; set; }
    }
}
