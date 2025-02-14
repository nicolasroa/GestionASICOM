using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegPropiedades
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

      
        public Resultado<TipoInmuebleInfo> GuardarTipoInmueble(TipoInmuebleInfo Entidad)
        {

            Resultado<TipoInmuebleInfo> ObjetoResultado = new Resultado<TipoInmuebleInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoInmuebleInfo>(Entidad, GlobalDA.SP.TipoInmueble_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoInmuebleInfo> GuardarTipoInmueble(ref TipoInmuebleInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoInmuebleInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoInmuebleInfo>(ref Entidad, GlobalDA.SP.TipoInmueble_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoInmuebleInfo> BuscarTipoInmueble(TipoInmuebleInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoInmuebleInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoInmuebleInfo, TipoInmuebleInfo>(Entidad, GlobalDA.SP.TipoInmueble_QRY, BaseSQL);
                lstTipoInmuebles = new List<TipoInmuebleInfo>();
                lstTipoInmuebles = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo de Inmueble";
                return ObjetoResultado;
            }
        }



        public Resultado<TipoConstruccionInfo> GuardarTipoConstruccion(TipoConstruccionInfo Entidad)
        {

            Resultado<TipoConstruccionInfo> ObjetoResultado = new Resultado<TipoConstruccionInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoConstruccionInfo>(Entidad, GlobalDA.SP.TipoConstruccion_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoConstruccionInfo> GuardarTipoConstruccion(ref TipoConstruccionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoConstruccionInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoConstruccionInfo>(ref Entidad, GlobalDA.SP.TipoConstruccion_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoConstruccionInfo> BuscarTipoConstruccion(TipoConstruccionInfo Entidad, bool cache = false)
        {

            var ObjetoResultado = new Resultado<TipoConstruccionInfo>();
            try
            {
                if (cache)
                {
                    if (lstTipoConstruccion != null)
                    {
                        ObjetoResultado.Lista = lstTipoConstruccion;
                        return ObjetoResultado;
                    }

                }
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoConstruccionInfo, TipoConstruccionInfo>(Entidad, GlobalDA.SP.TipoConstruccion_QRY, BaseSQL);
                lstTipoConstruccion = new List<TipoConstruccionInfo>();
                lstTipoConstruccion = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo de Inmueble";
                return ObjetoResultado;
            }
        }

        public Resultado<PropiedadInfo> GuardarPropiedad(PropiedadInfo Entidad)
        {

            Resultado<PropiedadInfo> ObjetoResultado = new Resultado<PropiedadInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<PropiedadInfo>(Entidad, GlobalDA.SP.Propiedad_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Propiedad";
                return ObjetoResultado;
            }
        }
        public Resultado<PropiedadInfo> GuardarPropiedad(ref PropiedadInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PropiedadInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<PropiedadInfo>(ref Entidad, GlobalDA.SP.Propiedad_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Propiedad";
                return ObjetoResultado;
            }
        }
        public Resultado<PropiedadInfo> BuscarPropiedad(PropiedadInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PropiedadInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<PropiedadInfo, PropiedadInfo>(Entidad, GlobalDA.SP.Propiedad_QRY, BaseSQL);
                lstPropiedades = new List<PropiedadInfo>();
                lstPropiedades = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Propiedad";
                return ObjetoResultado;
            }
        }
        public Resultado<PropiedadInfo> EliminarPropiedad(PropiedadInfo Entidad)
        {

            var ObjetoResultado = new Resultado<PropiedadInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<PropiedadInfo>(Entidad, GlobalDA.SP.Propiedad_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }



        public Resultado<TasacionInfo> GuardarTasacion(TasacionInfo Entidad)
        {

            Resultado<TasacionInfo> ObjetoResultado = new Resultado<TasacionInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TasacionInfo>(Entidad, GlobalDA.SP.Tasaciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tasacion";
                return ObjetoResultado;
            }
        }

        public Resultado<TasacionInfo> GuardarTasacion(ref TasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TasacionInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TasacionInfo>(ref Entidad, GlobalDA.SP.Tasaciones_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tasacion";
                return ObjetoResultado;
            }
        }

        public Resultado<TasacionInfo> BuscarTasacion(TasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TasacionInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TasacionInfo, TasacionInfo>(Entidad, GlobalDA.SP.Tasaciones_QRY, BaseSQL);
                lstTasaciones = new List<TasacionInfo>();
                lstTasaciones = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tasacion";
                return ObjetoResultado;
            }
        }

        public Resultado<TasacionInfo> EliminarTasacion(TasacionInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TasacionInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TasacionInfo>(Entidad, GlobalDA.SP.Tasaciones_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Tasacion";
                return ObjetoResultado;
            }
        }


        public Resultado<UsoPropiedadInfo> BuscarUsoPropiedad(UsoPropiedadInfo Entidad, bool cache = false)
        {

            var ObjetoResultado = new Resultado<UsoPropiedadInfo>();
            try
            {
                if (cache)
                {
                    if (lstUso != null)
                    {
                        ObjetoResultado.Lista = lstUso;
                        return ObjetoResultado;
                    }

                }
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UsoPropiedadInfo, UsoPropiedadInfo>(Entidad, GlobalDA.SP.UsoPropiedad_QRY, BaseSQL);
                lstUso = new List<UsoPropiedadInfo>();
                lstUso = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Uso Propiedad";
                return ObjetoResultado;
            }
        }


        #endregion



        #region PROPIEDADES

        public static List<TipoInmuebleInfo> lstTipoInmuebles
        {
            get { return (List<TipoInmuebleInfo>)HttpContext.Current.Session[ISesiones.lstTipoInmuebles]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoInmuebles, value); }
        }
        public static List<UsoPropiedadInfo> lstUso
        {
            get { return (List<UsoPropiedadInfo>)HttpContext.Current.Session[ISesiones.lstUso]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstUso, value); }
        }
        public static List<TipoConstruccionInfo> lstTipoConstruccion
        {
            get { return (List<TipoConstruccionInfo>)HttpContext.Current.Session[ISesiones.lstTipoConstruccion]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoConstruccion, value); }
        }
        public static List<PropiedadInfo> lstPropiedades
        {
            get { return (List<PropiedadInfo>)HttpContext.Current.Session[ISesiones.lstPropiedad]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstPropiedad, value); }
        }
        public static List<TasacionInfo> lstTasaciones
        {
            get { return (List<TasacionInfo>)HttpContext.Current.Session[ISesiones.lstTasacionProp]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTasacionProp, value); }
        }
        public static List<TasacionInfo> lstPropiedadesTasaciones
        {
            get { return (List<TasacionInfo>)HttpContext.Current.Session[ISesiones.lstPropiedadesTasaciones]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstPropiedadesTasaciones, value); }
        }
        public static List<TasacionInfo> lstPropiedadesEETT
        {
            get { return (List<TasacionInfo>)HttpContext.Current.Session[ISesiones.lstPropiedadesEETT]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstPropiedadesEETT, value); }
        }
        public static TasacionInfo objTasacion
        {
            get => (TasacionInfo)HttpContext.Current.Session[ISesiones.objTasaciones];
            set => HttpContext.Current.Session.Add(ISesiones.objTasaciones, value);
        }
        public static PropiedadInfo objPropiedad
        {
            get => (PropiedadInfo)HttpContext.Current.Session[ISesiones.objPropiedad];
            set => HttpContext.Current.Session.Add(ISesiones.objPropiedad, value);
        }

        public static TasacionInfo objEETT
        {
            get => (TasacionInfo)HttpContext.Current.Session[ISesiones.objEETT];
            set => HttpContext.Current.Session.Add(ISesiones.objEETT, value);
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstTipoConstruccion = "lstTipoConstruccions";
            public const string lstTipoInmuebles = "lstTipoInmuebles";
            public const string lstPropiedad = "lstPropiedad";
            public const string lstTasacionProp = "lstTasacionProp";
            public const string lstPropiedadesEETT = "lstPropiedadesEETT";
            public const string lstPropiedadesTasaciones = "lstPropiedadesTasaciones";
            public const string objTasaciones = "objTasaciones";
            public const string objPropiedad = "objPropiedad";
            public const string objEETT = "objEETT";
            public const string lstUso = "lstUso";

        }
        #endregion


    }
}
