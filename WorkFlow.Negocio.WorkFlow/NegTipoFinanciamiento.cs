using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;



namespace WorkFlow.Negocio.WorkFlow
{
     public class NegTipoFinanciamiento
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;




        /// <summary>
        /// Método que Inserta o Modifica una Entidad Eventos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TipoFinanciamientoInfo> Guardar(TipoFinanciamientoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoFinanciamientoInfo>(Entidad, GlobalDA.SP.TipoFinanciamiento_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Financiamiento";
                return ObjetoResultado;
            }
        }

        public Resultado<TipoFinanciamientoInfo> Guardar(ref TipoFinanciamientoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoFinanciamientoInfo>(ref Entidad, GlobalDA.SP.TipoFinanciamiento_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Financiamiento";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto TipoFinanciamientoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoFinanciamientoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TipoFinanciamientoInfo> Buscar(TipoFinanciamientoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoFinanciamientoInfo, TipoFinanciamientoInfo>(Entidad, GlobalDA.SP.TipoFinanciamiento_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo Financiamiento";
                return ObjetoResultado;
            }
        }

    }
}
