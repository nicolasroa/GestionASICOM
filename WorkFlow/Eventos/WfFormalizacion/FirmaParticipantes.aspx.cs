using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class FirmaParticipantes : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioDatepicker();");
            if (!Page.IsPostBack)
            {
                CargarAcciones();
                CargaData();
            }
        }
        private class MesEscritura
        {
            public string Id { get; set; }
            public string Descripcion { get; set; }
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
                    if (!ValidaInfo()) return;
                    if (!ValidacionEvento()) return;
                    if (!GrabaRegistroFirma()) return;

                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                    if (!ValidaInfo()) return;
                    if (!ValidacionEvento()) return;
                    if (!GrabaRegistroFirma()) return;
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

        public bool ValidacionEvento()
        {
            ParticipanteInfo oInfo = new ParticipanteInfo();
            oInfo = NegParticipante.lstParticipantes.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.FechaFirma == null);
            if (oInfo != null)
            {
                Controles.MostrarMensajeInfo("Debe Registrar la Fecha de Firma del Participante " + oInfo.NombreCliente + "(" + oInfo.DescripcionTipoParticipante + ")");
                return false;
            }

            return true;
        }

        public bool ValidaInfo()
        {
            try
            {
                if (txtFechaRepertorioNotarial.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fecha Repertorio Notarial");
                    return false;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.FechaRepertorioNotarial = DateTime.Parse(txtFechaRepertorioNotarial.Text);
                }
                if (txtNroRepertorio.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Nro de Repertorio");
                    return false;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.NroRepertorioNotarial = int.Parse(txtNroRepertorio.Text);
                }
                if (txtFolioFormulario.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Folio asociado a formulario.");
                    return false;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.FolioFormulario = int.Parse(txtFolioFormulario.Text);
                }
                if (ddlTasaImpuesto.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeInfo("Debe seleccionar Tasa Impuesto al mutuo.");
                    return false;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.TasaImpuestoAlMutuo_Id = int.Parse(ddlTasaImpuesto.SelectedValue);
                }
                if (txtMontoImpuesto.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar de Impuesto.");
                    return false;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.MontoImpuestoAlMutuo = decimal.Parse(txtMontoImpuesto.Text.Replace("$", "").Replace(".", ""));
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
                    Controles.MostrarMensajeError("Error al Validar Formulario");
                }
                return false;
            }
        }

        protected void btnGuardaFirmaVendedor_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((Anthem.ImageButton)sender).Parent.Parent as GridViewRow;
                var fechaFirma = ((Anthem.TextBox)row.Cells[3].FindControl("txtFechaFirmaVendedor")).Text;
                int Participante_Id = int.Parse(GvVendedor.DataKeys[row.RowIndex].Values["Id"].ToString());

                if (fechaFirma.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar Fecha de Firma Vendedor");
                    return;
                }

                NegParticipante.ObjParticipante = new ParticipanteInfo();
                NegParticipante.ObjParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Participante_Id);

                if (NegParticipante.ObjParticipante.FechaRegistroFirma != null)
                {
                    if (DateTime.Parse(NegParticipante.ObjParticipante.FechaRegistroFirma.Value.ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                    {
                        Controles.MostrarMensajeAlerta("Fecha de Firma Registrada con fecha " + NegParticipante.ObjParticipante.FechaRegistroFirma.Value.ToShortDateString() + ", para actualizarla se debe desactivar la Operación");
                        return;
                    }
                }
                NegParticipante.ObjParticipante.FechaFirma = Convert.ToDateTime(fechaFirma);
                GrabaFechaFirma();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al guardar Fecha Firma Vendedor");
                }
            }
        }

        protected void btnGuardaFirmaDeudor_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((Anthem.ImageButton)sender).Parent.Parent as GridViewRow;
                var fechaFirma = ((Anthem.TextBox)row.Cells[3].FindControl("txtFechaFirmaDeudor")).Text;
                int Participante_Id = int.Parse(GvDeudores.DataKeys[row.RowIndex].Values["Id"].ToString());

                DateTime oPrimerDiaDeFirma = new DateTime((int)NegSolicitudes.objSolicitudInfo.AñoEscritura,(int)NegSolicitudes.objSolicitudInfo.MesEscritura, 1);
                DateTime oUltimoDiaDeFirma = oPrimerDiaDeFirma.AddMonths(1).AddDays(-1);
                NegParticipante.ObjParticipante = new ParticipanteInfo();
                NegParticipante.ObjParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Participante_Id);
                if (fechaFirma.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar Fecha de Firma Deudor");
                    return;
                }
                if ((DateTime.Parse(fechaFirma) < oPrimerDiaDeFirma || DateTime.Parse(fechaFirma) > oUltimoDiaDeFirma) && NegParticipante.ObjParticipante.TipoParticipacion_Id == 1)//Solo para el deudor principal
                {
                    Controles.MostrarMensajeAlerta("La fecha de Firma no corresponde a la Fecha de Escrituración, esta debe ser estar entre los dias " + oPrimerDiaDeFirma.ToShortDateString() + " y " + oUltimoDiaDeFirma.ToShortDateString());
                    return;
                }


                if (NegParticipante.ObjParticipante.FechaRegistroFirma != null)
                {
                    if (DateTime.Parse(NegParticipante.ObjParticipante.FechaRegistroFirma.Value.ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                    {
                        Controles.MostrarMensajeAlerta("Fecha de Firma Registrada con fecha " + NegParticipante.ObjParticipante.FechaRegistroFirma.Value.ToShortDateString() + ", para actualizarla se debe desactivar la Operación");
                        return;
                    }
                }

                //validar repertorio cuando firme el deudor principal


                if (!ValidaInfo()) return;

                NegParticipante.ObjParticipante.FechaFirma = Convert.ToDateTime(fechaFirma);
                GrabaFechaFirma();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al guardar Fecha Firma Deudor");
                }
            }
        }
        public bool GrabaRegistroFirma()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<SolicitudInfo>();
                var oNegocio = new NegSolicitudes();

                oResultado = oNegocio.Guardar(NegSolicitudes.objSolicitudInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información Registrada");
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
                    Controles.MostrarMensajeError("Error al guardar información del registro de firmas para el Evento");
                }
                return false;
            }
        }

        public void GrabaFechaFirma()
        {
            try
            {

                var lst = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id != NegParticipante.ObjParticipante.Id);


                if (lst != null)
                {
                    var PrimeraFecha = NegParticipante.lstParticipantes.Where(p => p.Id != NegParticipante.ObjParticipante.Id).Min(p => p.FechaFirma);

                    if (PrimeraFecha != null)

                        if (PrimeraFecha > NegParticipante.ObjParticipante.FechaFirma)
                        {
                            Controles.MostrarMensajeAlerta("La Fecha Ingresada es Menor a la última Fecha Ingresada (" + string.Format("{0:d}", PrimeraFecha) + ")");
                            return;
                        }
                }


                //Declaracion de Variables
                var oResultado = new Resultado<ParticipanteInfo>();
                var oNegocio = new NegParticipante();

                NegParticipante.ObjParticipante.FechaRegistroFirma = DateTime.Now;
                oResultado = oNegocio.GuardarParticipante(NegParticipante.ObjParticipante);
                if (oResultado.ResultadoGeneral)
                {

                    if (!GrabaRegistroFirma()) return;

                    if (!ActivarOperacion(NegParticipante.ObjParticipante)) return;

                    Controles.MostrarMensajeExito("Información Registrada");
                    CargarGrillas();
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
                    Controles.MostrarMensajeError("Error al guardar fecha firma del participante.");
                }
            }
        }
        private bool ActivarOperacion(ParticipanteInfo oParticipante)
        {
            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();

                NegSolicitudes.objSolicitudInfo.Usuario_Id = NegUsuarios.Usuario.Rut;

                rSolicitud = nSolicitud.ActivarOperacion(NegSolicitudes.objSolicitudInfo);
                if (!rSolicitud.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
                }
                else
                {
                    var ResultadoActivacion = rSolicitud.Lista.FirstOrDefault();
                    if (ResultadoActivacion != null)
                    {
                        if (!ResultadoActivacion.ResultadoActivacion)
                        {

                            var oResultado = new Resultado<ParticipanteInfo>();
                            var oNegocio = new NegParticipante();

                            oParticipante.FechaRegistroFirma = null;
                            oParticipante.FechaFirma = null;

                            oResultado = oNegocio.GuardarParticipante(oParticipante);
                            if (!oResultado.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError(oResultado.Mensaje);
                                return false;
                            }


                            Controles.MostrarMensajeAlerta(ResultadoActivacion.ErrorActivacion);
                            return false;
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
                    Controles.MostrarMensajeError("Error al Activar la Operación");
                }
                return false;
            }
        }
        private void CargaData()
        {
            CargaComboTasaImpuesto();
            CargarGrillas();
            CargarFirmaParticipante();
        }

        public void CargarFirmaParticipante()
        {
            try
            {
                if (NegSolicitudes.objSolicitudInfo != null)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = NegSolicitudes.lstSolicitudInfo.FirstOrDefault(a => a.Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id));
                }
                DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                decimal TasaImpuesto = decimal.Zero;
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                decimal UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                if (NegSolicitudes.objSolicitudInfo.MesEscritura != 0)
                    txtMesEscritura.Text = dtinfo.GetMonthName((int)NegSolicitudes.objSolicitudInfo.MesEscritura).ToUpper() + " - " + NegSolicitudes.objSolicitudInfo.AñoEscritura.ToString();

                txtFechaRepertorioNotarial.Text = NegSolicitudes.objSolicitudInfo.FechaRepertorioNotarial == null ? "" : NegSolicitudes.objSolicitudInfo.FechaRepertorioNotarial.Value.ToShortDateString();
                txtNroRepertorio.Text = NegSolicitudes.objSolicitudInfo.NroRepertorioNotarial.ToString() == "-1" ? "" : NegSolicitudes.objSolicitudInfo.NroRepertorioNotarial.ToString();
                txtFolioFormulario.Text = NegSolicitudes.objSolicitudInfo.FolioFormulario.ToString() == "-1" ? "" : NegSolicitudes.objSolicitudInfo.FolioFormulario.ToString();
                ddlTasaImpuesto.SelectedValue = NegSolicitudes.objSolicitudInfo.TasaImpuestoAlMutuo_Id == 0 ? "-1" : NegSolicitudes.objSolicitudInfo.TasaImpuestoAlMutuo_Id.ToString();
                if (decimal.TryParse(ddlTasaImpuesto.SelectedItem.Text.Replace(".", ","), out TasaImpuesto))
                    txtMontoImpuesto.Text = string.Format("{0:C}", NegSolicitudes.objSolicitudInfo.MontoCredito * UF * TasaImpuesto);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Formulario de Registro de Firmas");
                }
            }
        }

        public void CargarGrillas()
        {
            try
            {
                NegParticipante oNegocio = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();
                ParticipanteInfo oInfo = new ParticipanteInfo();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oResultado = oNegocio.BuscarParticipante(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    //CARGA GRILLA TIPO PARTICIPANTE DEUDORES
                    Controles.CargarGrid(ref GvDeudores, NegParticipante.lstParticipantes.Where(pi => pi.TipoParticipacion_Id != 4).ToList(), new string[] { "Id" });

                    foreach (GridViewRow Row in GvDeudores.Rows)
                    {
                        TextBox txtFechaFirmaDeudor = (TextBox)Row.FindControl("txtFechaFirmaDeudor");
                        oInfo = new ParticipanteInfo();
                        int Participante_Id = int.Parse(GvDeudores.DataKeys[Row.RowIndex].Values["Id"].ToString());
                        oInfo = NegParticipante.lstParticipantes.FirstOrDefault(d => d.Id == Participante_Id);
                        if (oInfo.FechaFirma != null)
                        {
                            txtFechaFirmaDeudor.Text = oInfo.FechaFirma.Value.ToShortDateString();
                        }
                        else
                        {
                            txtFechaFirmaDeudor.Text = "";
                        }

                    }

                    //CARGA GRILLA TIPO PARTICIPANTE VENDEDOR
                    ParticipanteInfo oInfoVendedor = new ParticipanteInfo();

                    Controles.CargarGrid(ref GvVendedor, NegParticipante.lstParticipantes.Where(pi => pi.TipoParticipacion_Id == 4).ToList(), new string[] { "Id" });

                    foreach (GridViewRow Row in GvVendedor.Rows)
                    {
                        TextBox txtFechaFirmaVendedor = (TextBox)Row.FindControl("txtFechaFirmaVendedor");
                        oInfoVendedor = new ParticipanteInfo();
                        int Participante_Id = int.Parse(GvVendedor.DataKeys[Row.RowIndex].Values["Id"].ToString());
                        oInfoVendedor = NegParticipante.lstParticipantes.FirstOrDefault(d => d.Id == Participante_Id);
                        if (oInfoVendedor.FechaFirma != null)
                        {
                            txtFechaFirmaVendedor.Text = oInfoVendedor.FechaFirma.Value.ToShortDateString();
                        }
                        else
                        {
                            txtFechaFirmaVendedor.Text = "";
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
                    Controles.MostrarMensajeError("Error al Cargar los Participantes asociados al Evento");
                }
            }
        }

        private void CargaComboTasaImpuesto()
        {
            try
            {
                var Lista = new List<TablaInfo>();
                Lista = NegTablas.BuscarCatalogo("TASA_IMPUESTO_AL_MUTUO");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlTasaImpuesto, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tasa Impuesto --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Tablas Tasa Impuesto");
                }
            }
        }

        protected void ddlTasaImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

            decimal UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
            if (ddlTasaImpuesto.SelectedValue == "-1")
                txtMontoImpuesto.Text = "";
            else
                txtMontoImpuesto.Text = string.Format("{0:C}", NegSolicitudes.objSolicitudInfo.MontoCredito * UF * decimal.Parse(ddlTasaImpuesto.SelectedItem.Text.Replace(".", ",")));


        }


    }
}