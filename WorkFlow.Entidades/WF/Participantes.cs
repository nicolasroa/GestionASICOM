using System;

namespace WorkFlow.Entidades
{
    public class ParticipantesBase : ClientesInfo
    {
        public int Solicitud_Id { get; set; }
        public int SolicitudDPS_Id { get; set; }
        public int TipoParticipacion_Id { get; set; }
        public decimal PorcentajeDeuda { get; set; }
        public decimal PorcentajeDominio { get; set; }
        public decimal PorcentajeDesgravamen { get; set; }
        public int SeguroDesgravamen_Id { get; set; }
        public decimal TasaSeguroDesgravamen { get; set; }
        public decimal PrimaSeguroDesgravamen { get; set; }
        public int SeguroCesantia_Id { get; set; }
        public decimal TasaSeguroCesantia { get; set; }
        public decimal PrimaSeguroCesantia { get; set; }
        public int EstadoDps_Id { get; set; }
        public DateTime? FechaIngresoDPS { get; set; }
        public DateTime? FechaAprobacionDPS { get; set; }
        public decimal MontoAseguradoDPS { get; set; }
        public decimal TotalPasivosCMF { get; set; }
        public int MesPasivoCMF { get; set; }
        public int AñoPasivoCMF { get; set; }

        public ParticipantesBase()
        {
            this.Solicitud_Id = -1;
            this.Rut = -1;
            this.TipoParticipacion_Id = -1;
        }
    }
    public class ParticipanteInfo : ParticipantesBase
    {
        public string NombreCliente { get; set; }
        public string DescripcionTipoParticipante { get; set; }
        public string AntiguedadLaboral { get; set; }
        public int EdadPlazo { get; set; }
        public decimal RentaPromedio { get; set; }
        public string DescripcionEstadoDps { get; set; }
        public int flagPDF { get; set; }
        public DateTime? FechaRegistroFirma { get; set; }
        public DateTime? FechaFirma { get; set; }
        public string DescripcionFechaPasivosCMF { get; set; }
        public string DescripcionSeguroDesgravamen { get; set; }
    }

    public class TipoParticipanteBase : Base
    {
        public string Descripcion { get; set; }
        public int Estado_Id { get; set; }
        public int CodigoADM { get; set; }
        public bool? IndDominio { get; set; }
        public bool? IndDesgravamen { get; set; }
        public bool? IndCesantia { get; set; }
        public bool? IndDeudor { get; set; }
    }
    public class TipoParticipanteInfo : TipoParticipanteBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class AntecedentesLaboralesBase : Base
    {
        public int RutCliente { get; set; }
        public string RutEmpleador { get; set; }
        public string NombreEmpleador { get; set; }
        public int TipoActividad_Id { get; set; }
        public int SituacionLaboral_Id { get; set; }
        public string Cargo { get; set; }
        public DateTime? FechaInicioContrato { get; set; }
        public DateTime? FechaTerminoContrato { get; set; }
        public int Comuna_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Region_Id { get; set; }
        public string Direccion { get; set; }
        public string Fono { get; set; }
        public string Mail { get; set; }
        public AntecedentesLaboralesBase()
        {
            this.FechaTerminoContrato = null;
        }

    }
    public class AntecedentesLaboralesInfo : AntecedentesLaboralesBase
    {
        public string DescripcionTipoActividad { get; set; }
        public string DescripcionSituacionLaboral { get; set; }
        public string DescripcionComuna { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionRegion { get; set; }

    }


    public class ClienteRelacionadoInfo : Base
    {
        public int Cliente_Id { get; set; }
        public int ClienteRelacionado_Id { get; set; }
        public int TipoRelacion_Id { get; set; }
        public int NotariaPersoneria_Id { get; set; }
        public DateTime? FechaPersoneria { get; set; }
        public string DescripcionTipoRelacion { get; set; }
        public string NombreClienteRelacionado { get; set; }
        public string DescripcionNotariaPersoneria { get; set; }

    }







}
