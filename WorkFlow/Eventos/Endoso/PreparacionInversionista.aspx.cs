using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Eventos
{
    public partial class PreparacionInversionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Anthem.Manager.Register(Page);
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargaFormulario();
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

        protected void btnAgregaInversionista_Click(object sender, EventArgs e)
        {
            GuardarInversionista();
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
                    if (!GuardarInversionista()) return;
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

        private void CargaFormulario()
        {
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
            lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
            txtValorUF.Text = string.Format("{0:0,0}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");
            CargarInversionista();
            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        txtTasaEndoso.Text = ObjInfo.TasaEndoso == 0 ? "" : string.Format("{0:F4}", ObjInfo.TasaEndoso); ;
                        txtTasaCredito.Text = string.Format("{0:F4}", ObjInfo.TasaFinal);
                        txtPlazoCredito.Text = ObjInfo.Plazo.ToString();
                        txtMontoCredito.Text = ObjInfo.MontoCredito.ToString();
                        txtGracia.Text = ObjInfo.Gracia.ToString();
                        ddlInversionista.SelectedValue = ObjInfo.Inversionista_Id.ToString();
                        txtPlazoCredito.Enabled = false;
                        txtMontoCredito.Enabled = false;
                        txtGracia.Enabled = false;
                        txtTasaCredito.Enabled = false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Simular");
                }
            }

        }

        private void CargarInversionista()
        {
            try
            {
                var oInfo = new InversionistaInfo();
                var oNegocio = new NegInversionistas();
                var oResultado = new Resultado<InversionistaInfo>();
                oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oResultado = oNegocio.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InversionistaInfo>(ref ddlInversionista, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Inversionista --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Sucursal");
                }
            }
        }

        private bool GuardarInversionista()
        {
            try
            {

                if (!ValidarFormulario()) { return false; }

                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        ObjInfo.TasaEndoso = decimal.Parse(txtTasaEndoso.Text);
                        ObjInfo.Inversionista_Id = int.Parse(ddlInversionista.SelectedValue);
                        ObjInfo.FechaEnvioAntecedentes = DateTime.Now;
                        ObjInfo.EstadoEndoso_Id = 1; //Enviado

                        var objResultado = new Resultado<SolicitudInfo>();
                        var objNegocio = new NegSolicitudes();

                        objResultado = objNegocio.Guardar(ref ObjInfo);
                        if (objResultado.ResultadoGeneral)
                        {
                            return true;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(objResultado.Mensaje);
                            return false;
                        }
                    }
                    else
                    {
                        Controles.MostrarMensajeError("Problemas al Cargar la Solicitud");
                        return false;

                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Inversionista");
                }
                return false;
            }
        }

        private bool ValidarFormulario()
        {
            if (ddlInversionista.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlInversionista.ClientID);
                return false;
            }
            if (txtTasaEndoso.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtTasaEndoso.ClientID);
                return false;
            }

            return true;
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}