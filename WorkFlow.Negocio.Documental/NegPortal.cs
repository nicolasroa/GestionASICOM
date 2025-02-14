using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlow.Negocio.Documental
{
    public class NegPortal
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region PROPIEDADES

        public static DatosWorkflow getsetDatosWorkFlow
        {
            get { return (DatosWorkflow)HttpContext.Current.Session[ISesiones.DatosWorkflow]; }
            set { HttpContext.Current.Session.Add(ISesiones.DatosWorkflow, value); }
        }

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<DatosWorkflow> Buscar(DatosWorkflow Entidad)
        {
            var ObjetoResultado = new Resultado<DatosWorkflow>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DatosWorkflow, DatosWorkflow>(Entidad, GlobalDA.SP.DOC_DatosWorkflow_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " DatosWorkflow";
                return ObjetoResultado;
            }
        }

        #region SESIONES

        private class ISesiones
        {
            public static string DatosWorkflow = "DatosWorkflow";
        }
        #endregion
    }
}
