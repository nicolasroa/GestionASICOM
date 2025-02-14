using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
  public class NegFlujo
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        public Resultado<FlujoInfo> Guardar(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.Flujo_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Flujo";
                return ObjetoResultado;
            }
        }

        public Resultado<FlujoInfo> Eliminar(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.Flujo_DEL, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Flujo";
                return ObjetoResultado;
            }
        }

        public Resultado<FlujoInfo> Buscar(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<FlujoInfo, FlujoInfo>(Entidad, GlobalDA.SP.Flujo_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Flujo";
                return ObjetoResultado;
            }
        }

        public Resultado<SolicitudInfo> IniciarFlujo(SolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<SolicitudInfo>(Entidad, GlobalDA.SP.IniciarFlujo_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Problemas al Inciar el Flujo";
                return ObjetoResultado;
            }
        }
        
        public Resultado<FlujoInfo> TerminarEvento(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.TerminarEvento_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Terminar el Flujo";
                return ObjetoResultado;
            }
        }

        public Resultado<FlujoInfo> IniciarFlujoTasacion(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.IniciarFlujoTasacion_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Iniciar el Flujo de Tasación";
                return ObjetoResultado;
            }
        }

        public Resultado<FlujoInfo> IniciarFlujoEETT(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.IniciarFlujoEETT_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Iniciar el Flujo de EETT";
                return ObjetoResultado;
            }
        }

        public Resultado<FlujoInfo> IniciarFlujoDPS(FlujoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<FlujoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FlujoInfo>(Entidad, GlobalDA.SP.IniciarFlujoDPS_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Iniciar el Flujo de EETT";
                return ObjetoResultado;
            }
        }

        #endregion
        #region PROPIEDADES
        public static List<FlujoInfo> lstFlujos
        {
            get { return (List<FlujoInfo>)HttpContext.Current.Session[ISesiones.lstFlujos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstFlujos, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstFlujos = "lstFlujos";

        }
        #endregion

    }
}
