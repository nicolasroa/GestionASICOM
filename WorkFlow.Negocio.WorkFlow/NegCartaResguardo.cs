using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegCartaResguardo
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        ///// <summary>
        ///// Método que realiza una consulta de Apoderados Tasacion
        ///// </summary>
        ///// <param name = "Entidad" > Objeto ApoderadoTasacionInfo con el Filtro de Búsqueda correspondiente</param>
        ///// <returns>Lista de la Entidad ApoderadoTasacionInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ApoderadoTasacionInfo> BuscarApoderadoTasacion(ApoderadoTasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ApoderadoTasacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ApoderadoTasacionInfo, ApoderadoTasacionInfo>(Entidad, GlobalDA.SP.ApoderadoTasacion_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstApoderadoTasacionInfo = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }


        public Resultado<ApoderadoTasacionInfo> GuardarApoderadoTasacion(ApoderadoTasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ApoderadoTasacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.ApoderadoTasacion_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objApoderadoTasacionInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        public Resultado<ApoderadoTasacionInfo> EliminarApoderadoTasacion(ApoderadoTasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ApoderadoTasacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.ApoderadoTasacion_DEL, GlobalDA.Accion.Eliminar, BaseSQL);
                objApoderadoTasacionInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        ///// <summary>
        ///// Método que realiza una consulta de Documentos
        ///// </summary>
        ///// <param name = "Entidad" > Objeto DocumentosCartaResguardoInfo con el Filtro de Búsqueda correspondiente</param>
        ///// <returns>Lista de la Entidad DocumentosCartaResguardoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<DocumentosCartaResguardoInfo> BuscarDocumentosCartaResguardo(DocumentosCartaResguardoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DocumentosCartaResguardoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DocumentosCartaResguardoInfo, DocumentosCartaResguardoInfo>(Entidad, GlobalDA.SP.DocumentosCartaResguardo_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstDocumentos = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        public Resultado<RegistroDocumentosCartaResguardoInfo> BuscarRegistroDocumentosCartaResguardo(RegistroDocumentosCartaResguardoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosCartaResguardoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosCartaResguardoInfo, RegistroDocumentosCartaResguardoInfo>(Entidad, GlobalDA.SP.RegistroDocumentosCartaResguardo_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        public Resultado<RegistroDocumentosCartaResguardoInfo> GuardarRegistroDocumentos(RegistroDocumentosCartaResguardoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosCartaResguardoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.RegistroDocumentosCartaResguardo_INS, GlobalDA.Accion.Guardar, BaseSQL);
                objRegistroDocumentos = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        public Resultado<RegistroDocumentosCartaResguardoInfo> EliminaRegistroDocumentos(RegistroDocumentosCartaResguardoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosCartaResguardoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.RegistroDocumentosCartaResguardo_DEL, GlobalDA.Accion.Eliminar, BaseSQL);
                objRegistroDocumentos = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Apoderado Tasación.";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda enla Tabla UsuarioRol
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioRol con el filtro de búsqueda</param>
        /// <returns>Lista de UsuarioRol en el Atributo Lista del Objeto Resultado</returns>
        public Resultado<UsuarioRolInfo> BuscarUsuarioRol(UsuarioRolInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UsuarioRolInfo, UsuarioRolInfo>(Entidad, GlobalDA.SP.UsuarioRol_QRY, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " UsuarioRol";
                return ObjetoResultado;
            }
        }
        #endregion

        #region PROPIEDADES

        public static List<DocumentosCartaResguardoInfo> lstDocumentos
        {
            get { return (List<DocumentosCartaResguardoInfo>)HttpContext.Current.Session[ISesiones.lstDocumentos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDocumentos, value); }
        }

        public static List<ApoderadoTasacionInfo> lstApoderadoTasacionInfo
        {
            get { return (List<ApoderadoTasacionInfo>)HttpContext.Current.Session[ISesiones.lstApoderadoTasacionInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstApoderadoTasacionInfo, value); }
        }

        public static RegistroDocumentosCartaResguardoInfo objRegistroDocumentos
        {
            get { return (RegistroDocumentosCartaResguardoInfo)HttpContext.Current.Session[ISesiones.objRegistroDocumentos]; }
            set { HttpContext.Current.Session.Add(ISesiones.objRegistroDocumentos, value); }
        }
        
        public static ApoderadoTasacionInfo objApoderadoTasacionInfo
        {
            get { return (ApoderadoTasacionInfo)HttpContext.Current.Session[ISesiones.objApoderadoTasacionInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objApoderadoTasacionInfo, value); }
        }

        public static TasacionInfo objTasacion
        {
            get { return (TasacionInfo)HttpContext.Current.Session[ISesiones.objTasacionCarRes]; }
            set { HttpContext.Current.Session.Add(ISesiones.objTasacionCarRes, value); }
        }
        public static List<TasacionInfo> lstTasacion
        {
            get { return (List<TasacionInfo>)HttpContext.Current.Session[ISesiones.lstTasacionCartResg]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTasacionCartResg, value); }
        }
        public static ReporteCartaResguardo oReporteCartaResguardo
        {
            get { return (ReporteCartaResguardo)HttpContext.Current.Session[ISesiones.oReporteCartaResguardo]; }
            set { HttpContext.Current.Session.Add(ISesiones.oReporteCartaResguardo, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstApoderadoTasacionInfo = "lstApoderadoTasacionInfo";
            public const string objApoderadoTasacionInfo = "objApoderadoTasacionInfo";
            public const string lstApoderadoInfo = "lstApoderadoInfo";
            public const string lstGarantias = "lstGarantias";
            public const string objTasacionCarRes = "objTasacion";
            public const string lstTasacionCartResg = "lstTasacionCartResg";
            public const string objGarantias = "objGarantias";
            public const string lstDocumentos = "lstDocumentos";
            public const string objRegistroDocumentos = "objRegistroDocumentos";
            public const string oReporteCartaResguardo = "oReporteCartaResguardo";
        }
        #endregion


    }
}
