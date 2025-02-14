using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;

namespace WebSite.Administracion
{
    public partial class ConfiguracionGeneralB : System.Web.UI.Page
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarConfiguracionGeneral();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarEntidad();
        }

        #endregion

        #region Metodos
        private ConfiguracionGeneralInfo DatosEntidad(ConfiguracionGeneralInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
                var ObjetoConfiguracionGeneral = new ConfiguracionGeneralInfo();
                var NegConfiguracionGeneral = new NegConfiguracionGeneral();

                ObjetoResultado = NegConfiguracionGeneral.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoConfiguracionGeneral = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoConfiguracionGeneral != null)
                    {
                        return ObjetoConfiguracionGeneral;
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "ConfiguracionGeneral");
                }
                return null;
            }
        }
        private void CargarConfiguracionGeneral()
        {
            try
            {
                var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
                var ObjetoConfiguracionGeneral = new ConfiguracionGeneralInfo();
                var NegConfiguracionGeneral = new NegConfiguracionGeneral();

                ObjetoResultado = NegConfiguracionGeneral.Buscar(ObjetoConfiguracionGeneral);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    if (ObjetoResultado.Lista.Count >= 1)
                    {
                        LlenarFormulario(ObjetoResultado.Lista.FirstOrDefault());
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Configuracion General");
                }
            }
        }
        private void LlenarFormulario(ConfiguracionGeneralInfo Entidad)
        {
            try
            {
                hfId.Value = Entidad.Id.ToString();
                lblAmbiente.Text = Entidad.Ambiente;

                //Validacion de Usuario
                txtTamanioClave.Text = Entidad.TamanioClave.ToString();
                txtPlazoValidez.Text = Entidad.PlazoValidez.ToString();
                txtIntentos.Text = Entidad.Intentos.ToString();
                txtNotificacion.Text = Entidad.Notificacion.ToString();
                

                // Correo Electronico
                txtUsuarioCorreo.Text = Entidad.UsuarioCorreo;
                txtCorreo.Text = Entidad.Correo;
                txtClaveCorreo.Text = Entidad.ClaveCorreo;
                txtPuertoCorreo.Text = Entidad.PuertoCorreo;
                txtServidorSalidaCorreo.Text = Entidad.ServidorSalidaCorreo;
                chkConexion.Checked = Entidad.ConexionSeguraMail;
                //Configuracion General
                txtUrlSistema.Text = Entidad.UrlSitio;
                chkEnvioMail.Checked = Entidad.EnviarMail;
                txtDescuentoTMC.Text = Entidad.DescuentoTMC.ToString();

                lblFechaActualizacion.Text = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.FechaActualizacion.ToString()) + Entidad.FechaModificacion == null ? Entidad.FechaCreacion.ToString("g") : Entidad.FechaModificacion.ToString("g");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }
        private void GuardarEntidad()
        {
            //Declaracion de Variables
            var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
            var ObjetoConfiguracionGeneral = new ConfiguracionGeneralInfo();
            var NegConfiguracionGeneral = new NegConfiguracionGeneral();

            if (!ValidarFormulario()) { return; }

            //Asignacion de Variables 
            if (hfId.Value.Length != 0)
            {
                ObjetoConfiguracionGeneral.Id = int.Parse(hfId.Value.ToString());
                ObjetoConfiguracionGeneral = DatosEntidad(ObjetoConfiguracionGeneral);
            }
            ObjetoConfiguracionGeneral.TamanioClave = byte.Parse(txtTamanioClave.Text);
            ObjetoConfiguracionGeneral.PlazoValidez = int.Parse(txtPlazoValidez.Text);
            ObjetoConfiguracionGeneral.Intentos = byte.Parse(txtIntentos.Text);
            ObjetoConfiguracionGeneral.Notificacion = byte.Parse(txtNotificacion.Text);

            //Correo General del Sistema
            ObjetoConfiguracionGeneral.ClaveCorreo = txtClaveCorreo.Text;
            ObjetoConfiguracionGeneral.Correo = txtCorreo.Text;
            ObjetoConfiguracionGeneral.UsuarioCorreo = txtUsuarioCorreo.Text;
            ObjetoConfiguracionGeneral.PuertoCorreo = txtPuertoCorreo.Text;
            ObjetoConfiguracionGeneral.ServidorSalidaCorreo = txtServidorSalidaCorreo.Text;
            ObjetoConfiguracionGeneral.ConexionSeguraMail = chkConexion.Checked;

            //Datos Generales del Sistema
            ObjetoConfiguracionGeneral.UrlSitio = txtUrlSistema.Text;
            ObjetoConfiguracionGeneral.EnviarMail = chkEnvioMail.Checked;
            ObjetoConfiguracionGeneral.DescuentoTMC = decimal.Parse(txtDescuentoTMC.Text);


            //Ejecucion del procedo para Guardar
            ObjetoResultado = NegConfiguracionGeneral.Guardar(ObjetoConfiguracionGeneral);

            if (ObjetoResultado.ResultadoGeneral)
            {
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }

        }
        private bool ValidarFormulario()
        {

            if (txtTamanioClave.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtTamanioClave.ClientID);
                return false;
            }
            if (txtPlazoValidez.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtPlazoValidez.ClientID);
                return false;
            }
            if (txtIntentos.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtIntentos.ClientID);
                return false;
            }
            if (txtNotificacion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtNotificacion.ClientID);
                return false;
            }

            if (txtUrlSistema.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtUrlSistema.ClientID);
                return false;
            }

            if (txtDescuentoTMC.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtDescuentoTMC.ClientID);
                return false;
            }


            return true;
        }

        #endregion


    }
}