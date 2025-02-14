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

namespace WorkFlow.Eventos
{
    public partial class FirmaBancoAlzante : System.Web.UI.Page
    {
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
                    if (!ValidacionFechaFirmaBcoAlzante()) return;
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

        public bool ValidacionFechaFirmaBcoAlzante()
        {
            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.FechaFirmaBcoAlzante != null);
            if (oInfo == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha de Firma de Banco Alzante.");
                return false;
            }
            return true;
        }

        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                List<TasacionInfo> oLstInfo = new List<TasacionInfo>();
                TasacionInfo oInfo = new TasacionInfo();
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oInfo.IndAlzamientoHipoteca = true;
                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, oResultado.Lista, new string[] { "Solicitud_Id", "InstitucionAlzamientoHipoteca_Id" });

                    foreach (GridViewRow Row in gvGarantias.Rows)
                    {
                        TextBox atxtFecha = (TextBox)Row.FindControl("atxtFecha");
                        oInfo = new TasacionInfo();
                        int InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[Row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());
                        oInfo = oResultado.Lista.FirstOrDefault(d => d.InstitucionAlzamientoHipoteca_Id == InstitucionAlzamientoHipoteca_Id);
                        if (oInfo.FechaFirmaBcoAlzante != null)
                        {
                            atxtFecha.Text = oInfo.FechaFirmaBcoAlzante.Value.ToShortDateString();
                        }
                        else
                        {
                            atxtFecha.Text = "";
                        }

                    }



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
                    Controles.MostrarMensajeError("Error al Banco Alzante con Carta de Resguardo del Evento");
                }
            }
        }




        public void GrabaFechaFirmaEnvio()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<TasacionInfo>();
                var oNegocio = new NegPropiedades();


                if (NegCartaResguardo.objTasacion.FechaEnvioBcoAlzante > NegCartaResguardo.objTasacion.FechaFirmaBcoAlzante)
                {
                    Controles.MostrarMensajeAlerta("La Fecha de Firma no puede ser menor a la Fecha de Envio (" + NegCartaResguardo.objTasacion.FechaEnvioBcoAlzante.Value.ToShortDateString() + ")");
                    return;
                }


                oResultado = oNegocio.GuardarTasacion(NegCartaResguardo.objTasacion);


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
                    Controles.MostrarMensajeError("Error al guardar fecha de envio a Bco Alzante para el Evento");
                }
            }
        }

        protected void btnGuardaFirma_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((Anthem.ImageButton)sender).Parent.Parent as GridViewRow;
                var fechaFirma = ((Anthem.TextBox)row.Cells[3].FindControl("atxtFecha")).Text;
                int InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());

                if (fechaFirma.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar Fecha de Firma Para Continuar");
                    return;
                }

                NegCartaResguardo.objTasacion = new TasacionInfo();
                NegCartaResguardo.objTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.InstitucionAlzamientoHipoteca_Id == InstitucionAlzamientoHipoteca_Id);
                NegCartaResguardo.objTasacion.Solicitud_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
                NegCartaResguardo.objTasacion.InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());
                NegCartaResguardo.objTasacion.FechaFirmaBcoAlzante = Convert.ToDateTime(fechaFirma);

                GrabaFechaFirmaEnvio();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al guardar Fecha Firma de Banco Alzante para el Evento");
                }
            }
        }

    }
}