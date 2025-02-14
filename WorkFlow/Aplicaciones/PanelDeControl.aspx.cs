using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WorkFlow.Aplicaciones
{
    public partial class PanelDeControl : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("Inicio();");
            if (!Page.IsPostBack)
            {
                CargarFiltroEjecutivos();
                CargarFiltroMallas();
                CargarFiltroSubEstadoSolicitud();
                Controles.CargarCombo(ref ddlEtapa, new List<EtapaInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                Controles.CargarCombo(ref ddlEvento, new List<EventosInfo>(), Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                CargarEtapas();
            }
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            Controles.CargarGrid<BandejaEntradaInfo>(ref gvBusqueda, NegBandejaEntrada.lstBandejaEntrada, new string[] { "Id" });
        }
        protected void btnAbrirAvento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            SeleccionarEvento(Id);

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarBandejaEntrada(Malla_Id: int.Parse(ddlMalla.SelectedValue), Etapa_Id: int.Parse(ddlEtapa.SelectedValue), Evento_Id: int.Parse(ddlEvento.SelectedValue));
        }
        private void CargarBandejaEntrada(int SubEstadoSolicitud = -1, int Malla_Id = -1, int Etapa_Id = -1, int Evento_Id = -1, int EstadoVencimiento_Id = -1)
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();
                List<BandejaEntradaInfo> lstBandeja = new List<BandejaEntradaInfo>();

                //Asignación de Variables de Búsqueda
                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;

                DateTime FechaInicio, FechaTermino;
                //Asignación de Variables de Búsqueda

                oFiltro.Usuario_Id = int.Parse(ddlEjecutivo.SelectedValue);
                oFiltro.Evento_Id = Evento_Id;
                oFiltro.Etapa_Id = Etapa_Id;
                oFiltro.Malla_Id = Malla_Id;
                oFiltro.SubEstadoSolicitud_Id = int.Parse(ddlEstadoSolicitud.SelectedValue);


                if (DateTime.TryParse(txtFechaInicioDesde.Text, out FechaInicio))
                {
                    oFiltro.FechaInicioDesde = FechaInicio;
                }
                if (DateTime.TryParse(txtFechaInicioHasta.Text, out FechaTermino))
                {
                    oFiltro.FechaInicioHasta = FechaTermino;
                }


                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;
                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    if (EstadoVencimiento_Id != -1)
                        lstBandeja = oResultado.Lista.Where(be => be.EstadoVencimiento_Id == EstadoVencimiento_Id).ToList<BandejaEntradaInfo>();
                    else
                        lstBandeja = oResultado.Lista;
                    DivEtapas.InnerHtml = "";
                    NegBandejaEntrada.lstBandejaEntrada = new List<BandejaEntradaInfo>();
                    NegBandejaEntrada.lstBandejaEntrada = lstBandeja;
                    Controles.CargarGrid<BandejaEntradaInfo>(ref gvBusqueda, lstBandeja, new string[] { "Id" });
                    btnGenerarReporteBandeja.Visible = true;
                    lblContador.Text = lstBandeja.Count.ToString() + " Registro(s) Encontrado(s)";
                    CargarReporteExcel(SubEstadoSolicitud, Malla_Id, Etapa_Id, Evento_Id, EstadoVencimiento_Id);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Eventos");
                }
            }
        }
        private void SeleccionarEvento(int Bandeja_Id)
        {
            try
            {
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.lstBandejaEntrada.FirstOrDefault(b => b.Id.Equals(Bandeja_Id));
                if (oBandeja != null)
                {
                    NegBandejaEntrada.oBandejaEntrada = new BandejaEntradaInfo();
                    NegBandejaEntrada.oBandejaEntrada = oBandeja;
                    NegBandejaEntrada.indEventoModal = true;
                    if (CargarInfoSolicitud())
                        Controles.AbrirEvento("~/Eventos/" + oBandeja.DescripcionPlantilla + ".aspx", oBandeja.DescripcionEvento + " Solicitud: " + oBandeja.Solicitud_Id.ToString());
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
                    Controles.MostrarMensajeError("Error al Seleccionar Evento");
                }
            }
        }
        public bool CargarInfoSolicitud()
        {
            try
            {
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = rSolicitud.Lista.FirstOrDefault();
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar la Solicitud");
                }
                return false;

            }
        }
        private void CargarEtapas()
        {

            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaEtapaInfo> oResultado = new Resultado<BandejaEntradaEtapaInfo>();
                DateTime FechaInicio, FechaTermino;
                //Asignación de Variables de Búsqueda

                oFiltro.Usuario_Id = int.Parse(ddlEjecutivo.SelectedValue);
                oFiltro.Evento_Id = int.Parse(ddlEvento.SelectedValue);
                oFiltro.Etapa_Id = int.Parse(ddlEtapa.SelectedValue);
                oFiltro.Malla_Id = int.Parse(ddlMalla.SelectedValue);
                oFiltro.SubEstadoSolicitud_Id = int.Parse(ddlEstadoSolicitud.SelectedValue);


                if (DateTime.TryParse(txtFechaInicioDesde.Text, out FechaInicio))
                {
                    oFiltro.FechaInicioDesde = FechaInicio;
                }
                if (DateTime.TryParse(txtFechaInicioHasta.Text, out FechaTermino))
                {
                    oFiltro.FechaInicioHasta = FechaTermino;
                }


                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEtapas(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    gvBusqueda.DataSource = null;
                    gvBusqueda.DataBind();
                    btnGenerarReporteBandeja.Visible = false;
                    lblContador.Text = " Etapas Encontradas: " + oResultado.ValorDecimal.ToString();
                    DivEtapas.InnerHtml = "";
                    foreach (var item in oResultado.Lista)
                    {
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-lg-3 col-md-4 col-sm-6 small'>";//Inicio Columna 1
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel panel-primary'>";//Inicio Panel
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-heading'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h3 class='panel-title center'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEtapa(" + item.Etapa_Id.ToString() + ");'>" + item.DescripcionEtapa + " (" + item.Total.ToString() + ")</a> ";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</h3>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-body'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-lg-12 '>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEtapa(" + item.Etapa_Id.ToString() + ",1);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Vigentes</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-success' style='color:lightgray; width:" + item.PorcVigentes.ToString().Replace(",", ".") + "%'>" + item.Vigentes.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEtapa(" + item.Etapa_Id.ToString() + ",2);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Por Vencer</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-warning' style='color:lightgray; width:" + item.PorcPorVencer.ToString().Replace(",", ".") + "%'>" + item.PorVencer.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEtapa(" + item.Etapa_Id.ToString() + ",3);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Vencidas</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-danger' style='color:lightgray; width:" + item.PorcVencidas.ToString().Replace(",", ".") + "%'>" + item.Vencidas.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";//Fin Panel
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";//Fin Columna 1
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
                    Controles.MostrarMensajeError("Error al Cargar Resumen por Etapas");
                }
            }
        }
        private void CargarEventos()
        {

            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaEventoInfo> oResultado = new Resultado<BandejaEntradaEventoInfo>();

                DateTime FechaInicio, FechaTermino;
                //Asignación de Variables de Búsqueda

                if (hdfEtapa_Id.Value != "")
                    oFiltro.Etapa_Id = int.Parse(hdfEtapa_Id.Value);
                else
                    oFiltro.Etapa_Id = int.Parse(ddlEtapa.SelectedValue);


                oFiltro.Usuario_Id = int.Parse(ddlEjecutivo.SelectedValue);
                oFiltro.Evento_Id = int.Parse(ddlEvento.SelectedValue);
                oFiltro.Malla_Id = int.Parse(ddlMalla.SelectedValue);
                oFiltro.SubEstadoSolicitud_Id = int.Parse(ddlEstadoSolicitud.SelectedValue);


                if (DateTime.TryParse(txtFechaInicioDesde.Text, out FechaInicio))
                {
                    oFiltro.FechaInicioDesde = FechaInicio;
                }
                if (DateTime.TryParse(txtFechaInicioHasta.Text, out FechaTermino))
                {
                    oFiltro.FechaInicioHasta = FechaTermino;
                }


                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;


                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEventos(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    gvBusqueda.DataSource = null;
                    gvBusqueda.DataBind();
                    btnGenerarReporteBandeja.Visible = false;
                    lblContador.Text = " Eventos Encontrados: " + oResultado.ValorDecimal.ToString();
                    DivEtapas.InnerHtml = "";

                    foreach (var item in oResultado.Lista)
                    {
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-lg-3 col-md-4 col-sm-6 small'>";//Inicio Columna 1
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel panel-primary'>";//Inicio Panel
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-heading'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h3 class='panel-title center'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEvento(" + item.Evento_Id.ToString() + ");'>" + item.DescripcionEvento + " (" + item.Total.ToString() + ")</a> ";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</h3>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-body'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-lg-12 '>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEvento(" + item.Evento_Id.ToString() + ",1);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Vigentes</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-success' style='color:lightgray; width:" + item.PorcVigentes.ToString().Replace(",", ".") + "%'>" + item.Vigentes.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEvento(" + item.Evento_Id.ToString() + ",2);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Por Vencer</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-warning' style='color:lightgray; width:" + item.PorcPorVencer.ToString().Replace(",", ".") + "%'>" + item.PorVencer.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEstadoEvento(" + item.Evento_Id.ToString() + ",3);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h6 style='margin-top: 3px; margin-bottom:3px;'>Vencidas</h6>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-danger' style='color:lightgray; width:" + item.PorcVencidas.ToString().Replace(",", ".") + "%'>" + item.Vencidas.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";//Fin Panel
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";//Fin Columna 1
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
                    Controles.MostrarMensajeError("Error al Cargar Resumen por Eventos");
                }
            }
        }
        protected void btnBuscarEtapa_Click(object sender, EventArgs e)
        {
            CargarEventos();
            lnkBuscarEtapas.Visible = true;
            lnkBuscarEventos.Visible = false;
        }
        protected void btnBuscarEvento_Click(object sender, EventArgs e)
        {
            CargarBandejaEntrada(Evento_Id: int.Parse(hdfEvento_Id.Value));
            lnkBuscarEventos.Visible = true;
            lnkBuscarEtapas.Visible = false;
        }
        protected void btnBuscarEstadoEtapa_Click(object sender, EventArgs e)
        {
            CargarBandejaEntrada(Etapa_Id: int.Parse(hdfEtapa_Id.Value), EstadoVencimiento_Id: int.Parse(hdfEstadoVigencia_Id.Value));
            lnkBuscarEtapas.Visible = true;
            lnkBuscarEventos.Visible = false;
        }
        protected void btnBuscarEstadoEvento_Click(object sender, EventArgs e)
        {
            CargarBandejaEntrada(Evento_Id: int.Parse(hdfEvento_Id.Value), EstadoVencimiento_Id: int.Parse(hdfEstadoVigencia_Id.Value));
            lnkBuscarEventos.Visible = true;
            lnkBuscarEtapas.Visible = false;
        }
        protected void lnkBuscarEtapas_Click(object sender, EventArgs e)
        {
            CargarEtapas();
            lnkBuscarEtapas.Visible = false;
            lnkBuscarEventos.Visible = false;

        }
        protected void lnkBuscarEventos_Click(object sender, EventArgs e)
        {
            CargarEventos();
            lnkBuscarEventos.Visible = false;
            lnkBuscarEtapas.Visible = true;
        }

        protected void CargarFiltroEjecutivos()
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                ////Asignacion de Variables
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlEjecutivo, ObjetoResultado.Lista, "Rut", "Nombre", "--Ejecutivo--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Ejecutivo");
                }
            }
        }

        private void CargarFiltroMallas()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("MALLAS");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMalla, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Malla Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Mallas");
                }
            }
        }
        private void CargarFiltroSubEstadoSolicitud()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("SUBEST_SOLICITUD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoSolicitud, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Estado Solicitud Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado Solicitud");
                }
            }
        }
        private void CargarFiltroEtapas(int Malla_Id)
        {
            try
            {
                NegEtapas nEtapa = new NegEtapas();
                Resultado<EtapaInfo> rEtapa = new Resultado<EtapaInfo>();
                EtapaInfo oEtapa = new EtapaInfo();

                oEtapa.Malla_Id = Malla_Id;
                oEtapa.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                rEtapa = nEtapa.Buscar(oEtapa);
                if (rEtapa.ResultadoGeneral)
                {
                    Controles.CargarCombo(ref ddlEtapa, rEtapa.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
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
        private void CargarFiltroEventos(int Etapa_Id = -1)
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oEvento = new EventosInfo();
                var NegEventos = new NegEventos();
                var oResultado = new Resultado<EventosInfo>();

                //Asignación de Variables de Búsqueda
                oEvento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oEvento.Etapa_Id = Etapa_Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = NegEventos.Buscar(oEvento);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarCombo<EventosInfo>(ref ddlEvento, oResultado.Lista, Constantes.StringId, "DescripcionCompleta", "-- Evento Destino --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tabla Acciones");
                }
            }


        }

        protected void ddlMalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFiltroEtapas(int.Parse(ddlMalla.SelectedValue));

        }

        protected void ddlEtapa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFiltroEventos(int.Parse(ddlEtapa.SelectedValue));
        }

        protected void btnGenerarReporteBandeja_Click(object sender, EventArgs e)
        {
            GenerarReporteExcelBandeja();
        }
        private bool CargarReporteExcel(int SubEstadoSolicitud = -1, int Malla_Id = -1, int Etapa_Id = -1, int Evento_Id = -1, int EstadoVencimiento_Id = -1)
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<ReporteBandejaEntrada> oResultado = new Resultado<ReporteBandejaEntrada>();
                List<ReporteBandejaEntrada> lstBandeja = new List<ReporteBandejaEntrada>();


                var dsGeneral = new DataSet();
                var dtReporte = new DataTable();

                DateTime FechaInicio, FechaTermino;
                //Asignación de Variables de Búsqueda

                oFiltro.Usuario_Id = int.Parse(ddlEjecutivo.SelectedValue);
                oFiltro.Evento_Id = Evento_Id;
                oFiltro.Etapa_Id = Etapa_Id;
                oFiltro.Malla_Id = Malla_Id;
                oFiltro.SubEstadoSolicitud_Id = int.Parse(ddlEstadoSolicitud.SelectedValue);


                if (DateTime.TryParse(txtFechaInicioDesde.Text, out FechaInicio))
                {
                    oFiltro.FechaInicioDesde = FechaInicio;
                }
                if (DateTime.TryParse(txtFechaInicioHasta.Text, out FechaTermino))
                {
                    oFiltro.FechaInicioHasta = FechaTermino;
                }


                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;
                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarReporte(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count > 0)
                    {
                        Session["ReporteExcelPanel"] = oResultado.Lista;
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
                    Controles.MostrarMensajeError("Error al Cargar Reporte");
                }
                return false;
            }
        }

        private void GenerarReporteExcelBandeja()
        {
            try
            {
                var dsGeneral = new DataSet();
                var dtReporte = new DataTable();

                if (Session["ReporteExcelPanel"] != null)
                {
                    dtReporte = Excel.GenerarDataTable<ReporteBandejaEntrada>((List<ReporteBandejaEntrada>)Session["ReporteExcelPanel"]);
                    dsGeneral.Tables.Add(dtReporte);
                    dsGeneral.Tables[0].TableName = "Panel de Control";
                    string Resultado = Excel.ExportarDataSet(dsGeneral, "Reporte del Panel de Control " + DateTime.Now.ToLongDateString());
                    if (Resultado != "")
                    {
                        Controles.MostrarMensajeError(Resultado);
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
                    Controles.MostrarMensajeError("Error al Generar Reporte");
                }
            }
        }

        protected void btnBuscarEtapas_Click(object sender, EventArgs e)
        {
            CargarEtapas();
        }

        public override void VerifyRenderingInServerForm(Control control) { }
    }


}