using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegGastosOperacionales
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        public Resultado<GastoOperacionalInfo> CalcularGastosSimulacion(GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GastoOperacionalInfo, GastoOperacionalInfo>(Entidad, GlobalDA.SP.GastosOperacionalesSimulacion_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstGastosOperacionales = new List<GastoOperacionalInfo>();
                lstGastosOperacionales = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Gastos Operacionales";
                return ObjetoResultado;
            }
        }

        public Resultado<TipoGastoOperacionalInfo> BuscarTipoGastos(TipoGastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoGastoOperacionalInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoGastoOperacionalInfo, TipoGastoOperacionalInfo>(Entidad, GlobalDA.SP.TipoGastoOperacional_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                    lstTipoGastosOperacionales = new List<TipoGastoOperacionalInfo>();
                    lstTipoGastosOperacionales = ObjetoResultado.Lista;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo Gastos Operacionales";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoGastoOperacionalInfo> GuardarTipoGasto(TipoGastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoGastoOperacionalInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoGastoOperacionalInfo>(Entidad, GlobalDA.SP.TipoGastoOperacional_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Gasto Operacional " + EX.Message;
                return ObjetoResultado;
            }
        }


        public Resultado<GastoOperacionalInfo> BuscarGastos(GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GastoOperacionalInfo, GastoOperacionalInfo>(Entidad, GlobalDA.SP.GastosOperacionales_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                lstGastosOperacionales = new List<GastoOperacionalInfo>();
                lstGastosOperacionales = ObjetoResultado.Lista;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Gastos Operacionales";
                return ObjetoResultado;
            }
        }

        public Resultado<GastoOperacionalInfo> ValorizarGastos(GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<GastoOperacionalInfo, GastoOperacionalInfo>(Entidad, GlobalDA.SP.ValorizarGastoOperacional_PRC, BaseSQL);
             
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Gastos Operacionales";
                return ObjetoResultado;
            }
        }


        public Resultado<GastoOperacionalInfo> GuardarGasto(GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<GastoOperacionalInfo>(Entidad, GlobalDA.SP.GastosOperacionales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Gasto Operacional " + EX.Message;
                return ObjetoResultado;
            }
        }
        public Resultado<GastoOperacionalInfo> GuardarGasto(ref GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<GastoOperacionalInfo>(ref Entidad, GlobalDA.SP.GastosOperacionales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Gasto Operacional " + EX.Message;
                return ObjetoResultado;
            }
        }
        public Resultado<GastoOperacionalInfo> EliminarGasto(GastoOperacionalInfo Entidad)
        {

            var ObjetoResultado = new Resultado<GastoOperacionalInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<GastoOperacionalInfo>(Entidad, GlobalDA.SP.GastosOperacionales_DEL, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Gasto Operacional " + EX.Message;
                return ObjetoResultado;
            }
        }

        public Resultado<QuienPagaGGOOInfo> BuscarQuienPaga(QuienPagaGGOOInfo Entidad)
        {

            var ObjetoResultado = new Resultado<QuienPagaGGOOInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<QuienPagaGGOOInfo, QuienPagaGGOOInfo>(Entidad, GlobalDA.SP.QuienPaga_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Quien Paga GGOO";
                return ObjetoResultado;
            }
        }
        public Resultado<ComoPagaGGOOInfo> BuscarComoPaga(ComoPagaGGOOInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ComoPagaGGOOInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ComoPagaGGOOInfo, ComoPagaGGOOInfo>(Entidad, GlobalDA.SP.ComoPaga_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Como Paga GGOO";
                return ObjetoResultado;
            }
        }

        public Resultado<IntegracionGGOOInfo> BuscarResumenCuenta(IntegracionGGOOInfo Entidad)
        {

            var ObjetoResultado = new Resultado<IntegracionGGOOInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<IntegracionGGOOInfo, IntegracionGGOOInfo>(Entidad, GlobalDA.SP.ResumenCuenta_QRY, ConfigBase.ConexionSQL_GGOO);
              
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Resumen de Cuenta";
                return ObjetoResultado;
            }
        }

        public Resultado<IntegracionGGOOInfo> SolicitarProvision(IntegracionGGOOInfo Entidad)
        {

            var ObjetoResultado = new Resultado<IntegracionGGOOInfo>();
            try
            {
                
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<IntegracionGGOOInfo>(Entidad, GlobalDA.SP.AgregarPoliticaGGOO_GRB, GlobalDA.Accion.Guardar, ConfigBase.ConexionSQL_GGOO);

                return ObjetoResultado;
            }
            catch (Exception EX)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Provisión de GGOO " + EX.Message;
                return ObjetoResultado;
            }
        }



        #region PROPIEDADES



        public static GastoOperacionalInfo objGastoOperacional
        {
            get { return (GastoOperacionalInfo)HttpContext.Current.Session[ISesiones.objGastoOperacional]; }
            set { HttpContext.Current.Session.Add(ISesiones.objGastoOperacional, value); }
        }
        public static List<GastoOperacionalInfo> lstGastosOperacionales
        {
            get { return (List<GastoOperacionalInfo>)HttpContext.Current.Session[ISesiones.lstGastosOperacionales]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstGastosOperacionales, value); }
        }



        public static TipoGastoOperacionalInfo objTipoGastoOperacional
        {
            get { return (TipoGastoOperacionalInfo)HttpContext.Current.Session[ISesiones.objTipoGastoOperacional]; }
            set { HttpContext.Current.Session.Add(ISesiones.objTipoGastoOperacional, value); }
        }
        public static List<TipoGastoOperacionalInfo> lstTipoGastosOperacionales
        {
            get { return (List<TipoGastoOperacionalInfo>)HttpContext.Current.Session[ISesiones.lstTipoGastosOperacionales]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoGastosOperacionales, value); }
        }



        #endregion

        #region SESIONES
        private class ISesiones
        {
           
            public const string objGastoOperacional = "objGastoOperacional";
            public const string lstGastosOperacionales = "lstGastosOperacionales";

            public const string objTipoGastoOperacional = "objTipoGastoOperacional";
            public const string lstTipoGastosOperacionales = "lstTipoGastosOperacionales";

        }
        #endregion
    }
}
