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
   public class NegTiposDocumentos
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_DOC;
        #region PROPIEDADES

        public static List<TiposDocumentosInfo> getLstTipoDoc
        {
            get { return (List<TiposDocumentosInfo>)HttpContext.Current.Session[ISesiones.lstTipoDoc]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoDoc, value); }
        }

        #endregion

        public NegTiposDocumentos()
        {

        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tipos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto TipoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoDocumento en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TiposDocumentosInfo> Buscar(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TiposDocumentosInfo, TiposDocumentosInfo>(Entidad, GlobalDA.SP.TiposDocumentos_QRY, BaseSQL);
              
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " TiposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tipos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto TipoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoDocumento en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TiposDocumentosInfo> ObtenerTipoDocumentos(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TiposDocumentosInfo, TiposDocumentosInfo>(Entidad, GlobalDA.SP.TiposDocumentosPortalNew_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " TiposDocumentosPortal";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TiposDocumentosInfo> Guardar(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TiposDocumentosInfo>(Entidad, GlobalDA.SP.TiposDocumentos_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " TiposDocumentos";
                return ObjetoResultado;
            }
        }

        public static TiposDocumentosInfo ObtenerTipoDocumento(int Id)
        {
            List<TiposDocumentosInfo> lstInfo = new List<TiposDocumentosInfo>();
            lstInfo = getLstTipoDoc;
            var ObjetoResultado = new TiposDocumentosInfo();

            ObjetoResultado = (from x in lstInfo.AsEnumerable() where x.Id == Id select x).ToList()[0];

            return ObjetoResultado;
        }

        #region SESIONES

        private class ISesiones
        {
            public static string lstTipoDoc = "lstTipoDoc";
        }
        #endregion
    }
}
