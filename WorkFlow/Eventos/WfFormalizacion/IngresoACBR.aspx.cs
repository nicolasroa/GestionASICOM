using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Eventos
{
    public partial class IngresoACBR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioDatepicker();");
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargaFormulario();
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
                    if (!ValidacionIngresoCBR()) return;

                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                    if (!ValidacionIngresoCBR()) return;
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

        public bool ValidacionIngresoCBR()
        {
            if (NegConservador.ObjInfoDatosCBR == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha de Firma de Banco Alzante.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.FechaIngresoCBR == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha Ingreso CBR.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.Conservador_Id == -1)
            {
                Controles.MostrarMensajeError("Debe seleccionar un Conservador.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.NroIngresoCBR.ToString() == null)
            {
                Controles.MostrarMensajeError("Debe ingresar Nro. Ingreso.");
                return false;
            }
            return true;
        }

        public void CargaFormulario()
        {
            CargarConservador();
            BuscaDatosCBR();
            NegConservador.ObjInfoDatosCBR = new DatosCBRInfo();
            NegConservador.ObjInfoDatosCBR = NegConservador.ObjLstDatosCBR.FirstOrDefault(p => p.Solicitud_Id == NegBandejaEntrada.oBandejaEntrada.Solicitud_Id);
            if (NegConservador.ObjInfoDatosCBR != null)
            {
                ddlConservador.SelectedValue = NegConservador.ObjInfoDatosCBR.Conservador_Id.ToString();
                txtNroIngreso.Text = NegConservador.ObjInfoDatosCBR.NroIngresoCBR.ToString();
                txtFecha.Text = NegConservador.ObjInfoDatosCBR.FechaIngresoCBR == null ? "" : NegConservador.ObjInfoDatosCBR.FechaIngresoCBR.Value.ToShortDateString();
            }
        }

        public void BuscaDatosCBR()
        {
            try
            {
                DatosCBRInfo oAcciones = new DatosCBRInfo();
                NegConservador negAcciones = new NegConservador();
                Resultado<DatosCBRInfo> oResultado = new Resultado<DatosCBRInfo>();
                oAcciones.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oResultado = negAcciones.Buscar(oAcciones);
                if (!oResultado.ResultadoGeneral)
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
                    Controles.MostrarMensajeError("Error al Cargar Datos CBR del Evento");
                }
            }
        }

        public void CargarConservador()
        {
            try
            {
                ConservadorInfo oAcciones = new ConservadorInfo();
                NegConservador negAcciones = new NegConservador();
                Resultado<ConservadorInfo> oResultado = new Resultado<ConservadorInfo>();
                oResultado = negAcciones.Buscar(oAcciones);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ConservadorInfo>(ref ddlConservador, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Conservador--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar listado de Conservadores.");
                }
            }
        }

        public void GrabaIngresoACBR()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<DatosCBRInfo>();
                var oNegocio = new NegConservador();
                //Asignacion de Variables
                NegConservador.ObjInfoDatosCBR = new DatosCBRInfo();
                if (NegConservador.ObjLstDatosCBR != null)
                {
                    NegConservador.ObjInfoDatosCBR = NegConservador.ObjLstDatosCBR.FirstOrDefault(p => p.Solicitud_Id == NegBandejaEntrada.oBandejaEntrada.Solicitud_Id);
                }
                if (NegConservador.ObjInfoDatosCBR == null)
                    NegConservador.ObjInfoDatosCBR = new DatosCBRInfo();


                NegConservador.ObjInfoDatosCBR.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                NegConservador.ObjInfoDatosCBR.Conservador_Id = int.Parse(ddlConservador.SelectedValue);
                NegConservador.ObjInfoDatosCBR.NroIngresoCBR = int.Parse(txtNroIngreso.Text);
                NegConservador.ObjInfoDatosCBR.FechaIngresoCBR = DateTime.Parse(txtFecha.Text);
                oResultado = oNegocio.GuardarDatosCBR(NegConservador.ObjInfoDatosCBR);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información Registrada");
                    CargaFormulario();
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
                    Controles.MostrarMensajeError("Error al guardar el Ingreso CBR");
                }
            }
        }

        public bool ValidaInfo()
        {
            try
            {
                if (ddlConservador.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeInfo("Debe Seleccionar Conservador.");
                    return false;
                }
                if (txtNroIngreso.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Nro Ingreso CBR");
                    return false;
                }
                if (txtFecha.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fecha Ingreso CBR");
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
            ddlConservador.SelectedIndex = -1;
            txtNroIngreso.Text = "";
            txtFecha.Text = "";

        }

        protected void AbtnCancela_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }

        protected void AbtnGuarda_Click(object sender, EventArgs e)
        {
            if (ValidaInfo())
            {
                GrabaIngresoACBR();
            }
            else
            {
                Controles.MostrarMensajeError("Debe completar la información del formulario.");
            }
        }
    }
}