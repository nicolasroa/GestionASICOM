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
    public partial class Producto : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarTipoFinanciamiento();


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

            if (ddlFormTipoFinanciamiento.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormTipoFinanciamiento.ClientID);
                return false;
            }

            if (chIndMesesGracia.Checked && txtFormMesesGracia.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormMesesGracia.ClientID);
                return false;
            }
            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oProducto = new ProductoInfo();
                var ObjetoResultado = new Resultado<ProductoInfo>();
                var oNegProducto = new NegProductos();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oProducto.Id = int.Parse(hfId.Value);
                    oProducto = DatosEntidad(oProducto);
                }
                oProducto.Descripcion = txtFormDescripcion.Text;
                oProducto.TipoFinanciamiento_Id = int.Parse(ddlFormTipoFinanciamiento.SelectedValue);
                oProducto.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                oProducto.IndMesesGracia = chIndMesesGracia.Checked;
                oProducto.IndDoblePlazo = chkIndDoblePlazo.Checked;

                if (chIndMesesGracia.Checked)
                {
                    oProducto.MaximoPeriodoGracia = int.Parse(txtFormMesesGracia.Text);
                } else
                {
                    oProducto.MaximoPeriodoGracia = 0;
                }
                
               
                    
                  
                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegProducto.Guardar(oProducto);

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
                var oEvento = new ProductoInfo();
                var NegProducto = new NegProductos();
                var ObjetoResultado = new Resultado<ProductoInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);
                oEvento.TipoFinanciamiento_Id = int.Parse(ddlTipoFinanciamiento.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegProducto.Buscar(oEvento);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ProductoInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Producto");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProductoInfo>();
                var oEvento = new ProductoInfo();
                var oNegProducto = new NegProductos();

                oEvento.Id = Id;
                ObjetoResultado = oNegProducto.Buscar(oEvento);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Producto");
                        }                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Producto");
                }
            }
        }
        private ProductoInfo DatosEntidad(ProductoInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProductoInfo>();
                var oEvento = new ProductoInfo();
                var oNegProducto = new NegProductos();

                ObjetoResultado = oNegProducto.Buscar(Entidad);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Producto");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Producto");

                }
                return null;
            }
        }
        private void LlenarFormulario(ProductoInfo oEvento)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oEvento.Id.ToString();
                txtFormDescripcion.Text = oEvento.Descripcion;
                ddlFormEstado.SelectedValue = oEvento.Estado_Id.ToString();
                ddlFormTipoFinanciamiento.SelectedValue = oEvento.TipoFinanciamiento_Id.ToString();
                chIndMesesGracia.Checked = oEvento.IndMesesGracia == true ? true : false;
                chkIndDoblePlazo.Checked = oEvento.IndDoblePlazo == true ? true : false;
                if (chIndMesesGracia.Checked)
                {
                    txtFormMesesGracia.Enabled = true;
                    txtFormMesesGracia.Text = oEvento.MaximoPeriodoGracia.ToString();
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Producto");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();
            ddlFormTipoFinanciamiento.ClearSelection();
            chIndMesesGracia.Checked = false;
            chkIndDoblePlazo.Checked = false;
            txtFormMesesGracia.Text = "";
            txtFormMesesGracia.Enabled = false;        

            hfId.Value = "";
        }

        private void CargarTipoFinanciamiento()
        {
            try
            {

                Resultado<TipoFinanciamientoInfo> oResultado = new Resultado<TipoFinanciamientoInfo>();
                NegTipoFinanciamiento oNegTipoFinan = new NegTipoFinanciamiento();
                TipoFinanciamientoInfo oFiltro = new TipoFinanciamientoInfo();
                oFiltro.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                oResultado = oNegTipoFinan.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<TipoFinanciamientoInfo>(ref ddlTipoFinanciamiento, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<TipoFinanciamientoInfo>(ref ddlFormTipoFinanciamiento, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Tipo de Financiamiento --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tabla Tipo Financiamiento");
                }
            }
        }

        #endregion

        protected void chIndMesesGracia_CheckedChanged(object sender, EventArgs e)
        {
            if (chIndMesesGracia.Checked)
            {
                txtFormMesesGracia.Enabled = true;
            }
            else
            {
                txtFormMesesGracia.Enabled = false;
                txtFormMesesGracia.Text = "";
            }
        }
    }
}