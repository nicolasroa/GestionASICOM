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

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class ConfirmarGastosOperacionales : System.Web.UI.Page
    {
        public decimal UF = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargarAcciones();
                CargarInfoSolicitud();
                CargarGastosOperacionales();
                CargarInfoCuentaGGOO();


                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);

            }
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
                    if (!ValidarGastosOperacionales()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarGastosOperacionales()) return;
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

        protected void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarAccion();
        }

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
        private void CargarInfoCuentaGGOO()
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                IntegracionGGOOInfo oIntegracion = new IntegracionGGOOInfo();
                Resultado<IntegracionGGOOInfo> rIntegracion = new Resultado<IntegracionGGOOInfo>();

                oIntegracion.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id;
                rIntegracion = nGastos.BuscarResumenCuenta(oIntegracion);
                if (rIntegracion.ResultadoGeneral)
                {
                    oIntegracion = rIntegracion.Lista.FirstOrDefault();
                    if (oIntegracion != null)
                    {
                        txtMontoDisponible.Text = string.Format("{0:C}", oIntegracion.MontoDisponible);
                        txtMontoProvisionado.Text = string.Format("{0:C}", oIntegracion.MontoProvisionado);
                        txtMontoUtilizado.Text = string.Format("{0:C}", oIntegracion.MontoUtilizado);
                    }
                }
                else
                { Controles.MostrarMensajeError(rIntegracion.Mensaje); }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Información de la Cuenta de GGOO");
                }
            }
        }
        private bool SolicitarProvision(GridViewRow row)
        {
            try
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                NegClientes nCliente = new NegClientes();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();
                ClientesInfo oCliente = new ClientesInfo();
                ParticipanteInfo oParticipante = new ParticipanteInfo();

                decimal ValorUF = decimal.Zero;
                decimal ValorPesos = decimal.Zero;



                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);//Solicitante Principal

                oCliente.Rut = oParticipante.Rut;
                rCliente = nCliente.Buscar(oCliente);
                if (rCliente.ResultadoGeneral)
                {
                    oCliente = rCliente.Lista.FirstOrDefault();
                }


                Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");

                Anthem.CheckBox chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");
                var TipoGasto_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString());


                int Moneda_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Moneda_Id"].ToString());

                if (Moneda_Id == 998)
                {
                    ValorUF = decimal.Parse(txtValorUF.Text);
                    ValorPesos = ValorUF * UF;
                }
                else
                {
                    ValorUF = decimal.Parse(txtValorUF.Text) / UF;
                    ValorPesos = ValorUF;
                }

                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                IntegracionGGOOInfo oIntegracion = new IntegracionGGOOInfo();
                Resultado<IntegracionGGOOInfo> rIntegracion = new Resultado<IntegracionGGOOInfo>();

                oIntegracion.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id;
                oIntegracion.NombreCliente = oCliente.NombreCompleto;
                oIntegracion.RutCliente = oCliente.RutCompleto;
                oIntegracion.MailCliente = oCliente.Mail;
                oIntegracion.CelularCliente = oCliente.TelefonoMovil;
                oIntegracion.TipoGasto_Id = TipoGasto_Id;
                oIntegracion.QuienPaga_Id = int.Parse(ddlQuienPaga.SelectedValue);
                oIntegracion.ComoPaga_Id = int.Parse(ddlComoPaga.SelectedValue);
                oIntegracion.ValorUF = ValorUF;
                oIntegracion.ValorPesos = ValorPesos;
                oIntegracion.FechaValorizacion = DateTime.Now;
                oIntegracion.IndProvisionSolicitada = chkProvisionSolicitada.Checked;

                rIntegracion = nGastos.SolicitarProvision(oIntegracion);
                if (rIntegracion.ResultadoGeneral)
                {
                    return true;

                }
                else
                {
                    Controles.MostrarMensajeError(rIntegracion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Solicitar Provisión de GGOO");
                }
                return false;
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
                        Controles.CargarGrid<GastoOperacionalInfo>(ref gvGastosOperacionales, rGastos.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor", "IndProvisionSolicitada" });
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
                            Controles.CargarGrid(ref gvGastosOperacionales, ObjResultado.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor", "IndProvisionSolicitada" });
                            NegGastosOperacionales.lstGastosOperacionales = new List<GastoOperacionalInfo>();
                            NegGastosOperacionales.lstGastosOperacionales = ObjResultado.Lista;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ObjResultado.Mensaje);
                        }



                    }
                    if (gvGastosOperacionales.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvGastosOperacionales.Rows)
                        {

                            Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                            Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                            Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");
                            Anthem.CheckBox chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");



                            Controles.CargarCombo<QuienPagaGGOOInfo>(ref ddlQuienPaga, lstQuienPaga, "Id", "Descripcion", "-- Quién Paga? --", "-1");
                            Controles.CargarCombo<ComoPagaGGOOInfo>(ref ddlComoPaga, lstComoPaga, "Id", "Descripcion", "-- Cómo Paga? --", "-1");

                            ddlQuienPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["QuienPaga_Id"].ToString();
                            ddlComoPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["ComoPaga_Id"].ToString();
                            chkProvisionSolicitada.Checked = bool.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["IndProvisionSolicitada"].ToString());
                            if (chkProvisionSolicitada.Checked)
                            {
                                chkProvisionSolicitada.Enabled = false;
                            }

                            int Moneda_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Moneda_Id"].ToString());
                            txtValorUF.Attributes.Remove("onKeyPress");
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
                if (CargaInicial)
                {
                    foreach (GridViewRow _Row in gvGastosOperacionales.Rows)
                    {
                        Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)_Row.FindControl("ddlQuienPaga");
                        Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)_Row.FindControl("ddlComoPaga");
                        Anthem.TextBox txtValorUF = (Anthem.TextBox)_Row.FindControl("txtValorUF");



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

                        oGastos = NegGastosOperacionales.lstGastosOperacionales.FirstOrDefault(go => go.TipoGastoOperacional_Id == int.Parse(gvGastosOperacionales.DataKeys[_Row.RowIndex].Values["TipoGastoOperacional_Id"].ToString()));


                        if (txtValorUF.Text == "")
                        {
                            txtValorUF.Text = "0";
                        }
                        oGastos.TipoGastoOperacional_Id = int.Parse(gvGastosOperacionales.DataKeys[_Row.RowIndex].Values["TipoGastoOperacional_Id"].ToString());
                        oGastos.QuienPaga_Id = int.Parse(ddlQuienPaga.SelectedValue);
                        oGastos.ComoPaga_Id = int.Parse(ddlComoPaga.SelectedValue);
                        oGastos.Valor = decimal.Parse(txtValorUF.Text);
                        oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                        oGastos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADO_GASTOS_OPERACIONALES", "I");
                        oGastos.IndProvisionSolicitada = false;
                        rGastos = nGastos.GuardarGasto(oGastos);
                        if (!rGastos.ResultadoGeneral)
                        {

                            Controles.MostrarMensajeError(rGastos.Mensaje);
                            return;
                        }
                    }
                }
                else
                {
                    Anthem.CheckBox chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");
                    if (!chkProvisionSolicitada.Enabled)
                    {
                        Controles.MostrarMensajeAlerta("Provisión ya Solcitada al Sistema de Adminsitración de GGOO, no se puede modificar.");
                        return;
                    }

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
                    oGastos.IndProvisionSolicitada = chkProvisionSolicitada.Checked;
                    rGastos = nGastos.GuardarGasto(oGastos);
                    if (!rGastos.ResultadoGeneral)
                    {

                        Controles.MostrarMensajeError(rGastos.Mensaje);
                        return;
                    }
                    else
                    {
                        if (!SolicitarProvision(row)) return;
                        if (CargaInicial == false)
                            Controles.MostrarMensajeExito("Gastos Operacionales Actualizados Correctamente");

                        if (CargaInicial == false)
                            CargarGastosOperacionales();
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
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            GrabarGastosOperacionales(row: row);
        }
        protected void btnGrabarConMontosCero_Click(object sender, EventArgs e)
        {
            GrabarGastosOperacionales(CargaInicial: false, ConValor0: true);
        }
    }
}