using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;


namespace WorkFlow.Negocio.WorkFlow
{
    public class NegSucursales
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Sucursales
        /// </summary>
        /// <param name="Entidad">Objeto SucursalInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SucursalInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SucursalInfo> Buscar(SucursalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SucursalInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SucursalInfo, SucursalInfo>(Entidad, GlobalDA.SP.Sucursales_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Sucursal";
                return ObjetoResultado;
            }
        }

        public Resultado<SucursalInfo> Guardar(SucursalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SucursalInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SucursalInfo>(Entidad, GlobalDA.SP.Sucursales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Sucursal";
                return ObjetoResultado;
            }
        }


        #endregion

        #region PROPIEDADES

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstSucursales = "lstSucursales";
        }
        #endregion
    }
}
