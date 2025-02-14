using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{

    public class NegBandejaEntrada
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        public Resultado<BandejaEntradaInfo> Buscar(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<BandejaEntradaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<BandejaEntradaInfo, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.BandejaEntrada_QRY, BaseSQL);
                lstBandejaEntrada = new List<BandejaEntradaInfo>();
                lstBandejaEntrada = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Entrada";
                return ObjetoResultado;
            }
        }

        public Resultado<BandejaEntradaInfo> Guardar(BandejaEntradaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<BandejaEntradaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<BandejaEntradaInfo>(Entidad, GlobalDA.SP.ControlEventos_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Gasto Operacional " + EX.Message;
                return ObjetoResultado;
            }
        }

        public Resultado<ReporteBandejaEntrada> BuscarReporte(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<ReporteBandejaEntrada>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ReporteBandejaEntrada, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.ReporteBandejaEntrada_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Entrada";
                return ObjetoResultado;
            }
        }

        public Resultado<BandejaEntradaInfo> BuscarEstadoAvance(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<BandejaEntradaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<BandejaEntradaInfo, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.BandejaEntradaAvance_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Entrada";
                return ObjetoResultado;
            }
        }

        public Resultado<BandejaEntradaEtapaInfo> BuscarEtapas(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<BandejaEntradaEtapaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<BandejaEntradaEtapaInfo, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.BandejaEntradaEtapas_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Entrada";
                return ObjetoResultado;
            }
        }

        public Resultado<BandejaEntradaEventoInfo> BuscarEventos(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<BandejaEntradaEventoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<BandejaEntradaEventoInfo, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.BandejaEntradaEventos_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Entrada";
                return ObjetoResultado;
            }
        }


        public Resultado<SolicitudInfo> BuscarBandejaSeguimiento(BandejaEntradaFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {


                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, BandejaEntradaFiltro>(Filtro, GlobalDA.SP.BandejaSeguimiento_QRY, BaseSQL);
                lstBandejaSeguimiento = new List<SolicitudInfo>();
                lstBandejaSeguimiento = ObjetoResultado.Lista;

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Bandeja de Seguimiento";
                return ObjetoResultado;
            }
        }



        #region PROPIEDADES
        public static List<BandejaEntradaInfo> lstBandejaEntrada
        {
            get { return (List<BandejaEntradaInfo>)HttpContext.Current.Session[ISesiones.lstBandejaEntrada]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstBandejaEntrada, value); }
        }
        public static List<SolicitudInfo> lstBandejaSeguimiento
        {
            get { return (List<SolicitudInfo>)HttpContext.Current.Session[ISesiones.lstBandejaSeguimiento]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstBandejaSeguimiento, value); }
        }

        public static List<BandejaEntradaInfo> lstControlEventos
        {
            get { return (List<BandejaEntradaInfo>)HttpContext.Current.Session[ISesiones.lstControlEventos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstControlEventos, value); }
        }

        public static BandejaEntradaInfo oBandejaEntrada
        {
            get { return (BandejaEntradaInfo)HttpContext.Current.Session[ISesiones.oBandejaEntrada]; }
            set { HttpContext.Current.Session.Add(ISesiones.oBandejaEntrada, value); }
        }
        public static bool? indEventoModal
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.indEventoModal]; }
            set { HttpContext.Current.Session.Add(ISesiones.indEventoModal, value); }
        }

        public static bool? ActualizarBandeja
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.ActualizarBandeja]; }
            set { HttpContext.Current.Session.Add(ISesiones.ActualizarBandeja, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstBandejaEntrada = "lstBandejaEntrada";
            public static string lstBandejaSeguimiento = "lstBandejaSeguimiento";
            public static string oBandejaEntrada = "oBandejaEntrada";
            public static string indEventoModal = "indEventoModal";
            public static string ActualizarBandeja = "ActualizarBandeja";
            public static string lstControlEventos = "lstControlEventos";
        }
        #endregion


    }
}
