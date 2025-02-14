using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class BandejaEntrada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (NegBandejaEntrada.ActualizarBandeja == true)
            {
                Controles.MostrarMensajeExito("Evento Procesado Correctamente");
                CargarBandejaEntrada();

                NegBandejaEntrada.ActualizarBandeja = false;

            }


            if (!Page.IsPostBack)
            {


                CargarEtapas();
                CargarActivaciones();
                CargarBandejaSeguimiento();
                if (ValidarActivacionesRechazadas()) btnVerActivacionesRechazadas.Visible = true;
                if (ValidarDesactivaciones()) btnVerOperacionesDesactivadas.Visible = true;

            }
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            Controles.CargarGrid<BandejaEntradaInfo>(ref gvBusqueda, NegBandejaEntrada.lstBandejaEntrada, new string[] { "Id" });

        }

        protected void gvBandejaSeguimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBandejaSeguimiento.PageIndex = e.NewPageIndex;
            Controles.CargarGrid<SolicitudInfo>(ref gvBandejaSeguimiento, NegBandejaEntrada.lstBandejaSeguimiento, new string[] { "Id" });

        }
        protected void btnAbrirAvento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            SeleccionarEvento(Id);

        }

        protected void btnAbrirSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBandejaSeguimiento.DataKeys[row.RowIndex].Values["Id"].ToString());
            SeleccionarSolicitud(Id);

        }
        protected void btnBuscar_Click(object sender, EventArgs e) => CargarBandejaEntrada();
        private void CargarBandejaEntrada(int Etapa_Id = -1, int Evento_Id = -1, int EstadoVencimiento_Id = -1)
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
                if (txtOperacion.Text.Length != 0)
                    oFiltro.Operacion = txtOperacion.Text;

                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;
                oFiltro.Evento_Id = Evento_Id;
                oFiltro.Etapa_Id = Etapa_Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    if (EstadoVencimiento_Id != -1)
                        lstBandeja = oResultado.Lista.Where(be => be.EstadoVencimiento_Id == EstadoVencimiento_Id).ToList<BandejaEntradaInfo>();
                    else
                        lstBandeja = oResultado.Lista;

                    NegBandejaEntrada.lstBandejaEntrada = lstBandeja;
                    DivEtapas.InnerHtml = "";
                    Controles.CargarGrid<BandejaEntradaInfo>(ref gvBusqueda, lstBandeja, new string[] { "Id" });


                    lblContador.Text = lstBandeja.Count.ToString() + " Registro(s) Encontrado(s)";
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

        private void CargarBandejaSeguimiento()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();

                List<SolicitudInfo> lstBandeja = new List<SolicitudInfo>();

                //Asignación de Variables de Búsqueda
                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;


                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarBandejaSeguimiento(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    Controles.CargarGrid<SolicitudInfo>(ref gvBandejaSeguimiento, oResultado.Lista, new string[] { "Id" });
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
                        Controles.AbrirEvento("~/Eventos/" + oBandeja.DescripcionPlantilla + ".aspx", oBandeja.DescripcionEvento, oBandeja.DescripcionEtapa, oBandeja.DescripcionMalla, oBandeja.Solicitud_Id.ToString(), oBandeja.UsuarioResponsable, oBandeja.FechaEsperada.Value.ToString(), oBandeja.FechaInicio.ToString());
                    //Controles.AbrirEvento("~/Eventos/" + oBandeja.DescripcionPlantilla + ".aspx", oBandeja.DescripcionEvento + " Solicitud: " + oBandeja.Solicitud_Id.ToString());
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
        private void SeleccionarSolicitud(int Solicitud_Id)
        {
            try
            {
                NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                NegSolicitudes.objSolicitudInfo = NegBandejaEntrada.lstBandejaSeguimiento.FirstOrDefault(b => b.Id.Equals(Solicitud_Id));
                if (NegSolicitudes.objSolicitudInfo != null)
                {
                    Controles.AbrirEvento("~/Aplicaciones/EstadoAvance.aspx", "Consulta - Solicitud: " + NegSolicitudes.objSolicitudInfo.Id.ToString());
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
                    Controles.MostrarMensajeError("Error al cargar Consulta Estado Avance.");
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

                //Asignación de Variables de Búsqueda

                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEtapas(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
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

                //Asignación de Variables de Búsqueda

                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;
                oFiltro.Etapa_Id = int.Parse(hdfEtapa_Id.Value);

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEventos(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    gvBusqueda.DataSource = null;
                    gvBusqueda.DataBind();
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
            gvBusqueda.Visible = true;
        }
        protected void btnBuscarEstadoEvento_Click(object sender, EventArgs e)
        {
            CargarBandejaEntrada(Evento_Id: int.Parse(hdfEvento_Id.Value), EstadoVencimiento_Id: int.Parse(hdfEstadoVigencia_Id.Value));
            lnkBuscarEventos.Visible = true;
            lnkBuscarEtapas.Visible = false;
            gvBusqueda.Visible = true;
        }

        protected void btnBuscarDetalleActivacion_Click(object sender, EventArgs e)
        {

        }




        protected void lnkBuscarEtapas_Click(object sender, EventArgs e)
        {
            CargarEtapas();
            lnkBuscarEtapas.Visible = false;
            lnkBuscarEventos.Visible = false;
            gvBusqueda.Visible = false;

        }
        protected void lnkBuscarEventos_Click(object sender, EventArgs e)
        {
            CargarEventos();
            lnkBuscarEventos.Visible = false;
            lnkBuscarEtapas.Visible = true;
            gvBusqueda.Visible = false;
        }

        private bool ValidarActivacionesRechazadas()
        {
            try
            {
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;
                oFiltro.Evento_Id = 100;
                oFiltro.Etapa_Id = 8;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar las Activaciones Rechazadas");
                }
                return false;
            }
        }

        private bool ValidarDesactivaciones()
        {
            try
            {
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;
                oFiltro.Evento_Id = 80;
                oFiltro.Etapa_Id = 8;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar las Activaciones Rechazadas");
                }
                return false;
            }
        }


        private void CargarActivacionesRechazadas()
        {
            try
            {
                CargarBandejaEntrada(Etapa_Id: 8, Evento_Id: 100);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Activaciones Rechazadas");
                }
            }
        }

        private void CargarOperacionesDesactivadas()
        {
            try
            {
                CargarBandejaEntrada(Etapa_Id: 8, Evento_Id: 80);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Desactivaciones");
                }
            }
        }

        private void CargarActivaciones()
        {

            try
            {

                //Declaración de Variables de Búsqueda
                SolicitudInfo oFiltro = new SolicitudInfo();
                NegSolicitudes oNeg = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();

                //Asignación de Variables de Búsqueda

                oFiltro.MesEscritura = -1;
                oFiltro.AñoEscritura = -1;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarActivaciones(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    divActivaciones.InnerHtml = "";
                    foreach (var item in oResultado.Lista)
                    {
                        divActivaciones.InnerHtml += "<div class='col-md-4'>";//1
                        divActivaciones.InnerHtml += "<div class='panel panel-info'>";//2
                        divActivaciones.InnerHtml += "<div class='panel-heading'>";//3
                        divActivaciones.InnerHtml += "<h4 class='panel-title'>" + item.Periodo + " (UF " + string.Format("{0:F4}", item.TotalActivacion) + ")</h4>";
                        divActivaciones.InnerHtml += "</div>";//3
                        divActivaciones.InnerHtml += "<div class='panel-body'>";//4
                        divActivaciones.InnerHtml += "<div class='progress progress-striped active'>";//5
                        divActivaciones.InnerHtml += "<div class='progress-bar progress-bar-success' style='width: " + string.Format("{0:F0}", item.PorcActivadas) + "%'>Activadas (" + string.Format("{0:D}", item.CantidadActivado) + ")</div>";
                        divActivaciones.InnerHtml += "</div>";//5
                        divActivaciones.InnerHtml += "<div class='progress progress-striped active'>";//6
                        divActivaciones.InnerHtml += "<div class='progress-bar progress-bar-warning' style='width: " + string.Format("{0:F0}", item.PorcNoActivadas) + "%'>No Activadas (" + string.Format("{0:D}", item.CantidadNoActivado) + ")</div>";
                        divActivaciones.InnerHtml += "</div>";//6
                        divActivaciones.InnerHtml += "</div>";//4
                        divActivaciones.InnerHtml += "</div>";//2
                        divActivaciones.InnerHtml += "</div>";//1
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

        protected void btnVerActivacionesRechazadas_Click(object sender, EventArgs e)
        {
            CargarActivacionesRechazadas();
        }

        protected void btnVerOperacionesDesactivadas_Click(object sender, EventArgs e)
        {
            CargarOperacionesDesactivadas();
        }
    }
}