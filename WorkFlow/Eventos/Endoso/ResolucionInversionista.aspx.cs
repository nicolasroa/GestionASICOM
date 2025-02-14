using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Eventos
{
    public partial class ResolucionInversionista : System.Web.UI.Page
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioResolucionInversionista();");
            if (!Page.IsPostBack)
            {



                NegSolicitudes.IndComentario = false;
                CargarAcciones();
                CargarEstados();
                CargaFormulario();
            }
        }
        private void CargarEstados()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("ESTADO_ENDOSO");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoEndoso, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado Endoso --", "-1");


                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Etapas Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Etapas");
                }
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

        protected void btnGrabaResolucion_Click(object sender, EventArgs e)
        {
            GrabaResolucionInversionista();
        }

        #endregion
        #region METODOS
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
                    if (!GrabaResolucionInversionista()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!GrabaResolucionInversionista()) return;
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

        private void CargaFormulario()
        {
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
            lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
            txtValorUF.Text = string.Format("{0:0,0}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");

            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        txtTasa.Text = ObjInfo.TasaFinal.ToString();
                        txtFechaEnvioAntecedentes.Text = ObjInfo.FechaEnvioAntecedentes.ToShortDateString();
                        txtTasaEndoso.Text = ObjInfo.TasaEndoso.ToString();
                        txtPlazoCredito.Text = ObjInfo.Plazo.ToString();
                        txtMontoCredito.Text = ObjInfo.MontoCredito.ToString();
                        txtGracia.Text = ObjInfo.Gracia.ToString();
                        txtInversionista.Text = ObjInfo.DescripcionInversionista.ToString();
                        ddlEstadoEndoso.SelectedValue = ObjInfo.EstadoEndoso_Id.ToString();
                        txtFechaResolucion.Text = ObjInfo.FechaResolucionInversionista != null ? ObjInfo.FechaResolucionInversionista.Value.ToShortDateString() : "";
                        txtFechaEnvioAntecedentes.Enabled = false;
                        txtPlazoCredito.Enabled = false;
                        txtMontoCredito.Enabled = false;
                        txtGracia.Enabled = false;
                        txtTasaEndoso.Enabled = false;
                        txtInversionista.Enabled = false;


                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Simular");
                }
            }

        }

        private bool ValidarFormulario()
        {
            if (ddlEstadoEndoso.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar una Resolución");
                return false;
            }
            if (txtFechaResolucion.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Resolución");
                return false;
            }
            return true;
        }
        private bool GrabaResolucionInversionista()
        {
            try
            {
                if (!ValidarFormulario()) return false;
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        ObjInfo.TasaEndoso = decimal.Parse(txtTasa.Text);

                        ObjInfo.FechaEnvioAntecedentes = DateTime.Parse(txtFechaEnvioAntecedentes.Text);
                        ObjInfo.FechaResolucionInversionista = DateTime.Parse(txtFechaEnvioAntecedentes.Text);
                        ObjInfo.FechaEnvioCierre = DateTime.Parse(txtFechaEnvioAntecedentes.Text);
                        ObjInfo.FechaRecepcionCierre = DateTime.Parse(txtFechaEnvioAntecedentes.Text);
                        ObjInfo.EstadoEndoso_Id = int.Parse(ddlEstadoEndoso.SelectedValue);
                        var objResultado = new Resultado<SolicitudInfo>();
                        var objNegocio = new NegSolicitudes();

                        objResultado = objNegocio.Guardar(ref ObjInfo);
                        if (objResultado.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeExito("Se ha actualizado la Información");
                            return true;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(objResultado.Mensaje);
                            return false;
                        }
                    }
                    return false;
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Inversionista");
                }
                return false;
            }

        }



        #endregion

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}