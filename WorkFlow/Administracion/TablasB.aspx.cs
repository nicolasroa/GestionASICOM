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
    public partial class TablasB : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("Inicio();");

            if (!Page.IsPostBack)
            {

                CargarEstados();
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
            LimpiarFormulario(false);
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

        protected void btnVerHijos_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            string Nombre = gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringNombre].ToString();
            CargarHijos(Id, Nombre);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(true);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        #endregion

        #region Metodos

        //Metodos de Carga Inicial
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

        //Metodos Generales

        private void CargarGrid()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoTabla = new TablaInfo();
                var NegTablas = new NegTablas();
                var ObjetoResultado = new Resultado<TablaInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoTabla.Nombre = txtNombre.Text;
                ObjetoTabla.NombreTablaPadre = txtNombreTablaPadre.Text;
                ObjetoTabla.Codigo = txtCodigo.Text;
                ObjetoTabla.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegTablas.Buscar(ObjetoTabla);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<TablaInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId, Constantes.StringNombre });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }

        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoTabla = new TablaInfo();
                var ObjetoResultado = new Resultado<TablaInfo>();
                var NegTabla = new NegTablas();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoTabla.Id = int.Parse(hfId.Value.ToString());
                    ObjetoTabla = DatosEntidad(ObjetoTabla);
                }
                if (hfTablaPadreId.Value.Length != 0) { ObjetoTabla.TablaPadre_Id = int.Parse(hfTablaPadreId.Value.ToString()); }
                ObjetoTabla.Nombre = txtFormNombre.Text;
                ObjetoTabla.NombreTablaPadre = txtFormNombrePadre.Text;
                ObjetoTabla.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                ObjetoTabla.Codigo = txtFormCodigo.Text;
                ObjetoTabla.CodigoInterno = int.Parse(txtFormCodigoInterno.Text);
                ObjetoTabla.Concepto = txtFormConcepto.Text;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegTabla.Guardar(ObjetoTabla);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario(false);
                    txtNombre.Text = ObjetoTabla.Nombre;
                    CargarGrid();
                    txtNombre.Text = "";
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }

        private bool ValidarFormulario()
        {
            if (txtFormNombre.Text.Length == 0)
            {

                Controles.MensajeEnControl(txtFormNombre.ClientID);
                return false;
            }
            if (ddlFormEstado.SelectedValue == "-1")
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
        private void LimpiarFormulario(bool Cancelacion)
        {
            hfId.Value = "";
            if (Cancelacion == true)
            {
                hfTablaPadreId.Value = "";
                txtFormNombrePadre.Text = "";
            }
            ddlFormEstado.SelectedIndex = 0;
            txtFormNombre.Text = "";
            txtFormCodigo.Text = "";
            txtFormCodigoInterno.Text = "";
            txtFormConcepto.Text = "";

        }

        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<TablaInfo>();
                var ObjetoTabla = new TablaInfo();
                var NegTabla = new NegTablas();

                ObjetoTabla.Id = Id;
                ObjetoResultado = NegTabla.Buscar(ObjetoTabla);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoTabla = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoTabla != null)
                    {
                        LlenarFormulario(ObjetoTabla);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tabla");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tabla");
                }
            }

        }

        private void LlenarFormulario(TablaInfo ObjetoTabla)
        {
            try
            {
                hfId.Value = ObjetoTabla.Id.ToString();
                hfTablaPadreId.Value = ObjetoTabla.TablaPadre_Id == 0 ? "" : ObjetoTabla.TablaPadre_Id.ToString();
                txtFormNombre.Text = ObjetoTabla.Nombre;
                txtFormNombrePadre.Text = ObjetoTabla.NombreTablaPadre;
                txtFormCodigo.Text = ObjetoTabla.Codigo;
                txtFormCodigoInterno.Text = ObjetoTabla.CodigoInterno.ToString();
                txtFormConcepto.Text = ObjetoTabla.Concepto;
                ddlFormEstado.SelectedValue = ObjetoTabla.Estado_Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Tablas");
                }
            }
        }

        private TablaInfo DatosEntidad(TablaInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<TablaInfo>();
                var ObjetoTabla = new TablaInfo();
                var NegTabla = new NegTablas();

                ObjetoResultado = NegTabla.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoTabla = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoTabla != null)
                    {
                        return ObjetoTabla;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tabla");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Tabla");
                }
                return null;
            }
        }


        //Metodos Particulares
        private void CargarHijos(int TablaPadre_Id, string NombrePadre)
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoTabla = new TablaInfo();
                var NegTablas = new NegTablas();
                var ObjetoResultado = new Resultado<TablaInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoTabla.TablaPadre_Id = TablaPadre_Id;
                ObjetoTabla.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegTablas.Buscar(ObjetoTabla);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<TablaInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId, Constantes.StringNombre });
                    lblContador.Text = ObjetoResultado.ValorDecimal.ToString() + " Registros Encontrado(s)";
                    hfTablaPadreId.Value = TablaPadre_Id.ToString();
                    txtFormNombrePadre.Text = NombrePadre;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }
        #endregion
    }


}