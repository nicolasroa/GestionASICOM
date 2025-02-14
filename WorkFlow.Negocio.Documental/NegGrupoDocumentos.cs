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
    public class NegGrupoDocumentos
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_DOC;
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<GruposDocumentos> Buscar(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GruposDocumentos, GruposDocumentos>(Entidad, GlobalDA.SP.GruposDocumentos_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " GruposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<GruposDocumentos> ObtenerGrupoDocumentos(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GruposDocumentos, GruposDocumentos>(Entidad, GlobalDA.SP.GruposDocumentosPortalNew_QRY, BaseSQL);
                
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " GruposDocumentosPortal";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<GruposDocumentos> Guardar(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<GruposDocumentos>(Entidad, GlobalDA.SP.GruposDocumentos_INS, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " GruposDocumentos";
                return ObjetoResultado;
            }
        }
    }
}
