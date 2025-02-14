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
    public class NegConservador
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto ConservadorInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ConservadorInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ConservadorInfo> Buscar(ConservadorInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ConservadorInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ConservadorInfo, ConservadorInfo>(Entidad, GlobalDA.SP.Conservador_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Conservadores";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto ConservadorInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ConservadorInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<DatosCBRInfo> Buscar(DatosCBRInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DatosCBRInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DatosCBRInfo, DatosCBRInfo>(Entidad, GlobalDA.SP.DatosCBR_QRY, BaseSQL);
                ObjLstDatosCBR = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Datos CBR";
                return ObjetoResultado;
            }
        }


        public Resultado<DatosCBRInfo> GuardarDatosCBR(DatosCBRInfo Entidad)
        {

            Resultado<DatosCBRInfo> ObjetoResultado = new Resultado<DatosCBRInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DatosCBRInfo>(Entidad, GlobalDA.SP.DatosCBR_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Datos CBR";
                return ObjetoResultado;
            }
        }

        #endregion
        #region PROPIEDADES
        public static List<ConservadorInfo> lstConservadores
        {
            get { return (List<ConservadorInfo>)HttpContext.Current.Session[ISesiones.lstConservador]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstConservador, value); }
        }

        public static List<DatosCBRInfo> ObjLstDatosCBR
        {
            get { return (List<DatosCBRInfo>)HttpContext.Current.Session[ISesiones.ObjLstSessDatosCBR]; }
            set { HttpContext.Current.Session.Add(ISesiones.ObjLstSessDatosCBR, value); }
        }

        public static DatosCBRInfo ObjInfoDatosCBR
        {
            get { return (DatosCBRInfo)HttpContext.Current.Session[ISesiones.ObjInfoSessDatosCBR]; }
            set { HttpContext.Current.Session.Add(ISesiones.ObjInfoSessDatosCBR, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstConservador = "lstConservador";
            public static string ObjLstSessDatosCBR = "ObjLstSessDatosCBR";
            public static string ObjInfoSessDatosCBR = "ObjInfoSessDatosCBR";

        }
        #endregion


    }
}
