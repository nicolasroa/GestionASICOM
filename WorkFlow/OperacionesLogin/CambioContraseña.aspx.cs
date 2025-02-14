using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.OperacionesLogin
{
    public partial class CambioContraseña : System.Web.UI.Page
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarPreguntas();
                lblUsuario.Text = NegUsuarios.Usuario.Nombre;
            }
        }
        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            CambiarContraseña();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Controles.CerrarModal(0);
        }
        #endregion


        #region Metodos



        private void CargarPreguntas()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigUsuario.TablaPreguntasSeguridad);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlPreguntaSeguridad, Lista, Constantes.StringNombre, Constantes.StringNombre, "-- Seleccione Pregunta --", 0.ToString());
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigUsuario.TablaPreguntasSeguridad + " Sin Datos");
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " " + ConfigUsuario.TablaPreguntasSeguridad);
                }
            }
        }

        private void CambiarContraseña()
        {
            var ObjetoUsuario = new UsuarioInfo();
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();

            ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;

            if (ddlPreguntaSeguridad.SelectedValue == 0.ToString())
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarPreguntaSecreta.ToString()));
                return;
            }

            if (txtRespuesta1.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarRespuesta.ToString()));
                return;
            }

            if (txtRespuesta1.Text != txtRespuesta2.Text)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarRespuestaDistintas.ToString()));
                return;
            }
            var clave = Seguridad.Desencriptar(ObjetoUsuario.Contraseña);
            if (Seguridad.Desencriptar(ObjetoUsuario.Contraseña) != txtClaveActual.Text)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClavesAnterior.ToString()));
                return;
            }
            if (txtClaveNueva.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClave.ToString()));
                return;
            }
            if (txtClaveNueva.Text.CompareTo(txtClaveNueva2.Text) != 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClavesDistintas.ToString()));
                return;
            }
            if (!NegUsuarios.ValidarClave(txtClaveNueva.Text))
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarFormatoClave.ToString()) + NegConfiguracionGeneral.Obtener().TamanioClave.ToString() + " Caracteres");
                return;
            }
            ObjetoUsuario.Contraseña = Seguridad.Encriptar(txtClaveNueva.Text);
            ObjetoUsuario.FechaUltimoCambioClave = DateTime.Now;
            ObjetoUsuario.PrimerInicio = false;
            ObjetoUsuario.PreguntaSeguridad = ddlPreguntaSeguridad.SelectedValue;
            ObjetoUsuario.RespuestaSeguridad = Seguridad.Encriptar(txtRespuesta1.Text);
            ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);
            if (ObjetoResultado.ResultadoGeneral == true)
            {
                pnlOperacion.Visible = false;
                btnCambiarClave.Visible = false;
                btnCancelar.Text = "Salir";
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.CambioClaveCorrecto.ToString()));
            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }
        }

        #endregion
    }
}