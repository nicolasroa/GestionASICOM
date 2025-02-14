using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegSubsidio
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;


        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Subsidio
        /// </summary>
        /// <param name="Entidad">Objeto SubsidioInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SubsidioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SubsidioInfo> Buscar(SubsidioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SubsidioInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SubsidioInfo, SubsidioInfo>(Entidad, GlobalDA.SP.Subsidio_QRY, BaseSQL);
                lstSubsidios = new List<SubsidioInfo>();
                lstSubsidios = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Subsidio";
                return ObjetoResultado;
            }
        }


        #region PROPIEDADES

        public static List<SubsidioInfo> lstSubsidios
        {
            get { return (List<SubsidioInfo>)HttpContext.Current.Session[ISesiones.lstSubsidios]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstSubsidios, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstSubsidios = "lstSubsidios";
        }
        #endregion



    }
}
