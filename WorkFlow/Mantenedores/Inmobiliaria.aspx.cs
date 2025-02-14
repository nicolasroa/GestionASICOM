using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Inmobiliaria : System.Web.UI.Page
    {
        #region Eventos
        public override void VerifyRenderingInServerForm(Control control) { }
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

            if (txtFormRut.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormRut.ClientID);
                return false;
            }
          

            if (chIndDesembolso.Checked && ddlFormEventoDesembolso.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEventoDesembolso.ClientID);
                return false;
            }


            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var oInmobiliaria = new InmobiliariaInfo();
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oNegInmobiliaria = new NegInmobiliarias();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    oInmobiliaria.Id = int.Parse(hfId.Value);
                    oInmobiliaria = DatosEntidad(oInmobiliaria);
                }
                oInmobiliaria.Descripcion = txtFormDescripcion.Text;
                oInmobiliaria.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                oInmobiliaria.IndDesembolso = chIndDesembolso.Checked;
                oInmobiliaria.EventoDesembolso_Id = int.Parse(ddlFormEventoDesembolso.SelectedValue);
                oInmobiliaria.RutInmobiliaria = txtFormRut.Text;
                oInmobiliaria.Contacto = txtFormContacto.Text;
                oInmobiliaria.CargoContacto = txtFormCargoContacto.Text;
                oInmobiliaria.MailContacto = txtFormMailContacto.Text;
                oInmobiliaria.FonoFijoContacto = txtFormFonoFijoContacto.Text;
                oInmobiliaria.CelularContacto = txtFormCelularContacto.Text;



                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegInmobiliaria.Guardar(oInmobiliaria);

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
                var oEvento = new InmobiliariaInfo();
                var NegInmobiliaria = new NegInmobiliarias();
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Descripcion = txtDescripcion.Text;
                oEvento.Estado_Id = int.Parse(ddlEstado.SelectedValue);
               
                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegInmobiliaria.Buscar(oEvento);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<InmobiliariaInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oEvento = new InmobiliariaInfo();
                var oNegInmobiliaria = new NegInmobiliarias();

                oEvento.Id = Id;
                ObjetoResultado = oNegInmobiliaria.Buscar(oEvento);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
                }
            }
        }
        private InmobiliariaInfo DatosEntidad(InmobiliariaInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oEvento = new InmobiliariaInfo();
                var oNegInmobiliaria = new NegInmobiliarias();

                ObjetoResultado = oNegInmobiliaria.Buscar(Entidad);

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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");

                }
                return null;
            }
        }
        private void LlenarFormulario(InmobiliariaInfo oInmobiliaria)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = oInmobiliaria.Id.ToString();
                txtFormDescripcion.Text = oInmobiliaria.Descripcion;
                ddlFormEstado.SelectedValue = oInmobiliaria.Estado_Id.ToString();
                txtFormRut.Text = oInmobiliaria.RutInmobiliaria;
                txtFormContacto.Text = oInmobiliaria.Contacto;
                txtFormCargoContacto.Text = oInmobiliaria.CargoContacto;
                txtFormMailContacto.Text = oInmobiliaria.MailContacto;
                txtFormFonoFijoContacto.Text = oInmobiliaria.FonoFijoContacto;
                txtFormCelularContacto.Text = oInmobiliaria.CelularContacto;
                chIndDesembolso.Checked = oInmobiliaria.IndDesembolso == true ? true : false;
                if (chIndDesembolso.Checked)
                {
                    ddlFormEventoDesembolso.SelectedValue = oInmobiliaria.EventoDesembolso_Id.ToString();
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormDescripcion.Text = "";
            ddlFormEstado.ClearSelection();
            txtFormRut.Text = "";
            txtFormContacto.Text = "";
            txtFormCargoContacto.Text = "";
            txtFormMailContacto.Text = "";
            txtFormFonoFijoContacto.Text = "";
            txtFormCelularContacto.Text = "";
            chIndDesembolso.Checked = false;
            ddlFormEventoDesembolso.ClearSelection();
            ddlFormEventoDesembolso.Enabled = false;
            Controles.CargarCombo<EventosInfo>(ref ddlFormEventoDesembolso, null, Constantes.StringId, "Descripcion", "-- Evento Desembolso --", "-1");

            hfId.Value = "";
        }
        private void CargarEventosDesembolso()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oEvento = new EventosInfo();
                var NegEventos = new NegEventos();
                var oResultado = new Resultado<EventosInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oEvento.IndDesembolso = true;

                //Ejecución del Proceso de Búsqueda
                oResultado = NegEventos.Buscar(oEvento);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<EventosInfo>(ref ddlFormEventoDesembolso, oResultado.Lista, Constantes.StringId, "Descripcion", "-- Evento Desembolso --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Evento Desembolso");
                }
            }
        }


        #endregion

        protected void chIndDesembolso_CheckedChanged(object sender, EventArgs e)
        {
            if (chIndDesembolso.Checked)
            {
                ddlFormEventoDesembolso.Enabled = true;
                CargarEventosDesembolso();
            }
            else
            {
                ddlFormEventoDesembolso.Enabled = false;
                ddlFormEventoDesembolso.ClearSelection();
            }

        }

        private void GenerarReporteExcel()
        {
            try
            {
                var dsGeneral = new DataSet();
                var dtReporte = new DataTable();
                List<InmobiliariaRep> lstReporte = new List<InmobiliariaRep>();
                InmobiliariaRep oInmobiliariaRep = new InmobiliariaRep();
                if (NegInmobiliarias.lstInmobiliarias != null)
                {

                    foreach (var item in NegInmobiliarias.lstInmobiliarias)
                    {
                        oInmobiliariaRep = new InmobiliariaRep();
                        oInmobiliariaRep.RutInmobiliaria = item.RutInmobiliaria;
                        oInmobiliariaRep.CargoContacto = item.CargoContacto;
                        oInmobiliariaRep.CelularContacto = item.CelularContacto;
                        oInmobiliariaRep.Contacto = item.Contacto;
                        oInmobiliariaRep.Descripcion = item.Descripcion;
                        oInmobiliariaRep.DescripcionEstado = item.DescripcionEstado;
                        oInmobiliariaRep.DescripcionEventoDesembolso = item.DescripcionEventoDesembolso;
                        oInmobiliariaRep.FonoFijoContacto = item.FonoFijoContacto;
                        oInmobiliariaRep.MailContacto = item.MailContacto;
                        lstReporte.Add(oInmobiliariaRep);

                    }




                    dtReporte = Excel.GenerarDataTable<InmobiliariaRep>(lstReporte);
                    dsGeneral.Tables.Add(dtReporte);
                    dsGeneral.Tables[0].TableName = "Nómina de Inmobiliarias";
                    string Resultado = Excel.ExportarDataSet(dsGeneral, "RPT_Inmobiliarias al " + DateTime.Now.ToLongDateString());
                    if (Resultado != "")
                    {
                        Controles.MostrarMensajeError(Resultado);
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
                    Controles.MostrarMensajeError("Error al Generar Reporte");
                }
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }
    }
}