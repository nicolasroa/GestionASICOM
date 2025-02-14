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

namespace WorkFlow.Eventos
{
    public partial class ConfirmaAprobacionComercial : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {

                CargarAcciones();
                CargarSolicitudInversionistas();
                CargarMotivoDesistimiento();
                CargarDocumentos();
                Session["ArchivoChecklistOriginacionComercial"] = null;
                Session["ArchivoAprobacion"] = null;

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

        protected void btnGeneraCertificadoAprobacion_Click(object sender, EventArgs e)
        {
            GenerarReportePDF();
        }
        protected void btnGenerarChecklistOriginacionComercial_Click(object sender, EventArgs e)
        {
            GenerarChecklistOriginacionComercialPDF();
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
                    if (!ValidarDocumentosOriginacionComercial()) return;
                    string resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id, 1, 85, "ConstanciaAprobacion_Sol_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["ArchivoAprobacion"]);
                    if (resultado != "")
                    {
                        Controles.MostrarMensajeAlerta(resultado);
                        return;
                    }
                    resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id, 1, 85, "ChecklistOriginacionComercial_Solicitud_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["ArchivoChecklistOriginacionComercial"]);
                    if (resultado != "")
                    {
                        Controles.MostrarMensajeAlerta(resultado);
                        return;
                    }
                  
                    if (!DatosSolicitud.GrabarSolicitud(true)) return;
                    if (!ActualizaSolicitud()) return;
                    //if (!EnviarAprobacion()) return;
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

        public bool ValidarDocumentosOriginacionComercial()
        {
            try
            {
                if (NegDocumentacion.lstRegistroDocumentosOriginacionComercial.Count(d => d.Estado_Id <= 0) > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Estado para todos los Documentos");
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
                    Controles.MostrarMensajeError("Error al Validar Documentos de Originación");
                }
                return false;
            }
        }
        private bool ActualizaSolicitud(int Estado_Id = -1)
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
                    return false;
                }


                if (Estado_Id == -1)
                    NegSolicitudes.objSolicitudInfo.SubEstado_Id = (int)NegTablas.IdentificadorMaestro("SUBEST_SOLICITUD", "AP");
                else
                    NegSolicitudes.objSolicitudInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_SOLICITUD", "EC");

                decimal DividendoRenta = decimal.Zero;
                ResumenResolucion.CargarRenta(ref DividendoRenta);

                NegSolicitudes.objSolicitudInfo.DividendoRenta = DividendoRenta;



                oSolicitud = NegSolicitudes.objSolicitudInfo;


                rSolicitud = nSolicitud.Guardar(ref oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = oSolicitud;
                    return true;
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
                    Controles.MostrarMensajeError("Error al Actualizar la Solicitud");
                }
            }
            return false;
        }
        private void GenerarReportePDF()
        {
            try
            {
                NegSimulacionHipotecaria.oReporteSimulacion = new ReporteSimulacion();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();
                ClientesInfo oCliente = new ClientesInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;


                string urlDocumento = "";
                Session["Aprobacion"] = true;
                urlDocumento = "~/Reportes/PDF/ConstanciaAprobacion.aspx";


                string ContenidoHtml = "";
                string Archivo = "ConstanciaAprobacion_Sol_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Constancia de Aprobación";

                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        NegSimulacionHipotecaria.oReporteSimulacion.oSolicitud = new SolicitudInfo();
                        NegSimulacionHipotecaria.oReporteSimulacion.oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                    }
                }

                oCliente.Rut = NegParticipante.lstParticipantes.FirstOrDefault(s => s.Solicitud_Id == oSolicitud.Id && s.TipoParticipacion_Id == 1).Rut;
                NegClientes oNegCliente = new NegClientes();
                Resultado<ClientesInfo> oResultado = new Resultado<ClientesInfo>();
                oResultado = oNegCliente.Buscar(oCliente);
                if (oResultado.ResultadoGeneral && oResultado.ValorDecimal != 0)
                {
                    NegSimulacionHipotecaria.oReporteSimulacion.oCliente = new ClientesInfo();
                    NegSimulacionHipotecaria.oReporteSimulacion.oCliente = NegClientes.objClienteInfo;
                }

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);

                Session["ArchivoAprobacion"] = Binarios;

                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                Response.BinaryWrite(Binarios);
                Response.End();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el Reporte PDF");
                }
            }
        }

        private void GenerarChecklistOriginacionComercialPDF()
        {
            try
            {

                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                string urlDocumento = "~/Reportes/PDF/ChecklistOriginacionComercial.aspx";
                string ContenidoHtml = "";
                string Archivo = "ChecklistOriginacionComercial_Solicitud_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Checklist Originacion Comercial";

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = ContenidoHtml + htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
                Session["ArchivoChecklistOriginacionComercial"] = Binarios;
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                Response.BinaryWrite(Binarios);
                Response.End();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el Reporte PDF");
                }
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

        #region SolicitudInversionistas
        public void CargarSolicitudInversionistas()
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                oInversionista.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oInversionista.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_REV_INVERSIONISTA", "A");// Aprobadas
                rInversionista = nInversionistas.BuscarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvSolicitudInversionistas, rInversionista.Lista, new string[] { "Inversionista_Id", "Solicitud_Id", "Asignado" });
                    foreach (GridViewRow Row in gvSolicitudInversionistas.Rows)
                    {
                        CheckBox chkAsignado = (CheckBox)Row.FindControl("chkAsignado");
                        chkAsignado.Checked = bool.Parse(gvSolicitudInversionistas.DataKeys[Row.RowIndex].Values["Asignado"].ToString());

                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rInversionista.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Inversinistas de la Solicitud");
                }
            }
        }


        protected void chkAsignado_CheckedChanged(object sender, EventArgs e)
        {


            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            int Inversionista_Id = int.Parse(gvSolicitudInversionistas.DataKeys[row.RowIndex].Values["Inversionista_Id"].ToString());

            GrabarSolicitudInversionistas(Inversionista_Id, ((CheckBox)sender).Checked);

        }
        public void GrabarSolicitudInversionistas(int Inversionista_Id, bool Asignado)
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                oInversionista = NegInversionistas.lstSolicitudesInversionista.FirstOrDefault(i => i.Solicitud_Id == NegSolicitudes.objSolicitudInfo.Id && i.Inversionista_Id == Inversionista_Id);

                oInversionista.Asignado = Asignado;
                rInversionista = nInversionistas.GrabarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Inversionista Actualizado");
                    CargarSolicitudInversionistas();
                }
                else
                {
                    Controles.MostrarMensajeError(rInversionista.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Inversinistas de la Solicitud");
                }
            }
        }
        public bool ValidaInversionistasAprobados()
        {

            var marcados = 0;

            foreach (GridViewRow Row in gvSolicitudInversionistas.Rows)
            {
                CheckBox chkAsignado = (CheckBox)Row.FindControl("chkAsignado");
                if (chkAsignado.Checked)
                    marcados = marcados + 1;
            }

            if (marcados < 1 || marcados > 2)
            {
                return false;
            }
            return true;
        }
        #endregion
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

                    Lista = NegTablas.BuscarCatalogo("EST_DOC_ORIGINACION_COM");//Lista los estados de los Documentos

                    foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                    {
                        RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");

                        Controles.CargarRadioButtonList(ref rblEstadoDocumento, Lista, "CodigoInterno", "Nombre");


                        oDocumentos = new RegistroDocumentosOriginacionComercialInfo();
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumentos = NegDocumentacion.lstRegistroDocumentosOriginacionComercial.FirstOrDefault(d => d.Documento_Id == Documento_Id);
                        rblEstadoDocumento.SelectedValue = oDocumentos.Estado_Id.ToString();
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
                    RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");
                    if (rblEstadoDocumento.SelectedItem != null)
                    {
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumentos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                        oDocumentos.Documento_Id = Documento_Id;

                        oDocumentos.Estado_Id = int.Parse(rblEstadoDocumento.SelectedItem.Value);

                        rDocumentos = nDocumentos.GuardarRegistroDocumentosOriginacionComercial(oDocumentos);


                        if (!rDocumentos.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rDocumentos.Mensaje);
                            return;
                        }
                    }
                }
                CargarDocumentos();
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