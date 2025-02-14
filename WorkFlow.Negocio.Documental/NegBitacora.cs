using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.Documental
{
    public class NegBitacora
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_DOC;
        #region PROPIEDADES

        public static List<BitacoraInfo> getLstBitacora
        {
            get { return (List<BitacoraInfo>)HttpContext.Current.Session[ISesiones.lstBitacora]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstBitacora, value); }
        }

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Bitacora
        /// </summary>
        /// <param name="Entidad">Objeto Bitacora con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Bitacora en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<BitacoraInfo> Buscar(BitacoraInfo Entidad)
        {

            var ObjetoResultado = new Resultado<BitacoraInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<BitacoraInfo, BitacoraInfo>(Entidad, GlobalDA.SP.Bitacora_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "Bitacora";
                return ObjetoResultado;
            }
        }

        #region SESIONES

        private class ISesiones
        {
            public static string lstBitacora = "lstBitacora";
        }
        #endregion
    }
}
