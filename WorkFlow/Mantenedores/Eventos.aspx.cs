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
    public partial class Eventos : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarEstadosSolicitud();
                CargarSubEstadosSolicitud();
                CargarMallas();
                CargarRoles();
                CargarAcciones();
                CargarEventos();
                Controles.CargarCombo(ref ddlFormEtapa, new List<EtapaInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Etapa --", "-1");
                Controles.CargarCombo(ref ddlEtapa, new List<EtapaInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
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
        protected void btnAsignarRol_Click(object sender, EventArgs e)
        {
            AsignarRoles();
        }
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvRolesEvento.DataKeys[row.RowIndex].Values["Rol_Id"].ToString());
            EliminarAsignacionRol(Id);
        }
        protected void btnAsignarFlujo_Click(object sender, EventArgs e)
        {
            AsignaFlujo();
        }
        protected void btnEliminarFlujo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int EventoDestino_Id = int.Parse(gvAsignacionFlujo.DataKeys[row.RowIndex].Values["EventoDestino_Id"].ToString());
            int Accion_Id = int.Parse(gvAsignacionFlujo.DataKeys[row.RowIndex].Values["Accion_Id"].ToString());
            EliminarFlujo(EventoDestino_Id, Accion_Id);
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

        private void CargarEstadosSolicitud()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("EST_SOLICITUD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoSolicitud, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Sin Cambio de Estado --", "-1");
                   

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sub Estado Solicitud Sin Datos");
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

        private void CargarSubEstadosSolicitud()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("SUBEST_SOLICITUD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlSubEstadoSolicitud, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Sin Cambio de SubEstado --", "-1");
                   

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sub Estado de Solicitud Sin Datos");
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
        private void CargarMallas()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("MALLAS");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMalla, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlFormMalla, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Malla --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Malla Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Mallas");
                }
            }
        }
        private void CargarEtapas(int Malla_Id)
        {
            try
            {
                NegEtapas nEtapa = new NegEtapas();
                Resultado<EtapaInfo> rEtapa = new Resultado<EtapaInfo>();
                EtapaInfo oEtapa = new EtapaInfo();

                oEtapa.Malla_Id = Malla_Id;
                oEtapa.Estado_Id  = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                rEtapa = nEtapa.Buscar(oEtapa);
                if (rEtapa.ResultadoGeneral)
                {
                    Controles.CargarCombo(ref ddlEtapa, rEtapa.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo(ref ddlFormEtapa, rEtapa.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Etapa --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Etapas");
                }
            }
        }
        private void CargarRoles()
        {
            try
            {
                var oNegRoles = new NegRoles();
                var oRoles = new RolesInfo();
                var oResultado = new Resultado<RolesInfo>();

                oRoles.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oRoles.Sistema_Id = (int)NegTablas.IdentificadorMaestro("SISTEMAS", "WF");

                oResultado = oNegRoles.Buscar(oRoles);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<RolesInfo>(ref ddlRolAsignado, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<RolesInfo>(ref ddlFormRolAsignado, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<RolesInfo>(ref ddlFormRoles, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Roles --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tabla Roles");
                }
            }


        }
        private void CargarAcciones()
        {
            try
            {
                var oNegAcciones = new NegAcciones();
                var oAcciones = new AccionesInfo();
                var oResultado = new Resultado<AccionesInfo>();

                oAcciones.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                oResultado = oNegAcciones.Buscar(oAcciones);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<AccionesInfo>(ref ddlFormAccion, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Acciones --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tabla Acciones");
                }
            }

        }

        private void CargarEventos(int Evento_Id = -1)
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oEvento = new EventosInfo();
                var NegEventos = new NegEventos();
                var oResultado = new Resultado<EventosInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                //Ejecución del Proceso de Búsqueda
                oResultado = NegEventos.Buscar(oEvento);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<EventosInfo>(ref ddlFormEventoDestino, oResultado.Lista.Where(e => e.Id != Evento_Id).ToList<EventosInfo>(), Constantes.StringId, "DescripcionCompleta", "-- Evento Destino --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tabla Acciones");
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
            if (ddlFormMalla.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormMalla.ClientID);
                return false;
            }
            if (ddlFormEtapa.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEtapa.ClientID);
                return false;
            }

            if (ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID);
                return false;
            }

            var Duracion = 0;
            if (int.TryParse(txtFormDuracionEstandar.Text, out Duracion))
            {
                if (Duracion <= 0)
                {
                    Controles.MensajeEnControl(txtFormDuracionEstandar.ClientID, "El Valor debe ser mayor a 0");
                    return false;
                }
            }
            else
            {
                Controles.MensajeEnControl(txtFormDuracionEstandar.ClientID);
                return false;
            }

            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oEvento = new EventosInfo();
                var oResultado = new Resultado<EventosInfo>();
                var oNegEventos = new NegEventos();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oEvento.Id = int.Parse(hfId.Value);
                    oEvento = DatosEntidad(oEvento);
                }
                oEvento.Descripcion = txtFormDescripcion.Text;
                oEvento.Malla_Id = int.Parse(ddlFormMalla.SelectedValue);
                oEvento.Etapa_Id = int.Parse(ddlFormEtapa.SelectedValue);
                oEvento.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                oEvento.IndDesembolso = chkFormDesembolso.Checked;
                oEvento.IndEndoso = chkFormEndoso.Checked;
                oEvento.DuracionEstandar = int.Parse(txtFormDuracionEstandar.Text);
                oEvento.Rol_Id = int.Parse(ddlFormRolAsignado.SelectedValue);
                oEvento.EventoInicial = chkFormEventoInicial.Checked;
                oEvento.EventoFinal = chkFormEventoFinal.Checked;
                oEvento.DescripcionPlantilla = txtFormDescripcionPlantilla.Text;
                oEvento.IndModificaDatosCredito = chkFormIndModificaDatosCredito.Checked;
                oEvento.IndModificaDatosParticipantes = chkFormIndModificaDatosParticipantes.Checked;
                oEvento.IndModificaDatosPropiedades = chkFormIndModificaDatosPropiedades.Checked;
                oEvento.IndModificaDatosSeguros = chkFormIndModificaDatosSeguros.Checked;
                oEvento.IndFlujoEspecial = chkFormIndFlujoEspecial.Checked;
                oEvento.ProcedimientoDeTermino = txtFormProcedimientoDeTermino.Text;
                //Ejecucion del procedo para Guardar
                oResultado = oNegEventos.Guardar(ref oEvento);

                if (oResultado.ResultadoGeneral)
                {
                    GrabarAsignacionRoles(oEvento.Id);
                    GrabarFlujo(oEvento.Id);
                    CargarEventos(oEvento.Id);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Eventos");
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oEvento = new EventosInfo();
                var NegEventos = new NegEventos();
                var oResultado = new Resultado<EventosInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Malla_Id = int.Parse(ddlMalla.SelectedValue);
                oEvento.Etapa_Id = int.Parse(ddlEtapa.SelectedValue);
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);
                oEvento.Rol_Id = int.Parse(ddlRolAsignado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                oResultado = NegEventos.Buscar(oEvento);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EventosInfo>(ref gvBusqueda, oResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Usuarios");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var oResultado = new Resultado<EventosInfo>();
                var oEvento = new EventosInfo();
                var oNegEventos = new NegEventos();

                oEvento.Id = Id;
                oResultado = oNegEventos.Buscar(oEvento);

                if (oResultado.ResultadoGeneral == true)
                {
                    oEvento = oResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        LlenarFormulario(oEvento);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
                }
            }
        }
        private EventosInfo DatosEntidad(EventosInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<EventosInfo>();
                var oEvento = new EventosInfo();
                var oNegEventos = new NegEventos();

                oResultado = oNegEventos.Buscar(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oEvento = oResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        return oEvento;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");

                }
                return null;
            }
        }
        private void LlenarFormulario(EventosInfo oEvento)
        {
            try
            {
                LimpiarFormulario();
                ListarAsignacionRoles(oEvento.Id);
                ListarFlujo(oEvento.Id);
                CargarEventos(oEvento.Id);
                hfId.Value = oEvento.Id.ToString();
                txtFormDescripcion.Text = oEvento.Descripcion;
                txtFormDuracionEstandar.Text = oEvento.DuracionEstandar.ToString();
                ddlFormMalla.SelectedValue = oEvento.Malla_Id.ToString();
                CargarEtapas(oEvento.Malla_Id);
                ddlFormEtapa.SelectedValue = oEvento.Etapa_Id.ToString();
                ddlFormEstado.SelectedValue = oEvento.Estado_Id.ToString();
                chkFormDesembolso.Checked = oEvento.IndDesembolso == true ? true : false;
                chkFormEndoso.Checked = oEvento.IndEndoso == true ? true : false;
                chkFormEventoInicial.Checked = oEvento.EventoInicial == true ? true : false;
                chkFormEventoFinal.Checked = oEvento.EventoFinal == true ? true : false;
                txtFormDescripcionPlantilla.Text = oEvento.DescripcionPlantilla;
                chkFormIndModificaDatosCredito.Checked = oEvento.IndModificaDatosCredito == true ? true : false;
                chkFormIndModificaDatosParticipantes.Checked = oEvento.IndModificaDatosParticipantes == true ? true : false;
                chkFormIndModificaDatosPropiedades.Checked = oEvento.IndModificaDatosPropiedades == true ? true : false;
                chkFormIndModificaDatosSeguros.Checked = oEvento.IndModificaDatosSeguros == true ? true : false;
                ddlFormRolAsignado.SelectedValue = oEvento.Rol_Id.ToString();
                chkFormIndFlujoEspecial.Checked = oEvento.IndFlujoEspecial == true ? true : false;
                ConfiguracionFlujoEspecial();
                txtFormProcedimientoDeTermino.Text = oEvento.ProcedimientoDeTermino;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Eventos");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormMalla.ClearSelection();
            ddlFormEtapa.ClearSelection();
            chkFormDesembolso.Checked = false;
            chkFormEndoso.Checked = false;
            ddlFormEstado.ClearSelection();
            gvRolesEvento.DataSource = null;
            gvRolesEvento.DataBind();
            ddlFormRoles.ClearSelection();
            NegEventos.lstEventosRoles = new List<EventoRolesInfo>();
            gvAsignacionFlujo.DataSource = null;
            gvAsignacionFlujo.DataBind();
            ddlFormAccion.ClearSelection();
            ddlFormEventoDestino.ClearSelection();
            ddlEstadoSolicitud.ClearSelection();
            ddlSubEstadoSolicitud.ClearSelection();
            NegFlujo.lstFlujos = new List<FlujoInfo>();
            ddlFormRolAsignado.ClearSelection();
            chkFormEventoInicial.Checked = false;
            chkFormEventoFinal.Checked = false;
            txtFormDescripcionPlantilla.Text = "";
            chkFormIndModificaDatosCredito.Checked = false;
            chkFormIndModificaDatosParticipantes.Checked = false;
            chkFormIndModificaDatosPropiedades.Checked = false;
            chkFormIndModificaDatosSeguros.Checked = false;
            txtFormDuracionEstandar.Text = "";
            btnAsignarFlujo.Enabled = true;
            ddlFormAccion.Enabled = true;
            ddlFormEventoDestino.Enabled = true;
            txtFormProcedimientoDeTermino.Text = "";
            txtFormProcedimientoDeTermino.Enabled = false;
            chkFormIndFlujoEspecial.Checked = false;
            hfId.Value = "";
        }
        private void AsignarRoles()
        {
            try
            {
                Resultado<EventoRolesInfo> oResultado = new Resultado<EventoRolesInfo>();
                EventoRolesInfo oEventoRol = new EventoRolesInfo();
                NegEventos oNegEvento = new NegEventos();

                if (ddlFormRoles.SelectedValue == "-1")
                {
                    Controles.MensajeEnControl(ddlFormRoles.ClientID);
                    return;
                }
                if (NegEventos.lstEventosRoles == null)
                    NegEventos.lstEventosRoles = new List<EventoRolesInfo>();


                if (NegEventos.lstEventosRoles.FirstOrDefault(er => er.Rol_Id == int.Parse(ddlFormRoles.SelectedValue) && er.Id != -1) != null)
                {
                    Controles.MostrarMensajeAlerta("El Rol Seleccionado ya se encuentra asignado al Evento");
                    return;
                }

                oEventoRol.DescripcionRol = ddlFormRoles.SelectedItem.Text;
                oEventoRol.Rol_Id = int.Parse(ddlFormRoles.SelectedValue);
                oEventoRol.Id = 99;

                NegEventos.lstEventosRoles.Add(oEventoRol);
                Controles.CargarGrid<EventoRolesInfo>(ref gvRolesEvento, NegEventos.lstEventosRoles.Where(er => er.Id != -1).ToList<EventoRolesInfo>(), new string[] { "Id", "Rol_Id" });

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Asignar Rol");

                }
            }
        }
        private void ListarAsignacionRoles(int Evento_Id)
        {
            try
            {
                Resultado<EventoRolesInfo> oResultado = new Resultado<EventoRolesInfo>();
                EventoRolesInfo oEventoRol = new EventoRolesInfo();
                NegEventos oNegEvento = new NegEventos();
                oEventoRol.Evento_Id = Evento_Id;

                oResultado = oNegEvento.ListarEventoRoles(oEventoRol);
                if (oResultado.ResultadoGeneral)
                {
                    NegEventos.lstEventosRoles = new List<EventoRolesInfo>();
                    NegEventos.lstEventosRoles = oResultado.Lista;
                    Controles.CargarGrid<EventoRolesInfo>(ref gvRolesEvento, oResultado.Lista, new string[] { "Id", "Rol_Id" });

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
        private void GrabarAsignacionRoles(int Evento_Id)
        {
            try
            {
                Resultado<EventoRolesInfo> oResultado = new Resultado<EventoRolesInfo>();
                NegEventos oNegEvento = new NegEventos();


                foreach (var item in NegEventos.lstEventosRoles.Where(er => er.Id == -1))
                {
                    item.Evento_Id = Evento_Id;
                    oResultado = oNegEvento.DesAsignarEventoRol(item);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }
                }

                foreach (var item in NegEventos.lstEventosRoles.Where(er => er.Id != -1))
                {
                    item.Evento_Id = Evento_Id;
                    oResultado = oNegEvento.AsignarEventoRol(item);

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
        private void EliminarAsignacionRol(int Id)
        {

            try
            {
                Resultado<EventoRolesInfo> oResultado = new Resultado<EventoRolesInfo>();
                NegEventos oNegEvento = new NegEventos();

                foreach (var item in NegEventos.lstEventosRoles.Where(er => er.Rol_Id == Id))
                {
                    item.Id = -1;
                }

                Controles.CargarGrid<EventoRolesInfo>(ref gvRolesEvento, NegEventos.lstEventosRoles.Where(er => er.Id != -1).ToList<EventoRolesInfo>(), new string[] { "Id", "Rol_Id" });
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Eliminar Rol");

                }
            }
        }
        private void AsignaFlujo()
        {
            try
            {
                FlujoInfo oFlujo = new FlujoInfo();

                if (ddlFormAccion.SelectedValue == "-1")
                {
                    Controles.MensajeEnControl(ddlFormAccion.ClientID);
                    return;
                }
                if (ddlFormEventoDestino.SelectedValue == "-1")
                {
                    Controles.MensajeEnControl(ddlFormEventoDestino.ClientID);
                    return;
                }
                if (NegFlujo.lstFlujos == null)
                    NegFlujo.lstFlujos = new List<FlujoInfo>();



                if (NegFlujo.lstFlujos.FirstOrDefault(f => f.EventoDestino_Id == int.Parse(ddlFormEventoDestino.SelectedValue) && f.Accion_Id == int.Parse(ddlFormAccion.SelectedValue) && f.Id != -1) != null)
                {
                    Controles.MostrarMensajeAlerta("El Flujo ya se encuentra asignado");
                    return;
                }

                oFlujo.DescripcionEventoDestino = ddlFormEventoDestino.SelectedItem.Text;
                oFlujo.EventoDestino_Id = int.Parse(ddlFormEventoDestino.SelectedValue);
                oFlujo.DescripcionAccion = ddlFormAccion.SelectedItem.Text;
                oFlujo.Accion_Id = int.Parse(ddlFormAccion.SelectedValue);
                oFlujo.EstadoSolicitud_Id = int.Parse(ddlEstadoSolicitud.SelectedValue);
                oFlujo.DescripcionEstadoSolicitud = ddlEstadoSolicitud.SelectedItem.Text;
                oFlujo.SubEstadoSolicitud_Id = int.Parse(ddlSubEstadoSolicitud.SelectedValue);
                oFlujo.DescripcionSubEstadoSolicitud = ddlSubEstadoSolicitud.SelectedItem.Text;

                oFlujo.Id = 99;

                NegFlujo.lstFlujos.Add(oFlujo);
                Controles.CargarGrid<FlujoInfo>(ref gvAsignacionFlujo, NegFlujo.lstFlujos.Where(f => f.Id != -1).ToList<FlujoInfo>(), new string[] { "Id", "EventoDestino_Id", "Accion_Id" });
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
        private void ListarFlujo(int EventoOrigen_Id)
        {
            try
            {
                Resultado<FlujoInfo> oResultado = new Resultado<FlujoInfo>();
                FlujoInfo oFlujo = new FlujoInfo();
                NegFlujo oNegFlujo = new NegFlujo();
                oFlujo.EventoOrigen_Id = EventoOrigen_Id;
                oResultado = oNegFlujo.Buscar(oFlujo);
                if (oResultado.ResultadoGeneral)
                {
                    NegFlujo.lstFlujos = new List<FlujoInfo>();
                    NegFlujo.lstFlujos = oResultado.Lista;
                    Controles.CargarGrid<FlujoInfo>(ref gvAsignacionFlujo, oResultado.Lista, new string[] { "Id", "EventoDestino_Id", "Accion_Id" });
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
        private void GrabarFlujo(int EventoOrigen_Id)
        {
            try
            {
                Resultado<FlujoInfo> oResultado = new Resultado<FlujoInfo>();
                NegFlujo oNegFlujo = new NegFlujo();


                foreach (var item in NegFlujo.lstFlujos.Where(er => er.Id == -1))
                {
                    item.EventoOrigen_Id = EventoOrigen_Id;
                    oResultado = oNegFlujo.Eliminar(item);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }
                }

                foreach (var item in NegFlujo.lstFlujos.Where(er => er.Id != -1))
                {
                    item.EventoOrigen_Id = EventoOrigen_Id;
                    oResultado = oNegFlujo.Guardar(item);

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
        private void EliminarFlujo(int EventoDestino_Id, int Accion_Id)
        {

            try
            {
                Resultado<FlujoInfo> oResultado = new Resultado<FlujoInfo>();
                NegFlujo oNegEvento = new NegFlujo();

                foreach (var item in NegFlujo.lstFlujos.Where(f => f.EventoDestino_Id == EventoDestino_Id && f.Accion_Id == Accion_Id))
                {
                    item.Id = -1;
                }

                Controles.CargarGrid<FlujoInfo>(ref gvAsignacionFlujo, NegFlujo.lstFlujos.Where(er => er.Id != -1).ToList<FlujoInfo>(), new string[] { "Id", "EventoDestino_Id", "Accion_Id" });
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Eliminar Flujo");

                }
            }
        }





        #endregion

        protected void chkFormIndFlujoEspecial_CheckedChanged(object sender, EventArgs e)
        {
            ConfiguracionFlujoEspecial();
        }

        private void ConfiguracionFlujoEspecial()
        {
            try
            {
                if (chkFormIndFlujoEspecial.Checked)
                {
                    txtFormProcedimientoDeTermino.Enabled = true;
                }
                else
                {

                    txtFormProcedimientoDeTermino.Text = "";
                    txtFormProcedimientoDeTermino.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Configurar Flujo Especial");
                }
            }
        }

        protected void ddlMalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMalla.SelectedValue != "-1")
                CargarEtapas(int.Parse(ddlMalla.SelectedValue));
            else
            {
                Controles.CargarCombo<EtapaInfo>(ref ddlEtapa, new List<EtapaInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
            }
        }

        protected void ddlFormMalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormMalla.SelectedValue != "-1")
                CargarEtapas(int.Parse(ddlFormMalla.SelectedValue));
            else
            {
                Controles.CargarCombo<EtapaInfo>(ref ddlFormEtapa, new List<EtapaInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Etapa --", "-1");
            }
        }
    }
}