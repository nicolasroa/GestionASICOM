using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;

namespace WorkFlow.Negocio.WorkFlow
{
   public class NegTarifado
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Eventos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TarifadoInfo> Guardar(TarifadoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<TarifadoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TarifadoInfo>(Entidad, GlobalDA.SP.Tarifado_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tarifado";
                return ObjetoResultado;
            }
        }

        public Resultado<TarifadoInfo> Guardar(ref TarifadoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TarifadoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TarifadoInfo>(ref Entidad, GlobalDA.SP.Tarifado_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tarifado";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto TarifadoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TarifadoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TarifadoInfo> Buscar(TarifadoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TarifadoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TarifadoInfo, TarifadoInfo>(Entidad, GlobalDA.SP.Tarifado_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tarifado";
                return ObjetoResultado;
            }
        }


        #endregion

    }
}
