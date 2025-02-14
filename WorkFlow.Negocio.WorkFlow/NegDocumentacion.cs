using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegDocumentacion
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS


        public Resultado<DocumentosPersonalesInfo> GuardarDocumentosPersonales(DocumentosPersonalesInfo Entidad)
        {

            Resultado<DocumentosPersonalesInfo> ObjetoResultado = new Resultado<DocumentosPersonalesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DocumentosPersonalesInfo>(Entidad, GlobalDA.SP.DocumentosPersonales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " DocumentosPersonales";
                return ObjetoResultado;
            }
        }
        public Resultado<DocumentosPersonalesInfo> GuardarDocumentosPersonales(ref DocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DocumentosPersonalesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DocumentosPersonalesInfo>(ref Entidad, GlobalDA.SP.DocumentosPersonales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " DocumentosPersonales";
                return ObjetoResultado;
            }
        }
        public Resultado<DocumentosPersonalesInfo> BuscarDocumentosPersonales(DocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DocumentosPersonalesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DocumentosPersonalesInfo, DocumentosPersonalesInfo>(Entidad, GlobalDA.SP.DocumentosPersonales_QRY, BaseSQL);
                lstDocumentosPersonales = new List<DocumentosPersonalesInfo>();
                lstDocumentosPersonales = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " DocumentosPersonales";
                return ObjetoResultado;
            }
        }

        public Resultado<RegistroDocumentosPersonalesInfo> GuardarRegistroDocumentosPersonales(RegistroDocumentosPersonalesInfo Entidad)
        {

            Resultado<RegistroDocumentosPersonalesInfo> ObjetoResultado = new Resultado<RegistroDocumentosPersonalesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosPersonalesInfo>(Entidad, GlobalDA.SP.RegistroDocumentosPersonales_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosPersonales";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosPersonalesInfo> GuardarRegistroDocumentosPersonales(ref RegistroDocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosPersonalesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosPersonalesInfo>(ref Entidad, GlobalDA.SP.RegistroDocumentosPersonales_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosPersonales";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosPersonalesInfo> EliminarRegistroDocumentosPersonales(RegistroDocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosPersonalesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosPersonalesInfo>(Entidad, GlobalDA.SP.RegistroDocumentosPersonales_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosPersonales";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosPersonalesInfo> BuscarRegistroDocumentosPersonales(RegistroDocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosPersonalesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosPersonalesInfo, RegistroDocumentosPersonalesInfo>(Entidad, GlobalDA.SP.RegistroDocumentosPersonales_QRY, BaseSQL);
                lstRegistroDocumentosPersonales = new List<RegistroDocumentosPersonalesInfo>();
                lstRegistroDocumentosPersonales = ObjetoResultado.Lista;
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
        public Resultado<RegistroDocumentosPersonalesInfo> BuscarRegistroDocumentosPersonalesSolicitados(RegistroDocumentosPersonalesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosPersonalesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosPersonalesInfo, RegistroDocumentosPersonalesInfo>(Entidad, GlobalDA.SP.RegistroDocumentosPersonalesSolicitados_QRY, BaseSQL);
                lstRegistroDocumentosPersonales = new List<RegistroDocumentosPersonalesInfo>();
                lstRegistroDocumentosPersonales = ObjetoResultado.Lista;
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








        public Resultado<RegistroDocumentosOriginacionComercialInfo> GuardarRegistroDocumentosOriginacionComercial(RegistroDocumentosOriginacionComercialInfo Entidad)
        {

            Resultado<RegistroDocumentosOriginacionComercialInfo> ObjetoResultado = new Resultado<RegistroDocumentosOriginacionComercialInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosOriginacionComercialInfo>(Entidad, GlobalDA.SP.RegistroDocumentosOriginacionComercial_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosOriginacionComercial";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosOriginacionComercialInfo> EliminarRegistroDocumentosOriginacionComercial(RegistroDocumentosOriginacionComercialInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosOriginacionComercialInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosOriginacionComercialInfo>(Entidad, GlobalDA.SP.RegistroDocumentosOriginacionComercial_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosOriginacionComercial";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosOriginacionComercialInfo> BuscarRegistroDocumentosOriginacionComercial(RegistroDocumentosOriginacionComercialInfo Entidad)
            {

            var ObjetoResultado = new Resultado<RegistroDocumentosOriginacionComercialInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosOriginacionComercialInfo, RegistroDocumentosOriginacionComercialInfo>(Entidad, GlobalDA.SP.RegistroDocumentosOriginacionComercial_QRY, BaseSQL);
                lstRegistroDocumentosOriginacionComercial = new List<RegistroDocumentosOriginacionComercialInfo>();
                lstRegistroDocumentosOriginacionComercial = ObjetoResultado.Lista;
               
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Docuemntos Originacion Comercial";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosOriginacionComercialInfo> BuscarRegistroDocumentosOriginacionComercialSolicitados(RegistroDocumentosOriginacionComercialInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosOriginacionComercialInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosOriginacionComercialInfo, RegistroDocumentosOriginacionComercialInfo>(Entidad, GlobalDA.SP.RegistroDocumentosOriginacionComercialSolicitados_QRY, BaseSQL);
                lstRegistroDocumentosOriginacionComercial = new List<RegistroDocumentosOriginacionComercialInfo>();
                lstRegistroDocumentosOriginacionComercial = ObjetoResultado.Lista;
               
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RegistroDocumentosOriginacionComercialInfo";
                return ObjetoResultado;
            }
        }





        #endregion
        #region PROPIEDADES

        public static List<DocumentosPersonalesInfo> lstDocumentosPersonales
        {
            get { return (List<DocumentosPersonalesInfo>)HttpContext.Current.Session[ISesiones.lstDocumentosPersonales]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDocumentosPersonales, value); }
        }

        public static List<RegistroDocumentosPersonalesInfo> lstRegistroDocumentosPersonales
        {
            get { return (List<RegistroDocumentosPersonalesInfo>)HttpContext.Current.Session[ISesiones.lstRegistroDocumentosPersonales]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstRegistroDocumentosPersonales, value); }
        }
        public static List<RegistroDocumentosOriginacionComercialInfo> lstRegistroDocumentosOriginacionComercial
        {
            get { return (List<RegistroDocumentosOriginacionComercialInfo>)HttpContext.Current.Session[ISesiones.lstRegistroDocumentosOriginacionComercial]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstRegistroDocumentosOriginacionComercial, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstDocumentosPersonales = "lstDocumentosPersonales";
            public const string lstRegistroDocumentosPersonales = "lstRegistroDocumentosPersonales";
            public const string lstRegistroDocumentosOriginacionComercial = "lstRegistroDocumentosOriginacionComercial";
        }
        #endregion
    }
}
