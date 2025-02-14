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
    public partial class RecopilacionDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
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
                    if (!ValidaRecopilacionDocumentos()) return;
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
            int Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Id"].ToString());

            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.Id == Id);
            NegCartaResguardo.objTasacion = oInfo;

            NegCartaResguardo.objRegistroDocumentos = new RegistroDocumentosCartaResguardoInfo();
            NegCartaResguardo.objRegistroDocumentos.Tasacion_Id = oInfo.Id;
            NegCartaResguardo.objRegistroDocumentos.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            ObtenerDatosParaResguardo(Id);
        }

        public bool ValidaRecopilacionDocumentos()
        {
            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.StrRecopilacionDocumentos != "-1");
            if (oInfo == null)
            {
                Controles.MostrarMensajeError("Debe seleccionar al menos los documentos obligatorios.");
                return false;
            }
            return true;
        }

        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                TasacionInfo oInfo = new TasacionInfo();
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oInfo.IndAlzamientoHipoteca = true;
                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, NegPropiedades.lstTasaciones, new string[] { "Solicitud_Id", "Id" });
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


        public void ObtenerDatosParaResguardo(int Resguardo_Id)
        {
            try
            {
                TasacionInfo oTasacion = new TasacionInfo();
                oTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.Id == Resguardo_Id);
                NegPropiedades.objTasacion = oTasacion;
                lblInformacionResguardo.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
                CargarDocumentos();
                MarcaDocumentos();
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

        public void CargarDocumentos()
        {
            try
            {

                //Declaracion de Variables
                var ObjetoDoc = new DocumentosCartaResguardoInfo();
                var oResultado = new Resultado<DocumentosCartaResguardoInfo>();
                var ONegocio = new NegCartaResguardo();

                //Asignacion de Variables

                oResultado = ONegocio.BuscarDocumentosCartaResguardo(ObjetoDoc);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarGrid(ref gvDocumentosResguardo, oResultado.Lista, new string[] { "Id", "Obligatorio" });
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
                    Controles.MostrarMensajeError("Error al Cargar Apoderados del Evento");
                }
            }
        }

        public void MarcaDocumentos()
        {
            try
            {

                //Declaracion de Variables
                var oResultado = new Resultado<RegistroDocumentosCartaResguardoInfo>();
                var ONegocio = new NegCartaResguardo();
                var oATInfo = new RegistroDocumentosCartaResguardoInfo();
                //Asignacion de Variables
                oResultado = ONegocio.BuscarRegistroDocumentosCartaResguardo(NegCartaResguardo.objRegistroDocumentos);
                if (oResultado.ResultadoGeneral)
                {
                    foreach (GridViewRow Row in gvDocumentosResguardo.Rows)
                    {
                        CheckBox chkValidado = (CheckBox)Row.FindControl("chkSeleccionado");
                        oATInfo = new RegistroDocumentosCartaResguardoInfo();
                        int Id = int.Parse(gvDocumentosResguardo.DataKeys[Row.RowIndex].Values["Id"].ToString());
                        oATInfo = oResultado.Lista.FirstOrDefault(d => d.Documento_Id == Id);
                        if (oATInfo != null)
                            chkValidado.Checked = String.IsNullOrEmpty(oATInfo.Id.ToString()) ? false : true;
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
                    Controles.MostrarMensajeError("Error al Cargar Apoderados del Evento");
                }
            }
        }


        private void LimpiarRegistroDocumentos()
        {
            NegCartaResguardo.objRegistroDocumentos = new RegistroDocumentosCartaResguardoInfo();
            lblInformacionResguardo.Text = "";
            gvDocumentosResguardo.DataSource = null;
            gvDocumentosResguardo.DataBind();
        }


        public bool ValidaApoderados()
        {

            var marcados = 0;
            var obligacont = 0;

            foreach (GridViewRow Row in gvDocumentosResguardo.Rows)
            {
                string Obligatorio = gvDocumentosResguardo.DataKeys[Row.RowIndex].Values["Obligatorio"].ToString();
                CheckBox chkValidado = (CheckBox)Row.FindControl("chkSeleccionado");

                if (Obligatorio == "True")
                    obligacont = obligacont + 1;
                if (Obligatorio == "True" && chkValidado.Checked)
                    marcados = marcados + 1;
            }
            if (marcados < obligacont)
                return false;

            return true;
        }


        public void GrabaRegistroDocumentos()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<RegistroDocumentosCartaResguardoInfo>();
                var oNegocio = new NegCartaResguardo();
                var oATInfo = new RegistroDocumentosCartaResguardoInfo();
                //Asignacion de Variables

                foreach (GridViewRow Row in gvDocumentosResguardo.Rows)
                {
                    CheckBox chkValidado = (CheckBox)Row.FindControl("chkSeleccionado");
                    oATInfo = new RegistroDocumentosCartaResguardoInfo();
                    int Documento_Id = int.Parse(gvDocumentosResguardo.DataKeys[Row.RowIndex].Values["Id"].ToString());

                    oATInfo.Documento_Id = Documento_Id;
                    oATInfo.Tasacion_Id = NegCartaResguardo.objRegistroDocumentos.Tasacion_Id;
                    oATInfo.Solicitud_Id = NegCartaResguardo.objRegistroDocumentos.Solicitud_Id;
                    if (chkValidado.Checked)
                        oResultado = oNegocio.GuardarRegistroDocumentos(oATInfo);
                    else
                        oResultado = oNegocio.EliminaRegistroDocumentos(oATInfo);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }

                }

                Controles.MostrarMensajeExito("Documentos asociados a Carta de Resguardo Registrados");
                CargarGarantiaConCartaResguardo();
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
                    Controles.MostrarMensajeError("Error al Ingresar la documentación de Carta de Resguardo para el Evento");
                }
            }
        }

        protected void AbtnCancela_Click(object sender, EventArgs e)
        {
            LimpiarRegistroDocumentos();
        }

        protected void AbtnGuarda_Click(object sender, EventArgs e)
        {
            if (NegCartaResguardo.objTasacion == null || NegCartaResguardo.objTasacion.Id <= 0)
            {
                Controles.MostrarMensajeError("Debe seleccionar una Garantía.");
                return;
            }

            if (ValidaApoderados())
            {
                GrabaRegistroDocumentos();
            }
            else
            {
                Controles.MostrarMensajeError("Completar y Seleccionar documentos Obligatorios.");
            }
        }
    }
}