using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
   public class NegRegistroAntecedentesSalud
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        public Resultado<RegistroAntecedentesSaludInfo> GuardarRegistroAntecedentesSalud(RegistroAntecedentesSaludInfo Entidad)
        {

            Resultado<RegistroAntecedentesSaludInfo> ObjetoResultado = new Resultado<RegistroAntecedentesSaludInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroAntecedentesSaludInfo>(Entidad, GlobalDA.SP.RegistroAntecedentesSalud_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroAntecedentesSalud";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroAntecedentesSaludInfo> GuardarRegistroAntecedentesSalud(ref RegistroAntecedentesSaludInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroAntecedentesSaludInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroAntecedentesSaludInfo>(ref Entidad, GlobalDA.SP.RegistroAntecedentesSalud_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroAntecedentesSalud";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroAntecedentesSaludInfo> EliminarRegistroAntecedentesSalud(RegistroAntecedentesSaludInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroAntecedentesSaludInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroAntecedentesSaludInfo>(Entidad, GlobalDA.SP.RegistroAntecedentesSalud_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroAntecedentesSalud";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroAntecedentesSaludInfo> BuscarRegistroAntecedentesSalud(RegistroAntecedentesSaludInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroAntecedentesSaludInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroAntecedentesSaludInfo, RegistroAntecedentesSaludInfo>(Entidad, GlobalDA.SP.RegistroAntecedentesSalud_QRY, BaseSQL);
                lstRegistroAntecedentesSalud = new List<RegistroAntecedentesSaludInfo>();
                lstRegistroAntecedentesSalud = ObjetoResultado.Lista;
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

        #endregion
        #region PROPIEDADES

        

        public static List<RegistroAntecedentesSaludInfo> lstRegistroAntecedentesSalud
        {
            get { return (List<RegistroAntecedentesSaludInfo>)HttpContext.Current.Session[ISesiones.lstRegistroAntecedentesSalud]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstRegistroAntecedentesSalud, value); }
        }

       
        public static bool IndEnvio
        {
            get { return (bool)HttpContext.Current.Session[ISesiones.IndEnvio]; }
            set { HttpContext.Current.Session.Add(ISesiones.IndEnvio, value); }
        }



        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string IndEnvio = "IndEnvio";
            public const string lstRegistroAntecedentesSalud = "lstRegistroAntecedentesSalud";
        }
        #endregion
    }
}
