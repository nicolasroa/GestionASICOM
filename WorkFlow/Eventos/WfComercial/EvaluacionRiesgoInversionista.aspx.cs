using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace WorkFlow.Eventos.WfComercial
{
    public partial class EvaluacionRiesgoInversionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargarMotivoRechazo();
                CargarSolicitudInversionistas();
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
                btnAccion.Attributes.Remove("OnMouseDown");
                btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);");
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
                    btnAccion.Attributes.Remove("OnMouseDown");
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de Rechazar la Solicitud?',this);");
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
                    if (!DatosSolicitud.ValidarDatosCredito()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!GrabarMotivoRechazo()) return;
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


        #region Inversionistas


        public void CargarSolicitudInversionistas()
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                oInversionista.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rInversionista = nInversionistas.BuscarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvSolicitudInversionistas, rInversionista.Lista, new string[] { "Inversionista_Id", "Solicitud_Id", "Estado_Id" });

                    var Lista = new List<TablaInfo>();

                    Lista = NegTablas.BuscarCatalogo("EST_REV_INVERSIONISTA");//Lista los estados de un Evento



                    foreach (GridViewRow Row in gvSolicitudInversionistas.Rows)
                    {
                        Anthem.DropDownList ddlEstadoEvaluacion = (Anthem.DropDownList)Row.FindControl("ddlEstadoEvaluacion");
                        Controles.CargarCombo<TablaInfo>(ref ddlEstadoEvaluacion, Lista, "CodigoInterno", "Nombre", "", "");
                        ddlEstadoEvaluacion.SelectedValue = gvSolicitudInversionistas.DataKeys[Row.RowIndex].Values["Estado_Id"].ToString();
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rInversionista.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Inversinistas de la Solicitud");
                }
            }
        }
        public void GrabarSolicitudInversionistas(int Inversionista_Id, int Estado_Id)
        {
            try
            {
                SolicitudInvercionistasInfo oInversionista = new SolicitudInvercionistasInfo();
                NegInversionistas nInversionistas = new NegInversionistas();
                Resultado<SolicitudInvercionistasInfo> rInversionista = new Resultado<SolicitudInvercionistasInfo>();
                oInversionista = NegInversionistas.lstSolicitudesInversionista.FirstOrDefault(i => i.Solicitud_Id == NegSolicitudes.objSolicitudInfo.Id && i.Inversionista_Id == Inversionista_Id);

                oInversionista.Estado_Id = Estado_Id;
                rInversionista = nInversionistas.GrabarSolicitudInversionistas(oInversionista);


                if (rInversionista.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Inversionista Actualizado");
                    CargarSolicitudInversionistas();
                }
                else
                {
                    Controles.MostrarMensajeError(rInversionista.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Inversinistas de la Solicitud");
                }
            }
        }


        protected void ddlEstadoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
            int Inversionista_Id = int.Parse(gvSolicitudInversionistas.DataKeys[row.RowIndex].Values["Inversionista_Id"].ToString());
            GrabarSolicitudInversionistas(Inversionista_Id, int.Parse(((DropDownList)sender).SelectedValue));
        }



        #endregion

        #region Rechazo
        private bool GrabarMotivoRechazo()
        {
            try
            {
                if (ddlMotivoRechazo.SelectedValue.Equals("-1"))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Motivo para el Rechazo");
                    return false;
                }
                NegSolicitudes.objSolicitudInfo.MotivoRechazo_Id = int.Parse(ddlMotivoRechazo.SelectedValue);
                return NegSolicitudes.ActualizarSolicitud(NegSolicitudes.objSolicitudInfo);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
                return false;
            }
        }
        private void CargarMotivoRechazo()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("mot_rechazo_sol");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMotivoRechazo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Motivo --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sexo Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Sexo");
                }
            }
        }
        protected void btnGrabarMotivoRechazo_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }

        #endregion
    }
}