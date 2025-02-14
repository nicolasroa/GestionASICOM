using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegObservaciones
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de ObservacionInfo
        /// </summary>
        /// <param name="Entidad">Objeto ObservacionInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ObservacionInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ObservacionInfo> Buscar(ObservacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObservacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ObservacionInfo, ObservacionInfo>(Entidad, GlobalDA.SP.Observaciones_QRY, BaseSQL);
                lstObservacionInfo = new List<ObservacionInfo>();
                lstObservacionInfo = ObjetoResultado.Lista;
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


        /// <summary>
        /// Método que guarda un objeto de ObservacionInfo
        /// </summary>
        /// <param name="Entidad">Objeto ObservacionInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad ObservacionInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<ObservacionInfo> Guardar(ref ObservacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObservacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.Observaciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objObservacionInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }
        public Resultado<ObservacionInfo> Guardar(ObservacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObservacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.Observaciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }
        #endregion


        #region PROPIEDADES

        public static ObservacionInfo objObservacionInfo
        {
            get { return (ObservacionInfo)HttpContext.Current.Session[ISesiones.objObservacionInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objObservacionInfo, value); }
        }
        public static bool indModificaObservacion
        {
            get
            {
                if (HttpContext.Current.Session[ISesiones.indModificaObservacion] == null)
                    return false;
                else
                    return (bool)HttpContext.Current.Session[ISesiones.indModificaObservacion];
            }
            set { HttpContext.Current.Session.Add(ISesiones.indModificaObservacion, value); }
        }

        public static List<ObservacionInfo> lstObservacionInfo
        {
            get { return (List<ObservacionInfo>)HttpContext.Current.Session[ISesiones.lstObservacionInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstObservacionInfo, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstObservacionInfo = "lstObservacionInfo";
            public const string objObservacionInfo = "objObservacionInfo";
            public const string indModificaObservacion = "indModificaObservacion";
        }
        #endregion



    }
}
