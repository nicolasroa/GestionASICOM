using System;

namespace WorkFlow.Entidades
{
    public class TipoInmuebleBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int PorcentajeSeguroIncendio { get; set; }
        public bool? VisibleEnSimulador { get; set; }
        public bool? IndEstudioTitulo { get; set; }
        public bool? IndTasacion { get; set; }
        public bool? IndPropiedadPrincipal { get; set; }



    }
    public class TipoInmuebleInfo : TipoInmuebleBase
    {
        public string DescripcionEstado { get; set; }
    }


    public class TipoConstruccionBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public bool? IndSeguroIncendio { get; set; }
    }
    public class TipoConstruccionInfo : TipoConstruccionBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class PropiedadBase : Base
    {
        public int TipoInmueble_Id { get; set; }
        public int Antiguedad_Id { get; set; }
        public int Destino_Id { get; set; }
        public int Via_Id { get; set; }
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string DeptoOficina { get; set; }
        public string Ubicacion { get; set; }
        public int Region_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Comuna_Id { get; set; }
        public int ComunaSii_Id { get; set; }
        public bool IndUsoGoce { get; set; }
        public int RolManzana { get; set; }
        public int RolSitio { get; set; }
        public bool IndDfl2 { get; set; }

        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }

    }
    public class PropiedadInfo : PropiedadBase
    {
        public string DescripcionTipoInmueble { get; set; }
        public string DescripcionAntiguedad { get; set; }
        public string DescripcionDestino { get; set; }
        public string DescripcionVia { get; set; }
        public string DescripcionRegion { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionComuna { get; set; }
        public int AñoConstruccion { get; set; }
        public int TipoConstruccion_Id { get; set; }
        public string DescripcionTipoConstruccion { get; set; }
        public int PorcentajeSeguroIncendio { get; set; }
        public string DescripcionInmobiliaria { get; set; }
        public string DescripcionProyecto { get; set; }
        public string DireccionCompleta { get; set; }
    }

    public class TasacionBase : Base
    {
        public int TasacionAnterior_Id { get; set; }
        public int TasacionPadre_Id { get; set; }
        public int Propiedad_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public int SolicitudTasacion_Id { get; set; }
        public int EstadoTasacion_Id { get; set; }
        public int SolicitudEstudioTitulo_Id { get; set; }
        public int EstadoEstudioTitulo_Id { get; set; }
        public decimal ValorComercial { get; set; }
        public decimal MontoTasacion { get; set; }
        public decimal MontoAsegurado { get; set; }
        public decimal MontoLiquidacion { get; set; }
        public decimal MetrosTerreno { get; set; }
        public decimal MetrosConstruidos { get; set; }
        public decimal MetrosLogia { get; set; }
        public decimal MetrosTerraza { get; set; }
        public DateTime? FechaTasacion { get; set; }
        public DateTime? FechaRecepcionFinal { get; set; }
        public string PermisoEdificacion { get; set; }
        public int Tasador_Id { get; set; }
        public int FabricaTasador_Id { get; set; }
        public DateTime? FechaSolicitudTasacion { get; set; }
        public DateTime? FechaTasacionPrevia { get; set; }
        public string NombreTasadorTasacionPrevia { get; set; }
        public int InstitucionTasacionPrevia_Id { get; set; }

        public string DescripcionInstitucionPrevia { get; set; }
        public DateTime? FechaCoordinacionTasacion { get; set; }

        public string NombreContactoTasacion { get; set; }
        public string TelefonoContactoTasacion { get; set; }
        public string EmailContactoTasacion { get; set; }
        public bool? IndAlzamientoHipoteca { get; set; }
        public int InstitucionAlzamientoHipoteca_Id { get; set; }
        public bool? IndFlujoEstudioTitulo { get; set; }
        public bool? IndFlujoTasacion { get; set; }
        public bool? IndEstudioTituloPiloto { get; set; }
        public bool? IndTasacionPiloto { get; set; }
        public int Seguro_Id { get; set; }
        public decimal TasaSeguro { get; set; }
        public decimal PrimaSeguro { get; set; }

        public DateTime? FechaEnvioBcoAlzante { get; set; }
        public DateTime? FechaFirmaBcoAlzante { get; set; }
        public bool? IndPropiedadPrincipal { get; set; }

        public string EmailContactoBcoAlzante { get; set; }
        public string FonoContactoBcoAlzante { get; set; }
        public string NombreContactoBcoAlzante { get; set; }
        public DateTime? FechaCorreccionCartaResguardo { get; set; }


        public TasacionBase()
        {
            Id = -1;
            TasacionAnterior_Id = -1;
            Propiedad_Id = -1;
            Solicitud_Id = -1;
            FechaTasacion = null;
            IndFlujoEstudioTitulo = null;
            IndFlujoTasacion = null;
            EstadoTasacion_Id = -1;
            EstadoEstudioTitulo_Id = -1;
            IndPropiedadPrincipal = null;
            InstitucionAlzamientoHipoteca_Id = -1;
        }
    }
    public class TasacionInfo : TasacionBase
    {
       
        public string DescripcionEstadoEETT { get; set; }
        public string DescripcionEstadoTasacion { get; set; }
        public string NombreInstitucionAlzamientoHipoteca { get; set; }
        public string DescripcionSeguroIncendio { get; set; }
        public decimal PrimaTotal { get; set; }
        public string DescripcionCompañia { get; set; }
        public string DireccionCompleta { get; set; }
        public string NombreEmpresaTasacion { get; set; }
        public string NombreTasador { get; set; }
        public int StrApoderados { get; set; }
        public string StrFirmaApoderados { get; set; }
        public string StrRecopilacionDocumentos { get; set; }

        public string DescripcionCompleta { get; set; }

        public int PorcentajeSeguroIncendio { get; set; }

        public string DescripcionComuna { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionRegion { get; set; }
        public string DescripcionTipoInmueble { get; set; }
        public string DescripcionAntiguedad { get; set; }
        public string DescripcionDestino { get; set; }
        public int flagPDF { get; set; }


    }
    public class UsoPropiedadInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }


}
