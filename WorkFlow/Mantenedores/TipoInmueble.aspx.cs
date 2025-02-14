using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WorkFlow.Mantenedores
{
    public partial class TipoInmueble : System.Web.UI.Page
    {
        #region TipoInmueble
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
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
        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }

        #endregion
        #region Metodos

        //Carga Inicial
        private void CargarEstados()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaEstado);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
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

        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }
        //Metodos Generales
        private bool ValidarFormulario()
        {
            if (txtFormDescripcion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormDescripcion.ClientID);
                return false;
            }
            if (txtFormPorcSegIncendio.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormPorcSegIncendio.ClientID);
                return false;
            }

            if (ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID);
                return false;
            }
            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oTipoInmueble = new TipoInmuebleInfo();
                var oResultado = new Resultado<TipoInmuebleInfo>();
                var oNegTipoInmueble = new NegPropiedades();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oTipoInmueble.Id = int.Parse(hfId.Value);
                    oTipoInmueble = DatosEntidad(oTipoInmueble);
                }
                oTipoInmueble.Descripcion = txtFormDescripcion.Text;
                oTipoInmueble.PorcentajeSeguroIncendio = int.Parse(txtFormPorcSegIncendio.Text);
                oTipoInmueble.VisibleEnSimulador = chkVisibleEnSimulador.Checked;
                oTipoInmueble.IndEstudioTitulo = chkIndEstudioTitulo.Checked;
                oTipoInmueble.IndTasacion = chkIndTasacion.Checked;
                oTipoInmueble.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                
                //Ejecucion del procedo para Guardar
                oResultado = oNegTipoInmueble.GuardarTipoInmueble(ref oTipoInmueble);

                if (oResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "TipoInmueble");
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oTipoInmueble = new TipoInmuebleInfo();
                var NegTipoInmueble = new NegPropiedades();
                var oResultado = new Resultado<TipoInmuebleInfo>();

                //Asignación de Variables de Búsqueda
                oTipoInmueble.Descripcion = txtDescripcion.Text;
                oTipoInmueble.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                oResultado = NegTipoInmueble.BuscarTipoInmueble(oTipoInmueble);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<TipoInmuebleInfo>(ref gvBusqueda, oResultado.Lista, new string[] { Constantes.StringId });
                    lblContador.Text = oResultado.ValorDecimal.ToString() + " Registro(s) Encontrado(s)";
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "TipoInmueble");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var oResultado = new Resultado<TipoInmuebleInfo>();
                var oTipoInmueble = new TipoInmuebleInfo();
                var oNegTipoInmueble = new NegPropiedades();

                oTipoInmueble.Id = Id;
                oResultado = oNegTipoInmueble.BuscarTipoInmueble(oTipoInmueble);

                if (oResultado.ResultadoGeneral == true)
                {
                    oTipoInmueble = oResultado.Lista.FirstOrDefault();

                    if (oTipoInmueble != null)
                    {
                        LlenarFormulario(oTipoInmueble);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");
                }
            }
        }
        private TipoInmuebleInfo DatosEntidad(TipoInmuebleInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<TipoInmuebleInfo>();
                var oTipoInmueble = new TipoInmuebleInfo();
                var oNegTipoInmueble = new NegPropiedades();

                oResultado = oNegTipoInmueble.BuscarTipoInmueble(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oTipoInmueble = oResultado.Lista.FirstOrDefault();

                    if (oTipoInmueble != null)
                    {
                        return oTipoInmueble;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");

                }
                return null;
            }
        }
        private void LlenarFormulario(TipoInmuebleInfo oTipoInmueble)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oTipoInmueble.Id.ToString();
                txtFormDescripcion.Text = oTipoInmueble.Descripcion;
                txtFormPorcSegIncendio.Text = oTipoInmueble.PorcentajeSeguroIncendio.ToString();
                chkVisibleEnSimulador.Checked = (bool)oTipoInmueble.VisibleEnSimulador;
                chkIndEstudioTitulo.Checked = (bool)oTipoInmueble.IndEstudioTitulo;
                chkIndTasacion.Checked = (bool)oTipoInmueble.IndTasacion;
                ddlFormEstado.SelectedValue = oTipoInmueble.Estado_Id.ToString();
                
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "TipoInmueble");
                }
            }
        }
        private void LimpiarFormulario()
        {

            Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Text="");
            Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Checked=false);
            Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.ClearSelection());
            hfId.Value = "";
        }

        #endregion
    }
}