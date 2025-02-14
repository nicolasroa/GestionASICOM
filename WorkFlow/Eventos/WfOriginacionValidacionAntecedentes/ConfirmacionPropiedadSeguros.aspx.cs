using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace WorkFlow.Eventos.WfOriginacionValidacionAntecedentes
{
    public partial class ConfirmacionPropiedadSeguros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioConfirmacion();");
            if (!Page.IsPostBack)
            {
                try
                {
                    CargarAcciones();
                    //CargarMotivoDesistimiento();


                    //EETT
                    CargarInstitucionesFinancieras();
                    CargarPropiedadesEETT();
                    repEETT.DataSource = GetEettRelacionadas();
                    repEETT.DataBind();
                    CargarEETTPiloto();
                    Controles.EjecutarJavaScript("DesplegarDatosEETT('0');");

                    //Tasaciones
                    rep.DataSource = GetTasacionesRelacionadas();
                    rep.DataBind();
                    CargarPropiedadesTasacion();
                    CargarTasacionesPiloto();
                    CargaComboTipoInmueble();
                    CargaComboAntiguedad();
                    CargaComboDestino();
                    CargarTipoConstruccion();
                    CargaComboInmobiliaria();
                    CargaSegIncendio();
                    Controles.EjecutarJavaScript("DesplegarDatosPropiedad('0');");

                    //DPS
                    CargarParticipantesDPS();
                    repDPS.DataSource = GetDpsRelacionadas();
                    repDPS.DataBind();
                }
                catch (Exception Ex)
                {
                    if (Constantes.ModoDebug == true)
                    {
                        Controles.MostrarMensajeError(Ex.Message, Ex);
                    }
                    else
                    {
                        Controles.MostrarMensajeError("Error al Cargar Inicio");
                    }

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
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de Desistir la Solicitud?',this);");
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
                    if (!ValidarGarantias()) return;
                    if (!ValidarParticipantes()) return;
                    if (!NegSolicitudes.RecalcularDividendo()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "D"))//DEVOLVER
                {

                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //if (!GrabarMotivoDesistimiento()) return;
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

        public bool ValidarGarantias()
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

                    if (rTasacion.Lista.Count(t => t.EstadoTasacion_Id != 3) > 0)
                    {
                        Controles.MostrarMensajeAlerta("Debe Confirmar la Tasación de la Propiedad " + rTasacion.Lista.FirstOrDefault(t => t.EstadoTasacion_Id != 3).DireccionCompleta);
                        return false;
                    }

                    if (rTasacion.Lista.Count(t => t.EstadoEstudioTitulo_Id != 3) > 0)
                    {
                        Controles.MostrarMensajeAlerta("Debe Confirmar el EETT de la Propiedad " + rTasacion.Lista.FirstOrDefault(t => t.EstadoEstudioTitulo_Id != 3).DireccionCompleta);
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
                    Controles.MostrarMensajeError("Error al Validar Garantías");
                }
                return false;
            }
        }
        public bool ValidarParticipantes()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                NegParticipante NegParticipantes = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count(t => t.EstadoDps_Id != 3) > 0)
                    {
                        Controles.MostrarMensajeAlerta("Debe Confirmar la DPS del Participante " + oResultado.Lista.FirstOrDefault(t => t.EstadoDps_Id != 3).NombreCliente);
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
                    Controles.MostrarMensajeError("Error al Validar DPS Participantes");
                }
                return false;
            }
        }


        #region Tasaciones
        private void CargarPropiedadesTasacion()
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
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedadesTasacion, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesTasaciones = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesTasaciones = rTasacion.Lista;

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

        private List<List<TasacionInfo>> GetTasacionesRelacionadas()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.SolicitudTasacion_Id = NegSolicitudes.objSolicitudInfo.Id;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    List<TasacionInfo> tasaciones = rTasacion.Lista.OrderBy(data => data.Solicitud_Id).ToList();
                    List<List<TasacionInfo>> tasacionesPorSolicitud = new List<List<TasacionInfo>>();
                    List<TasacionInfo> propiedadesSolicitud = new List<TasacionInfo>();
                    int idSolicitud = -1;
                    tasaciones.ForEach(delegate (TasacionInfo info)
                    {
                        if (idSolicitud == -1) idSolicitud = info.Solicitud_Id;
                        if (info.Solicitud_Id == idSolicitud)
                        {
                            propiedadesSolicitud.Add(info);
                        }
                        else
                        {
                            tasacionesPorSolicitud.Add(propiedadesSolicitud);
                            propiedadesSolicitud = new List<TasacionInfo>();
                            idSolicitud = info.Solicitud_Id;
                            propiedadesSolicitud.Add(info);
                        }

                    });
                    tasacionesPorSolicitud.Add(propiedadesSolicitud);


                    return tasacionesPorSolicitud;
                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                    return null;
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
                return null;
            }
        }
        private void CargarTasacionesPiloto()
        {
            try
            {
                if (NegSolicitudes.objSolicitudInfo.Proyecto_Id > 0)
                {
                    NegSolicitudes negSolicitudes = new NegSolicitudes();
                    SolicitudInfo oSolicitud = new SolicitudInfo();
                    Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();

                    oSolicitud.Estado_Id = 7; //Tasación Piloto Terminada
                    oSolicitud.Proyecto_Id = NegSolicitudes.objSolicitudInfo.Proyecto_Id;


                    rSolicitud = negSolicitudes.Buscar(oSolicitud, true);
                    if (rSolicitud.ResultadoGeneral)
                    {
                        Controles.CargarGrid<SolicitudInfo>(ref gvTasacionesPiloto, rSolicitud.Lista, new string[] { "Id" });
                    }
                    else
                    {
                        Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    }
                }
                else
                {
                    gvPropiedadesTasacion.Columns[gvPropiedadesTasacion.Columns.Count - 1].Visible = false;
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
                    Controles.MostrarMensajeError("Error al Cargar Tasaciones Piloto");
                }
            }
        }
        protected void rpt_tasaciones(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.GridView gv = (Anthem.GridView)e.Item.FindControl("gvRelacionados");
                Anthem.Label solId = (Anthem.Label)e.Item.FindControl("lblSolicitudTasacion");
                if (gv != null)
                {

                    List<TasacionInfo> drv = (List<TasacionInfo>)e.Item.DataItem;
                    if (drv.Count > 0)
                    {
                        solId.Text = drv[0].Solicitud_Id.ToString();
                        Controles.CargarGrid<TasacionInfo>(ref gv, drv, new string[] { "Id", "Propiedad_Id", "Solicitud_Id", "SolicitudTasacion_Id" });
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                gv.Rows[i].Cells[gv.Rows[i].Cells.Count - 1].RowSpan = gv.Rows.Count;
                            }
                            else
                            {
                                gv.Rows[i].Cells[gv.Rows[0].Cells.Count - 1].Visible = false;
                            }

                        }
                        if (drv[0].EstadoTasacion_Id == 4)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarData");
                            btn.Text = "Importado";
                            btn.CssClass = "btnsmall btn-secondary";
                            btn.Enabled = false;
                        }
                        else if (drv[0].EstadoTasacion_Id != 3)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarData");
                            btn.Text = "EN FLUJO";
                            btn.CssClass = "btnsmall btn-primary";
                            btn.Enabled = false;
                        }
                    }

                }
            }
        }
        public bool ImportarDataTasacion(int Solicitud_Id, int SolicitudTasacion_Id)
        {
            try
            {
                NegPropiedades nPropiedad = new NegPropiedades();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();


                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();
                Resultado<TasacionInfo> rTasacionGRB = new Resultado<TasacionInfo>();
                oTasacion.Solicitud_Id = Solicitud_Id;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                bool okData = true;
                List<TasacionInfo> respaldoPropiedades = NegPropiedades.lstPropiedadesTasaciones;
                if (rTasacion.ResultadoGeneral)
                {
                    rTasacion.Lista.ForEach(delegate (TasacionInfo tasacionIndividual)
                    {
                        TasacionInfo tasacionSolicitud = NegPropiedades.lstPropiedadesTasaciones.Where(x => x.Solicitud_Id == SolicitudTasacion_Id && x.Propiedad_Id == tasacionIndividual.Propiedad_Id).FirstOrDefault(); //tasacion.Propiedad_Id
                        if (tasacionSolicitud != null && okData)
                        {
                            tasacionSolicitud.ValorComercial = tasacionIndividual.ValorComercial;
                            tasacionSolicitud.MontoTasacion = tasacionIndividual.MontoTasacion;
                            tasacionSolicitud.MontoAsegurado = tasacionIndividual.MontoAsegurado;
                            tasacionSolicitud.MontoLiquidacion = tasacionIndividual.MontoLiquidacion;
                            tasacionSolicitud.MetrosTerreno = tasacionIndividual.MetrosTerreno;
                            tasacionSolicitud.MetrosConstruidos = tasacionIndividual.MetrosConstruidos;
                            tasacionSolicitud.MetrosLogia = tasacionIndividual.MetrosLogia;
                            tasacionSolicitud.MetrosTerraza = tasacionIndividual.MetrosTerraza;
                            tasacionSolicitud.FechaTasacion = tasacionIndividual.FechaTasacion;
                            tasacionSolicitud.FechaRecepcionFinal = tasacionIndividual.FechaRecepcionFinal;
                            tasacionSolicitud.PermisoEdificacion = tasacionIndividual.PermisoEdificacion;
                            tasacionSolicitud.Seguro_Id = tasacionIndividual.Seguro_Id;
                            tasacionSolicitud.PrimaSeguro = (tasacionIndividual.TasaSeguro * tasacionIndividual.MontoAsegurado);
                            tasacionSolicitud.EstadoTasacion_Id = tasacionIndividual.EstadoTasacion_Id;
                            tasacionSolicitud.TasaSeguro = tasacionIndividual.TasaSeguro;
                            tasacionSolicitud.Tasador_Id = tasacionIndividual.Tasador_Id;
                            tasacionSolicitud.FabricaTasador_Id = tasacionIndividual.FabricaTasador_Id;
                            tasacionSolicitud.IndTasacionPiloto = false;


                            rTasacionGRB = nTasacion.GuardarTasacion(tasacionSolicitud);
                            if (!rTasacionGRB.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError("Error al Importar Tasación");
                                okData = false;
                            }
                            else
                            {
                                tasacionIndividual.EstadoTasacion_Id = 4;
                                nTasacion.GuardarTasacion(tasacionIndividual);
                                ActualizarSegurosContratadosTasacion(tasacionSolicitud);
                            }

                        }
                        else
                        {
                            Controles.MostrarMensajeError("Error al validar Propiedad de Tasación");
                            okData = false;
                        }
                    });
                }
                if (okData) return true;
                else
                {
                    // respaldoPropiedades foreach GuardarTasacion()
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
                    Controles.MostrarMensajeError("Error al Importar datos de tasación");
                }
                return false;
            }
        }

        protected void btnImportarDataTasacion_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            GridView gv = ((Button)sender).Parent.Parent.Parent.Parent as GridView;
            int Solicitud_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
            int SolicitudTasacion_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["SolicitudTasacion_Id"].ToString());
            bool result = ImportarDataTasacion(Solicitud_Id, SolicitudTasacion_Id);
            if (result)
            {
                CargarPropiedadesTasacion();

                ((Button)sender).Enabled = false;
                ((Button)sender).CssClass = "btnsmall btn-secondary";
                ((Button)sender).Text = "Importado";


            }
        }
        protected void btnVerTasacionPiloto_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            GridView gv = ((Button)sender).Parent.Parent.Parent.Parent as GridView;
            int Solicitud_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["Id"].ToString());

            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            NegPortal.getsetDatosWorkFlow.Solicitud_Id = Solicitud_Id;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Informe de Tasación Piloto");
        }
        protected void btnModificarProp_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedadesTasacion.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            TasacionInfo oTasacionPadre = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objTasacion = new TasacionInfo();
            NegPropiedades.objTasacion = oTasacion;
            oTasacionPadre = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == oTasacion.TasacionPadre_Id);
            CargarDatosTasacion(oTasacion, oTasacionPadre);
            Controles.EjecutarJavaScript("DesplegarDatosPropiedad('1');");
        }
        private void CargarDatosTasacion(TasacionInfo oTasacion, TasacionInfo oTasacionPadre)
        {
            try
            {

                if (oTasacion.PorcentajeSeguroIncendio == 0)
                {
                    ddlSeguroIncendio.ClearSelection();
                    SeleccionarSeguroIncendio();
                    ddlSeguroIncendio.Enabled = false;
                }
                else
                {
                    ddlSeguroIncendio.SelectedValue = oTasacion.Seguro_Id.ToString();
                    ddlSeguroIncendio.Enabled = true;
                    txtTasaSeguroIncendio.Text = (oTasacion.TasaSeguro * 100).ToString();
                    txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado.ToString();
                    txtPrimaSeguroIncendio.Text = oTasacion.PrimaSeguro.ToString();
                }
                lblDireccion.Text = oTasacion.DireccionCompleta;

                if (oTasacion != null)
                {

                    NegComunas nComuna = new NegComunas();
                    Resultado<ComunaSiiInfo> rComuna = new Resultado<ComunaSiiInfo>();
                    ComunaSiiInfo oComuna = new ComunaSiiInfo();

                    NegPropiedades nPropiedades = new NegPropiedades();
                    Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                    PropiedadInfo oPropiedadPadre = new PropiedadInfo();
                    PropiedadInfo oPropiedad = new PropiedadInfo();



                    if (oTasacionPadre != null)
                    {
                        oPropiedadPadre.Id = oTasacion.Propiedad_Id;
                        rPropiedad = nPropiedades.BuscarPropiedad(oPropiedadPadre);
                        if (!rPropiedad.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rPropiedad.Mensaje);
                            return;
                        }

                        oPropiedadPadre = rPropiedad.Lista.FirstOrDefault();
                        NegPropiedades.objPropiedad = new PropiedadInfo();
                        NegPropiedades.objPropiedad = oPropiedadPadre;
                        //Datos Propiedad
                        oComuna.Descripcion = oPropiedadPadre.DescripcionComuna;
                        rComuna = nComuna.BuscarComunaSii(oComuna);
                        if (rComuna.ResultadoGeneral)
                        {
                            if (rComuna.Lista.Count == 1)
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "", "");
                            else
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
                        }
                        ddlInmobiliaria.SelectedValue = oPropiedadPadre.Inmobiliaria_Id.ToString();
                        CargaComboProyecto(oPropiedadPadre.Inmobiliaria_Id);
                        ddlProyecto.SelectedValue = oPropiedadPadre.Proyecto_Id.ToString();
                        chIndDfl2.Checked = oPropiedadPadre.IndDfl2;
                        txtAñoConstruccion.Text = oPropiedadPadre.AñoConstruccion == 0 ? "" : oPropiedadPadre.AñoConstruccion.ToString();
                        ddlTipoConstruccion.SelectedValue = oPropiedadPadre.TipoConstruccion_Id.ToString();
                        txtRolManzana.Text = oPropiedadPadre.RolManzana == 0 ? "" : oPropiedadPadre.RolManzana.ToString();
                        txtRolSitio.Text = oPropiedadPadre.RolSitio == 0 ? "" : oPropiedadPadre.RolSitio.ToString();
                        chkIndUsoGoce.Checked = oPropiedadPadre.IndUsoGoce == true ? true : false;
                        ProcesarUsoGoce(chkIndUsoGoce.Checked);
                        if (oPropiedadPadre.ComunaSii_Id > 0)
                            ddlComunaSii.SelectedValue = oPropiedadPadre.ComunaSii_Id.ToString();
                        ddlTipoInmueble.SelectedValue = oPropiedadPadre.TipoInmueble_Id.ToString();
                        ddlDestinoProp.SelectedValue = oPropiedadPadre.Destino_Id.ToString();
                        ddlAntiguedadProp.SelectedValue = oPropiedadPadre.Antiguedad_Id.ToString();

                        //Datos Tasacion
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);
                        txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);

                        //Datos Heredador de la Tasación Padre
                        txtFechaTasacion.Text = oTasacionPadre.FechaTasacion == null ? "" : oTasacionPadre.FechaTasacion.Value.ToShortDateString();
                        txtFechaRecepcionFinal.Text = oTasacionPadre.FechaRecepcionFinal == null ? "" : oTasacionPadre.FechaRecepcionFinal.Value.ToShortDateString();
                        txtPermisoEdificacion.Text = oTasacionPadre.PermisoEdificacion;
                    }
                    else
                    {



                        oPropiedad.Id = oTasacion.Propiedad_Id;
                        rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                        if (!rPropiedad.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rPropiedad.Mensaje);
                            return;
                        }
                        oPropiedad = rPropiedad.Lista.FirstOrDefault();
                        NegPropiedades.objPropiedad = new PropiedadInfo();
                        NegPropiedades.objPropiedad = oPropiedad;

                        //Datos de la Propiedad

                        //Datos Propiedad
                        oComuna.Descripcion = oPropiedad.DescripcionComuna;
                        rComuna = nComuna.BuscarComunaSii(oComuna);
                        if (rComuna.ResultadoGeneral)
                        {
                            if (rComuna.Lista.Count == 1)
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "", "");
                            else
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
                        }
                        ddlInmobiliaria.SelectedValue = oPropiedad.Inmobiliaria_Id.ToString();
                        CargaComboProyecto(oPropiedad.Inmobiliaria_Id);
                        ddlProyecto.SelectedValue = oPropiedad.Proyecto_Id.ToString();
                        chIndDfl2.Checked = oPropiedad.IndDfl2;
                        txtAñoConstruccion.Text = oPropiedad.AñoConstruccion == 0 ? "" : oPropiedad.AñoConstruccion.ToString();
                        ddlTipoConstruccion.SelectedValue = oPropiedad.TipoConstruccion_Id.ToString();
                        txtRolManzana.Text = oPropiedad.RolManzana == 0 ? "" : oPropiedad.RolManzana.ToString();
                        txtRolSitio.Text = oPropiedad.RolSitio == 0 ? "" : oPropiedad.RolSitio.ToString();
                        chkIndUsoGoce.Checked = oPropiedad.IndUsoGoce == true ? true : false;
                        ProcesarUsoGoce(chkIndUsoGoce.Checked);
                        if (oPropiedad.ComunaSii_Id > 0)
                            ddlComunaSii.SelectedValue = oPropiedad.ComunaSii_Id.ToString();
                        ddlTipoInmueble.SelectedValue = oPropiedad.TipoInmueble_Id.ToString();
                        ddlDestinoProp.SelectedValue = oPropiedad.Destino_Id.ToString();
                        ddlAntiguedadProp.SelectedValue = oPropiedad.Antiguedad_Id.ToString();





                        //Datos de la Tasacion
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);
                        txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtFechaTasacion.Text = oTasacion.FechaTasacion == null ? "" : oTasacion.FechaTasacion.Value.ToShortDateString();
                        txtFechaRecepcionFinal.Text = oTasacion.FechaRecepcionFinal == null ? "" : oTasacion.FechaRecepcionFinal.Value.ToShortDateString();
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);
                        txtPermisoEdificacion.Text = oTasacion.PermisoEdificacion;

                    }
                }
                NegPropiedades.objTasacion = new TasacionInfo();
                NegPropiedades.objTasacion = oTasacion;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Llenar el Formulario");
                }
            }








        }
        private void CargaSegIncendio()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 1;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroIncendio, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Incendio--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Incendio");
                }
            }
        }
        private void SeleccionarSeguroIncendio()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroIncendio.SelectedValue == "-1")
                {
                    txtTasaSeguroIncendio.Text = "0";
                    txtPrimaSeguroIncendio.Text = "0";
                }
                else
                {
                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * NegPropiedades.objTasacion.MontoAsegurado);

                        txtPrimaSeguroIncendio.Text = string.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroIncendio.Text = string.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
                    }
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
                    Controles.MostrarMensajeError("Error al Seleccionar el Seguro de Incendio");
                }
            }
        }

        protected void ddlSeguroIncendio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroIncendio();
        }
        protected void btnGuardarProp_Click(object sender, EventArgs e)
        {
            GrabarPropiedadTasacion();
        }
        protected void btnCancelarProp_Click(object sender, EventArgs e)
        {
            LimpiarFormularioPropiedadTasacion();

        }

        private PropiedadInfo DatosEntidadPropiedad(PropiedadInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<PropiedadInfo>();
                var oInfo = new PropiedadInfo();
                var oNeg = new NegPropiedades();

                oResultado = oNeg.BuscarPropiedad(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oInfo = oResultado.Lista.FirstOrDefault();

                    if (oInfo != null)
                    {
                        return oInfo;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Propiedad");
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Propiedad");

                }
                return null;
            }

        }
        private void GrabarPropiedadTasacion()
        {
            try
            {
                if (!ValidarFormularioPropiedadTasacion()) return;

                NegPropiedades nPropiedad = new NegPropiedades();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();

                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                if (NegPropiedades.objTasacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }


                oPropiedad.Id = NegPropiedades.objTasacion.Propiedad_Id;
                oPropiedad = DatosEntidadPropiedad(oPropiedad);
                oPropiedad.RolManzana = txtRolManzana.Text == "" ? 0 : int.Parse(txtRolManzana.Text);
                oPropiedad.RolSitio = txtRolSitio.Text == "" ? 0 : int.Parse(txtRolSitio.Text);
                oPropiedad.IndUsoGoce = chkIndUsoGoce.Checked;
                oPropiedad.ComunaSii_Id = int.Parse(ddlComunaSii.SelectedValue);
                oPropiedad.TipoInmueble_Id = int.Parse(ddlTipoInmueble.SelectedValue);
                oPropiedad.Antiguedad_Id = int.Parse(ddlAntiguedadProp.SelectedValue);
                oPropiedad.Destino_Id = int.Parse(ddlDestinoProp.SelectedValue);
                oPropiedad.TipoConstruccion_Id = int.Parse(ddlTipoConstruccion.SelectedValue);
                oPropiedad.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oPropiedad.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);
                oPropiedad.IndDfl2 = chIndDfl2.Checked;
                oPropiedad.AñoConstruccion = int.Parse(txtAñoConstruccion.Text);

                rPropiedad = nPropiedad.GuardarPropiedad(ref oPropiedad);

                if (rPropiedad.ResultadoGeneral)
                {
                    NegPropiedades.objTasacion.ValorComercial = decimal.Parse(txtValorComercial.Text);
                    NegPropiedades.objTasacion.MontoTasacion = decimal.Parse(txtMontoTasacion.Text);
                    NegPropiedades.objTasacion.MontoAsegurado = decimal.Parse(txtMontoAseguradoIncendio.Text);
                    NegPropiedades.objTasacion.MontoLiquidacion = decimal.Parse(txtMontoLiquidacion.Text);
                    NegPropiedades.objTasacion.MetrosConstruidos = decimal.Parse(txtMetrosConstruccion.Text);
                    NegPropiedades.objTasacion.MetrosTerreno = txtMetrosTerreno.Text == "" ? 0 : decimal.Parse(txtMetrosTerreno.Text);
                    NegPropiedades.objTasacion.MetrosLogia = txtMetrosLogia.Text == "" ? 0 : decimal.Parse(txtMetrosLogia.Text);
                    NegPropiedades.objTasacion.MetrosTerraza = txtMetrosTerraza.Text == "" ? 0 : decimal.Parse(txtMetrosTerraza.Text);

                    NegPropiedades.objTasacion.FechaTasacion = DateTime.Parse(txtFechaTasacion.Text);
                    NegPropiedades.objTasacion.FechaRecepcionFinal = DateTime.Parse(txtFechaRecepcionFinal.Text);
                    NegPropiedades.objTasacion.PermisoEdificacion = txtPermisoEdificacion.Text;
                    NegPropiedades.objTasacion.PrimaSeguro = (NegPropiedades.objTasacion.TasaSeguro * NegPropiedades.objTasacion.MontoAsegurado);

                    NegPropiedades.objTasacion.Seguro_Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    NegPropiedades.objTasacion.MontoAsegurado = txtMontoAseguradoIncendio.Text == "" ? 0 : decimal.Parse(txtMontoAseguradoIncendio.Text);
                    NegPropiedades.objTasacion.TasaSeguro = txtTasaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtTasaSeguroIncendio.Text) / 100;
                    NegPropiedades.objTasacion.PrimaSeguro = txtPrimaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtPrimaSeguroIncendio.Text);
                    NegPropiedades.objTasacion.EstadoTasacion_Id = 3;//Tasación Terminada
                    NegPropiedades.objTasacion.IndTasacionPiloto = true;

                    rTasacion = nTasacion.GuardarTasacion(NegPropiedades.objTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {

                        if (!NegSolicitudes.ActualizarSolicitud(NegSolicitudes.objSolicitudInfo)) return;
                        ActualizarSegurosContratadosTasacion(oTasacion);
                        CargarPropiedadesTasacion();
                        LimpiarFormularioPropiedadTasacion();
                        Controles.MostrarMensajeExito("Datos de la Propiedad Grabados");
                    }
                    else
                    {
                        Controles.MostrarMensajeError(rTasacion.Mensaje);
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar la Propiedad");
                }
            }
        }
        private bool ValidarFormularioPropiedadTasacion()
        {
            try
            {
                if (NegPropiedades.objTasacion.PorcentajeSeguroIncendio > 0 && ddlSeguroIncendio.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Incendio");
                    return false;
                }

                var ValorComercial = decimal.Zero;
                var MontoTasacion = decimal.Zero;
                var MontoAsegurado = decimal.Zero;
                var MontoLiquidacion = decimal.Zero;
                var MetrosContruidos = decimal.Zero;
                var MetrosTerreno = decimal.Zero;
                var MetrosLogia = decimal.Zero;
                var MetrosTerraza = decimal.Zero;
                var RolManzana = decimal.Zero;
                var RolSitio = decimal.Zero;
                var AñoConstruccion = 0;

                if (txtFechaTasacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Tasación");
                    return false;
                }
                if (txtFechaRecepcionFinal.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Recepción Final");
                    return false;
                }
                if (txtPermisoEdificacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Permiso de Edificación");
                    return false;
                }
                if (txtAñoConstruccion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Año de Construcción");
                    return false;
                }
                if (int.TryParse(txtAñoConstruccion.Text, out AñoConstruccion))
                {
                    if (AñoConstruccion > DateTime.Now.Year)
                    {
                        Controles.MostrarMensajeAlerta("El año de Construicción no puede ser mayor al Año en curso");
                        return false;
                    }
                    if (AñoConstruccion < 1900)
                    {
                        Controles.MostrarMensajeAlerta("Año de Construcción Invalido (Debe se entre los años 1900 y " + DateTime.Now.Year.ToString() + ")");
                        return false;
                    }
                }

                if (txtValorComercial.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor Comercial");
                    return false;
                }
                if (decimal.TryParse(txtValorComercial.Text, out ValorComercial))
                {
                    if (ValorComercial <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Valor Comercial debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMontoTasacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto de Tasación");
                    return false;
                }
                if (decimal.TryParse(txtMontoTasacion.Text, out MontoTasacion))
                {
                    if (MontoTasacion <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Monto de Tasación debe ser mayor a 0");
                        return false;
                    }
                }



                if (txtMontoAseguradoIncendio.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto Asegurado");
                    return false;
                }
                if (decimal.TryParse(txtMontoAseguradoIncendio.Text, out MontoAsegurado))
                {
                    if (MontoAsegurado <= 0 && txtMontoAseguradoIncendio.Enabled == true && chkIndUsoGoce.Checked == false)
                    {
                        Controles.MostrarMensajeAlerta("El Monto Asegurado debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMontoLiquidacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto de Liquidación");
                    return false;
                }
                if (decimal.TryParse(txtMontoLiquidacion.Text, out MontoLiquidacion))
                {
                    if (MontoLiquidacion <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Monto de Liquidación debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMetrosTerreno.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a los Metros del Terreno");
                    return false;
                }
                if (decimal.TryParse(txtMetrosTerreno.Text, out MetrosTerreno))
                {
                    if (MetrosTerreno <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros del Terreno deben ser mayor a 0");
                        return false;
                    }
                }

                if (txtMetrosConstruccion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a los Metros Construidos");
                    return false;
                }
                if (decimal.TryParse(txtMetrosConstruccion.Text, out MetrosContruidos))
                {
                    if (MetrosContruidos <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros Construidos deben ser mayor a 0");
                        return false;
                    }
                }

                if (txtRolManzana.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a Rol - Manzana");
                    return false;
                }
                if (decimal.TryParse(txtRolManzana.Text, out RolManzana))
                {
                    if (RolManzana <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Rol - Manzana deben ser mayor a 0");
                        return false;
                    }
                }
                if (txtRolSitio.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a Rol - Sitio");
                    return false;
                }
                if (decimal.TryParse(txtRolSitio.Text, out RolSitio))
                {
                    if (RolSitio <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Rol - Sitio deben ser mayor a 0");
                        return false;
                    }
                }

                if (decimal.TryParse(txtMetrosLogia.Text, out MetrosLogia))
                {
                    if (MetrosLogia <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros de la Logia deben ser mayor a 0");
                        return false;
                    }
                }

                if (decimal.TryParse(txtMetrosTerraza.Text, out MetrosTerraza))
                {
                    if (MetrosTerraza <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros de la Terraza deben ser mayor a 0");
                        return false;
                    }
                }
                if (ddlComunaSii.SelectedValue == "-1" || ddlComunaSii.SelectedValue == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Comuna - SII");
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
                    Controles.MostrarMensajeError("Error al Validar el Formulario de la Propiedad");
                }
                return false;
            }
        }
        private void LimpiarFormularioPropiedadTasacion()
        {
            NegPropiedades.objTasacion = null;

            ddlSeguroIncendio.ClearSelection();
            txtTasaSeguroIncendio.Text = "0";
            txtMontoAseguradoIncendio.Text = "0";
            txtPrimaSeguroIncendio.Text = "";
            ddlSeguroIncendio.Enabled = false;
            lblDireccion.Text = "";
            Controles.EjecutarJavaScript("DesplegarDatosPropiedad('0');");

        }
        private void ActualizarSegurosContratadosTasacion(TasacionInfo oTasacion)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();
                //Actualizacion del Seguro de Desgravamen

                if (ddlSeguroIncendio.Enabled)
                {
                    oSeguros = new SegurosContratadosInfo();
                    oSeguros.TipoSeguro_Id = 1;//Incendio
                    oSeguros.Solicitud_Id = oTasacion.Solicitud_Id;
                    oSeguros.Seguro_Id = oTasacion.Seguro_Id;
                    oSeguros.Tasacion_Id = oTasacion.Id;
                    oSeguros.MontoAsegurado = oTasacion.MontoAsegurado;
                    oSeguros.TasaMensual = oTasacion.TasaSeguro;
                    oSeguros.PrimaMensual = oTasacion.PrimaSeguro;
                    rSeguros = nSeguros.GuardarSegurosContratados(oSeguros);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return;
                    }
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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
            }
        }


        protected void chkIndUsoGoce_CheckedChanged(object sender, EventArgs e)
        {
            ProcesarUsoGoce(chkIndUsoGoce.Checked);
        }

        protected void ProcesarUsoGoce(bool IndUsoGoce)
        {
            if (IndUsoGoce)
            {
                txtRolManzana.Enabled = false;
                txtRolManzana.Text = "";
                txtRolSitio.Enabled = false;
                txtRolSitio.Text = "";
                txtMontoAseguradoIncendio.Text = "0";
            }
            else
            {
                txtRolManzana.Enabled = true;
                txtRolSitio.Enabled = true;
                txtRolManzana.Text = NegPropiedades.objPropiedad.RolManzana.ToString();
                txtRolSitio.Text = NegPropiedades.objPropiedad.RolSitio.ToString();
                txtMontoAseguradoIncendio.Text = NegPropiedades.objTasacion.MontoAsegurado.ToString();
                SeleccionarSeguroIncendio();
            }
        }
        private void CargaComboTipoInmueble()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TipoInmuebleInfo();
                var ObjResultado = new Resultado<TipoInmuebleInfo>();
                var objNegocio = new NegPropiedades();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.VisibleEnSimulador = true;
                ObjResultado = objNegocio.BuscarTipoInmueble(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoInmuebleInfo>(ref ddlTipoInmueble, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Tipo Inmueble--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Tipo Inmueble");
                }
            }
        }
        private void CargaComboAntiguedad()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("ANTIGUEDAD_PROPIEDAD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlAntiguedadProp, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Antigüedad --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Antigüedad");
                }
            }
        }
        private void CargaComboDestino()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("DESTINO_PROP");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlDestinoProp, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Destino --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Destino Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Destino");
                }
            }
        }
        private void CargarTipoConstruccion()
        {
            try
            {
                NegPropiedades nProp = new NegPropiedades();
                TipoConstruccionInfo oTipoConstruccion = new TipoConstruccionInfo();
                Resultado<TipoConstruccionInfo> rTipoConstruccion = new Resultado<TipoConstruccionInfo>();


                oTipoConstruccion.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rTipoConstruccion = nProp.BuscarTipoConstruccion(oTipoConstruccion);
                if (rTipoConstruccion.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoConstruccionInfo>(ref ddlTipoConstruccion, rTipoConstruccion.Lista, "Id", "Descripcion", "-- Tipo de Construcción --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoConstruccion.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Tipo de Construcción");
                }
            }
        }
        private void CargaComboInmobiliaria()
        {
            try
            {
                ////Declaracion de Variables
                var objInmobiliaria = new InmobiliariaInfo();
                var objResultado = new Resultado<InmobiliariaInfo>();
                var NegInmobiliaria = new NegInmobiliarias();

                ////Asignacion de Variables
                objInmobiliaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                objResultado = NegInmobiliaria.Buscar(objInmobiliaria);
                if (objResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(objResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void CargaComboProyecto(int Inmbobiliaria_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProyectoInfo();
                var ObjResultado = new Resultado<ProyectoInfo>();
                var NegProyecto = new NegProyectos();

                ////Asignacion de Variables
                ObjInfo.Inmobiliaria_Id = Inmbobiliaria_Id;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = NegProyecto.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Proyecto");
                }
            }
        }
        protected void ddlInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Seleccione una Inmobiliaria", "-1");
            }
            else
            {
                CargaComboProyecto(int.Parse(ddlInmobiliaria.SelectedValue));
            }
        }
        #endregion

        #region Estudio de Titulo
        private void CargarPropiedadesEETT()
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
                    Controles.CargarGrid<TasacionInfo>(ref gvEETT, rTasacion.Lista.Where(t => t.IndPropiedadPrincipal == true).ToList(), new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesEETT = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesEETT = rTasacion.Lista.Where(t => t.IndPropiedadPrincipal == true).ToList();

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
        private List<List<TasacionInfo>> GetEettRelacionadas()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.SolicitudEstudioTitulo_Id = NegSolicitudes.objSolicitudInfo.Id;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    List<TasacionInfo> tasaciones = rTasacion.Lista.OrderBy(data => data.Solicitud_Id).ToList();
                    List<List<TasacionInfo>> eettPorSolicitud = new List<List<TasacionInfo>>();
                    List<TasacionInfo> propiedadesSolicitud = new List<TasacionInfo>();
                    int idSolicitud = -1;
                    tasaciones.ForEach(delegate (TasacionInfo info)
                    {
                        if (idSolicitud == -1) idSolicitud = info.Solicitud_Id;
                        if (info.Solicitud_Id == idSolicitud)
                        {
                            propiedadesSolicitud.Add(info);
                        }
                        else
                        {
                            eettPorSolicitud.Add(propiedadesSolicitud);
                            propiedadesSolicitud = new List<TasacionInfo>();
                            idSolicitud = info.Solicitud_Id;
                            propiedadesSolicitud.Add(info);
                        }

                    });
                    eettPorSolicitud.Add(propiedadesSolicitud);


                    return eettPorSolicitud;
                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                    return null;
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
                return null;
            }
        }
        protected void rpt_eett(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.GridView gv = (Anthem.GridView)e.Item.FindControl("gvRelacionadosEETT");
                Anthem.Label lblSolicitudEETT = (Anthem.Label)e.Item.FindControl("lblSolicitudEETT");
                if (gv != null)
                {

                    List<TasacionInfo> drv = (List<TasacionInfo>)e.Item.DataItem;
                    if (drv.Count > 0)
                    {
                        lblSolicitudEETT.Text = drv[0].Solicitud_Id.ToString();
                        Controles.CargarGrid<TasacionInfo>(ref gv, drv, new string[] { "Id", "Propiedad_Id", "Solicitud_Id", "SolicitudEstudioTitulo_Id" });
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                gv.Rows[i].Cells[gv.Rows[i].Cells.Count - 1].RowSpan = gv.Rows.Count;
                            }
                            else
                            {
                                gv.Rows[i].Cells[gv.Rows[0].Cells.Count - 1].Visible = false;
                            }

                        }
                        if (drv[0].EstadoEstudioTitulo_Id == 4)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarDataEETT");
                            btn.Text = "Importado";
                            btn.CssClass = "btnsmall btn-secondary";
                            btn.Enabled = false;
                        }
                        else if (drv[0].EstadoEstudioTitulo_Id != 3)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarDataEETT");
                            btn.Text = "EN FLUJO";
                            btn.CssClass = "btnsmall btn-primary";
                            btn.Enabled = false;
                        }
                    }

                }
            }
        }
        public bool ImportarDataEett(int Solicitud_Id, int SolicitudEstudioTitulo_Id)
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();
                Resultado<TasacionInfo> rTasacionGRB = new Resultado<TasacionInfo>();
                oTasacion.Solicitud_Id = Solicitud_Id;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                bool okData = true;
                List<TasacionInfo> respaldoPropiedades = NegPropiedades.lstPropiedadesEETT;
                if (rTasacion.ResultadoGeneral)
                {
                    rTasacion.Lista.ForEach(delegate (TasacionInfo tasacion)
                    {
                        TasacionInfo propiedadSolicitud = NegPropiedades.lstPropiedadesEETT.Where(x => x.Solicitud_Id == SolicitudEstudioTitulo_Id && x.Propiedad_Id == tasacion.Propiedad_Id).FirstOrDefault(); //tasacion.Propiedad_Id
                        if (propiedadSolicitud != null && okData)
                        {
                            propiedadSolicitud.IndAlzamientoHipoteca = tasacion.IndAlzamientoHipoteca;
                            propiedadSolicitud.InstitucionAlzamientoHipoteca_Id = tasacion.InstitucionAlzamientoHipoteca_Id;
                            propiedadSolicitud.EstadoEstudioTitulo_Id = tasacion.EstadoEstudioTitulo_Id;
                            propiedadSolicitud.IndEstudioTituloPiloto = false;

                            rTasacionGRB = nTasacion.GuardarTasacion(propiedadSolicitud);
                            if (!rTasacionGRB.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError("Error al Importar Estudio de Títulos");
                                okData = false;
                            }
                            else
                            {
                                tasacion.EstadoTasacion_Id = 4;
                                nTasacion.GuardarTasacion(tasacion);
                            }
                        }
                        else
                        {
                            Controles.MostrarMensajeError("Error al validar Propiedad de Estudio de Títulos");
                            okData = false;
                        }
                    });
                }
                if (okData) return true;
                else
                {
                    // respaldoPropiedades foreach GuardarTasacion()
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
                    Controles.MostrarMensajeError("Error al Importar datos de tasación");
                }
                return false;
            }
        }
        protected void btnImportarDataEETT_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            GridView gv = ((Button)sender).Parent.Parent.Parent.Parent as GridView;
            int Solicitud_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
            int SolicitudEstudioTitulo_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["SolicitudEstudioTitulo_Id"].ToString());
            bool result = ImportarDataEett(Solicitud_Id, SolicitudEstudioTitulo_Id);
            if (result)
            {
                CargarPropiedadesEETT();

                ((Button)sender).Enabled = false;
                ((Button)sender).CssClass = "btnsmall btn-secondary";
                ((Button)sender).Text = "Importado";


            }
        }

        private void CargarEETTPiloto()
        {
            try
            {

                if (NegSolicitudes.objSolicitudInfo.Proyecto_Id > 0)// Solo solicitudes con Proyecto Inmobiliario
                {
                    NegSolicitudes negSolicitudes = new NegSolicitudes();
                    SolicitudInfo oSolicitud = new SolicitudInfo();
                    Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();

                    NegPropiedades nTasaciones = new NegPropiedades();
                    TasacionInfo oTasacion = new TasacionInfo();
                    Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                    oSolicitud.Estado_Id = 9; //EETT Piloto Terminada
                    oSolicitud.Proyecto_Id = NegSolicitudes.objSolicitudInfo.Proyecto_Id;
                    List<TasacionInfo> lstEstudiosPiloto = new List<TasacionInfo>();

                    rSolicitud = negSolicitudes.Buscar(oSolicitud, true);
                    if (rSolicitud.ResultadoGeneral)
                    {

                        foreach (var solicitudEETT in rSolicitud.Lista)
                        {
                            oTasacion.Solicitud_Id = solicitudEETT.Id;
                            rTasacion = nTasaciones.BuscarTasacion(oTasacion);
                            if (rTasacion.ResultadoGeneral)
                                lstEstudiosPiloto.AddRange(rTasacion.Lista);
                            else
                            {
                                Controles.MostrarMensajeError(rTasacion.Mensaje);
                                return;
                            }
                        }
                        Controles.CargarGrid<TasacionInfo>(ref gvEETTPiloto, lstEstudiosPiloto, new string[] { "Id", "Solicitud_Id" });
                    }
                    else
                    {
                        Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    }
                }
                else
                {
                    gvEETT.Columns[gvEETT.Columns.Count - 1].Visible = false;
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
                    Controles.MostrarMensajeError("Error al Cargar Tasaciones Piloto");
                }
            }
        }

        protected void btnVerEETTPiloto_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            GridView gv = ((Button)sender).Parent.Parent.Parent.Parent as GridView;
            int Solicitud_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());

            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            NegPortal.getsetDatosWorkFlow.Solicitud_Id = Solicitud_Id;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Informe de EETT Piloto");




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
        protected void btnCancelarAlzamiento_Click(object sender, EventArgs e)
        {
            LimpiarAlzamientoEETT();
        }
        private void GrabarAlzamientoEETT()
        {
            try
            {
                if (NegPropiedades.objEETT == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }
                if (!ValidarAlzamientoEETT()) return;

                NegPropiedades nTasacion = new NegPropiedades();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                if (chkIndAlzamientoHipoteca.Visible)
                {
                    NegPropiedades.objEETT.IndAlzamientoHipoteca = chkIndAlzamientoHipoteca.Checked;
                    NegPropiedades.objEETT.InstitucionAlzamientoHipoteca_Id = int.Parse(ddlInstitucionAlzamientoHipoteca.SelectedValue);
                    NegPropiedades.objEETT.IndEstudioTituloPiloto = true;
                    NegPropiedades.objEETT.EstadoEstudioTitulo_Id = 3;//EETT Terminado

                    rTasacion = nTasacion.GuardarTasacion(NegPropiedades.objEETT);
                    if (!rTasacion.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rTasacion.Mensaje);
                        return;
                    }
                }


                Controles.MostrarMensajeExito("Alzamiento Actualizado");
                CargarPropiedadesEETT();
                LimpiarAlzamientoEETT();


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
            }
        }

        private void LimpiarAlzamientoEETT()
        {
            lblDireccionEETT.Text = "";
            NegPropiedades.objEETT = null;
            chkIndAlzamientoHipoteca.Checked = false;
            ddlInstitucionAlzamientoHipoteca.ClearSelection();
            Controles.EjecutarJavaScript("DesplegarDatosEETT('0');");
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
                    Controles.MostrarMensajeError("Error al Validar la Información de EETT");
                }
                return false;
            }
        }
        protected void btnSeleccionarPropEETT_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvEETT.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesEETT.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objEETT = new TasacionInfo();
            NegPropiedades.objEETT = oTasacion;
            CargarInfoEstudioTitulo(oTasacion);
        }
        private void CargarInfoEstudioTitulo(TasacionInfo oEETT)
        {
            try
            {
                Controles.EjecutarJavaScript("DesplegarDatosEETT('1');");
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
        #endregion

        #region DPS
        public void CargarParticipantesDPS()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                NegParticipante NegParticipantes = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    NegParticipante.lstParticipantesDPS = new List<ParticipanteInfo>();
                    NegParticipante.lstParticipantesDPS = oResultado.Lista;
                    Controles.CargarGrid(ref gvParticipantesDPS, oResultado.Lista.Where(p => p.PorcentajeDesgravamen > 0).ToList(), new string[] { Constantes.StringId, "Rut" });
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
        private List<List<ParticipanteInfo>> GetDpsRelacionadas()
        {
            try
            {

                NegParticipante nParticipante = new NegParticipante();
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();


                oParticipante.SolicitudDPS_Id = NegSolicitudes.objSolicitudInfo.Id;

                rParticipante = nParticipante.BuscarParticipante(oParticipante);


                if (rParticipante.ResultadoGeneral)
                {
                    List<ParticipanteInfo> participantes = rParticipante.Lista.OrderBy(data => data.Solicitud_Id).ToList();
                    List<List<ParticipanteInfo>> dpsPorSolicitud = new List<List<ParticipanteInfo>>();
                    List<ParticipanteInfo> participantesSolicitud = new List<ParticipanteInfo>();
                    int idSolicitud = -1;
                    participantes.ForEach(delegate (ParticipanteInfo info)
                    {
                        if (idSolicitud == -1) idSolicitud = info.Solicitud_Id;
                        if (info.Solicitud_Id == idSolicitud)
                        {
                            participantesSolicitud.Add(info);
                        }
                        else
                        {
                            dpsPorSolicitud.Add(participantesSolicitud);
                            participantesSolicitud = new List<ParticipanteInfo>();
                            idSolicitud = info.Solicitud_Id;
                            participantesSolicitud.Add(info);
                        }
                    });
                    dpsPorSolicitud.Add(participantesSolicitud);
                    return dpsPorSolicitud;
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
                    return null;
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
                    Controles.MostrarMensajeError("Error al Cargar Participantes");
                }
                return null;
            }
        }
        protected void rpt_dps(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.GridView gv = (Anthem.GridView)e.Item.FindControl("gvRelacionadosDPS");
                Anthem.Label lblSolicitudDPS = (Anthem.Label)e.Item.FindControl("lblSolicitudDPS");
                if (gv != null)
                {
                    List<ParticipanteInfo> drv = (List<ParticipanteInfo>)e.Item.DataItem;
                    if (drv.Count > 0)
                    {
                        lblSolicitudDPS.Text = drv[0].Solicitud_Id.ToString();
                        Controles.CargarGrid<ParticipanteInfo>(ref gv, drv, new string[] { "Id", "Rut", "Solicitud_Id", "SolicitudDPS_Id" });
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                gv.Rows[i].Cells[gv.Rows[i].Cells.Count - 1].RowSpan = gv.Rows.Count;
                            }
                            else
                            {
                                gv.Rows[i].Cells[gv.Rows[0].Cells.Count - 1].Visible = false;
                            }

                        }
                        if (drv[0].EstadoDps_Id == 5)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarDataDPS");
                            btn.Text = "Importado";
                            btn.CssClass = "btnsmall btn-secondary";
                            btn.Enabled = false;
                        }
                        else if (drv[0].EstadoDps_Id != 3)
                        {
                            Button btn = (Button)gv.Rows[0].Cells[gv.Rows[0].Cells.Count - 1].FindControl("btnImportarDataDPS");
                            btn.Text = drv[0].DescripcionEstadoDps;
                            btn.CssClass = "btnsmall btn-primary";
                            btn.Enabled = false;
                        }
                    }
                }
            }
        }
        public bool ImportarDataDps(int Solicitud_Id, int SolicitudDps_Id)
        {
            try
            {

                NegParticipante nParticipante = new NegParticipante();
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();
                Resultado<ParticipanteInfo> rParticipanteGRB = new Resultado<ParticipanteInfo>();


                bool okData = true;
                List<ParticipanteInfo> respaldoParticipantes = NegParticipante.lstParticipantesDPS;
                oParticipante.SolicitudDPS_Id = SolicitudDps_Id;

                rParticipante = nParticipante.BuscarParticipante(oParticipante);


                if (rParticipante.ResultadoGeneral)
                {
                    //rParticipante.Lista.ForEach(delegate (ParticipanteInfo participante)
                    foreach (var participante in rParticipante.Lista)

                    {
                        ParticipanteInfo participanteSolicitud = NegParticipante.lstParticipantesDPS.Where(x => x.Solicitud_Id == SolicitudDps_Id && x.Rut == participante.Rut).FirstOrDefault(); //tasacion.Propiedad_Id
                        if (participanteSolicitud != null && okData)
                        {


                            participanteSolicitud.EstadoDps_Id = participante.EstadoDps_Id;
                            participanteSolicitud.FechaAprobacionDPS = participante.FechaAprobacionDPS;
                            participanteSolicitud.FechaIngresoDPS = participante.FechaIngresoDPS;
                            participanteSolicitud.SeguroDesgravamen_Id = participante.SeguroDesgravamen_Id;
                            participanteSolicitud.PrimaSeguroDesgravamen = participante.PrimaSeguroDesgravamen;
                            participanteSolicitud.TasaSeguroDesgravamen = participante.TasaSeguroDesgravamen;
                            participanteSolicitud.MontoAseguradoDPS = participante.MontoAseguradoDPS;

                            rParticipanteGRB = nParticipante.GuardarParticipante(participanteSolicitud);
                            if (!rParticipanteGRB.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError("Error al Importar DPS");
                                okData = false;
                            }
                            else
                            {
                                participante.EstadoDps_Id = 5;//Importada
                                nParticipante.GuardarParticipante(participante);
                                ActualizarSegurosContratadosParticipante(participanteSolicitud);
                            }
                        }
                        else
                        {
                            Controles.MostrarMensajeError("Error al validar Participante de la DPS");
                            okData = false;
                        }
                    }
                }
                if (okData) return true;
                else
                {
                    // respaldoPropiedades foreach GuardarTasacion()
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
                    Controles.MostrarMensajeError("Error al Importar datos de tasación");
                }
                return false;
            }
        }
        protected void btnImportarDataDPS_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
            GridView gv = ((Button)sender).Parent.Parent.Parent.Parent as GridView;
            int Solicitud_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["Solicitud_Id"].ToString());
            int SolicitudDPS_Id = int.Parse(gv.DataKeys[row.RowIndex].Values["SolicitudDPS_Id"].ToString());
            bool result = ImportarDataDps(Solicitud_Id, SolicitudDPS_Id);
            if (result)
            {
                CargarParticipantesDPS();

                ((Button)sender).Enabled = false;
                ((Button)sender).CssClass = "btnsmall btn-secondary";
                ((Button)sender).Text = "Importado";


            }
        }

        private void ActualizarSegurosContratadosParticipante(ParticipanteInfo oParticipante)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSegurosContratados = new SegurosContratadosInfo();
                //Actualizacion del Seguro de Desgravamen

                if (oParticipante.PorcentajeDesgravamen > 0)
                {
                    oSegurosContratados = new SegurosContratadosInfo();
                    oSegurosContratados.TipoSeguro_Id = 2;//Desgravamen
                    oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                    oSegurosContratados.RutCliente = oParticipante.Rut;
                    oSegurosContratados.Seguro_Id = oParticipante.SeguroDesgravamen_Id;
                    if (oParticipante.MontoAseguradoDPS > 0)
                        oSegurosContratados.MontoAsegurado = oParticipante.MontoAseguradoDPS;
                    else
                        oSegurosContratados.MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito * oParticipante.PorcentajeDesgravamen / 100;
                    oSegurosContratados.TasaMensual = oParticipante.TasaSeguroDesgravamen;
                    oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroDesgravamen;

                    rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return;
                    }
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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
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