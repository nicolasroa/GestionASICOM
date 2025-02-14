using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;

namespace WorkFlow.Eventos
{
    public partial class PreAprobacionComercial : System.Web.UI.Page
    {

        #region METODOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargaClientes();
                CargarMotivoRechazo();
               
                Session["ArchivoPreaprobacion"] = null;

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

        protected void btnGeneraCertificadoPreAprobacion_Click(object sender, EventArgs e)
        {
            GenerarReportePDF();
        }

        #endregion

        #region METODOS
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

              
                btnGeneraCertificadoPreAprobacion.Visible = false;
                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));
                btnAccion.Attributes.Remove("OnMouseDown");
                btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);");

                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-forward'></span>Avanzar";
                    btnAccion.CssClass = "btn btn-sm btn-success";
                    btnAccion.Visible = true;
                   
                    btnGeneraCertificadoPreAprobacion.Visible = true;
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
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de Rechazar la Solicitud?',this);");
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
                    

                    if (Session["ArchivoPreaprobacion"] != null)
                    {
                        string resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id, 1, 85, "ConstanciaPreAprobacion_Sol_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["ArchivoPreaprobacion"]);
                        if (resultado != "")
                        {
                            Controles.MostrarMensajeAlerta(resultado);
                            return;
                        }
                    }

                }
                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//Finalizar
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
        private bool ValidarChekList()
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
                urlDocumento = "~/Reportes/PDF/ConstanciaPreAprobacion.aspx";

                string ContenidoHtml = "";
                string Archivo = "ConstanciaPreAprobacion_Sol_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Constancia de Pre-Aprobación";

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

                NegSimulacionHipotecaria.oReporteSimulacion.VigenciaDoc = ddlVigenciaDoc.SelectedValue;

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
                Session["ArchivoPreaprobacion"] = Binarios;
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
        private bool GenerarFicha(bool Descargar = true)
        {
            try
            {
                string urlDocumento = "";
                urlDocumento = "~/Reportes/PDF/FichaAprobacion.aspx";

                string ContenidoHtml = "";
                string Archivo = "FichaEvaluacionCrediticia_Sol" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Ficha de Evaluación Crediticia";

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
                if (Descargar)
                {
                    Response.AddHeader("Content-Type", "application/pdf");
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                    Response.BinaryWrite(Binarios);
                    Response.Flush();
                }
                Session["ArchivoFicha"] = Binarios;


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
                    Controles.MostrarMensajeError("Error al Generar Ficha");
                }
                return false;
            }
        }




        #endregion

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow
            {
                Rut = -1
            };
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        #region INGRESO DEUDA
        private void CargaClientes()
        {
            try
            {
                DatosParticipante.CargarParticipantes();
                if (NegParticipante.lstParticipantes.Count != 0)
                {
                    NegActivosCliente.lstActivosClienteInfo = new List<ActivosClienteInfo>();
                    NegPasivosCliente.lstPasivosClienteInfo = new List<PasivosClienteInfo>();
                    NegParticipante.ObjParticipante = new ParticipanteInfo();
                    Controles.CargarGrid(ref EstadoSituacion.AgdvParticipante, NegParticipante.lstParticipantes.Where(p => p.PorcentajeDeuda > 0).ToList<ParticipanteInfo>(), new string[] { "Id" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Clientes");
                }
            }
        }
        protected void btnIngresoDeuda_Click(object sender, EventArgs e)
        {
            CargaClientes();
            ResumenResolucion.ActualizarInformacion();
        }

        #endregion

        #region Rechazo
        private bool GrabarMotivoRechazo()
        {
            try
            {
                if (ddlMotivoRechazo.SelectedValue.Equals("-1"))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Motivo para el Rechazo");
                    return false;
                }
                NegSolicitudes.objSolicitudInfo.MotivoRechazo_Id = int.Parse(ddlMotivoRechazo.SelectedValue);
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
        private void CargarMotivoRechazo()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("mot_rechazo_sol");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMotivoRechazo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Motivo --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sexo Sin Datos");
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

        protected void btnGenerarFicha_Click(object sender, EventArgs e)
        {
            GenerarFicha();
        }
    }
}