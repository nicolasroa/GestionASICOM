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
    public partial class Visado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = false;
                CargarAcciones();
                CargarSegurosContratados();
                CargarParticipantes();
                CargarPropiedades();

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
                    if (!ValidarParticipantes()) return;
                    if (!ValidarObservaciones()) return;
                    if (!ValidarActivacion()) return;
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
        private void CargarSegurosContratados()
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                if (rSeguros.ResultadoGeneral)
                    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSegurosContratados, rSeguros.Lista, new string[] { "Id" });
                else
                    Controles.MostrarMensajeError(rSeguros.Mensaje);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Seguros");
                }
            }
        }
        public void CargarPropiedades()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedades, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });

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
        public void CargarParticipantes()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ParticipanteInfo>(ref gvParticipantes, oResultado.Lista, new string[] { Constantes.StringId, "Rut", "PorcentajeDesgravamen", "FechaIngresoDPS", "FechaAprobacionDPS" });

                    foreach (GridViewRow row in gvParticipantes.Rows)
                    {
                        Anthem.Label lblFechaIngresoDPS = (Anthem.Label)row.FindControl("lblFechaIngresoDPS");
                        Anthem.Label lblFechaAprobacionDPS = (Anthem.Label)row.FindControl("lblFechaAprobacionDPS");

                        decimal PorcentajeDesgravamen = decimal.Parse(gvParticipantes.DataKeys[row.RowIndex].Values["PorcentajeDesgravamen"].ToString());

                        if (PorcentajeDesgravamen > 0)
                        {
                            lblFechaIngresoDPS.Text = gvParticipantes.DataKeys[row.RowIndex].Values["FechaIngresoDPS"] == null ? "Pendiente" : gvParticipantes.DataKeys[row.RowIndex].Values["FechaIngresoDPS"].ToString();
                            lblFechaAprobacionDPS.Text = gvParticipantes.DataKeys[row.RowIndex].Values["FechaAprobacionDPS"] == null ? "Pendiente" : gvParticipantes.DataKeys[row.RowIndex].Values["FechaAprobacionDPS"].ToString();
                        }
                        else
                        {
                            lblFechaIngresoDPS.Text = "No Aplica";
                            lblFechaAprobacionDPS.Text = "No Aplica";
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Participantes");
                }
            }
        }
        private bool ValidarObservaciones()
        {
            try
            {
                if (NegObservaciones.lstObservacionInfo.Count(o => o.Evento_Id == NegBandejaEntrada.oBandejaEntrada.Evento_Id && o.Estado_Id == 1) != 0)
                {
                    Controles.MostrarMensajeAlerta("Se deben subsanar todas las Observaciones realizaras en el Evento para continuar");
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
                    Controles.MostrarMensajeError("Error al Validar las Observaciones");
                }
            }
            return false;
        }

        private bool ValidarParticipantes()
        {
            try
            {

                if (NegParticipante.lstParticipantes == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar los participantes de la Solicitud");
                    return false;
                }

                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 1).Count() == 0)
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Participante Principal Ingresado");
                    return false;
                }

                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 4).Count() == 0 && (NegSolicitudes.objSolicitudInfo.Destino_Id == 1 || NegSolicitudes.objSolicitudInfo.Destino_Id == 4))// Se Valida Vendedor solo para las CompraVenta
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Vendedor Ingresado");
                    return false;
                }

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);

                if (oParticipante.SeguroDesgravamen_Id == -1 && oParticipante.PorcentajeDesgravamen >= 0)
                {
                    Controles.MostrarMensajeAlerta("El Participante Principal debe Tener Seguro de Desgravamen");
                    return false;
                }

                decimal MontoTotalAseguradoDPS = decimal.Zero;


                MontoTotalAseguradoDPS = NegParticipante.lstParticipantes.Sum(p => p.MontoAseguradoDPS);

                if (MontoTotalAseguradoDPS < NegSolicitudes.objSolicitudInfo.MontoCredito)
                {
                    Controles.MostrarMensajeAlerta("EL Monto Total de las DPS es Menor al Monto del Crédito");
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
                    Controles.MostrarMensajeError("Error al Validar los Participantes");
                }
                return false;
            }
        }


        private bool ValidarActivacion()
        {
            try
            {
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();
                NegSolicitudes oNegocio = new NegSolicitudes();
                SolicitudInfo oSolicitudValidada = new SolicitudInfo();

                oResultado = oNegocio.ValidarActivacion(NegSolicitudes.objSolicitudInfo);
                if(oResultado.ResultadoGeneral)
                {
                    oSolicitudValidada = oResultado.Lista.FirstOrDefault();
                    if(oSolicitudValidada== null)
                    {
                        Controles.MostrarMensajeAlerta("Problemas al Validar la Solicitud");
                        return false;
                    }
                    else
                    {
                        if(oSolicitudValidada.ResultadoActivacion==false)
                        {
                            Controles.MostrarMensajeAlerta(oSolicitudValidada.ErrorActivacion);
                            return false;
                        }
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar la Activación");
                }
                return false;
            }
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}