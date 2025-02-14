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
    public partial class Destino : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarObjetivo();
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
            CargarGrid();
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

            if (ddlFormObjetivo.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormObjetivo.ClientID);
                return false;
            }
            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oEvento = new DestinoInfo();
                var ObjetoResultado = new Resultado<DestinoInfo>();
                var oNegDestino = new NegDestino();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oEvento.Id = int.Parse(hfId.Value);
                    oEvento = DatosEntidad(oEvento);
                }
                oEvento.Descripcion = txtFormDescripcion.Text;
                oEvento.Objetivo_Id = int.Parse(ddlFormObjetivo.SelectedValue);
                oEvento.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);

                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegDestino.Guardar(oEvento);

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
                var oEvento = new DestinoInfo();
                var NegDestino = new NegDestino();
                var ObjetoResultado = new Resultado<DestinoInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegDestino.Buscar(oEvento);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<DestinoInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Destino");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<DestinoInfo>();
                var oEvento = new DestinoInfo();
                var oNegDestino = new NegDestino();

                oEvento.Id = Id;
                ObjetoResultado = oNegDestino.Buscar(oEvento);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Destino");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Destino");
                }
            }
        }
        private DestinoInfo DatosEntidad(DestinoInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<DestinoInfo>();
                var oEvento = new DestinoInfo();
                var oNegDestino = new NegDestino();

                ObjetoResultado = oNegDestino.Buscar(Entidad);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Destino");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Destino");

                }
                return null;
            }
        }
        private void LlenarFormulario(DestinoInfo oEvento)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oEvento.Id.ToString();
                txtFormDescripcion.Text = oEvento.Descripcion;
                ddlFormEstado.SelectedValue = oEvento.Estado_Id.ToString();
                ddlFormObjetivo.SelectedValue = oEvento.Objetivo_Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Destino");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();
            ddlFormObjetivo.ClearSelection();

            hfId.Value = "";
        }

        private void CargarObjetivo()
        {
            try
            {
                Resultado<ObjetivoInfo> oResultado = new Resultado<ObjetivoInfo>();
                NegObjetivo oNegObjetivo = new NegObjetivo();
                ObjetivoInfo oFiltro = new ObjetivoInfo();
                oFiltro.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");


                oResultado = oNegObjetivo.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<ObjetivoInfo>(ref ddlObjetivo, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<ObjetivoInfo>(ref ddlFormObjetivo, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Objetivo --", "-1");

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Objetivo");
                }
            }
        }
        #endregion
    }
}