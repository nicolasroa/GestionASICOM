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
    public class NegObjetivo
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;




        /// <summary>
        /// Método que Inserta o Modifica una Entidad Objetivo según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<ObjetivoInfo> Guardar(ObjetivoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObjetivoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ObjetivoInfo>(Entidad, GlobalDA.SP.Objetivo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Objetivo";
                return ObjetoResultado;
            }
        }

        public Resultado<ObjetivoInfo> Guardar(ref ObjetivoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObjetivoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ObjetivoInfo>(ref Entidad, GlobalDA.SP.Objetivo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Objetivo";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto ObjetivoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ObjetivoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ObjetivoInfo> Buscar(ObjetivoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ObjetivoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ObjetivoInfo, ObjetivoInfo>(Entidad, GlobalDA.SP.Objetivo_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Objetivo";
                return ObjetoResultado;
            }
        }

    }
}
