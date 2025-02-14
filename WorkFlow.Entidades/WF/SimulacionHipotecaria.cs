using System;
using System.Collections.Generic;

namespace WorkFlow.Entidades
{
    public class SimulacionHipotecaria : Base
    {
        public int RutCliente { get; set; }
        public int TipoFinanciamiento_Id { get; set; }
        public int Producto_Id { get; set; }
        public int Objetivo_Id { get; set; }
        public int Destino_Id { get; set; }


        public int TipoPropiedad_Id { get; set; }
        public int Subsidio_Id { get; set; }
        public int Cooperativa_Id { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }
        public bool IndDfl2 { get; set; }
        public int Antiguedad_Id { get; set; }
        public int Comuna_Id { get; set; }
        public int Gracia { get; set; }
        public int Plazo { get; set; }
        public decimal TasaAnual { get; set; }
        public decimal TasaBaseAnual { get; set; }
        public decimal TasaSpreadAnual { get; set; }
        public int MesExclusion { get; set; }
        public bool IndPrepago { get; set; }
        public int DiaVencimiento { get; set; }
        public decimal ValorPropiedad { get; set; }
        public decimal MontoSubsidio { get; set; }
        public decimal MontoBonoIntegracion { get; set; }
        public decimal MontoBonoCaptacion { get; set; }
        public decimal MontoContado { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal PorcentajeFinanciamiento { get; set; }
        public int CantidadDeudores { get; set; }
        public int SeguroDesgravamen_Id { get; set; }
        public int SeguroCesantia_Id { get; set; }
        public int SeguroIncendio_Id { get; set; }
        public decimal MontoAseguradoSeguroIncendio { get; set; }
        public decimal SegDesgravamen { get; set; }
        public decimal SegIncendio { get; set; }
        public decimal SegCesantia { get; set; }
        public decimal SegCesantiaMinvu { get; set; }
        public decimal DividendoUF { get; set; }
        public decimal UltimoDividendoUF { get; set; }
        public decimal DividendoPesos { get; set; }
        public decimal DividendoTotal { get; set; }
        public decimal DividendoTotalPesos { get; set; }
        public int PlazoFlexible { get; set; }
        public decimal DividendoFlexibleTotal { get; set; }
        public decimal DividendoFlexibleTotalPesos { get; set; }

        public decimal DividendoFlexibleUF { get; set; }
        public decimal DividendoFlexiblePesos { get; set; }

        public decimal CAE { get; set; }
        public decimal CTC { get; set; }
        public decimal? DividendoMinvuUf { get; set; }
        public decimal? DividendoMinvuPesos { get; set; }
        public decimal? DividendoMinvuTotal { get; set; }
        public decimal? DividendoMinvuTotalPesos { get; set; }
        public decimal? CAEMinvu { get; set; }
        public decimal? CTCMinvu { get; set; }
        public int Solicitud_Id { get; set; }
        public int Sucursal_Id { get; set; }
        public int Ejecutivo_Id { get; set; }
        public DateTime FechaSimulacion { get; set; }
        public bool IndTasaEspecial { get; set; }

        public SimulacionHipotecaria()
        {
            Gracia = 0;
        }
    }

    public class SimulacionHipotecariaInfo : SimulacionHipotecaria
    {

        public string DescripcionTipoInmueble { get; set; }
        public string DescripcionComuna { get; set; }
        public string DescripcionCooperativa { get; set; }
        public string DescripcionInmobiliaria { get; set; }
        public string DescripcionProyecto { get; set; }
        public string DescripcionTipoFinanciamiento { get; set; }
        public string DescripcionProducto { get; set; }
        public string DescripcionObjetivo { get; set; }
        public string DescripcionDestino { get; set; }
        public string DescripcionSubsidio { get; set; }
        public string DescripcionGracia { get; set; }
        public string DescripcionSegDesgravamen { get; set; }
        public string DescripcionSegIncendio { get; set; }
        public string DescripcionSegCesantia { get; set; }
        public string DescripcionSucursal { get; set; }
        public string NombreEjecutivo { get; set; }
        public decimal RentaMininaRequerida { get; set; }
        public decimal RentaMininaRequeridaMinvu { get; set; }
        public string StrValorUF { get; set; }
        public string StrPrecioVentaPesos { get; set; }
        public string StrMontoSubsidioPesos { get; set; }
        public string StrMontoBonoIntegracionPesos { get; set; }
        public string StrMontoBonoCaptacionPesos { get; set; }
        public string StrMontoContadoPesos { get; set; }
        public string StrMontoCreditoPesos { get; set; }
        public bool PlazoUnico { get; set; }
        public int Canal { get; set; }
        public string NombreCliente { get; set; }
        public string DescripcionAntiguedad { get; set; }
        public string StrPorcentajeFinanciamiento { get; set; }

      
    }

    public class ReporteSimulacion
    {
        public ClientesInfo oCliente { get; set; }
        public List<SimulacionHipotecariaInfo> lstSimulacion { get; set; }
        public List<GastoOperacionalInfo> lstGastoOperacional { get; set; }
        public UsuarioInfo oEjecutivo { get; set; }
        public List<TextoReportesInfo> lstParrafos { get; set; }
        public SimulacionHipotecariaInfo oSimulacion { get; set; }
        public SolicitudInfo oSolicitud { get; set; }
        public string VigenciaDoc { get; set; }
        public ReporteSimulacion()
        {
            this.VigenciaDoc = "90";
        }
    }




    public class DatosSimulacion
    {
        public int Canal { get; set; }
        public int TipoFinanciamiento_Id { get; set; }
        public int Producto_Id { get; set; }
        public int Objetivo_Id { get; set; }
        public int Destino_Id { get; set; }
        public int TipoPropiedad_Id { get; set; }
        public int Subsidio_Id { get; set; }
        public bool IndDfl2 { get; set; }
        public int Antiguedad_Id { get; set; }
        public int MesExclusion { get; set; }
        public int Gracia { get; set; }
        public bool IndPrepago { get; set; }
        public int DiaVencimiento { get; set; }
        public decimal ValorPropiedad { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal MontoContado { get; set; }
        public int Plazo { get; set; }
        public int CantidadDeudores { get; set; }
        public int SeguroDesgravamen_Id { get; set; }
        public int SeguroCesantia_Id { get; set; }
        public int SeguroIncendio_Id { get; set; }
        public decimal MontoAseguradoSeguroIncendio { get; set; }
        public decimal TasaAnual { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }
        public bool PlazoUnico { get; set; }



    }



    public class ResultadoSimulacion
    {
        public int Plazo { get; set; }  
        public decimal TasaAnual { get; set; }
        public decimal TasaBaseAnual { get; set; }
        public decimal TasaSpreadAnual { get; set; }
        public decimal SegDesgravamen { get; set; }
        public decimal SegIncendio { get; set; }
        public decimal SegCesantia { get; set; }
        public decimal SegCesantiaMinvu { get; set; }
        public decimal DividendoUF { get; set; }
        public decimal UltimoDividendoUF { get; set; }
        public decimal DividendoPesos { get; set; }
        public decimal DividendoTotal { get; set; }
        public decimal DividendoTotalPesos { get; set; }
        public decimal CAE { get; set; }
        public decimal CTC { get; set; }
        public decimal? DividendoMinvuUf { get; set; }
        public decimal? DividendoMinvuPesos { get; set; }
        public decimal? DividendoMinvuTotal { get; set; }
        public decimal? DividendoMinvuTotalPesos { get; set; }
        public decimal? CAEMinvu { get; set; }
        public decimal? CTCMinvu { get; set; }
        public decimal RentaMininaRequerida { get; set; }
        public decimal RentaMininaRequeridaMinvu { get; set; }
    }




    public class ResultadoSimulacionWeb
    {
        public bool ResultadoGeneral { get; set; }
        public string Mensaje { get; set; }
        public List<ResultadoSimulacion> lstResultadoSimulacion { get; set; }
        public List<GastosOperacionalesSimulacion> lstGastosOperacionales { get; set; }

        public ResultadoSimulacionWeb()
        {
            ResultadoGeneral = true;
            Mensaje = "Ok";
            lstResultadoSimulacion = new List<ResultadoSimulacion>();
            lstGastosOperacionales = new List<GastosOperacionalesSimulacion>();
        }
    }
    public class GastosOperacionalesSimulacion
    {
        public int TipoGastoOperacional_Id { get; set; }
        public string DescripcionTipoGasto { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPesos { get; set; }
        public int Moneda_Id { get; set; }
    }
    public class GastoOperacionaSimulacionWeb
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdRolWF { get; set; }
        public int IdUsuarioWF { get; set; }
        public int Estado_Id { get; set; }
        public int TipoGastoOperacional_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public decimal Valor { get; set; }
        public int Proveedor_Id { get; set; }
        public int NumeroBoletaFactura { get; set; }
        public decimal ValorPagado { get; set; }
        public DateTime FechaPago { get; set; }
      
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
        public string DescripcionProvisiona { get; set; }
    }



    
    









    public class ClientesSimulacionWeb
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdRolWF { get; set; }
        public int IdUsuarioWF { get; set; }
        public int Rut { get; set; }
        public string Dv { get; set; }
        public int TipoPersona_Id { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Mail { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Sexo_Id { get; set; }
        public int Nacionalidad_Id { get; set; }
        public int EstadoCivil_Id { get; set; }
        public int RegimenMatrimonial_Id { get; set; }
        public int Profesion_Id { get; set; }
        public int Educacion_Id { get; set; }
        public string TituloEducacional { get; set; }
        public int Residencia_Id { get; set; }
        public int NivelEducacional_Id { get; set; }
        public int CategoriaSii_Id { get; set; }
        public int SubCategoriaSii_Id { get; set; }
        public int ActividadSii_Id { get; set; }
        public string DescripcionEstado { get; set; }
        public string NombreCompleto { get; set; }
        public string RutCompleto { get; set; }
        public string DescripcionResidencia { get; set; }
        public string Accion { get; set; }
        public string strEdad { get; set; }
        public int Edad { get; set; }
        public string DescripcionEstadoCivil { get; set; }
        public string DescricpionRegimenMatrimonial { get; set; }
        public string DescripcionNacionalidad { get; set; }

        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string Departamento { get; set; }
        public int Region_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Comuna_Id { get; set; }

        public string DescripcionRegion { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionComuna { get; set; }
        public string DireccionCompleta { get; set; }

        public string DescripcionProfesion { get; set; }
        public ClientesSimulacionWeb()
        {
            this.Rut = -1;
        }
    }

}
