using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegPlazosSimulador
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;


        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Plazos Simulador Hipotecario
        /// </summary>
        /// <param name="Entidad">Objeto PlazosSimuladorInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad PlazosSimuladorInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<PlazosSimuladorInfo> Buscar(PlazosSimuladorInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PlazosSimuladorInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<PlazosSimuladorInfo, PlazosSimuladorInfo>(Entidad, GlobalDA.SP.PlazosSimulador_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Plazos";
                return ObjetoResultado;
            }
        }


    }
}
