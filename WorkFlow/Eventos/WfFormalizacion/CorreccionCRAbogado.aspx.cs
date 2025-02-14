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
    public partial class CorreccionCRAbogado : System.Web.UI.Page
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
                    if (!ValidaFechaCorreccionCRAbogado()) return;
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

        public bool ValidaFechaCorreccionCRAbogado()
        {
            try
            {
                TasacionInfo oInfo = new TasacionInfo();
                oInfo = NegPropiedades.lstTasaciones.FirstOrDefault(a => a.Solicitud_Id.Equals(NegBandejaEntrada.oBandejaEntrada.Solicitud_Id) && a.FechaCorreccionCartaResguardo != null);
                if (oInfo == null)
                {
                    Controles.MostrarMensajeError("Debe ingresar fecha de Corrección de Carta de Resguardo Abogado.");
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
        public void CargarGarantiaConCartaResguardo()
        {
            try
            {
                List<TasacionInfo> oLstInfo = new List<TasacionInfo>();
                TasacionInfo oInfo = new TasacionInfo();
                NegPropiedades oNegocio = new NegPropiedades();
                Resultado<TasacionInfo> oResultado = new Resultado<TasacionInfo>();

                oInfo.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                oResultado = oNegocio.BuscarTasacion(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvGarantias, oResultado.Lista, new string[] { "Id","Solicitud_Id", "InstitucionAlzamientoHipoteca_Id" });

                    foreach (GridViewRow Row in gvGarantias.Rows)
                    {
                        TextBox txtFechaCorreccion = (TextBox)Row.FindControl("txtFechaCorreccion");
                        oInfo = new TasacionInfo();
                        int InstitucionAlzamientoHipoteca_Id = int.Parse(gvGarantias.DataKeys[Row.RowIndex].Values["InstitucionAlzamientoHipoteca_Id"].ToString());
                        oInfo = oResultado.Lista.FirstOrDefault(d => d.InstitucionAlzamientoHipoteca_Id == InstitucionAlzamientoHipoteca_Id);
                        if (oInfo.FechaCorreccionCartaResguardo != null)
                        {
                            txtFechaCorreccion.Text = oInfo.FechaCorreccionCartaResguardo.Value.ToShortDateString();
                        }
                        else
                        {
                            txtFechaCorreccion.Text = "";
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
                    Controles.MostrarMensajeError("Error al cargar Institución con Carta de Resguardo del Evento");
                }
            }
        }


        public static List<TasacionInfo> Bancos(List<TasacionInfo> dt)
        {
            return dt.AsEnumerable()
                     .Select(row => new TasacionInfo
                     {
                         InstitucionAlzamientoHipoteca_Id = row.InstitucionAlzamientoHipoteca_Id,
                         NombreInstitucionAlzamientoHipoteca = row.NombreInstitucionAlzamientoHipoteca,
                         Solicitud_Id = row.Solicitud_Id
                     })
                     .Distinct()
                     .ToList();
        }


        public void GrabaFechaFirmaCorreccion()
        {
            try
            {
                //Declaracion de Variables
                var oResultado = new Resultado<TasacionInfo>();
                var oNegocio = new NegPropiedades();
                //Asignacion de Variables
            
                oResultado = oNegocio.GuardarTasacion(NegCartaResguardo.objTasacion);

                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Información Registrada");
                    CargarGarantiaConCartaResguardo();
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
                    Controles.MostrarMensajeError("Error al guardar fecha de corrección de Carta de Resguardo para el Evento");
                }
            }
        }

        protected void btnGuardaFirma_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((Anthem.ImageButton)sender).Parent.Parent as GridViewRow;
                var fechaFirma = ((Anthem.TextBox)row.Cells[2].FindControl("txtFechaCorreccion")).Text;


                if (fechaFirma.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar Fecha para Continuar");
                    return;
                }
                var Tasacion_Id = int.Parse(gvGarantias.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegCartaResguardo.objTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.Id == Tasacion_Id);

                NegCartaResguardo.objTasacion.FechaCorreccionCartaResguardo = Convert.ToDateTime(fechaFirma);

                GrabaFechaFirmaCorreccion();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al guardar Fecha Corrección de Carta Resguardo para el Evento");
                }
            }
        }

    }
}