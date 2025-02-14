using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos
{
    public partial class RecepcionAntecedentes : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarAcciones();
                CargarParticipantes();
                NegSolicitudes.IndComentario = false;
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
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de anular la Solicitud?',this);");
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
                    if (!ValidarEvento()) return;
                }
                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//AVANZAR
                {
                    if (!GrabarMotivoRechazo()) return;
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
        protected void btnGrabarMotivoRechazo_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }
        private bool GrabarMotivoRechazo()
        {
            try
            {
                if (txtMotivoRechazo.Text.Equals(""))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Motivo para la Anulación");
                    return false;
                }

                var oInfo = new ObservacionInfo();

                if (NegObservaciones.objObservacionInfo != null)
                {
                    oInfo = NegObservaciones.objObservacionInfo;
                }
                oInfo.UsuarioIngreso_Id = NegUsuarios.Usuario.Rut.Value;
                oInfo.Descripcion = txtMotivoRechazo.Text;
                oInfo.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oInfo.Evento_Id = NegBandejaEntrada.oBandejaEntrada == null ? -1 : NegBandejaEntrada.oBandejaEntrada.Evento_Id;
                oInfo.TipoObservacion_Id = int.Parse(NegTablas.IdentificadorMaestro("TIPOOBSERVACION", "INFO").ToString());
                oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTAOBSERVACION", "I"); //ESTADO INGRESADA



                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Guardar(ref oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    return true;
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
                return false;
            }
        }
        private bool ValidarEvento()
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosPersonalesInfo oDocumentos = new RegistroDocumentosPersonalesInfo();
                Resultado<RegistroDocumentosPersonalesInfo> rDocumentos = new Resultado<RegistroDocumentosPersonalesInfo>();

                oDocumentos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oDocumentos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");


                rDocumentos = nDocumentos.BuscarRegistroDocumentosPersonales(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    if (rDocumentos.Lista.Where(d => d.Obligatorio == true && d.Validado == false).Count() > 0)
                    {
                        var Participante = new ParticipanteInfo();
                        int IdParticipante = 0;
                        IdParticipante = rDocumentos.Lista.FirstOrDefault(d => d.Obligatorio == true && d.Validado == false).RutCliente;
                        Participante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Rut == IdParticipante);
                        Controles.MostrarMensajeAlerta("Falta validar el Documento: " + rDocumentos.Lista.FirstOrDefault(d => d.Obligatorio == true && d.Validado == false).DescripcionDocumento + " Del Participante " + Participante.NombreCliente);
                        return false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rDocumentos.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar el Evento");
                }
                return false;
            }
        }

        private void CargarParticipantes()
        {
            try
            {

                DatosParticipante.CargarParticipantes();
                if (NegParticipante.lstParticipantes.Count != 0)
                {
                    Controles.CargarGrid(ref gvParticipantesAntecedentes, NegParticipante.lstParticipantes, new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Participantes");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Participantes");
                }
            }
        }
        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvParticipantesAntecedentes.DataKeys[row.RowIndex].Values["Id"].ToString());
            ParticipanteInfo oParticipante = new ParticipanteInfo();
            oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Id);
            NegParticipante.ObjParticipante = new ParticipanteInfo();
            NegParticipante.ObjParticipante = oParticipante;
            CargarDocumentos(oParticipante);
        }
        private void CargarDocumentos(ParticipanteInfo oParticipante)
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosPersonalesInfo oDocumentos = new RegistroDocumentosPersonalesInfo();
                Resultado<RegistroDocumentosPersonalesInfo> rDocumentos = new Resultado<RegistroDocumentosPersonalesInfo>();

                oDocumentos.Solicitud_Id = oParticipante.Solicitud_Id;
                oDocumentos.RutCliente = oParticipante.Rut;
                oDocumentos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                lblParticipanteAntecedentes.Text = oParticipante.NombreCliente;
                rDocumentos = nDocumentos.BuscarRegistroDocumentosPersonalesSolicitados(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosRequeridos, rDocumentos.Lista, new string[] { "Documento_Id", "Solicitud_Id", "RutCliente" });
                    var Lista = new List<TablaInfo>();

                    Lista = NegTablas.BuscarCatalogo("EST_DOC_PERSONALES");//Lista los estados de los Documentos

                    foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                    {
                        RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");

                        Controles.CargarRadioButtonList(ref rblEstadoDocumento, Lista, "CodigoInterno", "Nombre");


                        oDocumentos = new RegistroDocumentosPersonalesInfo();
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumentos = NegDocumentacion.lstRegistroDocumentosPersonales.FirstOrDefault(d => d.Documento_Id == Documento_Id);
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
        protected void btnCancelarDocumentos_Click(object sender, EventArgs e)
        {
            LimpiarRegistroDocumentos();
        }
        private void ProcesarRegistroDocumentos()
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosPersonalesInfo oDocumentos = new RegistroDocumentosPersonalesInfo();
                Resultado<RegistroDocumentosPersonalesInfo> rDocumentos = new Resultado<RegistroDocumentosPersonalesInfo>();

                foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                {
                    RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");
                    int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                    oDocumentos.Solicitud_Id = NegParticipante.ObjParticipante.Solicitud_Id;
                    oDocumentos.RutCliente = NegParticipante.ObjParticipante.Rut;
                    oDocumentos.Documento_Id = Documento_Id;

                    oDocumentos.Estado_Id = int.Parse(rblEstadoDocumento.SelectedItem.Value);

                    rDocumentos = nDocumentos.GuardarRegistroDocumentosPersonales(oDocumentos);


                    if (!rDocumentos.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rDocumentos.Mensaje);
                        return;
                    }

                }
                Controles.MostrarMensajeExito("Documentos Registrados");
                LimpiarRegistroDocumentos();



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
        private void LimpiarRegistroDocumentos()
        {
            NegParticipante.ObjParticipante = new ParticipanteInfo();
            NegDocumentacion.lstRegistroDocumentosPersonales = new List<RegistroDocumentosPersonalesInfo>();
            lblParticipanteAntecedentes.Text = "";
            gvDocumentosRequeridos.DataSource = null;
            gvDocumentosRequeridos.DataBind();
        }
        private void GenerarReportePDF()
        {
            try
            {

                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                string urlDocumento = "~/Reportes/PDF/DocumentacionRequeridaEvaluacion.aspx";
                string ContenidoHtml = "";
                string Archivo = "DocRequerida_Solicitud_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Documentacion Requerida Evaluación";

                int cont = 1;

                foreach (ParticipanteInfo oInfo in NegParticipante.lstParticipantes)
                {
                    oInfo.flagPDF = 0;
                    if (cont == NegParticipante.lstParticipantes.Count)
                    {
                        oInfo.flagPDF = 1;
                    }

                    StringWriter htmlStringWriter = new StringWriter();
                    NegParticipante.ObjParticipantePDF = oInfo;
                    Server.Execute(urlDocumento, htmlStringWriter);
                    ContenidoHtml = ContenidoHtml + htmlStringWriter.GetStringBuilder().ToString();
                    htmlStringWriter.Close();

                    cont += 1;
                }

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
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

        protected void btnAntecedentes_Click(object sender, EventArgs e)
        {
            CargarParticipantes();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarReportePDF();
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}