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
    public partial class SolicitudPreAprobacion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarAcciones();
                CargarInfoSolicitud();

                CargarGastosOperacionales();
                CargarParticipantes();
                NegSolicitudes.IndComentario = false;
                Session["ArchivoChekListInicial"] = null;
            }
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
        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        public void btnAccion_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
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

                    if (!ValidarChekList()) return;
                    if (!DatosSolicitud.GrabarSolicitud(true)) return;
                    if (!ValidarParticipantes()) return;
                    if (!ValidarPropiedades()) return;
                    if (!ValidarGastosOperacionales()) return;

                    if (!NegSolicitudes.RecalcularDividendo()) return;

                    if (!GenerarReportePDF(false)) return;
                    string resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id, 1, 85, "DocRequerida_Solicitud_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["ArchivoChekListInicial"]);
                    if (resultado != "")
                    {
                        Controles.MostrarMensajeAlerta(resultado);
                        return;
                    }

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
                oInfo.Descripcion = "[Anulación de Solicitud] Motivo: " + txtMotivoRechazo.Text;
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

        private bool ValidarParticipantes()
        {
            try
            {

                if (NegParticipante.lstParticipantes == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar los participantes de la Solicitud");
                    return false;
                }

                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 1).Count() == 0)
                {
                    Controles.MostrarMensajeAlerta("Solicitud son Participante Principal Ingresado");
                    return false;
                }

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);

                if (oParticipante.SeguroDesgravamen_Id == -1 && oParticipante.PorcentajeDesgravamen >= 0 && oParticipante.TipoPersona_Id == 1)
                {
                    Controles.MostrarMensajeAlerta("El Participante Principal debe Tener Seguro de Desgravamen");
                    return false;
                }
                AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                NegParticipante nLaborales = new NegParticipante();
                Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                foreach (var participante in NegParticipante.lstParticipantes)
                {
                    rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                    if (participante.TipoPersona_Id == 1 && participante.PorcentajeDeuda > 0 && (participante.TipoParticipacion_Id == 1 || participante.TipoParticipacion_Id == 2)) //Validación Solo para el Deudor Principal y Codeudor
                    {
                        oFiltro.RutCliente = participante.Rut;
                        rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                        if (rLaborales.ResultadoGeneral)
                        {
                            if (rLaborales.Lista.Count == 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar los Antecedentes Laborales del Participante " + participante.NombreCliente);
                                return false;
                            }
                        }
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
                    Controles.MostrarMensajeError("Error al Validar los Participantes");
                }
                return false;
            }
        }
        private bool ValidarPropiedades()
        {
            try
            {
                if (NegPropiedades.lstTasaciones == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
                    return false;
                }

                if (NegPropiedades.lstTasaciones.Count == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
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
                    Controles.MostrarMensajeError("Error al Validar la Propiedad");
                }
            }
            return false;
        }


        //Gastos Operacionales
        private void CargarInfoSolicitud()
        {
            try
            {
                txtRutSolicitante.Text = NegSolicitudes.objSolicitudInfo.RutCliente;
                txtNombreSolicitante.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
                txtDestino.Text = NegSolicitudes.objSolicitudInfo.DescripcionDestino;
                chkIndDfl2.Checked = NegSolicitudes.objSolicitudInfo.IndDfl2;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Información de la Solicitud");
                }
            }
        }
        private void CargarGastosOperacionales()
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();


                ComoPagaGGOOInfo oComoPaga = new ComoPagaGGOOInfo();
                QuienPagaGGOOInfo oQuienPaga = new QuienPagaGGOOInfo();
                Resultado<ComoPagaGGOOInfo> rComoPaga = new Resultado<ComoPagaGGOOInfo>();
                Resultado<QuienPagaGGOOInfo> rQuienPaga = new Resultado<QuienPagaGGOOInfo>();

                List<ComoPagaGGOOInfo> lstComoPaga = new List<ComoPagaGGOOInfo>();
                List<QuienPagaGGOOInfo> lstQuienPaga = new List<QuienPagaGGOOInfo>();





                oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                rGastos = nGastos.BuscarGastos(oGastos);
                if (rGastos.ResultadoGeneral)
                {

                    rComoPaga = nGastos.BuscarComoPaga(oComoPaga);
                    if (rComoPaga.ResultadoGeneral)
                    {
                        lstComoPaga = rComoPaga.Lista;
                    }
                    rQuienPaga = nGastos.BuscarQuienPaga(oQuienPaga);
                    if (rQuienPaga.ResultadoGeneral)
                    {
                        lstQuienPaga = rQuienPaga.Lista;
                    }





                    if (rGastos.Lista.Count > 0)
                    {
                        Controles.CargarGrid<GastoOperacionalInfo>(ref gvGastosOperacionales, rGastos.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor" });
                    }
                    else
                    {
                        ////Declaracion de Variables
                        var ObjInfo = new GastoOperacionalInfo();
                        var ObjResultado = new Resultado<GastoOperacionalInfo>();
                        var objNegocio = new NegGastosOperacionales();


                        ObjInfo.ValorPropiedad = NegSolicitudes.objSolicitudInfo.MontoPropiedad;
                        ObjInfo.MontoCredito = NegSolicitudes.objSolicitudInfo.MontoCredito;
                        ObjInfo.IndDfl2 = NegSolicitudes.objSolicitudInfo.IndDfl2;
                        ObjInfo.ViviendaSocial = NegSolicitudes.objSolicitudInfo.IndViviendaSocial;
                        ObjInfo.IndSimulacion = false;
                        ObjInfo.Destino_Id = NegSolicitudes.objSolicitudInfo.Destino_Id;

                        ////Asignacion de Variables
                        ObjResultado = objNegocio.CalcularGastosSimulacion(ObjInfo);
                        if (ObjResultado.ResultadoGeneral)
                        {
                            Controles.CargarGrid(ref gvGastosOperacionales, ObjResultado.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor" });
                            NegGastosOperacionales.lstGastosOperacionales = new List<GastoOperacionalInfo>();
                            NegGastosOperacionales.lstGastosOperacionales = ObjResultado.Lista;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ObjResultado.Mensaje);
                        }


                        GrabarGastosOperacionales(true);

                    }
                    if (gvGastosOperacionales.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvGastosOperacionales.Rows)
                        {

                            Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                            Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                            Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");


                            Controles.CargarCombo<QuienPagaGGOOInfo>(ref ddlQuienPaga, lstQuienPaga, "Id", "Descripcion", "-- Quién Paga? --", "-1");
                            Controles.CargarCombo<ComoPagaGGOOInfo>(ref ddlComoPaga, lstComoPaga, "Id", "Descripcion", "-- Cómo Paga? --", "-1");

                            ddlQuienPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["QuienPaga_Id"].ToString();
                            ddlComoPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["ComoPaga_Id"].ToString();

                            txtValorUF.Attributes.Remove("onKeyPress");
                            int Moneda_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Moneda_Id"].ToString());
                            if (Moneda_Id == 998)
                            {

                                txtValorUF.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,4,this);");
                                txtValorUF.Text = string.Format("{0:F4}", decimal.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Valor"].ToString()));
                            }
                            else
                            {
                                txtValorUF.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,0,this);");
                                txtValorUF.Text = string.Format("{0:F0}", decimal.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Valor"].ToString()));
                            }

                        }
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
                    Controles.MostrarMensajeError("Error al Cargar los Gastos Operacionales");
                }
            }
        }
        private void GrabarGastosOperacionales(bool CargaInicial = false, bool ConValor0 = false, GridViewRow row = null)
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();

                if (row != null)
                {
                    Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                    Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                    Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");



                    if (ddlQuienPaga.SelectedValue == "-1" && CargaInicial == false)
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar Quien Paga");
                        return;
                    }
                    if (ddlComoPaga.SelectedValue == "-1" && CargaInicial == false)
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar Como Paga");
                        return;
                    }

                    oGastos = NegGastosOperacionales.lstGastosOperacionales.FirstOrDefault(go => go.TipoGastoOperacional_Id == int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString()));


                    if (txtValorUF.Text == "")
                    {
                        txtValorUF.Text = "0";
                    }
                    oGastos.TipoGastoOperacional_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString());
                    oGastos.QuienPaga_Id = int.Parse(ddlQuienPaga.SelectedValue);
                    oGastos.ComoPaga_Id = int.Parse(ddlComoPaga.SelectedValue);
                    oGastos.Valor = decimal.Parse(txtValorUF.Text);
                    oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    oGastos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADO_GASTOS_OPERACIONALES", "I");

                    rGastos = nGastos.GuardarGasto(oGastos);
                    if (!rGastos.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rGastos.Mensaje);
                        return;
                    }
                    else
                    {
                        if (CargaInicial == false)
                            Controles.MostrarMensajeExito("Gastos Operacionales Actualizados Correctamente");

                        if (CargaInicial == false)
                            CargarGastosOperacionales();
                    }
                }
                else
                {


                    foreach (var gasto in NegGastosOperacionales.lstGastosOperacionales)
                    {
                        oGastos.TipoGastoOperacional_Id = gasto.TipoGastoOperacional_Id;
                        oGastos.Valor = gasto.Valor;
                        oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                        oGastos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADO_GASTOS_OPERACIONALES", "I");

                        rGastos = nGastos.GuardarGasto(oGastos);
                        if (!rGastos.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rGastos.Mensaje);
                            return;
                        }
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
                    Controles.MostrarMensajeError("Error al Grabar Gasto Operacional");
                }
            }
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            GrabarGastosOperacionales(row: row);
        }
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GrabarGastosOperacionales();
        }
        private bool ValidarGastosOperacionales()
        {
            try
            {

                if (NegGastosOperacionales.lstGastosOperacionales.Count(go => go.QuienPaga_Id == 0) > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Indicar quien Provisiona para todos los Gastos Operacionales");
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
                    Controles.MostrarMensajeError("Error al Validar Gasto Operacional");
                }
                return false;
            }
        }
        private bool GenerarReportePDF(bool Generar = true)
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
                Session["ArchivoChekListInicial"] = Binarios;
                if (Generar == true)
                {
                    Response.AddHeader("Content-Type", "application/pdf");
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                    Response.BinaryWrite(Binarios);
                    Response.End();
                }
                return true;
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
                return false;
            }
        }

        //CheckList
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
                oDocumentos.TipoPersona_Id = oParticipante.TipoPersona_Id;
                oDocumentos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                lblParticipanteAntecedentes.Text = oParticipante.NombreCliente;
                rDocumentos = nDocumentos.BuscarRegistroDocumentosPersonales(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosRequeridos, rDocumentos.Lista, new string[] { "Documento_Id", "Solicitud_Id", "RutCliente" });


                    foreach (GridViewRow Row in gvDocumentosRequeridos.Rows)
                    {
                        CheckBox chkValidado = (CheckBox)Row.FindControl("chkDocumentoValidado");
                        oDocumentos = new RegistroDocumentosPersonalesInfo();
                        int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumentos = NegDocumentacion.lstRegistroDocumentosPersonales.FirstOrDefault(d => d.Documento_Id == Documento_Id && d.RutCliente == oParticipante.Rut);
                        if (oDocumentos != null)
                            chkValidado.Checked = oDocumentos.Validado;
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
                    CheckBox chkValidado = (CheckBox)Row.FindControl("chkDocumentoValidado");
                    int Documento_Id = int.Parse(gvDocumentosRequeridos.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                    oDocumentos.Solicitud_Id = NegParticipante.ObjParticipante.Solicitud_Id;
                    oDocumentos.RutCliente = NegParticipante.ObjParticipante.Rut;
                    oDocumentos.Documento_Id = Documento_Id;
                    oDocumentos.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_DOC_PERSONALES", "P");//Solicitado

                    if (chkValidado.Checked)
                        rDocumentos = nDocumentos.GuardarRegistroDocumentosPersonales(oDocumentos);
                    else
                        rDocumentos = nDocumentos.EliminarRegistroDocumentosPersonales(oDocumentos);

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
        protected void btnAntecedentes_Click(object sender, EventArgs e)
        {
            CargarParticipantes();
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



        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarReportePDF();
        }


    }
}