using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegInstitucionesFinancieras
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de InstitucionesFinancierasBase
        /// </summary>
        /// <param name="Entidad">Objeto InstitucionesFinancierasBase con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad InstitucionesFinancierasBase en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<InstitucionesFinancierasBase> Buscar(InstitucionesFinancierasBase Entidad)
        {

            var ObjetoResultado = new Resultado<InstitucionesFinancierasBase>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<InstitucionesFinancierasBase, InstitucionesFinancierasBase>(Entidad, GlobalDA.SP.InstitucionesFinancieras_QRY, BaseSQL);
                lstIIFF = new List<InstitucionesFinancierasBase>();
                lstIIFF = ObjetoResultado.Lista;
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

        public static InstitucionesFinancierasBase objIIFF
        {
            get { return (InstitucionesFinancierasBase)HttpContext.Current.Session[ISesiones.objIIFF]; }
            set { HttpContext.Current.Session.Add(ISesiones.objIIFF, value); }
        }

        public static List<InstitucionesFinancierasBase> lstIIFF
        {
            get { return (List<InstitucionesFinancierasBase>)HttpContext.Current.Session[ISesiones.lstIIFF]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstIIFF, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstIIFF = "lstIIFF";
            public const string objIIFF = "objIIFF";
        }
        #endregion

    }
}
