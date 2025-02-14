using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegSeguros
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;


        public Resultado<SeguroInfo> Buscar(SeguroInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SeguroInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SeguroInfo, SeguroInfo>(Entidad, GlobalDA.SP.Seguros_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Seguros";
                return ObjetoResultado;
            }
        }


        /// <summary>
        /// Método que Inserta o Modifica una Entidad Eventos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<SegurosContratadosInfo> GuardarSegurosContratados(SegurosContratadosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SegurosContratadosInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SegurosContratadosInfo>(Entidad, GlobalDA.SP.SegurosContratados_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " SegurosContratados";
                return ObjetoResultado;
            }
        }

        public Resultado<SegurosContratadosInfo> GuardarSegurosContratados(ref SegurosContratadosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SegurosContratadosInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SegurosContratadosInfo>(ref Entidad, GlobalDA.SP.SegurosContratados_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " SegurosContratados";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto SegurosContratadosInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SegurosContratadosInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SegurosContratadosInfo> BuscarSegurosContratados(SegurosContratadosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SegurosContratadosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SegurosContratadosInfo, SegurosContratadosInfo>(Entidad, GlobalDA.SP.SegurosContratados_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " SegurosContratados";
                return ObjetoResultado;
            }
        }




    }
}
