using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegNotarias
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Notariass
        /// </summary>
        /// <param name="Entidad">Objeto NotariasInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad NotariasInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<NotariasInfo> Buscar(NotariasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<NotariasInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<NotariasInfo, NotariasInfo>(Entidad, GlobalDA.SP.Notarias_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstNotarias = new List<NotariasInfo>();
                lstNotarias = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Notariass";
                return ObjetoResultado;
            }
        }

        public Resultado<NotariasInfo> Guardar(NotariasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<NotariasInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<NotariasInfo>(Entidad, GlobalDA.SP.Notarias_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Notarias";
                return ObjetoResultado;
            }
        }

        public Resultado<NotariasInfo> Guardar(ref NotariasInfo Entidad)
        {

            var ObjetoResultado = new Resultado<NotariasInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<NotariasInfo>(ref Entidad, GlobalDA.SP.Notarias_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Notarias";
                return ObjetoResultado;
            }
        }

        #endregion

        #region PROPIEDADES
        public static List<NotariasInfo> lstNotarias
        {
            get { return (List<NotariasInfo>)HttpContext.Current.Session[ISesiones.lstNotarias]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstNotarias, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstNotarias = "lstNotarias";

        }
        #endregion

    }
}
