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
    public partial class DatosDocumentosEstudioTitulo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarInstitucionesFinancieras();
                CargarPropiedadesEETT();
               
            }
        }
        public void PermitirActualizacionDocumentos(bool Permitir)
        {
            if (!Permitir)
            {
                btnCancelarEstudioTitulo.Text = "Limpiar";
                btnGrabarEstudioTitulo.Visible = false;
            }
        }
        public void DesplegarDatosEstudioTitulo(bool Visible)
        {
            if (Visible)
            {
                chkIndAlzamientoHipoteca.Enabled = true;
                ddlInstitucionAlzamientoHipoteca.Enabled = true;
                btnGrabarAlzamiento.Enabled = true;
                Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('1');");
            }
            else
            { 

                chkIndAlzamientoHipoteca.Enabled = false;
                ddlInstitucionAlzamientoHipoteca.Enabled =false;
                btnGrabarAlzamiento.Enabled = false;
                Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('1');");
            }
        }
        protected void gvPropiedadesEETT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void btnSeleccionarPropEETT_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedadesEETT.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesEETT.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objEETT = new TasacionInfo();
            NegPropiedades.objEETT = oTasacion;
            CargarInfoEstudioTitulo(oTasacion);
        }
        protected void btnGrabarEstudioTitulo_Click(object sender, EventArgs e)
        {
            GrabarInfoEETT();
        }

        protected void btnCancelarEstudioTitulo_Click(object sender, EventArgs e)
        {
            LimpiarInfoEETT();
        }
        private void CargarInfoEstudioTitulo(TasacionInfo oEETT)
        {
            try
            {
                RegistroDocumentosEstudioTituloInfo oDocumento = new RegistroDocumentosEstudioTituloInfo();
                Resultado<RegistroDocumentosEstudioTituloInfo> rAntecedentes = new Resultado<RegistroDocumentosEstudioTituloInfo>();
                NegDocumentosEstudioTitulo nDocumentos = new NegDocumentosEstudioTitulo();


                oDocumento.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oDocumento.Tasacion_Id = oEETT.Id;
                oDocumento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");


                rAntecedentes = nDocumentos.BuscarRegistroDocumentosEstudioTitulo(oDocumento);
                if (rAntecedentes.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosEETT, rAntecedentes.Lista, new string[] { Constantes.StringId, "Solicitud_Id", "Tasacion_Id", "Documento_Id" });
                    var Lista = new List<TablaInfo>();

                    Lista = NegTablas.BuscarCatalogo("EST_DOC_EETT");//Lista los estados de los Documentos

                    foreach (GridViewRow Row in gvDocumentosEETT.Rows)
                    {
                        RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");
                        Controles.CargarRadioButtonList(ref rblEstadoDocumento, Lista, "CodigoInterno", "Nombre");
                        oDocumento = new RegistroDocumentosEstudioTituloInfo();
                        int Documento_Id = int.Parse(gvDocumentosEETT.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                        oDocumento = NegDocumentosEstudioTitulo.lstRegistroDocumentosEstudioTitulo.FirstOrDefault(d => d.Documento_Id == Documento_Id);
                        rblEstadoDocumento.SelectedValue = oDocumento.Estado_Id.ToString();
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rAntecedentes.Mensaje);
                }


                chkIndAlzamientoHipoteca.Checked = oEETT.IndAlzamientoHipoteca == null ? false : oEETT.IndAlzamientoHipoteca.Value;
                lblDireccionEETT.Text = oEETT.DireccionCompleta;
                if (oEETT.IndAlzamientoHipoteca == true)
                {
                    ddlInstitucionAlzamientoHipoteca.Enabled = true;
                    ddlInstitucionAlzamientoHipoteca.SelectedValue = oEETT.InstitucionAlzamientoHipoteca_Id.ToString();
                }
                else
                {
                    ddlInstitucionAlzamientoHipoteca.ClearSelection();
                    ddlInstitucionAlzamientoHipoteca.Enabled = false;
                }
                lblDireccionEETT.Text = oEETT.DireccionCompleta;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Información del EETT");
                }
            }
        }
        private void GrabarInfoEETT()
        {
            try
            {
                if (NegPropiedades.objEETT == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }
                if (!ValidarInfoEETT()) return;


                RegistroDocumentosEstudioTituloInfo oDocumento = new RegistroDocumentosEstudioTituloInfo();
                Resultado<RegistroDocumentosEstudioTituloInfo> rAntecedentes = new Resultado<RegistroDocumentosEstudioTituloInfo>();
                NegDocumentosEstudioTitulo nDocumentos = new NegDocumentosEstudioTitulo();

                foreach (GridViewRow Row in gvDocumentosEETT.Rows)
                {
                    RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");
                    int Documento_Id = int.Parse(gvDocumentosEETT.DataKeys[Row.RowIndex].Values["Documento_Id"].ToString());
                    oDocumento.Solicitud_Id = NegPropiedades.objEETT.Solicitud_Id;
                    oDocumento.Tasacion_Id = NegPropiedades.objEETT.Id; ;
                    oDocumento.Documento_Id = Documento_Id;
                    oDocumento.Estado_Id = int.Parse(rblEstadoDocumento.SelectedValue);

                    rAntecedentes = nDocumentos.GuardarRegistroDocumentosEstudioTitulo(oDocumento);

                    if (!rAntecedentes.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rAntecedentes.Mensaje);
                        return;

                    }
                }
                Controles.MostrarMensajeExito("Documentos Registrados");
                CargarPropiedadesEETT();
                LimpiarInfoEETT();


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Grabar los datos de la Tasación");
                }
            }
        }
        private bool ValidarInfoEETT()
        {
            try
            {
                var DocumentosSeleccionados = 0;
                foreach (GridViewRow Row in gvDocumentosEETT.Rows)
                {
                    RadioButtonList rblEstadoDocumento = (RadioButtonList)Row.FindControl("rblEstadoDocumento");

                    if (rblEstadoDocumento.SelectedValue !="")
                        DocumentosSeleccionados++;
                }
                if (DocumentosSeleccionados != NegDocumentosEstudioTitulo.lstRegistroDocumentosEstudioTitulo.Count())
                {
                    Controles.MostrarMensajeAlerta("Debe Asignarle un estado a Todos los Documentos de la Propiedad " + NegPropiedades.objEETT.DireccionCompleta);
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
                    Controles.MostrarMensajeError("Error al Validar la Informaci{on de Tasación");
                }
                return false;
            }
        }
        private void LimpiarInfoEETT()
        {
            if (gvPropiedadesEETT.Visible)
            {
                gvDocumentosEETT.DataSource = null;
                gvDocumentosEETT.DataBind();
                lblDireccionEETT.Text = "";
                NegPropiedades.objEETT = null;
                chkIndAlzamientoHipoteca.Checked = false;
                ddlInstitucionAlzamientoHipoteca.ClearSelection();
            }
        }



        public bool GrabarAlzamientoEETT(bool desdeEvento = false)
        {
            try
            {
                if (NegPropiedades.objEETT == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return false;
                }
                if (!ValidarAlzamientoEETT()) return false;

                NegPropiedades nTasacion = new NegPropiedades();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                if (chkIndAlzamientoHipoteca.Visible)
                {
                    NegPropiedades.objEETT.IndAlzamientoHipoteca = chkIndAlzamientoHipoteca.Checked;
                    NegPropiedades.objEETT.InstitucionAlzamientoHipoteca_Id = int.Parse(ddlInstitucionAlzamientoHipoteca.SelectedValue);
                    NegPropiedades.objEETT.IndFlujoEstudioTitulo = false;

                    rTasacion = nTasacion.GuardarTasacion(NegPropiedades.objEETT);
                    if (!rTasacion.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rTasacion.Mensaje);
                        return false;
                    }
                }

                if (!desdeEvento)
                {
                    Controles.MostrarMensajeExito("Alzamiento Actualizado");
                    CargarPropiedadesEETT();
                    LimpiarInfoEETT();
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
                    Controles.MostrarMensajeError("Error al Grabar los datos del EETT");
                }
                return false;
            }
        }
        private bool ValidarAlzamientoEETT()
        {
            try
            {
                if (NegPropiedades.objEETT == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Propiedad");
                    return false;
                }
                if (chkIndAlzamientoHipoteca.Visible)
                {
                    if (chkIndAlzamientoHipoteca.Checked == true && ddlInstitucionAlzamientoHipoteca.SelectedValue == "-1")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar una Institución Carta Resguardo");
                        return false;
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
                    Controles.MostrarMensajeError("Error al Validar la Informaci{on de Tasación");
                }
                return false;
            }
        }
       
        public void CargarPropiedadesEETT()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oTasacion.IndPropiedadPrincipal = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedadesEETT, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesEETT = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesEETT = rTasacion.Lista;


                    if (rTasacion.Lista.Count == 1)
                    {
                        gvPropiedadesEETT.Visible = false;
                        oTasacion = rTasacion.Lista.FirstOrDefault();
                        NegPropiedades.objEETT = oTasacion;
                        CargarInfoEstudioTitulo(oTasacion);
                    }
                    else
                    {
                        gvPropiedadesEETT.Visible = true;
                    }


                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Propiedades");
                }
            }
        }

        
        protected void chkIndAlzamientoHipoteca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndAlzamientoHipoteca.Checked == true)
            {
                ddlInstitucionAlzamientoHipoteca.Enabled = true;
            }
            else
            {
                ddlInstitucionAlzamientoHipoteca.ClearSelection();
                ddlInstitucionAlzamientoHipoteca.Enabled = false;
            }
        }
        private void CargarInstitucionesFinancieras()
        {
            try
            {
                NegInstitucionesFinancieras oNegocio = new NegInstitucionesFinancieras();
                InstitucionesFinancierasBase oFiltro = new InstitucionesFinancierasBase();
                Resultado<InstitucionesFinancierasBase> oResultado = new Resultado<InstitucionesFinancierasBase>();

                oResultado = oNegocio.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo(ref ddlInstitucionAlzamientoHipoteca, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Institución Financiera --", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Tasadores");
                }
            }
        }

        protected void btnGrabarAlzamiento_Click(object sender, EventArgs e)
        {
            GrabarAlzamientoEETT();
        }

       
    }
}