using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;

namespace WorkFlow.Negocio.WorkFlow
{
   public class NegDestino
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Destinos
        /// </summary>
        /// <param name="Entidad">Objeto DestinoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad DestinoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<DestinoInfo> Buscar(DestinoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DestinoInfo, DestinoInfo>(Entidad, GlobalDA.SP.Destino_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Destinos";
                return ObjetoResultado;
            }
        }

        public Resultado<DestinoInfo> Guardar(DestinoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DestinoInfo>(Entidad, GlobalDA.SP.Destino_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Destino";
                return ObjetoResultado;
            }
        }

        public Resultado<DestinoInfo> Guardar(ref DestinoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DestinoInfo>(ref Entidad, GlobalDA.SP.Destino_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Destino";
                return ObjetoResultado;
            }
        }

        #endregion
    }
}
