using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.WfOriginacionValidacionAntecedentes
{
    public partial class RevisionCarpetaComercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {

                CargarAcciones();
                CargarMotivoDesistimiento();
                CargarDocumentos();


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
                btnAccion.Attributes.Remove("OnMouseDown");
                btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);");
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
                    btnAccion.Attributes.Remove("OnMouseDown");
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de Desistir la Solicitud?',this);");
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
                    if (!ValidarDocumentosOriginacionComercial(oAccion)) return;
                    if (!DatosSolicitud.GrabarSolicitud(true)) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "D"))//DEVOLVER
                {
                    if (!ValidarDocumentosOriginacionComercial()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!GrabarMotivoDesistimiento()) return;
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
        public bool ValidarDocumentosOriginacionComercial(AccionesInfo oAccion = null)
        {
            try
            {


                if (NegDocumentacion.lstRegistroDocumentosOriginacionComercial.Count(d => d.EstadoRevision_Id <= 0) > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Estado de Revisión para todos los Documentos");
                    return false;
                }
                if (oAccion != null)
                {
                    if (NegDocumentacion.lstRegistroDocumentosOriginacionComercial.Count(d => d.EstadoRevision_Id == 2 && oAccion.Descripcion == "Aprobado") > 0)
                    {
                        Controles.MostrarMensajeAlerta("Sólo es válida la Aprobación con Excepción ya que hay Documentos Pendientes");
                        return false;
                    }

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
                    Controles.MostrarMensajeError("Error al Validar Documentos de Originación");
                }
                return false;
            }
        }
        private bool EnviarAprobacion()
        {
            try
            {
                byte[] Binarios = null;
                string Archivo = "Carta de Aprobación.pdf";

                if (Session["ArchivoAprobacion"] == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Generar La Carta de Aprobación antes de Avanzar el Evento");
                    return false;
                }

                Binarios = (byte[])Session["ArchivoAprobacion"];
                Mail.MailInfo oMail = new Mail.MailInfo();
                Resultado<Mail.MailInfo> oResultado = new Resultado<Mail.MailInfo>();


                oMail.Asunto = "Notificación de Aprobación Crédito Hipotecario";
                string urlPlantillaMail = "~/Reportes/PlantillasMail/EnvioAdjunto.aspx";
                string ContenidMail = "";
                Mail.objInformacionAdjunto = new Mail.InformacionAdjunto();
                Mail.objInformacionAdjunto.NombreAdjunto = "El Certificado de Aprobación de su Crédito Hipotecario";
                Mail.objInformacionAdjunto.NombreCliente = NegClientes.objClienteInfo.NombreCompleto;


                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlPlantillaMail, htmlStringWriter);
                ContenidMail = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                oMail.Texto = ContenidMail;
                oMail.Adjuntos.Add(new Mail.AdjuntosMail() { Archivo = Binarios, NombreArchivo = Archivo });
                oMail.Destinatarios = NegParticipante.lstParticipantes.FirstOrDefault(t => t.TipoParticipacion_Id == 1).Mail;
                oResultado = Mail.EnviarMail(oMail, NegConfiguracionGeneral.Obtener());
                if (oResultado.ResultadoGeneral)
                {
                    Session["ArchivoAprobacion"] = null;
                    return true;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Enviar Aprobación");
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


        #region Rechazo
        private bool GrabarMotivoDesistimiento()
        {
            try
            {
                if (ddlMotivoDesistimiento.SelectedValue.Equals("-1"))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Motivo para el Desistimiento");
                    return false;
                }
                NegSolicitudes.objSolicitudInfo.MotivoDesistimiento_Id = int.Parse(ddlMotivoDesistimiento.SelectedValue);
                return NegSolicitudes.ActualizarSolicitud(NegSolicitudes.objSolicitudInfo);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
                return false;
            }
        }
        private void CargarMotivoDesistimiento()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("mot_desistido_sol");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMotivoDesistimiento, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Motivo --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Sexo");
                }
            }
        }
        protected void btnGrabarMotivoRechazo_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }

        #endregion


        #region Documentos
        private void CargarDocumentos()
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosOriginacionComercialInfo oDocumentos = new RegistroDocumentosOriginacionComercialInfo();
                Resultado<RegistroDocumentosOriginacionComercialInfo> rDocumentos = new Resultado<RegistroDocumentosOriginacionComercialInfo>();

                oDocumentos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                
                rDocumentos = nDocumentos.BuscarRegistroDocumentosOriginacionComercial(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosRequeridos, rDocumentos.Lista, new string[] { "Documento_Id", "Solicitud_Id" });
                    var Lista = new List<TablaInfo>();

                    Lista = NegTablas.BuscarCatalogo("EST_REV_DOC_ORIGINACION_COM");//Lista los estados de los Documentos

                    foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                    {
                        RadioButtonList rblEstadoRevisionDocumento = (RadioButtonList)Row.FindControl("rblEstadoRevisionDocumento");
                        TextBox txtObservacionDocumento = (TextBox)Row.FindControl("txtObservacionDocumento");
                        Controles.CargarRadioButtonList(ref rblEstadoRevisionDocumento, Lista, "CodigoInterno", "Nombre");


                        oDocumentos = new RegistroDocumentosOriginacionComercialInfo();
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumentos = NegDocumentacion.lstRegistroDocumentosOriginacionComercial.FirstOrDefault(d => d.Documento_Id == Documento_Id);
                        rblEstadoRevisionDocumento.SelectedValue = oDocumentos.EstadoRevision_Id.ToString();
                        txtObservacionDocumento.Text = oDocumentos.Observacion;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rDocumentos.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Documentos A Validar");
                }
            }
        }
        protected void btnGuardarDocumentos_Click(object sender, EventArgs e)
        {
            ProcesarRegistroDocumentos();
        }

        private void ProcesarRegistroDocumentos()
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosOriginacionComercialInfo oDocumentos = new RegistroDocumentosOriginacionComercialInfo();
                Resultado<RegistroDocumentosOriginacionComercialInfo> rDocumentos = new Resultado<RegistroDocumentosOriginacionComercialInfo>();

                foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                {
                    RadioButtonList rblEstadoRevisionDocumento = (RadioButtonList)Row.FindControl("rblEstadoRevisionDocumento");
                    TextBox txtObservacionDocumento = (TextBox)Row.FindControl("txtObservacionDocumento");
                    if (rblEstadoRevisionDocumento.SelectedItem != null)
                    {
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());

                        oDocumentos = NegDocumentacion.lstRegistroDocumentosOriginacionComercial.FirstOrDefault(d => d.Solicitud_Id == NegSolicitudes.objSolicitudInfo.Id && d.Documento_Id == Documento_Id);

                        oDocumentos.EstadoRevision_Id = int.Parse(rblEstadoRevisionDocumento.SelectedItem.Value);
                        oDocumentos.Observacion = txtObservacionDocumento.Text;
                        rDocumentos = nDocumentos.GuardarRegistroDocumentosOriginacionComercial(oDocumentos);


                        if (!rDocumentos.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rDocumentos.Mensaje);
                            return;
                        }
                    }
                }
                Controles.MostrarMensajeExito("Documentos Registrados");

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Procesar los Documentos");
                }
            }
        }

        #endregion
    }
}