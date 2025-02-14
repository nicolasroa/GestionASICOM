using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegConfiguracionHipotecaria
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Productos
        /// </summary>
        /// <param name="Entidad">Objeto ConfiguracionHipotecariaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ConfiguracionHipotecariaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ConfiguracionHipotecariaInfo> Buscar(ConfiguracionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ConfiguracionHipotecariaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ConfiguracionHipotecariaInfo, ConfiguracionHipotecariaInfo>(Entidad, GlobalDA.SP.ConfiguracionHipotecaria_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Configuración Hipotecaria";
                return ObjetoResultado;
            }
        }
        public Resultado<ConfiguracionHipotecariaInfo> Guardar(ConfiguracionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ConfiguracionHipotecariaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ConfiguracionHipotecariaInfo>(Entidad, GlobalDA.SP.ConfiguracionHipotecaria_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Configuración Hipotecaria";
                return ObjetoResultado;
            }
        }
        #endregion
    }
}
