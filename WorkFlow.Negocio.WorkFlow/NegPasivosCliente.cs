using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegPasivosCliente
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS


        public Resultado<TipoPasivoInfo> BuscarTipo(TipoPasivoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoPasivoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoPasivoInfo, TipoPasivoInfo>(Entidad, GlobalDA.SP.TipoPasivo_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;



                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Observaciones";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoPasivoInfo> GuardarTipo(TipoPasivoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<TipoPasivoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.TipoPasivo_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }




        /// <summary>
        /// Método que realiza una consulta de PasivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto PasivosClienteInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad PasivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<PasivosClienteInfo> Buscar(PasivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PasivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<PasivosClienteInfo, PasivosClienteInfo>(Entidad, GlobalDA.SP.PasivosCliente_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                lstPasivosClienteInfo = new List<PasivosClienteInfo>();

                lstPasivosClienteInfo = ObjetoResultado.Lista;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar el Total de Activos";
                return ObjetoResultado;
            }
        }
        public Resultado<PasivosClienteInfo> BuscarTotal(PasivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PasivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<PasivosClienteInfo, PasivosClienteInfo>(Entidad, GlobalDA.SP.PasivosClienteTotal_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                lstPasivosClienteInfo = new List<PasivosClienteInfo>();

                lstPasivosClienteInfo = ObjetoResultado.Lista;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar el Total de Pasivos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que guarda un objeto de PasivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto PasivosClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad PasivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<PasivosClienteInfo> Guardar(ref PasivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PasivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.PasivosCliente_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objPasivosClienteInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que elimina un objeto de PasivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto PasivosClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad PasivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<PasivosClienteInfo> Eliminar(PasivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PasivosClienteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<PasivosClienteInfo>(Entidad, GlobalDA.SP.PasivosCliente_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<PasivosSolicitudInfo> BuscarPasivosSolicitud(PasivosSolicitudInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PasivosSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<PasivosSolicitudInfo, PasivosSolicitudInfo>(Entidad, GlobalDA.SP.PasivosSolicitud_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;



                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Observaciones";
                return ObjetoResultado;
            }
        }
        #endregion


        #region PROPIEDADES

        public static List<PasivosClienteInfo> lstPasivosClienteInfo
        {
            get { return (List<PasivosClienteInfo>)HttpContext.Current.Session[ISesiones.lstPasivosClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstPasivosClienteInfo, value); }
        }

        public static PasivosClienteInfo objPasivosClienteInfo
        {
            get { return (PasivosClienteInfo)HttpContext.Current.Session[ISesiones.objPasivosClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objPasivosClienteInfo, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string objPasivosClienteInfo = "objPasivosClienteInfo";
            public const string lstPasivosClienteInfo = "lstPasivosClienteInfo";
          
        }
        #endregion

    }
}
