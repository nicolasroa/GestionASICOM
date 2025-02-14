using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlow.Negocio.Documental
{
    public class NegArchivosRepositorios
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_DOC;
        #region PROPIEDADES

        public static List<ArchivosRepositoriosInfo> getsetlstArchivoRepositorio
        {
            get { return (List<ArchivosRepositoriosInfo>)HttpContext.Current.Session[ISesiones.ArchivoRepositorio]; }
            set { HttpContext.Current.Session.Add(ISesiones.ArchivoRepositorio, value); }
        }

        public static List<ArchivosRepositoriosInfo> getsetlstRepositorioDoc
        {
            get { return (List<ArchivosRepositoriosInfo>)HttpContext.Current.Session[ISesiones.ArchivoRepositorioPersona]; }
            set { HttpContext.Current.Session.Add(ISesiones.ArchivoRepositorioPersona, value); }
        }

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> Buscar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.ArchivosRepositoriosNew_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> BuscarBandejaGrupoDocumento(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.BandejaGrupoDocumentoSolicitud_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "BandejaGruposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> BuscarBandejaGrupoDocumentoRepositorio(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.BandejaGrupoDocumentoRepositorio_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "BandejaGruposDocumentosRepositorio";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<ArchivosRepositoriosInfo> Guardar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.ArchivosRepositorios_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        public static ArchivosRepositoriosInfo ObtenerArchivoRepositorio(int Id)
        {
            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            var Entidad = new ArchivosRepositoriosInfo();
            Entidad.Id = Id;
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.ArchivosRepositoriosNew_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado.Lista.FirstOrDefault();
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "ArchivosRepositorios";
                return new ArchivosRepositoriosInfo();
            }
        }

        public static ArchivosRepositoriosInfo ObtenerRepositorioDocumento(int Id)
        {
            var lstInfo = new List<ArchivosRepositoriosInfo>();
            lstInfo = (List<ArchivosRepositoriosInfo>)NegArchivosRepositorios.getsetlstRepositorioDoc;
            var ObjetoResultado = new ArchivosRepositoriosInfo();

            ObjetoResultado = (from x in lstInfo.AsEnumerable() where x.Id == Id select x).ToList()[0];

            return ObjetoResultado;







        }

        public Resultado<ArchivosRepositoriosInfo> Eliminar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.ArchivosRepositorios_DEL, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        public Resultado<ArchivosRepositoriosInfo> AsignarDocumentoSolicitud(ref ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ArchivosRepositoriosInfo>(Entidad, GlobalDA.SP.AsignarDocumentoSolicitud, GlobalDA.Accion.Guardar, BaseSQL);
                if (Entidad.Id == -1)
                {
                    ObjetoResultado.Mensaje = "Documento ya Asignado a la Solicitud";
                    ObjetoResultado.ResultadoGeneral = false;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        public static string GuardarDocumentoSolicitud(int? Solicitud_Id, int Grupo_Id, int TipoDocumento_Id, string NombreArchivo, Byte[] Archivo)
        {
            try
            {


                if (Archivo == null)
                {
                    return "Debe Generar el Archivo: " + NombreArchivo;
                }
                //Declaración de Variables
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();


                ObjetoArchivoRep.IdArchivo = -1;
                ObjetoArchivoRep.IdGrupoDocumento = Grupo_Id;//Antecedentes Comerciales
                ObjetoArchivoRep.IdTipoDocumento = TipoDocumento_Id;//Solicitud Crédito Hipotecario
                ObjetoArchivoRep.FechaEmision = DateTime.Now;
                ObjetoArchivoRep.Descripcion = "";
                ObjetoArchivoRep.NombreArchivo = NombreArchivo;
                ObjetoArchivoRep.ExtensionArchivo = ".pdf";
                ObjetoArchivoRep.SizeKB = Archivo.Length;
                ObjetoArchivoRep.ContentType = "application/pdf";
                ObjetoArchivoRep.ArchivoVB = Archivo;
                ObjetoArchivoRep.Estado_Id = 1;
                ObjetoArchivoRep.Rut = -1; //NegPortal.getsetDatosWorkFlow.Rut;
                ObjetoArchivoRep.NumeroSolicitud = Solicitud_Id.ToString();
                ObjetoArchivoRep.SistemaWF = "0";
                ObjetoArchivoRep.UsuarioCreacion_Id = (int)NegUsuarios.Usuario.Rut;
                ObjetoArchivoRep.UsuarioModificacion_Id = (int)NegUsuarios.Usuario.Rut;
                ObjetoArchivoRep.IdUsuarioWF = (int)NegUsuarios.Usuario.Rut;


                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.Guardar(ObjetoArchivoRep);

                if (!ObjetoResultado.ResultadoGeneral)
                {
                    return ObjetoResultado.Mensaje;

                }
                return "";
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    return Ex.Message;
                }
                else
                {
                    return "Error al Guardar el Documento en la Carpeta Digital";
                }

            }
        }


        #region SESIONES

        private class ISesiones
        {
            public static string ArchivoRepositorio = "ArchivoRepositorio";
            public static string ArchivoRepositorioPersona = "ArchivoRepositorioPersona";
            public static string RepositorioDoc = "RepositorioDoc";
        }
        #endregion

    }
}
