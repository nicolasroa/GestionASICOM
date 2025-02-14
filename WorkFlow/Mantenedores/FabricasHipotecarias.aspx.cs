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
    public partial class FabricasHipotecarias : System.Web.UI.Page
    {
        #region Fabrica
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarTipoFabrica();
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
        protected void btnAsignarTipoFabrica_Click(object sender, EventArgs e)
        {
            AsignarTipoFabrica();
        }
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvTipoFabrica.DataKeys[row.RowIndex].Values["TipoFabrica_Id"].ToString());
            EliminarAsignacionTipoFabrica(Id);
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
        private void CargarTipoFabrica()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPO_FABRICA");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlFormTipoFabrica, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Fábrica --", "-1");

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
                var oFabrica = new FabricaInfo();
                var oResultado = new Resultado<FabricaInfo>();
                var oNegFabrica = new NegFabricas();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oFabrica.Id = int.Parse(hfId.Value);
                    oFabrica = DatosEntidad(oFabrica);
                }
                oFabrica.Descripcion = txtFormDescripcion.Text;
                oFabrica.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                //Ejecucion del procedo para Guardar
                oResultado = oNegFabrica.GuardarFabrica(ref oFabrica);

                if (oResultado.ResultadoGeneral)
                {

                    GrabarAsignacionTipoFabrica(oFabrica.Id);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Fabrica");
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oFabrica = new FabricaInfo();
                var NegFabrica = new NegFabricas();
                var oResultado = new Resultado<FabricaInfo>();

                //Asignación de Variables de Búsqueda
                oFabrica.Descripcion = txtDescripcion.Text;
                oFabrica.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                oResultado = NegFabrica.BuscarFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<FabricaInfo>(ref gvBusqueda, oResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Fábricas");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var oResultado = new Resultado<FabricaInfo>();
                var oFabrica = new FabricaInfo();
                var oNegFabrica = new NegFabricas();

                oFabrica.Id = Id;
                oResultado = oNegFabrica.BuscarFabrica(oFabrica);

                if (oResultado.ResultadoGeneral == true)
                {
                    oFabrica = oResultado.Lista.FirstOrDefault();

                    if (oFabrica != null)
                    {
                        LlenarFormulario(oFabrica);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Fábricas");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Fábricas");
                }
            }
        }
        private FabricaInfo DatosEntidad(FabricaInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<FabricaInfo>();
                var oFabrica = new FabricaInfo();
                var oNegFabrica = new NegFabricas();

                oResultado = oNegFabrica.BuscarFabrica(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oFabrica = oResultado.Lista.FirstOrDefault();

                    if (oFabrica != null)
                    {
                        return oFabrica;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Fábricas");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Fábricas");

                }
                return null;
            }
        }
        private void LlenarFormulario(FabricaInfo oFabrica)
        {
            try
            {
                LimpiarFormulario();
                ListarAsignacionTipoFabrica(oFabrica.Id);
                hfId.Value = oFabrica.Id.ToString();
                txtFormDescripcion.Text = oFabrica.Descripcion;
                ddlFormEstado.SelectedValue = oFabrica.Estado_Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Fabrica");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();
            gvTipoFabrica.DataSource = null;
            gvTipoFabrica.DataBind();
            ddlFormTipoFabrica.ClearSelection();
            NegFabricas.lstAsignacionTipoFabrica = new List<AsignacionTipoFabricaInfo>();
            hfId.Value = "";
        }

        private void AsignarTipoFabrica()
        {
            try
            {
                AsignacionTipoFabricaInfo oAsingacion = new AsignacionTipoFabricaInfo();

                if (ddlFormTipoFabrica.SelectedValue == "-1")
                {
                    Controles.MensajeEnControl(ddlFormTipoFabrica.ClientID);
                    return;
                }

                if (NegFabricas.lstAsignacionTipoFabrica == null)
                    NegFabricas.lstAsignacionTipoFabrica = new List<AsignacionTipoFabricaInfo>();



                if (NegFabricas.lstAsignacionTipoFabrica.FirstOrDefault(a => a.TipoFabrica_Id == int.Parse(ddlFormTipoFabrica.SelectedValue) && a.Id != -1) != null)
                {
                    Controles.MostrarMensajeAlerta("El Tipo de Fábrica ya se encuentra asignado");
                    return;
                }

                oAsingacion.DescripcionTipoFabrica = ddlFormTipoFabrica.SelectedItem.Text;
                oAsingacion.TipoFabrica_Id = int.Parse(ddlFormTipoFabrica.SelectedValue);
                oAsingacion.Id = 99;

                NegFabricas.lstAsignacionTipoFabrica.Add(oAsingacion);
                Controles.CargarGrid<AsignacionTipoFabricaInfo>(ref gvTipoFabrica, NegFabricas.lstAsignacionTipoFabrica.Where(a => a.Id != -1).ToList<AsignacionTipoFabricaInfo>(), new string[] { "Id", "Fabrica_Id", "TipoFabrica_Id" });
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Asignar Flujo");

                }
            }
        }
        private void EliminarAsignacionTipoFabrica(int TipoFabrica_Id)
        {
            try
            {
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                NegFabricas oNeg = new NegFabricas();

                foreach (var item in NegFabricas.lstAsignacionTipoFabrica.Where(a => a.TipoFabrica_Id == TipoFabrica_Id))
                {
                    item.Id = -1;
                }

                Controles.CargarGrid<AsignacionTipoFabricaInfo>(ref gvTipoFabrica, NegFabricas.lstAsignacionTipoFabrica.Where(a => a.Id != -1).ToList<AsignacionTipoFabricaInfo>(), new string[] { "Id", "Fabrica_Id", "TipoFabrica_Id" });
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Eliminar Asignacion Tipo FabricaInfo");

                }
            }
        }
        private void GrabarAsignacionTipoFabrica(int Fabrica_Id)
        {
            try
            {
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                NegFabricas oNeg = new NegFabricas();


                foreach (var item in NegFabricas.lstAsignacionTipoFabrica.Where(er => er.Id == -1))
                {
                    item.Fabrica_Id = Fabrica_Id;
                    oResultado = oNeg.EliminarAsignacionTipoFabrica(item);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }
                }

                foreach (var item in NegFabricas.lstAsignacionTipoFabrica.Where(er => er.Id != -1))
                {
                    item.Fabrica_Id = Fabrica_Id;
                    oResultado = oNeg.GuardarAsignacionTipoFabrica(item);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
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
                    Controles.MostrarMensajeError("Error al Grabar Rol");

                }
            }
        }
        private void ListarAsignacionTipoFabrica(int Fabrica_Id)
        {
            try
            {
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                AsignacionTipoFabricaInfo oAsignacion = new AsignacionTipoFabricaInfo();
                NegFabricas oNegAsignacion = new NegFabricas();
                oAsignacion.Fabrica_Id = Fabrica_Id;
                oResultado = oNegAsignacion.BuscarAsignacionTipoFabrica(oAsignacion);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<AsignacionTipoFabricaInfo>(ref gvTipoFabrica, oResultado.Lista, new string[] { "Id", "Fabrica_Id", "TipoFabrica_Id" });
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
                    Controles.MostrarMensajeError("Error al Listar Rol");

                }
            }
        }



        #endregion
    }
}