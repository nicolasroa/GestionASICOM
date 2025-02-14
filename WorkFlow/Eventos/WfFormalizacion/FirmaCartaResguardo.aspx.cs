using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class FirmaCartaResguardo : System.Web.UI.Page
    {

        private List<InstitucionesFinancierasBase> lstIIFF = new List<InstitucionesFinancierasBase>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioDatepicker();");
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargarGarantiaConCartaResguardo();
            }
        }

        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        protected void btnAccion_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }
        public void CargarAcciones()
        {
            try
            {
                AccionesInfo oAcciones = new AccionesInfo();
                NegAcciones negAcciones = new NegAcciones();
                Resultado<AccionesInfo> oResultado = new Resultado<AccionesInfo>();
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();

                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                oAcciones.Evento_Id = oBandeja.Evento_Id;

                oResultado = negAcciones.BuscarAccionesEvento(oAcciones);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<AccionesInfo>(ref ddlAccionEvento, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Acciones--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar las Acciones del Evento");
                }
            }
        }
        public void SeleccionarAccion()
        {
            try
            {
                int Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);
                if (Accion_Id == -1)
                {
                    btnAccion.Visible = false;
                    return;
                }


                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));

                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-forward'></span>Avanzar";
                    btnAccion.CssClass = "btn btn-sm btn-success";
                    btnAccion.Visible = true;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "D"))//DEVOLVER
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-backward'></span>Devolver";
                    btnAccion.CssClass = "btn btn-sm btn-primary";
                    btnAccion.Visible = true;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-trash'></span>Finalizar Solicitud";
                    btnAccion.CssClass = "btn btn-sm btn-danger";
                    btnAccion.Visible = true;
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Acción no Configurada");
                    btnAccion.Visible = false;
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
                    Controles.MostrarMensajeError("Error al Seleccionar la Acción del Evento");
                }
            }
        }
        public void ProcesarEvento()
        {
            try
            {
                int Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);

                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));

                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    //validacion
                    if (!ValidaFirmaCartaResguardo()) return;

                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                }

                FlujoInfo oFlujo = new FlujoInfo();
                NegFlujo nFLujo = new NegFlujo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();

                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                oFlujo.Solicitud_Id = oBandeja.Solicitud_Id;
                oFlujo.Evento_Id = oBandeja.Evento_Id;
                oFlujo.Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);

                oFlujo.Usuario_Id = NegUsuarios.Usuario.Rut;
                oFlujo.Bandeja_Id = oBandeja.Id;

                rFlujo = nFLujo.TerminarEvento(oFlujo);
                if (rFlujo.ResultadoGeneral)
                {
                    NegBandejaEntrada.ActualizarBandeja = true;
                    Controles.CerrarModal(1);
                }
                else
                {
                    Controles.MostrarMensajeError(rFlujo.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Procesar el Evento");
                }
            }
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Id"].ToString());

            NegCartaResguardo.objTasacion = new TasacionInfo();
            NegCartaResguardo.objTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.Id == Id);
            NegCartaResguardo.objApoderadoTasacionInfo = new ApoderadoTasacionInfo();
            NegCartaResguardo.objApoderadoTasacionInfo.Tasacion_Id = NegCartaResguardo.objTasacion.Id;
            ObtenerDatosParaFirma();
        }

        public bool ValidaFirmaCartaResguardo()
        {
            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.StrFirmaApoderados != "-1");
            if (oInfo == null)
            {
                Controles.MostrarMensajeError("Debe ingresar Fecha de Firma de los apoderados.");
                return false;
            }
            return true;
        }
        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                TasacionInfo oInfo = new TasacionInfo();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oInfo.IndAlzamientoHipoteca = true;

                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, NegPropiedades.lstTasaciones, new string[] { "Id", "StrFirmaApoderados" });
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
                    Controles.MostrarMensajeError("Error al Cargar las Garantias con Resguardo del Evento");
                }
            }
        }

        public void ObtenerDatosParaFirma()
        {
            try
            {
                //Declaracion de Variables
                var OResultado = new Resultado<ApoderadoTasacionInfo>();
                var ONegocio = new NegCartaResguardo();

                var oInfo = new ApoderadoTasacionInfo();

                //Asignacion de Variables
                OResultado = ONegocio.BuscarApoderadoTasacion(NegCartaResguardo.objApoderadoTasacionInfo);
                if (OResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvApoderados, NegCartaResguardo.lstApoderadoTasacionInfo, new string[] { "Id", "Tasacion_Id", "Apoderado_Id" });

                    foreach (GridViewRow Row in gvApoderados.Rows)
                    {
                        TextBox txtFechaFirma = (TextBox)Row.FindControl("txtFechaFirma");
                        oInfo = new ApoderadoTasacionInfo();
                        int Apoderado_Id = int.Parse(gvApoderados.DataKeys[Row.RowIndex].Values["Apoderado_Id"].ToString());
                        oInfo = NegCartaResguardo.lstApoderadoTasacionInfo.FirstOrDefault(d => d.Apoderado_Id == Apoderado_Id);
                        if (oInfo.FechaFirma != null)
                        {
                            txtFechaFirma.Text = oInfo.FechaFirma.Value.ToShortDateString();
                        }
                        else
                        {
                            txtFechaFirma.Text = "";
                        }

                    }



                    //AsignaFormato();
                }
                else
                {
                    Controles.MostrarMensajeError(OResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Apoderados del Evento");
                }
            }
        }

        public void GrabaFechaFirmaApoderado()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<ApoderadoTasacionInfo>();
                var oNegocio = new NegCartaResguardo();
                //Asignacion de Variables

                oResultado = oNegocio.GuardarApoderadoTasacion(NegCartaResguardo.objApoderadoTasacionInfo);

                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información Registrada");
                    CargarGarantiaConCartaResguardo();
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return;
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
                    Controles.MostrarMensajeError("Error al Cargar las Instituciones de Resguardo para el Evento");
                }
            }
        }

        private void LimpiarRegistroApoderados()
        {
            NegCartaResguardo.objApoderadoTasacionInfo = new ApoderadoTasacionInfo();
            NegCartaResguardo.objTasacion = new TasacionInfo();
            gvApoderados.DataSource = null;
            gvApoderados.DataBind();
        }

        protected void gvGarantiasConResguardo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGarantias.PageIndex = e.NewPageIndex;
            CargarGarantiaConCartaResguardo();
        }


        protected void btnGuardaFirma_Click(object sender, ImageClickEventArgs e)
        {
            if (NegCartaResguardo.objTasacion == null || NegCartaResguardo.objTasacion.Id == -1)
            {
                Controles.MostrarMensajeError("Debe seleccionar una Garantía.");
                return;
            }

            try
            {
                GridViewRow row = ((Anthem.ImageButton)sender).Parent.Parent as GridViewRow;
                var fechaFirma = Convert.ToDateTime(((Anthem.TextBox)row.Cells[3].FindControl("txtFechaFirma")).Text);

                NegCartaResguardo.objApoderadoTasacionInfo = new ApoderadoTasacionInfo()
                {
                    Tasacion_Id = int.Parse(gvApoderados.DataKeys[row.RowIndex].Values["Tasacion_Id"].ToString()),
                    Apoderado_Id = int.Parse(gvApoderados.DataKeys[row.RowIndex].Values["Apoderado_Id"].ToString()),
                    FechaFirma = fechaFirma
                    
                };

                GrabaFechaFirmaApoderado();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al grabar Fecha de Firma Apoderado para el Evento");
                }
            }
        }

    }
}