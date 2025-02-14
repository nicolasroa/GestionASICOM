using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class EnvioBancoAlzante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioDatepicker();");
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargarGarantiaConCartaResguardo();
            }
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
                    if (!ValidaFechaEnvioBancoAlzante()) return;
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

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());

            NegCartaResguardo.objTasacion = new TasacionInfo();
            NegCartaResguardo.objTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.InstitucionAlzamientoHipoteca_Id == InstitucionAlzamientoHipoteca_Id);
            ObtenerDatosParaResguardo();
        }

        public bool ValidaFechaEnvioBancoAlzante()
        {
            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.FechaEnvioBcoAlzante != null);
            if (oInfo == null)
            {
                Controles.MostrarMensajeAlerta("Debe ingresar fecha de Envio a Banco Alzante.");
                return false;
            }
            return true;
        }

        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                List<TasacionInfo> oLstInfo = new List<TasacionInfo>();
                TasacionInfo oInfo = new TasacionInfo();
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oInfo.IndAlzamientoHipoteca = true;
                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, oResultado.Lista, new string[] { "Solicitud_Id", "InstitucionAlzamientoHipoteca_Id" });
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
                    Controles.MostrarMensajeError("Error al Cargar las Garantias con Resguardo del Evento");
                }
            }
        }

        public void ObtenerDatosParaResguardo()
        {
            try
            {
                txtEmail.Text = NegCartaResguardo.objTasacion.EmailContactoBcoAlzante;
                txtFechaEnvio.Text = NegCartaResguardo.objTasacion.FechaEnvioBcoAlzante == null ? "" : NegCartaResguardo.objTasacion.FechaEnvioBcoAlzante.Value.ToShortDateString();
                txtNombreContacto.Text = NegCartaResguardo.objTasacion.NombreContactoBcoAlzante;
                txtTelefono.Text = NegCartaResguardo.objTasacion.FonoContactoBcoAlzante;

                lblInstitucionCarta.Text = NegCartaResguardo.objTasacion.NombreInstitucionAlzamientoHipoteca;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar las Garantias con Resguardo del Evento");
                }
            }
        }

        public void GrabaFechaEnvioBancoAlzante()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<TasacionInfo>();
                var oNegocio = new NegPropiedades();
                //Asignacion de Variables
                NegCartaResguardo.objTasacion.EmailContactoBcoAlzante = txtEmail.Text;
                NegCartaResguardo.objTasacion.FechaEnvioBcoAlzante = DateTime.Parse(txtFechaEnvio.Text);
                NegCartaResguardo.objTasacion.NombreContactoBcoAlzante = txtNombreContacto.Text;
                NegCartaResguardo.objTasacion.FonoContactoBcoAlzante = txtTelefono.Text;

                oResultado = oNegocio.GuardarTasacion(NegCartaResguardo.objTasacion);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información Registrada");
                    CargarGarantiaConCartaResguardo();
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
                    Controles.MostrarMensajeError("Error al guardar fecha de envio a Bco Alzante para el Evento");
                }
            }
        }

        protected void AbtnGuardaCartaResguardo_Click(object sender, EventArgs e)
        {
            if (NegCartaResguardo.objTasacion == null || NegCartaResguardo.objTasacion.InstitucionAlzamientoHipoteca_Id == 0)
            {
                Controles.MostrarMensajeAlerta("Debe seleccionar un Banco Alzante.");
                return;
            }
            if (ValidaInfo())
            {
                GrabaFechaEnvioBancoAlzante();
            }
            else
            {
                Controles.MostrarMensajeAlerta("Debe completar la información del formulario.");
            }

        }


        public bool ValidaInfo()
        {
            try
            {
                if (txtNombreContacto.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar un Contacto para Continuar");
                    return false;
                }
                if (txtTelefono.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar un Teléfono para Continuar");
                    return false;
                }
                if (txtEmail.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar un Email válido para Continuar");
                    return false;
                }
                if (txtFechaEnvio.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe ingresar Fecha de Envío a Banco Alzante para Continuar");
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
                    Controles.MostrarMensajeError("Error al Validar Formulario");
                }
                return false;
            }
        }

        private void LimpiarRegistro()
        {
            NegCartaResguardo.objTasacion = new TasacionInfo();

            txtEmail.Text = "";
            txtFechaEnvio.Text = "";
            txtNombreContacto.Text = "";
            txtTelefono.Text = "";
        }

        protected void AbtnCancela_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }
    }
}