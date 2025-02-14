using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegEventos
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Eventos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Eventos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<EventosInfo> Guardar(EventosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventosInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EventosInfo>(Entidad, GlobalDA.SP.Eventos_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Eventos";
                return ObjetoResultado;
            }
        }

        public Resultado<EventosInfo> Guardar(ref EventosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventosInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EventosInfo>(ref Entidad, GlobalDA.SP.Eventos_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Eventos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto EventosInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad EventosInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<EventosInfo> Buscar(EventosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EventosInfo, EventosInfo>(Entidad, GlobalDA.SP.Eventos_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Eventos";
                return ObjetoResultado;
            }
        }




        public Resultado<EventoRolesInfo> AsignarEventoRol(EventoRolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventoRolesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EventoRolesInfo>(Entidad, GlobalDA.SP.EventoRoles_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Evento Roles";
                return ObjetoResultado;
            }
        }


        public Resultado<EventoRolesInfo> DesAsignarEventoRol(EventoRolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventoRolesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<EventoRolesInfo>(Entidad, GlobalDA.SP.EventoRoles_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Evento Roles";
                return ObjetoResultado;
            }
        }
        public Resultado<EventoRolesInfo> ListarEventoRoles(EventoRolesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventoRolesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EventoRolesInfo, EventoRolesInfo>(Entidad, GlobalDA.SP.EventoRoles_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Evento Roles";
                return ObjetoResultado;
            }
        }


        public Resultado<EventosInfo> ListarResponsablesEvento(EventosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<EventosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<EventosInfo, EventosInfo>(Entidad, GlobalDA.SP.ResponsablesEvento_QEY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar Responsables por Evento";
                return ObjetoResultado;
            }
        }









        #endregion



        #region PROPIEDADES
        public static List<EventoRolesInfo> lstEventosRoles
        {
            get { return (List<EventoRolesInfo>)HttpContext.Current.Session[ISesiones.lstEventosRoles]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstEventosRoles, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstEventosRoles = "lstEventosRoles";

        }
        #endregion
    }
}
