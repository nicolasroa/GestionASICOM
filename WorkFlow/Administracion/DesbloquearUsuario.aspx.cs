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
    public partial class DesbloquearUsuario : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarUsuariosBloqueados();
            }

        }
        protected void btnDesbloquea_Click(object sender, EventArgs e)
        {
            DesbloqueoUsuario();
        }

        #endregion


        #region Metodos


        private void CargarUsuariosBloqueados()
        {
            var ObjetoUsuarios = new UsuarioInfo();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var NegUsuario = new NegUsuarios();


            try
            {

                ObjetoUsuarios.Estado_Id = (int)NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoInactivo);
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuarios);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlUsuarioBloqueado, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringNombre, "-- Usuarios Bloqueados --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");

                }
            }
        }

        private void DesbloqueoUsuario()
        {
            var ObjetoUsuario = new UsuarioInfo();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var NegUsuario = new NegUsuarios();

            try
            {
                if (ddlUsuarioBloqueado.SelectedValue == 0.ToString() || ddlUsuarioBloqueado.SelectedValue == "-1")
                {
                    Controles.MensajeEnControl(ddlUsuarioBloqueado.ClientID);
                    return;
                }

                ObjetoUsuario.Rut = int.Parse(ddlUsuarioBloqueado.SelectedValue);
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral)
                {

                    if (ObjetoResultado.Lista.Count() != 0)
                    {
                        ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();

                        ObjetoUsuario.PrimerInicio = true;
                        ObjetoUsuario.FechaUltimoCambioClave = DateTime.Now;
                        ObjetoUsuario.IntentosFallidos = 0;
                        ObjetoUsuario.Contraseña = NegUsuarios.GenerarClave();
                        ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoActivo);
                        //if (Mail.EnviarMensajeDesbloqueoUsuario(ObjetoUsuario, NegConfiguracionGeneral.Obtener()))
                        //{
                        //    ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);
                        //    if (ObjetoResultado.ResultadoGeneral)
                        //    {
                        //        Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.DesbloqueoUsuarioCorrecto.ToString()));
                        //    }
                        //    else
                        //    {
                        //        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        //    }
                        //}
                        //else
                        //{
                        //    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        //}
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        #endregion


    }
}