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
    public partial class SalidaCBR : System.Web.UI.Page
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
                    if (!ValidacionEvento()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                    if (!ValidacionEvento()) return;
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
            if (NegConservador.ObjInfoDatosCBR == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha de Firma de Banco Alzante.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.FechaSalidaCBR == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha Salida CBR.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.FechaEnvioInformeCBR == null)
            {
                Controles.MostrarMensajeError("Debe ingresar fecha de envío a Informe.");
                return false;
            }
            if (NegConservador.ObjInfoDatosCBR.NroDominio.ToString() == null)
            {
                Controles.MostrarMensajeError("Debe ingresar Nro. Dominio.");
                return false;
            }
            return true;
        }

        public void CargaFormulario()
        {
            BuscaDatosCBR();
            NegConservador.ObjInfoDatosCBR = new DatosCBRInfo();
            NegConservador.ObjInfoDatosCBR = NegConservador.ObjLstDatosCBR.FirstOrDefault(p => p.Solicitud_Id == NegBandejaEntrada.oBandejaEntrada.Solicitud_Id);

            txtFechaSalidaCBR.Text = NegConservador.ObjInfoDatosCBR.FechaSalidaCBR == null ? "" : NegConservador.ObjInfoDatosCBR.FechaSalidaCBR.Value.ToShortDateString();
            txtFechaEnvioInforme.Text = NegConservador.ObjInfoDatosCBR.FechaEnvioInformeCBR == null ? "" : NegConservador.ObjInfoDatosCBR.FechaEnvioInformeCBR.Value.ToShortDateString();
            txtNumeroDominio.Text = NegConservador.ObjInfoDatosCBR.NroDominio == -1 ? "" : NegConservador.ObjInfoDatosCBR.NroDominio.ToString();
            txtFojasDominio.Text = NegConservador.ObjInfoDatosCBR.FojasDominio;
            txtNumeroProhibicion.Text = NegConservador.ObjInfoDatosCBR.NroProhibicion == -1 ? "" : NegConservador.ObjInfoDatosCBR.NroProhibicion.ToString();
            txtFojasProhibicion.Text = NegConservador.ObjInfoDatosCBR.FojasProhibicion;

        }

        public void BuscaDatosCBR()
        {
            try
            {
                DatosCBRInfo oInfo = new DatosCBRInfo();
                NegConservador negAcciones = new NegConservador();
                Resultado<DatosCBRInfo> oResultado = new Resultado<DatosCBRInfo>();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oResultado = negAcciones.Buscar(oInfo);
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
                    Controles.MostrarMensajeError("Error al Cargar las Acciones del Evento");
                }
            }
        }

        public void GrabaFechaFirmaEnvio()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<DatosCBRInfo>();
                var oNegocio = new NegConservador();
                //Asignacion de Variables

                if (txtFechaEnvioInforme.Text.Length != 0)
                {
                    NegConservador.ObjInfoDatosCBR.FechaEnvioInformeCBR = DateTime.Parse(txtFechaEnvioInforme.Text);
                }
                if (txtNumeroDominio.Text.Length != 0)
                {
                    NegConservador.ObjInfoDatosCBR.NroDominio = int.Parse(txtNumeroDominio.Text);
                }
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
                    Controles.MostrarMensajeError("Error al guardar Fecha Salida CBR");
                }
            }
        }

        protected void AbtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaInfo())
            {
                GrabaFechaFirmaEnvio();
            }
            //else
            //{
            //    Controles.MostrarMensajeError("Debe completar la información del formulario.");
            //}
        }

        public bool ValidaInfo()
        {
            try
            {
                if (txtFechaSalidaCBR.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fecha Salida CBR");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.FechaSalidaCBR = DateTime.Parse(txtFechaSalidaCBR.Text);
                }
                if (txtFechaEnvioInforme.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fecha Envío de Informe");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.FechaEnvioInformeCBR = DateTime.Parse(txtFechaEnvioInforme.Text);
                }
                if (txtFojasDominio.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fojas del Dominio");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.FojasDominio = txtFojasDominio.Text;
                }

                if (txtNumeroDominio.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Número de Dominio");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.NroDominio = int.Parse(txtNumeroDominio.Text);
                }

                if (txtFojasProhibicion.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Fojas de la Prohibición");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.FojasProhibicion = txtFojasProhibicion.Text;
                }

                if (txtNumeroProhibicion.Text.Length == 0)
                {
                    Controles.MostrarMensajeInfo("Debe Ingresar Número de la Prohibición");
                    return false;
                }
                else
                {
                    NegConservador.ObjInfoDatosCBR.NroProhibicion = int.Parse(txtNumeroProhibicion.Text);
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
            txtFechaSalidaCBR.Text = "";
            txtFechaEnvioInforme.Text = "";
            txtNumeroDominio.Text = "";
            txtFojasDominio.Text = "";
            txtFojasProhibicion.Text = "";
            txtNumeroProhibicion.Text = "";
        }

        protected void AbtnCancela_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }

    }
}