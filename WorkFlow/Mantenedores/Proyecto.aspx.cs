using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;

namespace WorkFlow.Mantenedores
{
    public partial class Proyecto : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargaComboInmobiliaria();
                CargaComboRegion();
                CargaComboTipoInmueble();
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        private void CargaComboTipoInmueble()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TipoInmuebleInfo();
                var ObjResultado = new Resultado<TipoInmuebleInfo>();
                var objNegocio = new NegPropiedades();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.IndPropiedadPrincipal = true;
                ObjResultado = objNegocio.BuscarTipoInmueble(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoInmuebleInfo>(ref ddlFormTipoInmueble, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Tipo Inmueble--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Tipo Inmueble");
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

            if (ddlFormInmobiliaria.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormInmobiliaria.ClientID);
                return false;
            }

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
                var oProyecto = new ProyectoInfo();
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oNegProyecto = new NegProyectos();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oProyecto.Id = int.Parse(hfId.Value);
                    oProyecto = DatosEntidad(oProyecto);
                }
                oProyecto.Descripcion = txtFormDescripcion.Text;
                oProyecto.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                oProyecto.Inmobiliaria_Id = int.Parse(ddlFormInmobiliaria.SelectedValue);
                oProyecto.Comuna_Id = int.Parse(ddlFormComuna.SelectedValue);
                oProyecto.Direccion = txtFormDireccion.Text;
                oProyecto.RolMatriz = txtFormRolMatriz.Text;
                oProyecto.ComunaSII_Id = int.Parse(ddlFormComunaSII.SelectedValue);
                oProyecto.TipoInmueble_Id = int.Parse(ddlFormTipoInmueble.SelectedValue);

                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegProyecto.Guardar(oProyecto);

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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
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
                var oEvento = new ProyectoInfo();
                var NegProyecto = new NegProyectos();
                var ObjetoResultado = new Resultado<ProyectoInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);
                oEvento.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegProyecto.Buscar(oEvento);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ProyectoInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Proyecto");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oEvento = new ProyectoInfo();
                var oNegProyecto = new NegProyectos();

                oEvento.Id = Id;
                ObjetoResultado = oNegProyecto.Buscar(oEvento);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
                }
            }
        }
        private ProyectoInfo DatosEntidad(ProyectoInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oEvento = new ProyectoInfo();
                var oNegProyecto = new NegProyectos();

                ObjetoResultado = oNegProyecto.Buscar(Entidad);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");

                }
                return null;
            }
        }
        private void LlenarFormulario(ProyectoInfo oProyecto)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oProyecto.Id.ToString();
                txtFormDescripcion.Text = oProyecto.Descripcion;
                ddlFormEstado.SelectedValue = oProyecto.Estado_Id.ToString();
                ddlFormInmobiliaria.SelectedValue = oProyecto.Inmobiliaria_Id.ToString();
                CargaComboRegion();
                ddlFormRegion.SelectedValue = oProyecto.Region_Id.ToString();
                CargaComboProvincia(int.Parse(ddlFormRegion.SelectedValue));
                ddlFormProvincia.SelectedValue = oProyecto.Provincia_Id.ToString();
                CargaComboComuna(int.Parse(ddlFormProvincia.SelectedValue));
                ddlFormComuna.SelectedValue = oProyecto.Comuna_Id.ToString();
                txtFormDireccion.Text = oProyecto.Direccion;
                txtFormRolMatriz.Text = oProyecto.RolMatriz;
                ddlFormTipoInmueble.SelectedValue = oProyecto.TipoInmueble_Id.ToString();
                if (oProyecto.ComunaSII_Id == -1)
                    CargarComboComunaSII();
                else
                    CargarComboComunaSII(oProyecto.DescripcionComuna);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Proyecto");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();
            ddlFormInmobiliaria.ClearSelection();
            ddlFormRegion.ClearSelection();
            ddlFormProvincia.ClearSelection();
            ddlFormComuna.ClearSelection();
            txtFormDireccion.Text = "";
            ddlFormTipoInmueble.ClearSelection();

            hfId.Value = "";
        }

        private void CargaComboInmobiliaria()
        {
            try
            {
                ////Declaracion de Variables
                var objInmobiliaria = new InmobiliariaInfo();
                var objResultado = new Resultado<InmobiliariaInfo>();
                var NegInmobiliaria = new NegInmobiliarias();

                ////Asignacion de Variables
                objInmobiliaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                objResultado = NegInmobiliaria.Buscar(objInmobiliaria);
                if (objResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Todas--", "-1");
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlFormInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Inmobiliaria--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(objResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Inmobiliaria");
                }
            }
        }

        private void CargaComboRegion()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new RegionInfo();
                var ObjetoResultado = new Resultado<RegionInfo>();
                var NegRegion = new NegRegion();

                ////Asignacion de Variables
                ObjetoResultado = NegRegion.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<RegionInfo>(ref ddlFormRegion, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Región--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Región");
                }
            }
        }
        private void CargaComboProvincia(int Region_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProvinciaInfo();
                var ObjetoResultado = new Resultado<ProvinciaInfo>();
                var NegProvincia = new NegProvincia();

                ////Asignacion de Variables
                ObjInfo.Region_Id = Region_Id;
                ObjetoResultado = NegProvincia.Buscar(ObjInfo);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProvinciaInfo>(ref ddlFormProvincia, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Provincia--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Provincia");
                }
            }
        }
        private void CargaComboComuna(int Provincia_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ComunaInfo();
                var ObjetoResultado = new Resultado<ComunaInfo>();
                var NegComuna = new NegComunas();

                ////Asignacion de Variables
                ObjInfo.Provincia_Id = Provincia_Id;
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ComunaInfo>(ref ddlFormComuna, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Comuna--", "-1");




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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }

        private void CargarComboComunaSII(string DescripcionComuna = "")
        {
            try
            {
                NegComunas nComuna = new NegComunas();
                Resultado<ComunaSiiInfo> rComuna = new Resultado<ComunaSiiInfo>();
                ComunaSiiInfo oComuna = new ComunaSiiInfo();

                oComuna.Descripcion = DescripcionComuna;
                rComuna = nComuna.BuscarComunaSii(oComuna);
                if (rComuna.ResultadoGeneral)
                {
                    if (rComuna.Lista.Count == 1)
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "", "");
                    else
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Comuna SII");
                }
            }
        }

        #endregion

        protected void ddlFormRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboProvincia(int.Parse(ddlFormRegion.SelectedValue));
        }

        protected void ddlFormProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboComuna(int.Parse(ddlFormProvincia.SelectedValue));
        }

        protected void ddlFormComuna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormComuna.SelectedValue == "-1")
                CargarComboComunaSII();
            else
                CargarComboComunaSII(ddlFormComuna.SelectedItem.Text);
        }
    }
}