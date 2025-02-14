using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosRegistroAntecedentesSalud : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioAntecedentesSalud();");
            if (!Page.IsPostBack)
            {
                //CargaSegDesgravamen();
                CargarEstadosDps();

            }
        }
       
        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Rut = int.Parse(gvParticipantesAntecedentes.DataKeys[row.RowIndex].Values["Rut"].ToString());
            CargarInfoParticipante(Rut);
            CargarAntecedentes(Rut);
        }
        private void CargarEstadosDps()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("EST_DPS");
                if (Lista != null)
                {

                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoDps, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "", "");
                    if (NegRegistroAntecedentesSalud.IndEnvio)
                    {
                        lblEstadoDps.Visible = false;
                        ddlEstadoDps.Visible = false;
                    }
                    else
                    {
                        lblEstadoDps.Visible = true;
                        ddlEstadoDps.Visible = true;
                    }
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Estado DPS Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        protected void ddlSeguroDesgravamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroDesgravamen();
        }
        public void CargarParticipantes(bool Reparo = false)
        {
            try
            {
                Anthem.Manager.Register(Page);
                //Declaración de Variables de Búsqueda
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                NegParticipante NegParticipantes = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    if (NegRegistroAntecedentesSalud.IndEnvio)
                        lblFechaEnvioDPS.Text = "Fecha de Envio DPS:";
                    else
                        lblFechaEnvioDPS.Text = "Fecha de Resolución DPS:";
                    if (Reparo)
                        txtFechaEnvioDPS.Enabled = false;
                    else
                        txtFechaEnvioDPS.Enabled = true;

                    Controles.CargarGrid(ref gvParticipantesAntecedentes, oResultado.Lista.Where(p => p.PorcentajeDesgravamen > 0).ToList(), new string[] { Constantes.StringId, "Rut" });

                    if (oResultado.Lista.Where(p => p.PorcentajeDesgravamen > 0).ToList().Count == 1)
                    {
                        int Rut = oResultado.Lista.Where(p => p.PorcentajeDesgravamen > 0).ToList().FirstOrDefault().Rut;
                        CargarInfoParticipante(Rut);
                        CargarAntecedentes(Rut);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Participantes");
                }
            }
        }
        private void CargarInfoParticipante(int Participante_Id)
        {
            try
            {

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Rut == Participante_Id);
                if (oParticipante != null)
                {
                    CargaSegDesgravamen();
                    NegParticipante.ObjParticipante = new ParticipanteInfo();
                    NegParticipante.ObjParticipante = oParticipante;
                    txtRut.Text = oParticipante.RutCompleto;
                    txtNombreParticipante.Text = oParticipante.NombreCliente;
                    txtParticipacion.Text = oParticipante.DescripcionTipoParticipante;
                    txtEdadPlazo.Text = oParticipante.EdadPlazo.ToString();
                    txtPorcentajeDesgravamen.Text = oParticipante.PorcentajeDesgravamen.ToString();
                    txtPorcentajeDeuda.Text = oParticipante.PorcentajeDeuda.ToString();
                    ddlSeguroDesgravamen.SelectedValue = oParticipante.SeguroDesgravamen_Id.ToString();
                    SeleccionarSeguroDesgravamen();
                    if (NegRegistroAntecedentesSalud.IndEnvio)
                        txtFechaEnvioDPS.Text = oParticipante.FechaIngresoDPS == null ? "" : oParticipante.FechaIngresoDPS.Value.ToShortDateString();
                    else
                        txtFechaEnvioDPS.Text = oParticipante.FechaAprobacionDPS == null ? "" : oParticipante.FechaAprobacionDPS.Value.ToShortDateString();
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
                    Controles.MostrarMensajeError("Error al Cargar la Información del Participante");
                }
            }
        }
        private void CargaSegDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 2;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroDesgravamen, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Desgravamen--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Desgravamen");
                }
            }
        }
        private void SeleccionarSeguroDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroDesgravamen.SelectedValue == "-1")
                {
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";
                    txtCompañia.Text = "";
                    txtPlazo.Text = "";
                    txtMontoAsegurado.Text = "0";
                }
                else
                {
                    decimal PorcentajeSeguro = decimal.Zero;

                    if (!decimal.TryParse(txtPorcentajeDesgravamen.Text, out PorcentajeSeguro))
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                        ddlSeguroDesgravamen.ClearSelection();
                        return;
                    }


                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoCredito * PorcentajeSeguro) / 100;
                        txtPrimaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
                        txtCompañia.Text = ObjInfo.DescripcionCompañia;
                        txtPlazo.Text = NegSolicitudes.objSolicitudInfo.Plazo.ToString();
                        txtMontoAsegurado.Text = (NegSolicitudes.objSolicitudInfo.MontoCredito * PorcentajeSeguro / 100).ToString();
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Seleccionar el Seguro de Desgravamen");
                }
            }
        }
        private bool ValidarIngresoAntecedentes()
        {
            try
            {
                if (NegParticipante.ObjParticipante == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return false;
                }
                if (txtFechaEnvioDPS.Text.Length == 0)
                {
                    if (NegRegistroAntecedentesSalud.IndEnvio)
                        Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Envío DPS");
                    else
                        Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Aprobación DPS");
                    return false;
                }

                var AntecedentesSeleccionados = 0;
                var ObservacionesIngresadas = 0;
                foreach (GridViewRow Row in gvAntecedentesSalud.Rows)
                {
                    CheckBox chkAntecedenteSeleccionado = (CheckBox)Row.FindControl("chkAntecedenteSeleccionado");
                    TextBox txtObservacionAntecedente = (TextBox)Row.FindControl("txtObservacionAntecedente");
                    if (chkAntecedenteSeleccionado.Checked)
                        AntecedentesSeleccionados++;

                    if (txtObservacionAntecedente.Text != "")
                        ObservacionesIngresadas++;

                }
                if (AntecedentesSeleccionados == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar al menos un Antecedente de Salud para el participante " + NegParticipante.ObjParticipante.NombreCliente);
                    return false;
                }
                if (ObservacionesIngresadas == 0)
                {
                    Controles.MostrarMensajeAlerta("Todos los Antecendetes Seleccionados del Participante" + NegParticipante.ObjParticipante.NombreCliente + " deben tener una Observación");
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
                    Controles.MostrarMensajeError("Error al Validar los Antecedentes de Salud");
                }
                return false;
            }
        }
        private void CargarAntecedentes(int Participante_Id)
        {
            try
            {
                RegistroAntecedentesSaludInfo oAntecedentes = new RegistroAntecedentesSaludInfo();
                Resultado<RegistroAntecedentesSaludInfo> rAntecedentes = new Resultado<RegistroAntecedentesSaludInfo>();
                NegRegistroAntecedentesSalud nAntecedentes = new NegRegistroAntecedentesSalud();


                oAntecedentes.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oAntecedentes.Participante_Id = Participante_Id;
                oAntecedentes.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");


                rAntecedentes = nAntecedentes.BuscarRegistroAntecedentesSalud(oAntecedentes);
                if (rAntecedentes.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvAntecedentesSalud, rAntecedentes.Lista, new string[] { Constantes.StringId, "Solicitud_Id", "Antecedente_Id", "Participante_Id" });


                    foreach (GridViewRow Row in gvAntecedentesSalud.Rows)
                    {
                        CheckBox chkAntecedenteSeleccionado = (CheckBox)Row.FindControl("chkAntecedenteSeleccionado");
                        TextBox txtObservacionAntecedente = (TextBox)Row.FindControl("txtObservacionAntecedente");
                        oAntecedentes = new RegistroAntecedentesSaludInfo();
                        int Antecedente_Id = int.Parse(gvAntecedentesSalud.DataKeys[Row.RowIndex].Values["Antecedente_Id"].ToString());
                        oAntecedentes = NegRegistroAntecedentesSalud.lstRegistroAntecedentesSalud.FirstOrDefault(d => d.Antecedente_Id == Antecedente_Id);
                        chkAntecedenteSeleccionado.Checked = oAntecedentes.Seleccionado;
                        if (oAntecedentes.Seleccionado)
                        {
                            txtObservacionAntecedente.Enabled = true;
                            txtObservacionAntecedente.Text = oAntecedentes.Observacion;
                        }
                        else
                        {
                            txtObservacionAntecedente.Enabled = false;
                            txtObservacionAntecedente.Text = "";
                        }

                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rAntecedentes.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Antecedentes de Salud");
                }
            }


        }
        private bool ProcesarRegistroAntecedentes()
        {
            try
            {
                if (!ValidarIngresoAntecedentes()) return false;

                RegistroAntecedentesSaludInfo oAntecedentes = new RegistroAntecedentesSaludInfo();
                Resultado<RegistroAntecedentesSaludInfo> rAntecedentes = new Resultado<RegistroAntecedentesSaludInfo>();
                NegRegistroAntecedentesSalud nAntecedentes = new NegRegistroAntecedentesSalud();

                foreach (GridViewRow Row in gvAntecedentesSalud.Rows)
                {
                    CheckBox chkAntecedenteSeleccionado = (CheckBox)Row.FindControl("chkAntecedenteSeleccionado");
                    TextBox txtObservacionAntecedente = (TextBox)Row.FindControl("txtObservacionAntecedente");
                    int Antecedente_Id = int.Parse(gvAntecedentesSalud.DataKeys[Row.RowIndex].Values["Antecedente_Id"].ToString());
                    oAntecedentes.Solicitud_Id = NegParticipante.ObjParticipante.Solicitud_Id;
                    oAntecedentes.Participante_Id = NegParticipante.ObjParticipante.Rut;
                    oAntecedentes.Antecedente_Id = Antecedente_Id;
                    oAntecedentes.Observacion = txtObservacionAntecedente.Text;

                    if (chkAntecedenteSeleccionado.Checked)
                        rAntecedentes = nAntecedentes.GuardarRegistroAntecedentesSalud(oAntecedentes);
                    else
                        rAntecedentes = nAntecedentes.EliminarRegistroAntecedentesSalud(oAntecedentes);

                    if (!rAntecedentes.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rAntecedentes.Mensaje);
                        return false;
                    }
                }
                Controles.MostrarMensajeExito("Antecedentes Registrados");
                ActualizarParticipante();
                CargarParticipantes();
                LimpiarRegistroAntecedentes();
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
                    Controles.MostrarMensajeError("Error al Procesar los Documentos");
                }
                return false;
            }
        }
        private bool ActualizarParticipante()
        {
            try
            {
                NegParticipante nParticipante = new NegParticipante();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();

                if (NegRegistroAntecedentesSalud.IndEnvio)
                { 
                    NegParticipante.ObjParticipante.FechaIngresoDPS = DateTime.Parse(txtFechaEnvioDPS.Text);
                    NegParticipante.ObjParticipante.EstadoDps_Id = (int)NegTablas.IdentificadorMaestro("EST_DPS", "E");
                }
                else
                { 
                    NegParticipante.ObjParticipante.FechaAprobacionDPS = DateTime.Parse(txtFechaEnvioDPS.Text);
                    NegParticipante.ObjParticipante.EstadoDps_Id = int.Parse(ddlEstadoDps.SelectedValue);
                }

                NegParticipante.ObjParticipante.MontoAseguradoDPS = decimal.Parse(txtMontoAsegurado.Text);
                NegParticipante.ObjParticipante.SeguroDesgravamen_Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                NegParticipante.ObjParticipante.TasaSeguroDesgravamen = decimal.Parse(txtTasaSeguroDesgravamen.Text);
                NegParticipante.ObjParticipante.PrimaSeguroDesgravamen = decimal.Parse(txtPrimaSeguroDesgravamen.Text);
               rParticipante = nParticipante.GuardarParticipante(NegParticipante.ObjParticipante);
                if (rParticipante.ResultadoGeneral)
                    return true;
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar el Participante");
                }
                return false;
            }
        }
        private void LimpiarRegistroAntecedentes()
        {
            NegParticipante.ObjParticipante = null;
            NegRegistroAntecedentesSalud.lstRegistroAntecedentesSalud = new List<RegistroAntecedentesSaludInfo>();
            txtRut.Text = "";
            txtNombreParticipante.Text = "";
            txtParticipacion.Text = "";
            txtEdadPlazo.Text = "";
            txtPorcentajeDesgravamen.Text = "";
            txtPorcentajeDeuda.Text = "";
            ddlSeguroDesgravamen.ClearSelection();
            txtFechaEnvioDPS.Text = "";
            ddlEstadoDps.ClearSelection();
            txtTasaSeguroDesgravamen.Text = "0";
            txtPrimaSeguroDesgravamen.Text = "0";
            txtCompañia.Text = "";
            txtPlazo.Text = "";
            txtMontoAsegurado.Text = "0";
            gvAntecedentesSalud.DataSource = null;
            gvAntecedentesSalud.DataBind();
        }

        protected void chkAntecedenteSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((Anthem.CheckBox)sender).Parent.Parent as GridViewRow;
            CheckBox chkAntecedenteSeleccionado = (Anthem.CheckBox)row.FindControl("chkAntecedenteSeleccionado");
            var gvtxtObservaciones = (Anthem.TextBox)row.FindControl("txtObservacionAntecedente");
            if (chkAntecedenteSeleccionado.Checked)
            {
                gvtxtObservaciones.Enabled = true;
            }
            else
            {
                gvtxtObservaciones.Text = "";
                gvtxtObservaciones.Enabled = false;
            }


        }

        protected void btnGuardarRegistroAntecedentes_Click(object sender, EventArgs e)
        {
            ProcesarRegistroAntecedentes();
        }

        protected void btnCancelarRegistroAntecedentes_Click(object sender, EventArgs e)
        {
            LimpiarRegistroAntecedentes();
        }

        protected void ddlEstadoDps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}