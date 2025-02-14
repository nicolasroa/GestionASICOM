using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegProyectos
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;


        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Proyecto 
        /// </summary>
        /// <param name="Entidad">Objeto ProyectoInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ProyectoInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ProyectoInfo> Buscar(ProyectoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProyectoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ProyectoInfo, ProyectoInfo>(Entidad, GlobalDA.SP.Proyectos_QRY, BaseSQL);
                lstProyectos = new List<ProyectoInfo>();
                lstProyectos = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Proyectos";
                return ObjetoResultado;
            }
        }

        public Resultado<ProyectoInfo> Guardar(ProyectoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProyectoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ProyectoInfo>(Entidad, GlobalDA.SP.Proyectos_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Proyectos";
                return ObjetoResultado;
            }
        }

        public Resultado<ProyectoInfo> Guardar(ref ProyectoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProyectoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ProyectoInfo>(ref Entidad, GlobalDA.SP.Proyectos_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Proyectos";
                return ObjetoResultado;
            }
        }



        #region PROPIEDADES

        public static List<ProyectoInfo> lstProyectos
        {
            get { return (List<ProyectoInfo>)HttpContext.Current.Session[ISesiones.lstProyectos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstProyectos, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstProyectos = "lstProyectos";
        }
        #endregion

    }
}
