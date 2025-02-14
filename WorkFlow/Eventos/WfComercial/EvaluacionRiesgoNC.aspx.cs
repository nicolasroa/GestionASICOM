using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;

namespace WorkFlow.Eventos
{
    public partial class EvaluacionRiesgoNC : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargarInversionistas();
                CargarSolicitudInversionistas();
                CargarMotivoRechazo();
                Session["ArchivoFicha"] = null;
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
                    Controles.MostrarMensajeError(Ex.Message,Ex);
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

                btnGenerarFicha.Visible = false;
                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));
                btnAccion.Attributes.Remove("OnMouseDown");
                btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);");

                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-forward'></span>Avanzar";
                    btnAccion.CssClass = "btn btn-sm btn-success";
                    btnAccion.Visible = true;
                    btnGenerarFicha.Visible = true;
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
                    Controles.MostrarMensajeError(Ex.Message,Ex);
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
                    if (!DatosSolicitud.ValidarDatosCredito()) return;
                    if (!GenerarFicha(false)) return;
                    string resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id, 1, 85, "FichaEvaluacionCrediticia_Sol" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["ArchivoFicha"]);
                    if (resultado != "")
                    {
                        Controles.MostrarMensajeAlerta(resultado);
                        return;
                    }
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
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
                    Controles.MostrarMensajeError(Ex.Message,Ex);
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
                    ResumenResolucion.ActualizarInformacion();
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
        }

        #endregion

        #region Inversionistas

        public void CargarInversionistas()
        {
            try
            {
                InversionistaInfo oInversionista = new InversionistaInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<InversionistaInfo> rInversionista = new Resultado<InversionistaInfo>();

                rInversionista = nInversionistas.Buscar(oInversionista);

               
                if (rInversionista.ResultadoGeneral)
                {
                    Controles.CargarCombo<InversionistaInfo>(ref ddlInversionistas, rInversionista.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Inversionistas--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar los Inversinistas");
                }
            }
        }
        public void CargarSolicitudInversionistas()
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                oInversionista.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rInversionista = nInversionistas.BuscarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvSolicitudInversionistas, rInversionista.Lista, new string[] { "Inversionista_Id", "Solicitud_Id" });
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
        public void GrabarSolicitudInversionistas()
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                
                if (ddlInversionistas.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inversionista");
                    return;
                }
                if (txtTasaEndoso.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar una Tasa de Endoso");
                    return;
                }
                oInversionista.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oInversionista.Inversionista_Id = int.Parse(ddlInversionistas.SelectedValue);
                oInversionista.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_REV_INVERSIONISTA", "EE");
                oInversionista.TasaEndoso = decimal.Parse(txtTasaEndoso.Text);
                rInversionista = nInversionistas.GrabarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                   Controles.MostrarMensajeExito("Inversionista Agregado");
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
        protected void btnAgregarInversionista_Click(object sender, EventArgs e)
        {
            GrabarSolicitudInversionistas();
        }
        protected void btnEliminarInversionista_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Solicitud_Id = int.Parse(gvSolicitudInversionistas.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
                int Inversionista_Id = int.Parse(gvSolicitudInversionistas.DataKeys[row.RowIndex].Values["Inversionista_Id"].ToString());
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();

                oInversionista.Solicitud_Id = Solicitud_Id;
                oInversionista.Inversionista_Id = Inversionista_Id;


                rInversionista = nInversionistas.EliminarSolicitudInversionistas(oInversionista);
                if (rInversionista.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
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
                    Controles.MostrarMensajeError("Error al Eliminar el Participante");
                }
            }
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

    }
}