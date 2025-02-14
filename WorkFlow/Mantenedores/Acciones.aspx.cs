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
    public partial class Acciones : System.Web.UI.Page
    {
        #region Acciones
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarEstadosEvento();
                CargarSentidos();

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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        private void CargarEstadosEvento()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("EST_EVE");
                if (Lista != null)
                {

                    Controles.CargarCombo<TablaInfo>(ref ddlFormEstadoEvento, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado Evento --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Estado de Evento Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        private void CargarSentidos()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("SENTIDOS_WF");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlSentido, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlFormSentido, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Sentido --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sentidos Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Sentidos");
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
            if (ddlFormSentido.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormSentido.ClientID);
                return false;
            }

            if (ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID);
                return false;
            }
            if (ddlFormEstadoEvento.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstadoEvento.ClientID);
                return false;
            }
            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oAccion = new AccionesInfo();
                var oResultado = new Resultado<AccionesInfo>();
                var oNegAcciones = new NegAcciones();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oAccion.Id = int.Parse(hfId.Value);
                    oAccion = DatosEntidad(oAccion);
                }
                oAccion.Descripcion = txtFormDescripcion.Text;
                oAccion.Sentido_Id = int.Parse(ddlFormSentido.SelectedValue);
                oAccion.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                oAccion.EstadoEvento_Id = int.Parse(ddlFormEstadoEvento.SelectedValue);
                //Ejecucion del procedo para Guardar
                oResultado = oNegAcciones.Guardar(ref oAccion);

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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Acciones");
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oAccion = new AccionesInfo();
                var NegAcciones = new NegAcciones();
                var oResultado = new Resultado<AccionesInfo>();
               
                //Asignación de Variables de Búsqueda
                oAccion.Descripcion = txtDescripcion.Text;
                oAccion.Sentido_Id = int.Parse(ddlSentido.SelectedValue);
                oAccion.Estado_Id = int.Parse(ddlEstado.SelectedValue);


                //Ejecución del Proceso de Búsqueda
                oResultado = NegAcciones.Buscar(oAccion);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<AccionesInfo>(ref gvBusqueda, oResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Acciones");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var oResultado = new Resultado<AccionesInfo>();
                var oAccion = new AccionesInfo();
                var oNegAcciones = new NegAcciones();

                oAccion.Id = Id;
                oResultado = oNegAcciones.Buscar(oAccion);

                if (oResultado.ResultadoGeneral == true)
                {
                    oAccion = oResultado.Lista.FirstOrDefault();

                    if (oAccion != null)
                    {
                        LlenarFormulario(oAccion);
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");
                }
            }
        }
        private AccionesInfo DatosEntidad(AccionesInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<AccionesInfo>();
                var oAccion = new AccionesInfo();
                var oNegAcciones = new NegAcciones();

                oResultado = oNegAcciones.Buscar(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oAccion = oResultado.Lista.FirstOrDefault();

                    if (oAccion != null)
                    {
                        return oAccion;
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");

                }
                return null;
            }
        }
        private void LlenarFormulario(AccionesInfo oAccion)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oAccion.Id.ToString();
                txtFormDescripcion.Text = oAccion.Descripcion;
                ddlFormSentido.SelectedValue = oAccion.Sentido_Id.ToString();
                ddlFormEstado.SelectedValue = oAccion.Estado_Id.ToString();
                ddlFormEstadoEvento.SelectedValue = oAccion.EstadoEvento_Id.ToString();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Acciones");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormSentido.ClearSelection();
            ddlFormEstado.ClearSelection();
            ddlFormEstadoEvento.ClearSelection();
            hfId.Value = "";
        }

        #endregion
    }
}