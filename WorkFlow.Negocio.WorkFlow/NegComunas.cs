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
    public class NegComunas
    {


        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Comunas
        /// </summary>
        /// <param name="Entidad">Objeto ComunaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ComunaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ComunaInfo> Buscar(ComunaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ComunaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ComunaInfo, ComunaInfo>(Entidad, GlobalDA.SP.Comunas_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Comunas";
                return ObjetoResultado;
            }
        }

        public Resultado<ComunaSiiInfo> BuscarComunaSii(ComunaSiiInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ComunaSiiInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ComunaSiiInfo, ComunaSiiInfo>(Entidad, GlobalDA.SP.ComunasSii_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Comunas Sii";
                return ObjetoResultado;
            }
        }


        #endregion


    }
}
