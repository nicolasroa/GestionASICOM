using System;

namespace WorkFlow.Entidades
{
    public class SolicitudBase : Base
    {
        public int Estado_Id { get; set; }
        public int SubEstado_Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Canal_Id { get; set; }
        public int Producto_Id { get; set; }
        public int Destino_Id { get; set; }
        public int Sucursal_Id { get; set; }
        public int TipoGarantia_Id { get; set; }
        public int Moneda_Id { get; set; }
        public decimal MontoPropiedad { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal MontoCreditoCapitalizado { get; set; }
        public decimal MontoCreditoPesos { get; set; }
        public decimal MontoContado { get; set; }
        public decimal MontoSubsidio { get; set; }
        public decimal MontoSubsidioPesos { get; set; }
        public decimal MontoBonoIntegracion { get; set; }
        public decimal MontoBonoIntegracionPesos { get; set; }
        public decimal MontoBonoCaptacion { get; set; }
        public decimal MontoBonoCaptacionPesos { get; set; }
        public decimal PorcentajeFinanciamiento { get; set; }
        public int Subsidio_Id { get; set; }
        public decimal AhorroPrevioSubsidio { get; set; }
        public string NumeroSerieSubsidio { get; set; }
        public string NumeroCertificadoSubsidio { get; set; }
        public int AñoCertificadoSubsidio { get; set; }
        public int MesCertificadoSubsidio { get; set; }
        public string NumeroLibretaSubsidio { get; set; }
        public int Plazo { get; set; }
        public int Plazo2 { get; set; }
        public decimal TasaBase { get; set; }
        public decimal Spread { get; set; }
        public decimal TasaFinal { get; set; }
        public bool IndTasaEspecial { get; set; }
        public int Gracia { get; set; }
        public int TipoVencimiento_Id { get; set; }
        public bool IndPac { get; set; }
        public int Banco_Id { get; set; }
        public bool IndPat { get; set; }
        public bool IndDfl2 { get; set; }
        public bool IndViviendaSocial { get; set; }
        public int Convenio_Id { get; set; }
        public int EjecutivoComercial_Id { get; set; }
        public int EjecutivoControlEtapa_Id { get; set; }
        public int Abogado_Id { get; set; }
        public int Tasador_Id { get; set; }
        public DateTime? FechaSolicitudEstudioTitulo { get; set; }
        public DateTime? FechaAprobacionEstudioTitulo { get; set; }
        public DateTime? FechaVisitaTasacionPiloto { get; set; }
        public DateTime? FechaAprobacionTasacion { get; set; }
        public int Notaria_Id { get; set; }
        public int CBR_Id { get; set; }
        public int MesEscritura { get; set; }
        public int AñoEscritura { get; set; }
        public decimal ValorDividendoUF { get; set; }
        public decimal ValorUltimoDividendoUF { get; set; }
        public decimal ValorDividendoSinCesantiaUF { get; set; }
        public decimal ValorDividendoSinCesantiaPesos { get; set; }
        public decimal ValorDividendoPesos { get; set; }
        public decimal ValorDividendoUfTotal { get; set; }
        public decimal ValorDividendoPesosTotal { get; set; }


        public decimal ValorDividendoFlexibleUF { get; set; }
        public decimal ValorDividendoFlexiblePesos { get; set; }
        public decimal ValorDividendoFlexibleUfTotal { get; set; }
        public decimal ValorDividendoFlexiblePesosTotal { get; set; }

        public decimal CAE { get; set; }
        public decimal CTC { get; set; }
        public string NumeroOperacion { get; set; }
        public string Serie { get; set; }
        public int Inversionista_Id { get; set; }
        public decimal TasaEndoso { get; set; }
        public int EstadoEndoso_Id { get; set; }
        public DateTime FechaEnvioAntecedentes { get; set; }
        public DateTime? FechaResolucionInversionista { get; set; }
        public string CuentaPac { get; set; }
        public int FabricaAbogado_Id { get; set; }
        public DateTime FechaEnvioCierre { get; set; }
        public DateTime FechaRecepcionCierre { get; set; }
        public decimal TasaCierreEndoso { get; set; }
        public int FabricaFormalizacion_Id { get; set; }
        public int FabricaFormalizacionGestion_Id { get; set; }
        public int BancoPac_Id { get; set; }
        public int FabricaNotaria_Id { get; set; }
        public int FabricaTasadores_Id { get; set; }
        public int Dividendos { get; set; }
        public int Cooperativa_Id { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }
        public bool? IndFlujoTasacion { get; set; }
        public bool? IndFlujoEstudioTitulo { get; set; }
        public int EventoDesembolso_Id { get; set; }
        public bool? GenerarNumeroOperacion { get; set; }
        public bool? GenerarSerie { get; set; }
        public DateTime? FechaCierreCondiciones { get; set; }
        public DateTime? FechaConfirmacionTasa { get; set; }
        public DateTime? FechaBorradorEscritura { get; set; }
        public DateTime? FechaConfeccionEscritura { get; set; }
        public DateTime? FechaCoordinacionFirmas { get; set; }
        public DateTime? FechaInstruccionNotarial { get; set; }
        public DateTime? FechaCorreccionEscrituraNotaria { get; set; }
        public DateTime? FechaVerificarEscrituraCorregida { get; set; }
        public DateTime? FechaFirmaRepresentanteMutuaria { get; set; }
        public DateTime? FechaEnvioACierreDeCopias { get; set; }
        public DateTime? FechaCierreDeCopias { get; set; }
        public DateTime? FechaReparoCierreDeCopias { get; set; }
        public DateTime? FechaRevisionInscripcion { get; set; }
        public DateTime? FechaInformeFinal { get; set; }
        public DateTime? FechaRegistroGarantia { get; set; }
        public DateTime? FechaCierreCarpeta { get; set; }
        public DateTime? FechaEntregaEscrituraCliente { get; set; }
        public DateTime? FechaEnvioExpedienteArchivo { get; set; }
        public DateTime? FechaRepertorioNotarial { get; set; }
        public DateTime? FechaValidacionProvisionGGOO { get; set; }
        public int NroRepertorioNotarial { get; set; }
        public int FolioFormulario { get; set; }
        public int TasaImpuestoAlMutuo_Id { get; set; }
        public decimal MontoImpuestoAlMutuo { get; set; }
        public int Finalidad_Id { get; set; }
        public int Utilidad_Id { get; set; }
        public bool? IndCartaResguardo { get; set; }
        public bool? IndInstruccionNotarial { get; set; }
        public DateTime? FechaLiquidacionMutuo { get; set; }
        public decimal MontoLiquidacionPesos { get; set; }
        public DateTime? FechaRevisionAbogado { get; set; }
        public bool? IndCierreCondiciones { get; set; }
        public int ResponsableCierreCondiciones_Id { get; set; }
        public bool? IndCierreCondicionesInterno { get; set; }
        public int MotivoRechazo_Id { get; set; }
        public int MotivoDesistimiento_Id { get; set; }
        public DateTime? FechaSolicitudMinutaComplementaria { get; set; }
        public DateTime? FechaConfeccionMinutaComplementaria { get; set; }
        public DateTime? FechaValidacionFinal { get; set; }
        public bool? IndArticulo150 { get; set; }
        public int BeneficiarioTributario_Id { get; set; }
        public SolicitudBase()
        {
            Gracia = 0;

        }

    }

    public class SolicitudInfo : SolicitudBase
    {
        public int TipoFinanciamiento_Id { get; set; }
        public int Objetivo_Id { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionSubEstado { get; set; }
        public string DescripcionProducto { get; set; }
        public string DescripcionTipoFinanciamiento { get; set; }
        public string DescripcionDestino { get; set; }
        public string DescripcionTipoGarantia { get; set; }
        public string DescripcionMoneda { get; set; }
        public string DescripcionSubsidio { get; set; }
        public string DescripcionObjetivo { get; set; }
        public bool IniciarFlujo { get; set; }
        public string DescripcionInversionista { get; set; }
        public string DescripcionBancoPac { get; set; }
        public string DescripcionEjecutivoComercial { get; set; }
        public string MailEjecutivoComercial { get; set; }
        public string NombreSucursal { get; set; }
        public int Rut { get; set; }
        public string NombreCliente { get; set; }
        public string RutCliente { get; set; }
        public string DescripcionEstadoEndoso { get; set; }
        public decimal CostoFondo { get; set; }
        public decimal MontoPropiedadPesos { get; set; }
        public decimal MontoContadoPesos { get; set; }
        public int DiaPago { get; set; }
        public string DescripcionCooperativa { get; set; }
        public string DescripcionInmobiliaria { get; set; }
        public int EventoDesembolsoInmobiliaria_Id { get; set; }
        public string DescripcionProyecto { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreAbogado { get; set; }
        public string NombreTasador { get; set; }
        public string DescripcionNotaria { get; set; }
        public string NombreResponsableCierreCondiciones { get; set; }
        public DateTime? FechaParidad { get; set; }
        public decimal ValorUF { get; set; }
        public DateTime? FechaPrimerDividendo { get; set; }
        public string NotariaProtocolizacion { get; set; }
        public DateTime? FechaProtocolizacion { get; set; }
        public string RepertorioProtocolizacion { get; set; }
        public string NumeroProtocolizacion { get; set; }
        public decimal DividendoRenta { get; set; }
        public int FabricaUsuario_Id { get; set; }

        //Proceso de Activacion
        public int CantidadNoActivado { get; set; }
        public int CantidadActivado { get; set; }
        public int CantidadTotalActivacion { get; set; }
        public string Periodo { get; set; }
        public decimal MontoNoActivado { get; set; }
        public decimal MontoActivado { get; set; }
        public decimal TotalActivacion { get; set; }

        public int PorcNoActivadas { get; set; }

        public int PorcActivadas { get; set; }




        public bool Activada { get; set; }
        public bool ResultadoActivacion { get; set; }
        public string ErrorActivacion { get; set; }
        public string EstadoActivacion { get; set; }

        public DateTime FechaRechazo { get; set; }
        public string DescripcionRechazo { get; set; }

        public string DescripcionMotivoRechazo { get; set; }
        public string DescripcionMotivoDesistimiento { get; set; }
        public string EventoEnCurso { get; set; }

        public string DescripcionMesCertificadoSubsidio { get; set; }

        public string NombreBeneficiarioTributario { get; set; }

        public SolicitudInfo()
        {
            this.FabricaUsuario_Id = -1;
            this.FabricaAbogado_Id = 1;
            this.FabricaFormalizacion_Id = 1;
            this.FabricaNotaria_Id = 1;
            this.FabricaTasadores_Id = 1;
            ResultadoActivacion = true;
            ErrorActivacion = "OK";
        }
    }

    public class AsignacionSolicitudBase : Base
    {
        public int Solicitud_Id { get; set; }
        public int Rol_Id { get; set; }
        public int Responsable_Id { get; set; }
    }

    public class AsignacionSolicitudInfo : AsignacionSolicitudBase
    {
        public string DescripcionRol { get; set; }
        public string NombreResponsable { get; set; }
        public string DescripcionProducto { get; set; }
        public string NombreCliente { get; set; }
        public string EventoEnCurso { get; set; }
        public int NuevoResponsable_Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int FabricaFormalizacion_Id { get; set; }
        public int FabricaFormalizacionGestion_Id { get; set; }
        public int FabricaAbogado_Id { get; set; }
        public int FabricaNotaria_Id { get; set; }
        public int FabricaTasadores_Id { get; set; }
    }

    public class ResumenIndicadores
    {
        public string PasivosPrincipal { get; set; }
        public string PasivosPrincipalPesos { get; set; }
        public string ActivosPrincipal { get; set; }
        public string ActivosPrincipalPesos { get; set; }
        public string PatrimonioPrincipal { get; set; }
        public string PatrimonioPrincipalPesos { get; set; }
        public string PasivosComplementados { get; set; }
        public string PasivosComplementadosPesos { get; set; }
        public string ActivosComplementados { get; set; }
        public string ActivosComplementadosPesos { get; set; }
        public string PatrimonioComplementado { get; set; }
        public string PatrimonioComplementadoPesos { get; set; }
        public string RentaNecesaria { get; set; }
        public string RentaNecesariaPesos { get; set; }
        public string RentaPrincipal { get; set; }
        public string RentaPrincipalPesos { get; set; }

        public string RentaCodeudor { get; set; }
        public string RentaCodeudorPesos { get; set; }
        public string RentaComplementada { get; set; }
        public string RentaComplementadaPesos { get; set; }
        public string DividendoRentaPrincipal { get; set; }
        public string DividendoRentaCodeudor { get; set; }
        public string DividendoRentaComplementada { get; set; }
        public string CargaFinancieraPrincipal { get; set; }
        public string CargaFinancieraComplementada { get; set; }

        public string CargaFinancieraHipotecariaPrincipal { get; set; }
        public string CargaFinancieraHipotecariaCodeudor { get; set; }
        public string CargaFinancieraHipotecariaComplementada { get; set; }
    }




    public class FinalidadCreditoInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int CodigoSii { get; set; }
    }
    public class UtilidadCreditoInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSvs { get; set; }
    }

    public class CaeCtcInfo
    {
        public int Solicitud_Id { get; set; }
        public decimal CAE { get; set; }
        public decimal CTC { get; set; }
        public decimal CAEMinvu { get; set; }
        public decimal CTCMinvu { get; set; }

    }
}
