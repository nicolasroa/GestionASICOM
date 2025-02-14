using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Eventos.Desembolso
{
    public partial class LiquidacionMutuo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioCoordinacion();");
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = false;
                CargarAcciones();
                txtMontoLiquidacion.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoCredito);
                CargarInfoLiquidacion();

            }
        }
        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        public void btnAccion_Click(object sender, EventArgs e)
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
                    if (!ValidarObservaciones()) return;
                    if (!ValidarLiquidacion()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarObservaciones()) return;
                    if (!ValidarLiquidacion()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "D"))//FINALIZAR
                {
                    ActualizarLiquidacion(false);
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
        private bool ValidarObservaciones()
        {
            try
            {
                if (NegObservaciones.lstObservacionInfo.Count(o => o.Evento_Id == NegBandejaEntrada.oBandejaEntrada.Evento_Id && o.Estado_Id == 1) != 0)
                {
                    Controles.MostrarMensajeAlerta("Se deben subsanar todas las Observaciones realizaras en el Evento para continuar");
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Validar las Observaciones");
                }
            }
            return false;
        }

        private bool ValidarLiquidacion()
        {
            try
            {

                if (NegSolicitudes.objSolicitudInfo.FechaLiquidacionMutuo == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Procesar la Liquidación para Avanzar");
                    return false;
                }


                return true;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Validar Liquidación");
                }
                return false;
            }
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        protected void txtFechaSolicitudTasacion_TextChanged(object sender, EventArgs e)
        {
            CalcularValorDesembolso();
        }

        private void CalcularValorDesembolso()
        {
            try
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
                DateTime FechaParidad = DateTime.MinValue;
                DateTime FechaLiquidacion = DateTime.MinValue;
                DateTime FechaHoy = DateTime.Parse(DateTime.Now.ToShortDateString());
                if (DateTime.TryParse(txtFechaLiquidacion.Text, out FechaParidad))
                {

                    if (FechaParidad > FechaHoy)
                    {
                        txtMontoLiquidacionPesos.Text = "";
                        lblMontoLiquidacionPesos.Text = "";
                        txtFechaLiquidacion.Text = "";
                        return;
                    }
                    txtMontoLiquidacionPesos.Text = string.Format("{0:C0}", NegParidad.ObtenerParidad(CodigoMoneda, FechaParidad) * NegSolicitudes.objSolicitudInfo.MontoCredito);
                    lblMontoLiquidacionPesos.Text = string.Format("{0:F0}", NegParidad.ObtenerParidad(CodigoMoneda, FechaParidad) * NegSolicitudes.objSolicitudInfo.MontoCredito);
                }
                else
                {
                    txtMontoLiquidacionPesos.Text = "";
                    lblMontoLiquidacionPesos.Text = "";
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
                    Controles.MostrarMensajeError("Error al Validar Calcular el Valor en Pesos");
                }
            }
        }


        private void CargarInfoLiquidacion()
        {
            try
            {
                if (NegSolicitudes.objSolicitudInfo.FechaLiquidacionMutuo != null)
                {
                    txtFechaLiquidacion.Text = NegSolicitudes.objSolicitudInfo.FechaLiquidacionMutuo.Value.ToShortDateString();
                    txtMontoLiquidacionPesos.Text = string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos);
                    lblMontoLiquidacionPesos.Text = string.Format("{0:F0}", NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos);
                    txtFechaLiquidacion.Enabled = false;
                    btnProcesarLiquidacion.Visible = false;
                    btnAnularLiquidacion.Visible = true;
                }
                else
                {
                    txtFechaLiquidacion.Enabled = true;
                    btnProcesarLiquidacion.Visible = true;
                    btnAnularLiquidacion.Visible = false;
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
                    Controles.MostrarMensajeError("Error al Cargar información de la Liquidación");
                }
            }
        }

        private void ActualizarLiquidacion(bool indProceso)//1: Procesar 0: Anular
        {
            try
            {
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = rSolicitud.Lista.FirstOrDefault();
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return;
                }

                if (indProceso)
                {
                    if (txtFechaLiquidacion.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Liquidación");
                        return;
                    }
                    NegSolicitudes.objSolicitudInfo.FechaLiquidacionMutuo = DateTime.Parse(txtFechaLiquidacion.Text);
                    NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos = decimal.Parse(lblMontoLiquidacionPesos.Text);
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.FechaLiquidacionMutuo = null;
                    NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos = -1;
                }
                rSolicitud = nSolicitud.Guardar(NegSolicitudes.objSolicitudInfo);
                if (rSolicitud.ResultadoGeneral)
                {
                    btnAnularLiquidacion.Visible = indProceso;
                    btnProcesarLiquidacion.Visible = !indProceso;
                    txtFechaLiquidacion.Enabled = !indProceso;
                    txtFechaLiquidacion.Text = indProceso == false ? "" : txtFechaLiquidacion.Text;
                    txtMontoLiquidacionPesos.Text = indProceso == false ? "" : txtMontoLiquidacionPesos.Text;
                    lblMontoLiquidacionPesos.Text = indProceso == false ? "" : lblMontoLiquidacionPesos.Text;
                    Controles.MostrarMensajeExito("Actualización Realizada");
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar Información de la Liquidación");
                }
            }
        }

        protected void btnProcesarLiquidacion_Click(object sender, EventArgs e)
        {
            ActualizarLiquidacion(true);
        }

        protected void btnAnularLiquidacion_Click(object sender, EventArgs e)
        {
            ActualizarLiquidacion(false);
        }
    }
}