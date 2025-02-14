using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegAcciones
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Eventos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<AccionesInfo> Guardar(AccionesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AccionesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AccionesInfo>(Entidad, GlobalDA.SP.Acciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        public Resultado<AccionesInfo> Guardar(ref AccionesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AccionesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AccionesInfo>(ref Entidad, GlobalDA.SP.Acciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto AccionesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad AccionesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AccionesInfo> Buscar(AccionesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AccionesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AccionesInfo, AccionesInfo>(Entidad, GlobalDA.SP.Acciones_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        public Resultado<AccionesInfo> BuscarAccionesEvento(AccionesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AccionesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AccionesInfo, AccionesInfo>(Entidad, GlobalDA.SP.AccionesEvento_QRY, BaseSQL);
                lstAccionesEvento = new List<AccionesInfo>();
                lstAccionesEvento = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Acciones Evento";
                return ObjetoResultado;
            }
        }


        #endregion
        #region PROPIEDADES
        public static List<AccionesInfo> lstAccionesEvento
        {
            get { return (List<AccionesInfo>)HttpContext.Current.Session[ISesiones.lstAccionesEvento]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstAccionesEvento, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstAccionesEvento = "lstAccionesEvento";

        }
        #endregion



    }
}
