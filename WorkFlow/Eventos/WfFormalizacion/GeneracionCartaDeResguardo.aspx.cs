using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class GeneracionCartaDeResguardo : System.Web.UI.Page
    {
        private List<InstitucionesFinancierasBase> lstIIFF = new List<InstitucionesFinancierasBase>();
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
                    if (!ValidacionCartaResguardo()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                    if (!ValidacionCartaResguardo()) return;
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


        protected void btnCResguardo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Solicitud_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
            int InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());

            NegCartaResguardo.lstTasacion = new List<TasacionInfo>();
            NegCartaResguardo.lstTasacion = NegPropiedades.lstTasaciones.Where(m => m.Solicitud_Id == Solicitud_Id
                                                    && m.InstitucionAlzamientoHipoteca_Id == InstitucionAlzamientoHipoteca_Id)
                                            .ToList();
            GeneraPDFCartaDeResguardo();
        }

        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Tasacion_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Id"].ToString());

            NegPropiedades.objTasacion = new TasacionInfo();
            NegPropiedades.objTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(p => p.Id == Tasacion_Id);
            NegCartaResguardo.objApoderadoTasacionInfo = new ApoderadoTasacionInfo();
            NegCartaResguardo.objApoderadoTasacionInfo.Tasacion_Id = NegPropiedades.objTasacion.Id;
            ObtenerDatosParaResguardo();
        }

        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                TasacionInfo oInfo = new TasacionInfo();
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();

                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oInfo.IndAlzamientoHipoteca = true;

                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, NegPropiedades.lstTasaciones, new string[] { "Id", "InstitucionAlzamientoHipoteca_Id", "Solicitud_Id" });
                    NegCartaResguardo.lstTasacion = new List<TasacionInfo>();
                    NegCartaResguardo.lstTasacion = NegPropiedades.lstTasaciones;

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

        public bool ValidacionCartaResguardo()
        {
            TasacionInfo oInfo = new TasacionInfo();
            oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.InstitucionAlzamientoHipoteca_Id != -1);
            if (oInfo == null)
            {
                Controles.MostrarMensajeError("Debe seleccionar Institución de Resguardo.");
                return false;
            }
            TasacionInfo oInfoApoderado = new TasacionInfo();
            oInfoApoderado = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.StrApoderados > 0 && a.StrApoderados < 3);
            if (oInfoApoderado==null)
            {
                Controles.MostrarMensajeError("Debe seleccionar al menos 1 Apoderados.");
                return false;
            }
            return  true;
        }

        public void ObtenerDatosParaResguardo()
        {
            try
            {

                NegPropiedades nPropiedades = new NegPropiedades();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                PropiedadInfo oPropiedadPadre = new PropiedadInfo();
                PropiedadInfo oPropiedad = new PropiedadInfo();


                oPropiedad.Id = NegPropiedades.objTasacion.Propiedad_Id;
                rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                if (!rPropiedad.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
                    return;
                }

                oPropiedad = rPropiedad.Lista.FirstOrDefault();

                txtTipoInmuebleDescripcion.Enabled = true;
                txtDireccionCompleta.Enabled = true;
                txtTipoInmuebleDescripcion.Text = oPropiedad.DescripcionTipoInmueble;
                txtDireccionCompleta.Text = NegPropiedades.objTasacion.DireccionCompleta;
                CargarInstitucionDeResguardo();
                CargarApoderados();
                MarcaApoderados();
                txtTipoInmuebleDescripcion.Enabled = false;
                txtDireccionCompleta.Enabled = false;
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

        public void CargarApoderados()
        {
            try
            {

                //Declaracion de Variables
                var ObjetoUsuarioRol = new UsuarioRolInfo();
                var ObjetoResultado = new Resultado<UsuarioRolInfo>();
                var NegUsuario = new NegUsuarios();

                //Asignacion de Variables
                ObjetoUsuarioRol.Rol_Id = Constantes.IdRolApoderado;
                ObjetoUsuarioRol.Usuario_Id = null;

                ObjetoResultado = NegUsuario.BuscarUsuarioRol(ObjetoUsuarioRol);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvApoderados, ObjetoResultado.Lista, new string[] { "Rol_Id", "Usuario_Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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

        public void MarcaApoderados()
        {
            try
            {

                //Declaracion de Variables
                var oInfo = new ApoderadoTasacionInfo();
                var oResultado = new Resultado<ApoderadoTasacionInfo>();
                var oNegocio = new NegCartaResguardo();
                var oATInfo = new ApoderadoTasacionInfo();
                //Asignacion de Variables
                oInfo.Tasacion_Id = NegPropiedades.objTasacion.Id;
                oResultado = oNegocio.BuscarApoderadoTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {

                    foreach (GridViewRow Row in gvApoderados.Rows)
                    {
                        CheckBox chkValidado = (CheckBox)Row.FindControl("chkApoderadoSeleccionado");
                        oATInfo = new ApoderadoTasacionInfo();
                        int Apoderado_Id = int.Parse(gvApoderados.DataKeys[Row.RowIndex].Values["Usuario_Id"].ToString());
                        oATInfo = oResultado.Lista.FirstOrDefault(d => d.Apoderado_Id == Apoderado_Id);
                        if (oATInfo != null)
                            chkValidado.Checked = String.IsNullOrEmpty(oATInfo.Apoderado_Id.ToString()) ? false : true;
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

        public void CargarInstitucionDeResguardo()
        {
            try
            {
                InstitucionesFinancierasBase oBase = new InstitucionesFinancierasBase();
                NegInstitucionesFinancieras oNegocio = new NegInstitucionesFinancieras();
                Resultado<InstitucionesFinancierasBase> oResultado = new Resultado<InstitucionesFinancierasBase>();
                oResultado = oNegocio.Buscar(oBase);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InstitucionesFinancierasBase>(ref ddlInstitucionFinanciera, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Institución Financiera--", "-1");
                    ddlInstitucionFinanciera.SelectedValue = NegPropiedades.objTasacion.InstitucionAlzamientoHipoteca_Id.ToString();
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
                    Controles.MostrarMensajeError("Error al Cargar las Instituciones de Resguardo para el Evento");
                }
            }
        }

        public void GrabaCartaResguardo()
        {
            try
            {
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();
                NegPropiedades oNegocio = new NegPropiedades();
                NegPropiedades.objTasacion.InstitucionAlzamientoHipoteca_Id = int.Parse(ddlInstitucionFinanciera.SelectedValue);
                oResultado = oNegocio.GuardarTasacion(NegPropiedades.objTasacion);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información de Institución de Resguardo Registrada");
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
                    Controles.MostrarMensajeError("Error al guardar la Institucion de Resguardo para el Evento");
                }
            }
        }
        public void GrabaApoderadoTasacion()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<ApoderadoTasacionInfo>();
                var oNegocio = new NegCartaResguardo();
                var oATInfo = new ApoderadoTasacionInfo();
                //Asignacion de Variables

                foreach (GridViewRow Row in gvApoderados.Rows)
                {
                    CheckBox chkValidado = (CheckBox)Row.FindControl("chkApoderadoSeleccionado");
                    oATInfo = new ApoderadoTasacionInfo();
                    int Apoderado_Id = int.Parse(gvApoderados.DataKeys[Row.RowIndex].Values["Usuario_Id"].ToString());

                    oATInfo.Apoderado_Id = Apoderado_Id;
                    oATInfo.Tasacion_Id = NegCartaResguardo.objApoderadoTasacionInfo.Tasacion_Id;

                    if (chkValidado.Checked)
                        oResultado = oNegocio.GuardarApoderadoTasacion(oATInfo);
                    else
                        oResultado = oNegocio.EliminarApoderadoTasacion(oATInfo);

                    if (!oResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }

                }

                Controles.MostrarMensajeExito("Apoderados Registrados");
                CargarGarantiaConCartaResguardo();
                LimpiarRegistroApoderados();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar las Instituciones de Resguardo para el Evento");
                }
            }
        }

        protected void gvGarantiasConResguardo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGarantias.PageIndex = e.NewPageIndex;
            CargarGarantiaConCartaResguardo();
        }

        public bool ValidaApoderados()
        {

            var marcados = 0;

            foreach (GridViewRow Row in gvApoderados.Rows)
            {
                CheckBox chkValidado = (CheckBox)Row.FindControl("chkApoderadoSeleccionado");
                if (chkValidado.Checked)
                    marcados = marcados + 1;
            }

            if (marcados < 1 || marcados > 2)
            {
                return false;
            }
            return true;
        }

        protected void AbtnGuardaCartaResguardo_Click(object sender, EventArgs e)
        {
            if (NegCartaResguardo.objApoderadoTasacionInfo == null || NegCartaResguardo.objApoderadoTasacionInfo.Tasacion_Id == 0)
            {
                Controles.MostrarMensajeError("Debe seleccionar una Garantía.");
                return;
            }

            if (ddlInstitucionFinanciera.SelectedValue != "-1")
            {
                GrabaCartaResguardo();

                if (ValidaApoderados())
                {
                    GrabaApoderadoTasacion();
                    CargarApoderados();
                    MarcaApoderados();
                    LimpiarRegistroApoderados();
                }
                else
                {
                    Controles.MostrarMensajeError("Seleccionar entre 1 y 2 apoderados.");
                }
            }
            else
            {
                Controles.MostrarMensajeError("Seleccionar Institución de Resguardo");
            }
        }

        private void LimpiarRegistroApoderados()
        {

            txtTipoInmuebleDescripcion.Text = "";
            txtDireccionCompleta.Text = "";
            ddlInstitucionFinanciera.SelectedValue = "-1";
            gvApoderados.DataSource = null;
            gvApoderados.DataBind();
            NegPropiedades.objTasacion = new TasacionInfo();
            NegCartaResguardo.lstApoderadoTasacionInfo = new List<ApoderadoTasacionInfo>();
        }

        protected void AbtnCancela_Click(object sender, EventArgs e)
        {
            LimpiarRegistroApoderados();
        }

        private void GeneraPDFCartaDeResguardo()
        {
            try
            {
                NegCartaResguardo.oReporteCartaResguardo = new ReporteCartaResguardo();

                if (NegCartaResguardo.lstTasacion != null && NegCartaResguardo.lstTasacion.Count != 0)
                {
                    NegCartaResguardo.oReporteCartaResguardo.lstTasacion = new List<TasacionInfo>();
                    NegCartaResguardo.oReporteCartaResguardo.lstTasacion = NegCartaResguardo.lstTasacion;
                }
                else
                {
                    Controles.MostrarMensajeError("Solicitud sin propiedades ingresadas.");
                    return;
                }

                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();
                ClientesInfo oCliente = new ClientesInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                string urlDocumento = "~/Reportes/PDF/CartaDeResguardo.aspx";

                string ContenidoHtml = "";
                string Archivo = "CR_Solicitud_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Carta de Resguardo";

                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        NegCartaResguardo.oReporteCartaResguardo.oSolicitud = new SolicitudInfo();
                        NegCartaResguardo.oReporteCartaResguardo.oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                    }
                }

                oCliente.Rut = NegParticipante.lstParticipantes.FirstOrDefault(s => s.Solicitud_Id == oSolicitud.Id && s.TipoParticipacion_Id == 1).Rut;
                NegClientes oNegCliente = new NegClientes();
                Resultado<ClientesInfo> oResultado = new Resultado<ClientesInfo>();
                oResultado = oNegCliente.Buscar(oCliente);
                if (oResultado.ResultadoGeneral && oResultado.ValorDecimal != 0)
                {
                    NegCartaResguardo.oReporteCartaResguardo.oCliente = new ClientesInfo();
                    NegCartaResguardo.oReporteCartaResguardo.oCliente = NegClientes.objClienteInfo;
                }

                NegCartaResguardo.oReporteCartaResguardo.lstParticipante = NegParticipante.lstParticipantes;

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                Response.BinaryWrite(Binarios);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
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
    }
}