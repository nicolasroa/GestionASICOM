using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;

namespace WorkFlow.Mantenedores
{
    public partial class TipoFinanciamiento : System.Web.UI.Page
    {
        #region Eventos
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
                var oEvento = new TipoFinanciamientoInfo();
                var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
                var oNegTipoFinanciamiento = new NegTipoFinanciamiento();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oEvento.Id = int.Parse(hfId.Value);
                    oEvento = DatosEntidad(oEvento);
                }
                oEvento.Descripcion = txtFormDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);

                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegTipoFinanciamiento.Guardar(oEvento);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Eventos");
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oEvento = new TipoFinanciamientoInfo();
                var NegTipoFinanciamiento = new NegTipoFinanciamiento();
                var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegTipoFinanciamiento.Buscar(oEvento);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<TipoFinanciamientoInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tipo Financiamiento");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
                var oEvento = new TipoFinanciamientoInfo();
                var oNegTipoFinanciamiento = new NegTipoFinanciamiento();

                oEvento.Id = Id;
                ObjetoResultado = oNegTipoFinanciamiento.Buscar(oEvento);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        LlenarFormulario(oEvento);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tipo Financiamiento");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tipo Financiamiento");
                }
            }
        }
        private TipoFinanciamientoInfo DatosEntidad(TipoFinanciamientoInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<TipoFinanciamientoInfo>();
                var oEvento = new TipoFinanciamientoInfo();
                var oNegTipoFinanciamiento = new NegTipoFinanciamiento();

                ObjetoResultado = oNegTipoFinanciamiento.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        return oEvento;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tipo Financiamiento");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tipo Financiamiento");

                }
                return null;
            }
        }
        private void LlenarFormulario(TipoFinanciamientoInfo oEvento)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oEvento.Id.ToString();
                txtFormDescripcion.Text = oEvento.Descripcion;
                ddlFormEstado.SelectedValue = oEvento.Estado_Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Tipo Financiamiento");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();

            hfId.Value = "";
        }
        #endregion
    }
}