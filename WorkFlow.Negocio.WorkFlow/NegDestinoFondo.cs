using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegDestinoFondo
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        public Resultado<TipoDestinoFondoInfo> GuardarTipoDestino(TipoDestinoFondoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoDestinoFondoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoDestinoFondoInfo>(Entidad, GlobalDA.SP.TipoDestinoFondo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Destino Fondo";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoDestinoFondoInfo> BuscarTipoDestino(TipoDestinoFondoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<TipoDestinoFondoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoDestinoFondoInfo, TipoDestinoFondoInfo>(Entidad, GlobalDA.SP.TipoDestinoFondo_QRY, BaseSQL);
                lstTipoDestinoFondos = new List<TipoDestinoFondoInfo>();
                lstTipoDestinoFondos = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo Destino Fondo";
                return ObjetoResultado;
            }
        }

        public Resultado<DestinoFondoInfo> BuscarBeneficiarioDestino(TipoDestinoFondoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<DestinoFondoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DestinoFondoInfo, TipoDestinoFondoInfo>(Entidad, GlobalDA.SP.BeneficiarioDestinoFondo_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo Destino Fondo";
                return ObjetoResultado;
            }
        }


        public Resultado<DestinoFondoInfo> GuardarDestinoFondo(DestinoFondoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoFondoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DestinoFondoInfo>(Entidad, GlobalDA.SP.DestinoFondo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Destino de Fondos";
                return ObjetoResultado;
            }
        }
        public Resultado<DestinoFondoInfo> EliminarDestinoFondo(DestinoFondoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoFondoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DestinoFondoInfo>(Entidad, GlobalDA.SP.DestinoFondo_DEL, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Destino de Fondos";
                return ObjetoResultado;
            }
        }

        public Resultado<DestinoFondoInfo> BuscarDestinoFondo(DestinoFondoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DestinoFondoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DestinoFondoInfo, DestinoFondoInfo>(Entidad, GlobalDA.SP.DestinoFondo_QRY, BaseSQL);
                lstDestinoFondos = new List<DestinoFondoInfo>();
                lstDestinoFondos = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Destino de Fondos";
                return ObjetoResultado;
            }
        }


        #endregion

        #region PROPIEDADES
        public static List<DestinoFondoInfo> lstDestinoFondos
        {
            get { return (List<DestinoFondoInfo>)HttpContext.Current.Session[ISesiones.lstDestinoFondos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDestinoFondos, value); }
        }
        public static DestinoFondoInfo objDestinoFondos
        {
            get { return (DestinoFondoInfo)HttpContext.Current.Session[ISesiones.objDestinoFondos]; }
            set { HttpContext.Current.Session.Add(ISesiones.objDestinoFondos, value); }
        }

        public static List<TipoDestinoFondoInfo> lstTipoDestinoFondos
        {
            get { return (List<TipoDestinoFondoInfo>)HttpContext.Current.Session[ISesiones.lstTipoDestinoFondos]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoDestinoFondos, value); }
        }
        public static TipoDestinoFondoInfo objTipoDestinoFondos
        {
            get { return (TipoDestinoFondoInfo)HttpContext.Current.Session[ISesiones.objTipoDestinoFondos]; }
            set { HttpContext.Current.Session.Add(ISesiones.objTipoDestinoFondos, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstDestinoFondos = "lstDestinoFondos";
            public static string objDestinoFondos = "objDestinoFondos";
            public static string lstTipoDestinoFondos = "lstTipoDestinoFondos";
            public static string objTipoDestinoFondos = "objTipoDestinoFondos";

        }

    }
    #endregion


}

