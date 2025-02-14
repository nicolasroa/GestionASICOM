using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegProvincia
    {


        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Provincia
        /// </summary>
        /// <param name="Entidad">Objeto ProvinciaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ProvinciaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ProvinciaInfo> Buscar(ProvinciaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProvinciaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ProvinciaInfo, ProvinciaInfo>(Entidad, GlobalDA.SP.Provincias_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Provincias";
                return ObjetoResultado;
            }
        }


        #endregion


    }
}
