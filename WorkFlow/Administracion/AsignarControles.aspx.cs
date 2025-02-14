using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;

namespace WebApp.Administracion
{
    public partial class AsignarControles : System.Web.UI.Page
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargaRoles();
                CargarConfiguracion();
            }
        }
        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarConfiguracion();
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            AsignaControles();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Controles.CerrarPopup();
        }
        #endregion
        #region Metodos
        //Metodos de Carga Inicial
        private void CargaRoles()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaRoles);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlRol, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
        private void CargarConfiguracion()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoControles = new RolMenuControlInfo();
                var NegControl = new NegControles();
                var ObjetoResultado = new Resultado<RolMenuControlInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoControles.Menu_Id = NegControl.MenuPadre.Id;
                ObjetoControles.Rol_Id = int.Parse(ddlRol.SelectedValue.ToString());

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegControl.BuscarConfiguracion(ObjetoControles);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<RolMenuControlInfo>(ref gvConfiguracionControles, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Permisos a Controles");

                }
            }
        }
        //Asignacion de Controles
        private void AsignaControles()
        {
            try
            {
                var NegControles = new NegControles();
                var ObjetoControlMenu = new RolMenuControlInfo();
                var ObjetoResultado = new Resultado<RolMenuControlInfo>();

                var chkVisible = new Anthem.CheckBox();
                var chkActivo = new Anthem.CheckBox();


                foreach (GridViewRow Row in gvConfiguracionControles.Rows)
                {
                    chkVisible = (Anthem.CheckBox)Row.FindControl(Constantes.chkVisible);
                    chkActivo = (Anthem.CheckBox)Row.FindControl(Constantes.chkActivo);

                    ObjetoControlMenu.Rol_Id = int.Parse(ddlRol.SelectedValue.ToString());
                    ObjetoControlMenu.Menu_Id = NegControles.MenuPadre.Id;
                    ObjetoControlMenu.Control_Id = int.Parse(gvConfiguracionControles.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                    ObjetoControlMenu.Activo = chkActivo.Checked;
                    ObjetoControlMenu.Visible = chkVisible.Checked;

                    ObjetoResultado = NegControles.GuardarConfiguracion(ObjetoControlMenu);


                    if (ObjetoResultado.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return;
                    }
                }
                CargarConfiguracion();
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.AsignacionControlesCompleta.ToString()));
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorProcesarAsignacionControles.ToString()));
                }

            }
        }

        #endregion





    }
}