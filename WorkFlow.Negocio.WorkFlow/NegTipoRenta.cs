using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegTipoRenta
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de TipoRentaInfo
        /// </summary>
        /// <param name="Entidad">Objeto TipoRentaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoRentaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TipoRentaInfo> Buscar(TipoRentaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoRentaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoRentaInfo, TipoRentaInfo>(Entidad, GlobalDA.SP.TipoRenta_QRY, BaseSQL);

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
        /// Método que guarda un objeto de TipoRentaInfo
        /// </summary>
        /// <param name="Entidad">Objeto TipoRentaInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad TipoRentaInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<TipoRentaInfo> Guardar(ref TipoRentaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoRentaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.TipoRenta_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objTipoRentaInfo = Entidad;
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

        public static TipoRentaInfo objTipoRentaInfo
        {
            get { return (TipoRentaInfo)HttpContext.Current.Session[ISesiones.objTipoRentaInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objTipoRentaInfo, value); }
        }

        #endregion

        #region SESIONES

        private class ISesiones
        {
            public const string objTipoRentaInfo = "objTipoRentaInfo";
        }

        #endregion

    }
}
