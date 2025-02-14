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
    public class NegProductos
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Productos
        /// </summary>
        /// <param name="Entidad">Objeto ProductoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ProductoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ProductoInfo> Buscar(ProductoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProductoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ProductoInfo, ProductoInfo>(Entidad, GlobalDA.SP.Producto_QRY, BaseSQL);
                lstProductos = new List<ProductoInfo>();
                lstProductos = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Producto";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Meses de Gracia
        /// </summary>
        /// <param name="Entidad">Objeto GraciaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad GraciaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<GraciaInfo> BuscarGracia(GraciaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GraciaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GraciaInfo, GraciaInfo>(Entidad, GlobalDA.SP.Gracia_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Gracia";
                return ObjetoResultado;
            }


        }

        public Resultado<ProductoInfo> Guardar(ProductoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProductoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ProductoInfo>(Entidad, GlobalDA.SP.Producto_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Producto";
                return ObjetoResultado;
            }
        }

        public Resultado<ProductoInfo> Guardar(ref ProductoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProductoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ProductoInfo>(ref Entidad, GlobalDA.SP.Producto_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Producto";
                return ObjetoResultado;
            }
        }

        #endregion

        #region PROPIEDADES
        public static List<ProductoInfo> lstProductos
        {
            get { return (List<ProductoInfo>)HttpContext.Current.Session[ISesiones.slstProductos]; }
            set { HttpContext.Current.Session.Add(ISesiones.slstProductos, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string slstProductos = "lstProductos";
        }
        #endregion
    }
}
