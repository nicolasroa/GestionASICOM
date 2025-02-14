using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Linq;
using System.Web;

namespace WorkFlow.Negocio.Administracion
{
    public class NegUsuarios
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_SEG;
        public static string BaseSQL_WF = ConfigBase.ConexionSQL_WF;
        public NegUsuarios()
        {

        }

        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Usuario según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Usuario</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<UsuarioInfo> Guardar(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.UsuarioId;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<UsuarioInfo>(Entidad, GlobalDA.SP.Usuario_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Usuarios";
                return ObjetoResultado;
            }
        }

        public Resultado<UsuarioInfo> Guardar(ref UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.UsuarioId;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<UsuarioInfo>(ref Entidad, GlobalDA.SP.Usuario_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Usuarios";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Usuarios
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<UsuarioInfo> Buscar(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UsuarioInfo, UsuarioInfo>(Entidad, GlobalDA.SP.Usuario_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Usuarios";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda enla Tabla UsuarioRol
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioRol con el filtro de búsqueda</param>
        /// <returns>Lista de UsuarioRol en el Atributo Lista del Objeto Resultado</returns>
        public Resultado<UsuarioRolInfo> BuscarUsuarioRol(UsuarioRolInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UsuarioRolInfo, UsuarioRolInfo>(Entidad, GlobalDA.SP.UsuarioRol_QRY, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " UsuarioRol";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que asigna un Rol a un Usuario
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioRol con el detalle de la Asignación</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<UsuarioRolInfo> AsignarUsuarioRol(UsuarioRolInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            try
            {
                Entidad.Responsable_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<UsuarioRolInfo>(Entidad, GlobalDA.SP.UsuarioRol_INS, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " UsuarioRol";
                return ObjetoResultado;
            }

        }

        /// <summary>
        /// Método que Desasigna un Rol a un Usuario
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioRol con el detalle de la Asignación</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<UsuarioRolInfo> DesasignarUsuarioRol(UsuarioRolInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<UsuarioRolInfo>(Entidad, GlobalDA.SP.UsuarioRol_DEL, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " UsuarioRol";
                return ObjetoResultado;
            }

        }

        /// <summary>
        /// Método que valida los datos de un Usuario al Ingresar al Sistema
        /// </summary>
        /// <param name="Entidad">Objeto Usuario con el Nombre de Usuario y la Clave a validar</param>
        /// <returns>Objeto Resultado con el mensaje y el Objeto Usuario validado.</returns>
        public Resultado<UsuarioInfo> Validar(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var ObjetoUsuario = new UsuarioInfo();
            var NegUsuario = new NegUsuarios();
            int? EstadoInactivo = new int();
            try
            {
                var ObjetoConfiguracion = new ConfiguracionGeneralInfo();
                ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
                if (ObjetoConfiguracion == null)
                {
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorConfiguracionNoEncontrada.ToString());
                    ObjetoResultado.ResultadoGeneral = false;
                    return ObjetoResultado;
                }
                EstadoInactivo = NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoInactivo);
                if (EstadoInactivo == null)
                {
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCodigoInactivoNoEncontrado.ToString());
                    ObjetoResultado.ResultadoGeneral = false;
                    return ObjetoResultado;
                }
                ObjetoUsuario.NombreUsuario = Entidad.NombreUsuario;
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();
                    //Usuario no Encontrado
                    if (ObjetoUsuario == null) { ObjetoResultado.ValorString = ConfigUsuario.ValidacionNoEncontrado; }
                    //Usuario Inactivo
                    else if (ObjetoUsuario.Estado_Id == EstadoInactivo) { ObjetoResultado.ValorString = ConfigUsuario.ValidacionInactivo; }
                    //Clave Incorrecta
                    else if (ObjetoUsuario.Contraseña != Seguridad.Encriptar(Entidad.Contraseña)) { ObjetoResultado.ValorString = ConfigUsuario.ValidacionErrorClave; ObjetoResultado.Objeto = ObjetoUsuario; }
                    // Cambio de Clave
                    else if (Funciones.DateDiff(DateInterval.Day, (DateTime)ObjetoUsuario.FechaUltimoCambioClave, DateTime.Now) >= ObjetoConfiguracion.PlazoValidez)
                    {
                        ObjetoResultado.ValorString = ConfigUsuario.ValidacionCambioClave;
                        ObjetoResultado.Objeto = ObjetoUsuario;
                    }
                    //Primer Inicio
                    else if (ObjetoUsuario.PrimerInicio == true)
                    {
                        ObjetoResultado.ValorString = ConfigUsuario.ValidacionPrimerInicio;
                        ObjetoResultado.Objeto = ObjetoUsuario;
                    }
                    //Aviso Cambio Clave
                    else if (ObjetoConfiguracion.PlazoValidez - Funciones.DateDiff(DateInterval.Day, (DateTime)ObjetoUsuario.FechaUltimoCambioClave, DateTime.Now) <= ObjetoConfiguracion.Notificacion)
                    {
                        ObjetoResultado.ValorString = ConfigUsuario.ValidacionAvisoCambioClave;
                        ObjetoResultado.ValorInt = int.Parse((ObjetoConfiguracion.PlazoValidez - Funciones.DateDiff(DateInterval.Day, (DateTime)ObjetoUsuario.FechaUltimoCambioClave, DateTime.Now)).ToString());
                        ObjetoResultado.Objeto = ObjetoUsuario;
                    }
                    //Aprobado
                    else
                    {
                        ObjetoResultado.ValorString = ConfigUsuario.ValidacionAprobado;
                        ObjetoResultado.Objeto = ObjetoUsuario;
                    }
                    ObjetoResultado.Objeto = ObjetoUsuario;
                }
                return ObjetoResultado;
            }
            catch (Exception Ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                if (Constantes.ModoDebug == true) { ObjetoResultado.Mensaje = Ex.Message; }
                else { ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Usuario"; }
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Metodo que genera una Clave Aleatorea y la Encripta
        /// </summary>
        /// <returns>Nueva Clave Encriptada</returns>
        public static string GenerarClave()
        {
            Random r = new Random();
            string newClave = "";
            newClave = r.Next(100000, 999999).ToString();
            newClave = Seguridad.Encriptar(newClave);
            return newClave;
        }


        /// <summary>
        /// Método que valida la clave del Usuario
        /// </summary>
        /// <param name="Clave">Clave a Validar</param>
        /// <returns>Resultado de la validacion</returns>
        public static bool ValidarClave(string Clave)
        {
            var ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            int Numeros = 0;
            int x = 0;
            Clave.ToArray();

            for (x = 0; x <= Clave.Length - 1; x++)
            {
                if (char.IsNumber(Clave[x]))
                    Numeros++;
            }

            if (Numeros == 0)
                return false;
            else if (Numeros == Clave.Length)
                return false;
            else if (Clave.Length < ObjetoConfiguracion.TamanioClave)
                return false;
            else
                return true;



        }

        /// <summary>
        /// Metodo que Obtiene un Usuario por el atributo NombreUsuario
        /// </summary>
        /// <param name="Entidad">Obeto Usuario</param>
        /// <returns>Resultado con el objeto Usuario en su atributo "Objeto"</returns>
        public Resultado<UsuarioInfo> ObtenerUsuario(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var ObjetoUsuario = new UsuarioInfo();
            var NegUsuario = new NegUsuarios();
            try
            {

                ObjetoUsuario.NombreUsuario = Entidad.NombreUsuario;
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {

                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();
                    ObjetoResultado.Objeto = ObjetoUsuario;

                }
                return ObjetoResultado;
            }
            catch (Exception Ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                if (Constantes.ModoDebug == true) { ObjetoResultado.Mensaje = Ex.Message; }
                else { ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Usuario"; }
                return ObjetoResultado;
            }
        }


        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Usuarios
        /// </summary>
        /// <param name="Entidad">Objeto UsuarioInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<UsuarioInfo> BuscarUsuariosPorRol(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UsuarioInfo, UsuarioInfo>(Entidad, GlobalDA.SP.UsuarioRol_QRY, BaseSQL_WF);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Usuarios";
                return ObjetoResultado;
            }
        }


        #endregion

        #region PROPIEDADES
        public static string NombreUsuario
        {
            get { return (string)HttpContext.Current.Session[ISesiones.NombreUsuario]; }
            set { HttpContext.Current.Session.Add(ISesiones.NombreUsuario, value); }
        }
        public static int? UsuarioId
        {
            get { return (int?)HttpContext.Current.Session[ISesiones.UsuarioId]; }
            set { HttpContext.Current.Session.Add(ISesiones.UsuarioId, value); }
        }
        public static UsuarioInfo Usuario
        {
            get { return (UsuarioInfo)HttpContext.Current.Session[ISesiones.Usuario]; }
            set { HttpContext.Current.Session.Add(ISesiones.Usuario, value); }
        }

        public static UsuarioInfo NuevoUsuario
        {
            get { return (UsuarioInfo)HttpContext.Current.Session[ISesiones.NuevoUsuario]; }
            set { HttpContext.Current.Session.Add(ISesiones.NuevoUsuario, value); }
        }
        public static UsuarioInfo UsuarioRecuperarClave
        {
            get { return (UsuarioInfo)HttpContext.Current.Session[ISesiones.UsuarioRecuperarClave]; }
            set { HttpContext.Current.Session.Add(ISesiones.UsuarioRecuperarClave, value); }
        }
        public static string MensajeCambioClave
        {
            get { return (string)HttpContext.Current.Session[ISesiones.MensajeCambioClave]; }
            set { HttpContext.Current.Session.Add(ISesiones.MensajeCambioClave, value); }
        }
        public static bool? DireccionarInicio
        {
            get
            {
                if (HttpContext.Current.Session[ISesiones.DireccionarInicio] == null)
                    return null;
                else
                    return (bool?)HttpContext.Current.Session[ISesiones.DireccionarInicio];
            }
            set { HttpContext.Current.Session.Add(ISesiones.DireccionarInicio, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string NombreUsuario = "NombreUsuario";
            public static string UsuarioId = "UsuarioId";
            public static string Usuario = "Usuario";
            public static string NuevoUsuario = "NuevoUsuario";
            public static string UsuarioRecuperarClave = "UsuarioRecuperarClave";
            public static string MensajeCambioClave = "MensajeCambioClave";
            public static string DireccionarInicio = "DireccionarInicio";
        }
        #endregion
    }
}
