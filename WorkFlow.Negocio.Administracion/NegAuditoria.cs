using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlow.Negocio.Administracion
{
    public class NegAuditoria
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_LOG;
        public NegAuditoria()
        {

        }

        #region METODOS
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Auditoria
        /// </summary>
        /// <param name="Entidad">Objeto AuditoriaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad AuditoriaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AuditoriaInfo> Buscar(AuditoriaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AuditoriaInfo, AuditoriaInfo>(Entidad, GlobalDA.SP.Auditoria_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Auditoria";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que lista a los Usuarios Respopnsables que estan presentes en la tablas Auditoria
        /// </summary>
        /// <returns>>Lista de la Entidad AuditoriaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AuditoriaInfo> ListarResponsables()
        {

            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AuditoriaInfo, AuditoriaInfo>(new AuditoriaInfo(), GlobalDA.SP.ResponsablesAuditoria_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Responsables Auditoria";
                return ObjetoResultado;
            }
        }

        #endregion


        #region PROPIEDADES
        public static List<AuditoriaInfo> ListaAuditoriaPDF
        {
            get { return (List<AuditoriaInfo>)HttpContext.Current.Session[ISesiones.ListaAuditoriaPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.ListaAuditoriaPDF, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string ListaAuditoriaPDF = "ListaAuditoriaPDF";
        }
        #endregion
    }
}
