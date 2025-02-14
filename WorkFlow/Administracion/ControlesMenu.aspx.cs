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
    public partial class ControlesMenu : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarGrid();
                lblFormMenuPadre.Text = new NegControles().MenuPadre.Descripcion;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }
        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var Permisos = (RolMenu)NegMenus.Permisos;
            if (Permisos.PermisoCrear == false && hfId.Value == "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoCrear.ToString());
                return;
            }
            if (Permisos.PermisoModificar == false && hfId.Value != "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoModificar.ToString());
                return;
            }
            GuardarEntidad();
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        #endregion
        #region Metodos
        private void CargarEstados()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaEstado);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlFormEstado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado --", "-1");
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
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoControles = new ControlesInfo();
                var NegControl = new NegControles();
                var ObjetoResultado = new Resultado<ControlesInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoControles.Menu_Id = NegControl.MenuPadre.Id;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegControl.Buscar(ObjetoControles);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ControlesInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
                    lblContador.Text = ObjetoResultado.ValorDecimal.ToString() + " Registro(s) Encontrado(s)";
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Menus");

                }
            }
        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoControles = new ControlesInfo();
                var ObjetoResultado = new Resultado<ControlesInfo>();
                var NegControl = new NegControles();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoControles.Id = int.Parse(hfId.Value.ToString());
                    ObjetoControles = DatosEntidad(ObjetoControles);
                }
                ObjetoControles.Menu_Id = NegControl.MenuPadre.Id;
                ObjetoControles.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                ObjetoControles.IdInterno = txtFormIdInterno.Text;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegControl.Guardar(ObjetoControles);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
                    CargarGrid();
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Menus");
                }
            }
        }
        private bool ValidarFormulario()
        {
            if (txtFormIdInterno.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormIdInterno.ClientID);
                return false;
            }
            if (ddlFormEstado.SelectedValue == null || ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID);
                return false;
            }
            return true;
        }
        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }
        private void LimpiarFormulario()
        {
            hfId.Value = "";
            txtFormIdInterno.Text = "";
            ddlFormEstado.SelectedIndex = 0;
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<ControlesInfo>();
                var ObjetoControles = new ControlesInfo();
                var NegControl = new NegControles();

                ObjetoControles.Id = Id;
                ObjetoResultado = NegControl.Buscar(ObjetoControles);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoControles = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoControles != null)
                    {
                        LlenarFormulario(ObjetoControles);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Controles");
                        }
                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
            }
        }
        private void LlenarFormulario(ControlesInfo ObjetoControles)
        {
            try
            {
                hfId.Value = ObjetoControles.Id.ToString();
                txtFormIdInterno.Text = ObjetoControles.IdInterno;
                ddlFormEstado.SelectedValue = ObjetoControles.Estado_Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Controles");
                }
            }
        }
        private ControlesInfo DatosEntidad(ControlesInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<ControlesInfo>();
                var ObjetoControles = new ControlesInfo();
                var NegControl = new NegControles();

                ObjetoResultado = NegControl.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoControles = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoControles != null)
                    {
                        return ObjetoControles;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Controles");
                        }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
                return null;
            }
        }

        #endregion
    }
}