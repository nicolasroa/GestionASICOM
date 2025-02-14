using System;

namespace WorkFlow.Entidades
{



    public class TipoGastoOperacionalBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Factor { get; set; }
        public decimal FactorDfl2 { get; set; }
        public bool? PorMontoCredito { get; set; }
        public bool? PorMontoPropiedad { get; set; }
        public bool? ValorFijo { get; set; }
        public bool? ViviendaSocial { get; set; }
        public bool? IndRefinanciamiento { get; set; }
        public decimal FactorRefinanciamiento { get; set; }
        public int Moneda_Id { get; set; }
        public decimal ValorMaximo { get; set; }


    }
    public class TipoGastoOperacionalInfo : TipoGastoOperacionalBase
    {
        public string DescripcionEstado { get; set; }
    }



    public class GastoOperacionalBase : Base
    {
        public int Estado_Id { get; set; }
        public int TipoGastoOperacional_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public decimal Valor { get; set; }
        public int Proveedor_Id { get; set; }
        public int NumeroBoletaFactura { get; set; }
        public decimal ValorPagado { get; set; }
        public DateTime FechaPago { get; set; }
        public int QuienPaga_Id { get; set; }
        public int ComoPaga_Id { get; set; }
    }
    public class GastoOperacionalInfo : GastoOperacionalBase
    {
        public bool IndSimulacion { get; set; }
        public int Cliente_Id { get; set; }
        public int Destino_Id { get; set; }
        public int Moneda_Id { get; set; }
        public string DescripcionMoneda { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal ValorPropiedad { get; set; }
        public bool IndDfl2 { get; set; }
        public bool ViviendaSocial { get; set; }
        public decimal ValorPesos { get; set; }

        public string DescripcionTipoGasto { get; set; }
        public string DescripcionProveedor { get; set; }
        public string DescripcionQuienPaga { get; set; }
        public string DescripcionComoPaga { get; set; }
        public bool IndProvisionSolicitada { get; set; }

    }

    public class QuienPagaGGOOInfo
    {
        public int Id { get; set; }
        public bool Vigente { get; set; }
        public string Descripcion { get; set; }

    }
    public class ComoPagaGGOOInfo
    {
        public int Id { get; set; }
        public bool Vigente { get; set; }
        public string Descripcion { get; set; }

    }

    public class IntegracionGGOOInfo
    {
        public int NumeroSolicitud { get; set; }
        public int codRespuesta { get; set; }
        public decimal MontoProvisionado { get; set; }
        public decimal MontoDisponible { get; set; }
        public decimal MontoUtilizado { get; set; }



        public string NombreCliente { get; set; }
        public string RutCliente { get; set; }
        public string MailCliente { get; set; }
        public string CelularCliente { get; set; }
        public int TipoGasto_Id { get; set; }
        public int QuienPaga_Id { get; set; }
        public int ComoPaga_Id { get; set; }
        public decimal ValorUF { get; set; }
        public decimal ValorPesos { get; set; }
        public DateTime FechaValorizacion { get; set; }
        public bool IndProvisionSolicitada { get; set; }




    }

}
