using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegRentasCliente
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de RentasClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto RentasClienteInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad RentasClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RentasClienteInfo> Buscar(RentasClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RentasClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RentasClienteInfo, RentasClienteInfo>(Entidad, GlobalDA.SP.RentasCliente_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                lstRentasClienteInfo = new List<RentasClienteInfo>();

                lstRentasClienteInfo = ObjetoResultado.Lista;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Observaciones";
                return ObjetoResultado;
            }
        }

        public Resultado<RentasClienteInfo> BuscarPromedio(RentasClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RentasClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RentasClienteInfo, RentasClienteInfo>(Entidad, GlobalDA.SP.RentaClientePromedio_QRY, BaseSQL);

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

        public Resultado<FlujosMensualesInfo> BuscarFlujoMensual(FlujosMensualesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujosMensualesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<FlujosMensualesInfo, FlujosMensualesInfo>(Entidad, GlobalDA.SP.FlujosMensuales_QRY, BaseSQL);

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

        /// <summary>
        /// Método que guarda un objeto de RentasClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto RentasClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad RentasClienteInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<RentasClienteInfo> Guardar(ref RentasClienteInfo Entidad)
        {
            var ObjetoResultado = new Resultado<RentasClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.RentasCliente_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objRentasClienteInfo = Entidad;
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
        /// Método que elimina un objeto de RentasClienteInfo
        /// </summary>
        /// <param name="Entidad">Objeto RentasClienteInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad RentasClienteInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RentasClienteInfo> Eliminar(RentasClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RentasClienteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RentasClienteInfo>(Entidad, GlobalDA.SP.RentasCliente_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

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

        public static RentasClienteInfo objRentasClienteInfo
        {
            get { return (RentasClienteInfo)HttpContext.Current.Session[ISesiones.objRentasClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objRentasClienteInfo, value); }
        }

        public static List<RentasClienteInfo> lstRentasClienteInfo
        {
            get { return (List<RentasClienteInfo>)HttpContext.Current.Session[ISesiones.lstRentasClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstRentasClienteInfo, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string objRentasClienteInfo = "objRentasClienteInfo";
            public const string lstRentasClienteInfo = "lstRentasClienteInfo";
        }
        #endregion

    }
}
