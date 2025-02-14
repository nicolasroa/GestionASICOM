using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;

namespace WorkFlow.Negocio.WorkFlow
{
  public  class NegEtapas
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de Etapas
        /// </summary>
        /// <param name="Entidad">Objeto EtapaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad EtapaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<EtapaInfo> Buscar(EtapaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EtapaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EtapaInfo, EtapaInfo>(Entidad, GlobalDA.SP.Etapas_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Etapas";
                return ObjetoResultado;
            }
        }

        public Resultado<EtapaInfo> Guardar(EtapaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EtapaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EtapaInfo>(Entidad, GlobalDA.SP.Etapas_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Etapas";
                return ObjetoResultado;
            }
        }

        public Resultado<EtapaInfo> Guardar(ref EtapaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EtapaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EtapaInfo>(ref Entidad, GlobalDA.SP.Etapas_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Etapas";
                return ObjetoResultado;
            }
        }

        #endregion
    }
}
