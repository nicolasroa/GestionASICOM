using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;


namespace WorkFlow.Negocio.Administracion
{


    public class NegRoles
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_SEG;
        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Roles según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Roles</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<RolesInfo> Guardar(RolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RolesInfo>(Entidad, GlobalDA.SP.Roles_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Roles";
                return ObjetoResultado;
            }
        }

        public Resultado<RolesInfo> Guardar(ref RolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RolesInfo>(ref Entidad, GlobalDA.SP.Roles_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Roles";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Roless
        /// </summary>
        /// <param name="Entidad">Objeto RolesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad RolesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RolesInfo> Buscar(RolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RolesInfo, RolesInfo>(Entidad, GlobalDA.SP.Roles_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Roles";
                return ObjetoResultado;
            }
        }
        #endregion



    }
}
