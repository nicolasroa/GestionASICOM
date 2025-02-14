using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegInversionistas
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;

        /// <summary>
        /// Método que realiza una Búsqueda en la Vista Inversionista
        /// </summary>
        /// <param name="Entidad">Objeto InversionistaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad InversionistaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<InversionistaInfo> Buscar(InversionistaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<InversionistaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<InversionistaInfo, InversionistaInfo>(Entidad, GlobalDA.SP.Inversionista_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Inversionista.";
                return ObjetoResultado;
            }
        }


        public Resultado<EtapasInversionistaResumen> BuscarResumen(EtapasInversionistaFiltro Entidad)
        {

            var ObjetoResultado = new Resultado<EtapasInversionistaResumen>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EtapasInversionistaResumen, EtapasInversionistaFiltro>(Entidad, GlobalDA.SP.ControlEventosInversionista_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Inversionista.";
                return ObjetoResultado;
            }
        }

        public Resultado<EtapasInversionistaDetalle> BuscarDetalle(EtapasInversionistaFiltro Entidad)
        {

            var ObjetoResultado = new Resultado<EtapasInversionistaDetalle>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EtapasInversionistaDetalle, EtapasInversionistaFiltro>(Entidad, GlobalDA.SP.ControlEventosInversionista_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Inversionista.";
                return ObjetoResultado;
            }
        }




        public Resultado<SolicitudInvercionistasInfo> BuscarSolicitudInversionistas(SolicitudInvercionistasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInvercionistasInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInvercionistasInfo, SolicitudInvercionistasInfo>(Entidad, GlobalDA.SP.SolicitudInversionistas_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstSolicitudesInversionista = new List<SolicitudInvercionistasInfo>();
                lstSolicitudesInversionista = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " SolicitudInvercionistasInfo";
                return ObjetoResultado;
            }
        }

        public Resultado<SolicitudInvercionistasInfo> GrabarSolicitudInversionistas(SolicitudInvercionistasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInvercionistasInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SolicitudInvercionistasInfo>(Entidad, GlobalDA.SP.SolicitudInversionistas_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " SolicitudInvercionistasInfo";
                return ObjetoResultado;
            }
        }

        public Resultado<SolicitudInvercionistasInfo> EliminarSolicitudInversionistas(SolicitudInvercionistasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInvercionistasInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SolicitudInvercionistasInfo>(Entidad, GlobalDA.SP.SolicitudInversionistas_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " SolicitudInvercionistasInfo";
                return ObjetoResultado;
            }
        }









        public static List<EtapasInversionistaDetalle> LstDetalleEtapa
        {
            get { return (List<EtapasInversionistaDetalle>)HttpContext.Current.Session[ISesiones.lstDetalleEtapa]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDetalleEtapa, value); }
        }

        public static List<SolicitudInvercionistasInfo> lstSolicitudesInversionista
        {
            get { return (List<SolicitudInvercionistasInfo>)HttpContext.Current.Session[ISesiones.lstSolicitudesInversionista]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstSolicitudesInversionista, value); }
        }



        #region SESIONES
        private class ISesiones
        {
            public static string lstDetalleEtapa = "lstDetalleEtapa";
            public static string lstSolicitudesInversionista = "lstSolicitudesInversionista";

        }
        #endregion

    }
}
