using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegActivosCliente
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS


        public Resultado<TipoActivoInfo> BuscarTipo(TipoActivoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoActivoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoActivoInfo, TipoActivoInfo>(Entidad, GlobalDA.SP.TipoActivo_QRY, BaseSQL);

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

        public Resultado<ActivosSolicitudInfo> BuscarActivosSolicitud(ActivosSolicitudInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ActivosSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ActivosSolicitudInfo, ActivosSolicitudInfo>(Entidad, GlobalDA.SP.ActivosSolicitud_QRY, BaseSQL);

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
        public Resultado<TipoActivoInfo> GuardarTipo(TipoActivoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<TipoActivoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.TipoActivo_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }



        /// <summary>
        /// Método que realiza una consulta de ActivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto ActivosClienteInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ActivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ActivosClienteInfo> Buscar(ActivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ActivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ActivosClienteInfo, ActivosClienteInfo>(Entidad, GlobalDA.SP.ActivosCliente_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                lstActivosClienteInfo = new List<ActivosClienteInfo>();

                lstActivosClienteInfo = ObjetoResultado.Lista;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Observaciones";
                return ObjetoResultado;
            }
        }



        public Resultado<ActivosClienteInfo> BuscarTotal(ActivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ActivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ActivosClienteInfo, ActivosClienteInfo>(Entidad, GlobalDA.SP.ActivosClienteTotal_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                lstActivosClienteInfo = new List<ActivosClienteInfo>();

                lstActivosClienteInfo = ObjetoResultado.Lista;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Observaciones";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que guarda un objeto de ActivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto ActivosClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad ActivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<ActivosClienteInfo> Guardar(ref ActivosClienteInfo Entidad)
        {
            var ObjetoResultado = new Resultado<ActivosClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.ActivosCliente_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objActivosClienteInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observaciones.";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que elimina un objeto de ActivosClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto ActivosClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad ActivosClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ActivosClienteInfo> Eliminar(ActivosClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ActivosClienteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ActivosClienteInfo>(Entidad, GlobalDA.SP.ActivosCliente_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }

        #endregion

        #region PROPIEDADES

        public static ActivosClienteInfo objActivosClienteInfo
        {
            get { return (ActivosClienteInfo)HttpContext.Current.Session[ISesiones.objActivosClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objActivosClienteInfo, value); }
        }

        public static List<ActivosClienteInfo> lstActivosClienteInfo
        {
            get { return (List<ActivosClienteInfo>)HttpContext.Current.Session[ISesiones.lstActivosClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstActivosClienteInfo, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string objActivosClienteInfo = "objActivosClienteInfo";
            public const string lstActivosClienteInfo = "lstActivosClienteInfo";
        }
        #endregion

    }
}
