using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegDocumentosEstudioTitulo
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS
        public Resultado<DocumentosEstudioTituloInfo> GuardarDocumentosEstudioTitulo(DocumentosEstudioTituloInfo Entidad)
        {

            Resultado<DocumentosEstudioTituloInfo> ObjetoResultado = new Resultado<DocumentosEstudioTituloInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DocumentosEstudioTituloInfo>(Entidad, GlobalDA.SP.DocumentosEstudioTitulo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " DocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }
        public Resultado<DocumentosEstudioTituloInfo> GuardarDocumentosEstudioTitulo(ref DocumentosEstudioTituloInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DocumentosEstudioTituloInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DocumentosEstudioTituloInfo>(ref Entidad, GlobalDA.SP.DocumentosEstudioTitulo_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " DocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }
        public Resultado<DocumentosEstudioTituloInfo> BuscarDocumentosEstudioTitulo(DocumentosEstudioTituloInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DocumentosEstudioTituloInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DocumentosEstudioTituloInfo, DocumentosEstudioTituloInfo>(Entidad, GlobalDA.SP.DocumentosEstudioTitulo_QRY, BaseSQL);
                lstDocumentosEstudioTitulo = new List<DocumentosEstudioTituloInfo>();
                lstDocumentosEstudioTitulo = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " DocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }

        public Resultado<RegistroDocumentosEstudioTituloInfo> GuardarRegistroDocumentosEstudioTitulo(RegistroDocumentosEstudioTituloInfo Entidad)
        {

            Resultado<RegistroDocumentosEstudioTituloInfo> ObjetoResultado = new Resultado<RegistroDocumentosEstudioTituloInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosEstudioTituloInfo>(Entidad, GlobalDA.SP.RegistroDocumentosEstudioTitulo_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosEstudioTituloInfo> GuardarRegistroDocumentosEstudioTitulo(ref RegistroDocumentosEstudioTituloInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosEstudioTituloInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosEstudioTituloInfo>(ref Entidad, GlobalDA.SP.RegistroDocumentosEstudioTitulo_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosEstudioTituloInfo> EliminarRegistroDocumentosEstudioTitulo(RegistroDocumentosEstudioTituloInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosEstudioTituloInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RegistroDocumentosEstudioTituloInfo>(Entidad, GlobalDA.SP.RegistroDocumentosEstudioTitulo_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RegistroDocumentosEstudioTitulo";
                return ObjetoResultado;
            }
        }
        public Resultado<RegistroDocumentosEstudioTituloInfo> BuscarRegistroDocumentosEstudioTitulo(RegistroDocumentosEstudioTituloInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RegistroDocumentosEstudioTituloInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RegistroDocumentosEstudioTituloInfo, RegistroDocumentosEstudioTituloInfo>(Entidad, GlobalDA.SP.RegistroDocumentosEstudioTitulo_QRY, BaseSQL);
                lstRegistroDocumentosEstudioTitulo = new List<RegistroDocumentosEstudioTituloInfo>();
                lstRegistroDocumentosEstudioTitulo = ObjetoResultado.Lista;
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

        public static List<DocumentosEstudioTituloInfo> lstDocumentosEstudioTitulo
        {
            get { return (List<DocumentosEstudioTituloInfo>)HttpContext.Current.Session[ISesiones.lstDocumentosEstudioTitulo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDocumentosEstudioTitulo, value); }
        }

        public static List<RegistroDocumentosEstudioTituloInfo> lstRegistroDocumentosEstudioTitulo
        {
            get { return (List<RegistroDocumentosEstudioTituloInfo>)HttpContext.Current.Session[ISesiones.lstRegistroDocumentosEstudioTitulo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstRegistroDocumentosEstudioTitulo, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstDocumentosEstudioTitulo = "lstDocumentosEstudioTitulo";
            public const string lstRegistroDocumentosEstudioTitulo = "lstRegistroDocumentosEstudioTitulo";
        }
        #endregion
    }

}
