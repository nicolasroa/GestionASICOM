using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System; 

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegRegion
    {


        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Region
        /// </summary>
        /// <param name="Entidad">Objeto RegionInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad RegionInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RegionInfo> Buscar(RegionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegionInfo, RegionInfo>(Entidad, GlobalDA.SP.Regiones_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Regiones";
                return ObjetoResultado;
            }
        }


        #endregion


    }
}
