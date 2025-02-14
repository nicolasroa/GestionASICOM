using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace WorkFlow
{
    public partial class Login : System.Web.UI.Page
    {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {

                this.Page.Title = ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString());
                Constantes.ModoDebug = bool.Parse(ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.ModoDebug.ToString()));

                var Configuracion = NegConfiguracionGeneral.Obtener();
                if (Configuracion == null)
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorConfiguracionNoEncontrada.ToString()));
                    return;
                }
                lblAmbiente.Text = Configuracion.Ambiente;

            }
        }
        protected void btnValidar_Click(object sender, EventArgs e)
        {
            ValidarUsuario();
        }

        #endregion
        #region Metodos

        private void LeerEventos()
        {
            if (NegUsuarios.DireccionarInicio != null)
            {
                var ObjetoUsuario = new UsuarioInfo();
                ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;
                NegUsuarios.DireccionarInicio = null;
                ReiniciarIntentos(ObjetoUsuario);
                Response.Redirect(Constantes.UrlInicio);
            }

        }
        private void ValidarUsuario()
        {
            //Declaración de Variables
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var ObjetoUsuario = new UsuarioInfo();
            var ObjetoConfiguracion = new ConfiguracionGeneralInfo();

            //Validacion de Formulario
            if (txtUsuario.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombreUsuario.ToString()));
                return;
            }
            if (txtClave.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClave.ToString()));
                return;
            }

            //Asignación de Variables
            ObjetoUsuario.NombreUsuario = txtUsuario.Text;
            ObjetoUsuario.Contraseña = txtClave.Text;


            //CargarConfiguracionGeneral
            ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            if (ObjetoConfiguracion == null)
            {
                Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorConfiguracionNoEncontrada.ToString()));
                return;
            }

            //Proceso de Validación

            ObjetoResultado = NegUsuario.Validar(ObjetoUsuario);
            if (ObjetoResultado.ResultadoGeneral == true)
            {
                if (ObjetoResultado.Objeto != null)
                {
                    NegUsuarios.UsuarioId = ObjetoResultado.Objeto.Rut;
                    ObjetoUsuario = ObjetoResultado.Objeto;
                }
                switch (ObjetoResultado.ValorString)
                {
                    case ConfigUsuario.ValidacionNoEncontrado:
                        Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.MensajeNoExiste));
                        break;
                    case ConfigUsuario.ValidacionInactivo:
                        Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.MensajeInactivo));
                        break;
                    case ConfigUsuario.ValidacionErrorClave:
                        IntentoFallido(ref ObjetoUsuario, ObjetoConfiguracion);
                        Controles.MostrarMensajeAlerta("Clave no corresponde, le quedan " + (ObjetoConfiguracion.Intentos - ObjetoUsuario.IntentosFallidos).ToString() + " Intentos");
                        break;
                    case ConfigUsuario.ValidacionCambioClave:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;
                        Controles.AbrirPopup(ConfigUsuario.UrlCambioContraseña, 700, 450, "Su Clave ha Caducado, Favor Realice el Cambio");
                        break;
                    case ConfigUsuario.ValidacionPrimerInicio:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;
                        Controles.AbrirPopup(ConfigUsuario.UrlCambioContraseña, 700, 450, "Primer Inicio, debe Cambiar la Contraseña.");
                        break;
                    case ConfigUsuario.ValidacionAvisoCambioClave:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;

                        NegUsuarios.MensajeCambioClave = "Le quedan " + ObjetoResultado.ValorInt.ToString() + " Dias para que la Contraseña caduque, desea realizar el cambio ahora?";
                        Controles.AbrirPopup(ConfigUsuario.UrlAvisoCambioContraseña, 700, 450, "Aviso de Cambio de Contraseña.");
                        break;
                    case ConfigUsuario.ValidacionAprobado:
                        NegUsuarios.Usuario = ObjetoUsuario;
                        NegUsuarios.UsuarioId = ObjetoResultado.Objeto.Rut;
                        HttpContext.Current.Session["UsuarioId"] = ObjetoUsuario.Rut;
                        ReiniciarIntentos(ObjetoUsuario);
                        if (ObjetoUsuario.Inversionista_Id == -1)
                            Response.Redirect(Constantes.UrlInicio);
                        else
                            Response.Redirect(Constantes.UrlInversionista);
                        break;
                }
            }
        }

        private void IntentoFallido(ref UsuarioInfo ObjetoUsuario, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {

            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                if (ObjetoUsuario != null)
                {
                    if (ObjetoUsuario.IntentosFallidos == 0)
                    {
                        ObjetoUsuario.IntentosFallidos = 1;
                    }
                    else
                    {
                        ObjetoUsuario.IntentosFallidos++;
                    }

                    if (ObjetoUsuario.IntentosFallidos >= ObjetoConfiguracion.Intentos)
                    {
                        ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoInactivo);
                    }

                    ObjetoResultado = NegUsuario.Guardar(ref ObjetoUsuario);

                    if (!ObjetoResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Usuario");

                }
            }
        }
        private void ReiniciarIntentos(UsuarioInfo ObjetoUsuario)
        {
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {

                ObjetoUsuario.IntentosFallidos = 0;
                ObjetoUsuario.Rut = ((UsuarioInfo)NegUsuarios.Usuario).Rut;

                ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);

                if (!ObjetoResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }

            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Usuario");
                }
            }

        }


        #endregion


    }
}