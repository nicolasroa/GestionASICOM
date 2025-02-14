using System;

namespace WorkFlow.Entidades
{
    public class BandejaEntradaInfo : Base
    {

        public int Solicitud_Id { get; set; }
        public int Evento_Id { get; set; }
        public int Secuencia { get; set; }
        public int Estado_Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public DateTime? FechaEsperada { get; set; }
        public string AccionTermino { get; set; }
        public int Responsable_Id { get; set; }
        public int UsuarioTermino_Id { get; set; }
        public int Malla_Id { get; set; }
        public int Etapa_Id { get; set; }
        public string DescripcionEvento { get; set; }
        public int EstadoEvento_Id { get; set; }
        public string DescripcionPlantilla { get; set; }
        public int Rut { get; set; }
        public string NombreSolicitantePrincipal { get; set; }
        public bool IndModificaDatosCredito { get; set; }
        public bool IndModificaDatosParticipantes { get; set; }
        public bool IndModificaDatosPropiedades { get; set; }
        public bool IndModificaDatosSeguros { get; set; }
        public string DescripcionMalla { get; set; }
        public string DescripcionEtapa { get; set; }
        public int EstadoVencimiento_Id { get; set; }

        public string UsuarioResponsable { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioTermino { get; set; }
        public string RutCompleto { get; set; }
        public string DescripcionEstado { get; set; }
    }

    public class BandejaEntradaEtapaInfo
    {
        public int Malla_Id { get; set; }
        public int Etapa_Id { get; set; }
        public string DescripcionEtapa { get; set; }
        public int Vigentes { get; set; }
        public decimal PorcVigentes { get; set; }
        public int PorVencer { get; set; }
        public decimal PorcPorVencer { get; set; }
        public int Vencidas { get; set; }
        public decimal PorcVencidas { get; set; }
        public int Total { get; set; }
    }

    public class BandejaEntradaEventoInfo
    {
        public int Malla_Id { get; set; }
        public int Evento_Id { get; set; }
        public string DescripcionEvento { get; set; }
        public int Vigentes { get; set; }
        public decimal PorcVigentes { get; set; }
        public int PorVencer { get; set; }
        public decimal PorcPorVencer { get; set; }
        public int Vencidas { get; set; }
        public decimal PorcVencidas { get; set; }
        public int Total { get; set; }
    }


    public class BandejaEntradaFiltro
    {
        public int Solicitud_Id { get; set; }
        public string Operacion { get; set; }
        public int Rut { get; set; }
        public int Usuario_Id { get; set; }
        public int EstadoEvento_Id { get; set; }
        public int Malla_Id { get; set; }
        public int Etapa_Id { get; set; }
        public int Evento_Id { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public int Proyecto_Id { get; set; }
        public int SubEstadoSolicitud_Id { get; set; }
        public DateTime? FechaInicioDesde { get; set; }
        public DateTime? FechaInicioHasta { get; set; }

        public BandejaEntradaFiltro()
        {
            this.Solicitud_Id = -1;
            this.Rut = -1;
            this.Usuario_Id = -1;
            this.EstadoEvento_Id = -1;
            this.Etapa_Id = -1;
            this.Evento_Id = -1;
            this.Inmobiliaria_Id = -1;
            this.Proyecto_Id = -1;
            this.SubEstadoSolicitud_Id = -1;


        }
    }


    public class ReporteBandejaEntrada
    {
        public int NumeroSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaEsperada { get; set; }
        public string Malla { get; set; }
        public string Etapa { get; set; }
        public string Evento { get; set; }
        public string NombreSolicitantePrincipal { get; set; }
        public string EstadoVencimiento { get; set; }
        public string UsuarioResponsable { get; set; }
        public decimal MontoPropiedad { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal MontoContado { get; set; }
        public decimal MontoSubsidio { get; set; }
        public decimal PorcentajeFinanciamiento { get; set; }
        public int Plazo { get; set; }
        public decimal TasaFinal { get; set; }
        public bool IndTasaEspecial { get; set; }
        public int Gracia { get; set; }
        public bool IndDfl2 { get; set; }
        public bool IndViviendaSocial { get; set; }
        public decimal CAE { get; set; }
        public decimal CTC { get; set; }
        public int MesEscritura { get; set; }
        public int AñoEscritura { get; set; }
        public int Repertorio { get; set; }
        public decimal ValorDividendoUfTotal { get; set; }
        public decimal ValorDividendoPesosTotal { get; set; }
        public decimal DividendoRenta { get; set; }
        public string NumeroOperacion { get; set; }
        public string Estado { get; set; }
        public string SubEstado { get; set; }
        public string TipoFinanciamiento { get; set; }
        public string Producto { get; set; }
        public string Destino { get; set; }
        public string TipoGarantia { get; set; }
        public string Moneda { get; set; }
        public string Subsidio { get; set; }
        public string Objetivo { get; set; }
        public string EjecutivoComercial { get; set; }
        public string Formalizador { get; set; }
        public string NombreCliente { get; set; }
        public string NombreSucursal { get; set; }
        public string Inmobiliaria { get; set; }
        public string Proyecto { get; set; }
        public string RutCliente { get; set; }
    }
}
