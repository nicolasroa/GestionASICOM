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
    public class NegTextoReportes
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de TextoReportesd
        /// </summary>
        /// <param name="Entidad">Objeto TextoReportesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TextoReportesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TextoReportesInfo> Buscar(TextoReportesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TextoReportesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TextoReportesInfo, TextoReportesInfo>(Entidad, GlobalDA.SP.TextoReportes_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Texto Reportes";
                return ObjetoResultado;
            }
        }


        #endregion
    }
}
