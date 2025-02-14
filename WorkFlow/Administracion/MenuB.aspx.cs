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
    public partial class MenuB : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargarMenuPadreForm(1);
                CargarMenuPadre();
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
        protected void btnVerControles_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            VerControles(Id);
        }

        protected void btnAsignarControles_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ConfigurarControles(Id);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        protected void ddlFormNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMenuPadreForm(byte.Parse(ddlFormNivel.SelectedValue));
        }
        #endregion

        #region Metodos

        //Metodos de Carga Inicial


        //Metodos Generales

        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoMenu = new MenuInfo();
                var NegMenus = new NegMenus();
                var ObjetoResultado = new Resultado<MenuInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoMenu.Descripcion = txtDescripcion.Text;
                ObjetoMenu.MenuPadre_Id = int.Parse(ddlMenuPadre.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegMenus.Buscar(ObjetoMenu);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<MenuInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
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
                var ObjetoMenu = new MenuInfo();
                var ObjetoResultado = new Resultado<MenuInfo>();
                var NegMenu = new NegMenus();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoMenu.Id = int.Parse(hfId.Value.ToString());
                    ObjetoMenu = DatosEntidad(ObjetoMenu);
                }
                ObjetoMenu.Descripcion = txtFormDescripcion.Text;
                ObjetoMenu.Url = txtFormUrl.Text;
                ObjetoMenu.Orden = byte.Parse(txtFormOrden.Text);
                ObjetoMenu.Nivel = byte.Parse(ddlFormNivel.SelectedValue);
                if (ddlFormMenuPadre.SelectedValue != "-1")
                    ObjetoMenu.MenuPadre_Id = int.Parse(ddlFormMenuPadre.SelectedValue);
                ObjetoMenu.Visible = (chkFormVisible.Checked);
                if (rblFormTipo.SelectedValue == "true")
                    ObjetoMenu.Mantenedor = true;
                else
                    ObjetoMenu.Mantenedor = false;
                ObjetoMenu.Administracion = chkAdministracion.Checked;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegMenu.Guardar(ObjetoMenu);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
                    txtDescripcion.Text = ObjetoMenu.Descripcion;
                    CargarGrid();
                    txtDescripcion.Text = "";
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Menus");
                }
            }
        }
        private bool ValidarFormulario()
        {
            if (txtFormDescripcion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormDescripcion.ClientID);
                return false;
            }
            if (txtFormOrden.Text.Length == 0)
            {
                txtFormOrden.Text = "0";
            }
            if (txtFormUrl.Text.Length == 0)
            {
                txtFormUrl.Text = "..";
            }
            if (rblFormTipo.SelectedValue == null)
            {
                Controles.MensajeEnControl(rblFormTipo.ClientID);
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
            CargarMenuPadreForm(1);
            ddlFormMenuPadre.SelectedIndex = 0;
            txtFormDescripcion.Text = "";
            txtFormOrden.Text = "";
            txtFormUrl.Text = "";
            rblFormTipo.SelectedValue = null;
            chkFormVisible.Checked = false;
            ddlFormMenuPadre.Enabled = false;
            ddlFormNivel.SelectedIndex = 0;
            chkAdministracion.Checked = false;

        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<MenuInfo>();
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();

                ObjetoMenu.Id = Id;
                ObjetoResultado = NegMenu.Buscar(ObjetoMenu);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoMenu = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoMenu != null)
                    {
                        LlenarFormulario(ObjetoMenu);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                        }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
            }
        }
        private void LlenarFormulario(MenuInfo ObjetoMenu)
        {
            try
            {
                hfId.Value = ObjetoMenu.Id.ToString();
                txtFormDescripcion.Text = ObjetoMenu.Descripcion;
                txtFormOrden.Text = ObjetoMenu.Orden.ToString();
                txtFormUrl.Text = ObjetoMenu.Url;
                ddlFormNivel.SelectedValue = ObjetoMenu.Nivel.ToString();
                CargarMenuPadreForm(byte.Parse(ddlFormNivel.SelectedValue));
                if (ObjetoMenu.MenuPadre_Id != 0) { ddlFormMenuPadre.SelectedValue = ObjetoMenu.MenuPadre_Id.ToString(); }
                chkFormVisible.Checked = (ObjetoMenu.Visible == true);
                if (ObjetoMenu.Mantenedor == true) { rblFormTipo.SelectedValue = "true"; }
                else { rblFormTipo.SelectedValue = "false"; }
                chkAdministracion.Checked = ObjetoMenu.Administracion == null ? false : (bool)ObjetoMenu.Administracion;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Menus");
                }
            }
        }
        private MenuInfo DatosEntidad(MenuInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<MenuInfo>();
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();

                ObjetoResultado = NegMenu.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoMenu = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoMenu != null)
                    {
                        return ObjetoMenu;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
                return null;
            }
        }
        //Metodos Particulares
        private void CargarMenuPadreForm(byte Nivel)
        {
            //Declaracion de Variables
            var ObjetoMenu = new MenuInfo();
            var ObjetoResultado = new Resultado<MenuInfo>();
            var NegMenu = new NegMenus();


            //Asignacion de Variables
            ObjetoMenu.Id = -1;
            ObjetoMenu.MenuPadre_Id = -1;
            ObjetoMenu.Descripcion = "";
            ObjetoMenu.Nivel = (byte)(Nivel - 1);

            //Ejecución del Proceso de Búsqueda

            if (Nivel != 1) { ddlFormMenuPadre.Enabled = true; }
            else { ddlFormMenuPadre.Enabled = false; }
            ObjetoResultado = NegMenu.Buscar(ObjetoMenu);
            if (ObjetoResultado.ResultadoGeneral)
            {
                if (ObjetoResultado.Lista.Count == 0)
                {
                    Controles.MostrarMensajeAlerta("Favor Crear Menu de Nivel " + (byte.Parse(ddlFormNivel.SelectedValue) - 1).ToString());
                }
                else
                {

                    if (ObjetoMenu.Nivel == 0)
                    {
                        Controles.CargarCombo<MenuInfo>(ref ddlFormMenuPadre, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "Menú de Nivel 1", "-1");

                    }
                    else
                    {
                        Controles.CargarCombo<MenuInfo>(ref ddlFormMenuPadre, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "", "");
                    }

                }
            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }

        }
        private void CargarMenuPadre()
        {
            //Declaracion de Variables
            var ObjetoMenu = new MenuInfo();
            var ObjetoResultado = new Resultado<MenuInfo>();
            var NegMenu = new NegMenus();


            //Asignacion de Variables
            ObjetoMenu.Id = -1;
            ObjetoMenu.MenuPadre_Id = -1;
            ObjetoMenu.Descripcion = "";
            ObjetoMenu.Nivel = 0;

            //Ejecución del Proceso de Búsqueda
            ObjetoResultado = NegMenu.Buscar(ObjetoMenu);
            if (ObjetoResultado.ResultadoGeneral)
            {
                Controles.CargarCombo<MenuInfo>(ref ddlMenuPadre, ObjetoResultado.Lista, Constantes.StringId, "DescripcionMenuCombo", "-- Todos --", "-1");
            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }

        }
        private void VerControles(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<MenuInfo>();
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();

                ObjetoMenu.Id = Id;
                ObjetoResultado = NegMenu.Buscar(ObjetoMenu);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoMenu = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoMenu != null)
                    {
                        new NegControles().MenuPadre = ObjetoMenu;
                        Controles.AbrirPopup(ConfigMenu.UrlControles, 1000, 800, "Controles del Menú " + ObjetoMenu.Descripcion);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                        }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
            }
        }


        private void ConfigurarControles(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<MenuInfo>();
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();

                ObjetoMenu.Id = Id;
                ObjetoResultado = NegMenu.Buscar(ObjetoMenu);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoMenu = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoMenu != null)
                    {
                        new NegControles().MenuPadre = ObjetoMenu;
                        Controles.AbrirPopup(ConfigMenu.UrlConfiguraControles, 1000, 800, "Configuración de Controles para el Menú " + ObjetoMenu.Descripcion);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                        }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
            }
        }







        #endregion


    }
}