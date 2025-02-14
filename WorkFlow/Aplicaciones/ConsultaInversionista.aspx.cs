using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class ConsultaInversionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
            

                if (NegUsuarios.Usuario.Inversionista_Id == -1)
                {
                    CargarInversionista();
                    ddlFormInversionista.Visible = true;
                }
                CargarEtapas();

            }
        }
        private void CargarEtapas()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                EtapasInversionistaFiltro oFiltro = new EtapasInversionistaFiltro();
                NegInversionistas oNeg = new NegInversionistas();
                Resultado<EtapasInversionistaResumen> oResultado = new Resultado<EtapasInversionistaResumen>();

                if (NegUsuarios.Usuario.Inversionista_Id == -1)
                    oFiltro.Inversionista_Id = int.Parse(ddlFormInversionista.SelectedValue);
                else
                    oFiltro.Inversionista_Id = NegUsuarios.Usuario.Inversionista_Id;
                oFiltro.TipoConsulta = 1;

                if (oFiltro.Inversionista_Id == -1)
                    return;
                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarResumen(oFiltro);
                if (oResultado.ResultadoGeneral)
                {

                    gvBusqueda.DataSource = null;
                    gvBusqueda.DataBind();
                    btnBuscarEtapas.Visible = true;
                    btnGenerarReporteBandeja.Visible = false;
                    lblContador.Text = " Etapas Encontradas: " + oResultado.ValorDecimal.ToString();
                    DivEtapas.InnerHtml = "";
                    foreach (var item in oResultado.Lista)
                    {

                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4 small'>";//Inicio Columna 1
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel panel-primary'>";//Inicio Panel
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-heading'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h3 class='panel-title center'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEtapa(" + item.IdEtapa.ToString() + ",99);'>" + item.DescripcionEtapa + " (" + (item.CantidadPorPromesar + item.CantidadPromesadas + item.CantidadVendidas).ToString() + ")</a> ";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</h3>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='panel-body'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-lg-12'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEtapa(" + item.IdEtapa.ToString() + ",1);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h5>Por Promesar</h5>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-default' style='width:" + item.PorcPorPromesar.ToString().Replace(",", ".") + "%'>" + item.CantidadPorPromesar.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEtapa(" + item.IdEtapa.ToString() + ",2);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h5>Promesadas</h5>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-warning' style='width:" + item.PorcPromesadas.ToString().Replace(",", ".") + "%'>" + item.CantidadPromesadas.ToString() + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='row'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-4'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<a href = '#' onclick='DetalleEtapa(" + item.IdEtapa.ToString() + ",3);'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<h5>Vendidas</h5>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</a>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "</div>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='col-md-8'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress progress-striped active'>";
                        DivEtapas.InnerHtml = DivEtapas.InnerHtml + "<div class='progress-bar progress-bar-success' style='width:" + item.PorcVendidas.ToString().Replace(",", ".") + "%'>" + item.CantidadVendidas.ToString() + "</div>";
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




        private void CargarDetalleEtapa(int Etapa_Id, int IndVenta = -1)
        {

            try
            {
                //Declaración de Variables de Búsqueda
                EtapasInversionistaFiltro oFiltro = new EtapasInversionistaFiltro();
                NegInversionistas oNeg = new NegInversionistas();
                Resultado<EtapasInversionistaDetalle> oResultado = new Resultado<EtapasInversionistaDetalle>();

                if (NegUsuarios.Usuario.Inversionista_Id == -1)
                    oFiltro.Inversionista_Id = int.Parse(ddlFormInversionista.SelectedValue);
                else
                    oFiltro.Inversionista_Id = NegUsuarios.Usuario.Inversionista_Id;
                oFiltro.TipoConsulta = 2;
                oFiltro.Etapa_Id = Etapa_Id;
                oFiltro.IndVenta = IndVenta;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarDetalle(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    DivEtapas.InnerHtml = "";
                    btnBuscarEtapas.Visible = true;
                    btnGenerarReporteBandeja.Visible = true;
                    lblContador.Text = " Solicitudes Encontradas: " + oResultado.ValorDecimal.ToString();
                    NegInversionistas.LstDetalleEtapa = oResultado.Lista;
                    Controles.CargarGrid<EtapasInversionistaDetalle>(ref gvBusqueda, oResultado.Lista, new string[] { "NumeroSolicitud" });
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



        protected void btnBuscarEtapa_Click(object sender, EventArgs e)
        {
            int IdEtapa = 0;
            int IndVenta = -1;

            if (hdfEtapa_Id.Value != "")
                IdEtapa = int.Parse(hdfEtapa_Id.Value);
            if (hdfIndVenta.Value == "1")
                IndVenta = 1;
            if (hdfIndVenta.Value == "2")
                IndVenta = 2;
            if (hdfIndVenta.Value == "3")
                IndVenta = 3;

            CargarDetalleEtapa(IdEtapa, IndVenta);

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDetalleEtapa(-1, -1);
        }

        protected void btnBuscarEtapas_Click(object sender, EventArgs e)
        {
            CargarEtapas();
        }

        protected void btnGenerarReporteBandeja_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            Controles.CargarGrid<EtapasInversionistaDetalle>(ref gvBusqueda, NegInversionistas.LstDetalleEtapa, new string[] { "NumeroSolicitud" });

        }

        private void GenerarReporteExcel()
        {
            try
            {
                var dsGeneral = new DataSet();
                var dtReporte = new DataTable();

                if (NegInversionistas.LstDetalleEtapa != null)
                {
                    dtReporte = Excel.GenerarDataTable<EtapasInversionistaDetalle>(NegInversionistas.LstDetalleEtapa);
                    dsGeneral.Tables.Add(dtReporte);
                    dsGeneral.Tables[0].TableName = "Detalle Consulta Inversionista";
                    string Resultado = Excel.ExportarDataSet(dsGeneral, "RPT_Inversionista " + DateTime.Now.ToLongDateString());
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

        protected void gvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Anthem.Image imgEstadoVenta = (Anthem.Image)e.Row.FindControl("imgEstadoVenta");
                Anthem.HiddenField hdfVendido = (Anthem.HiddenField)e.Row.FindControl("hdfVendido");
                
                imgEstadoVenta.ImageUrl = "~/Img/icons/"+ hdfVendido.Value + ".png";


            }
        }


        private void CargarInversionista()
        {
            try
            {
                var oInfo = new InversionistaInfo();
                var oNegocio = new NegInversionistas();
                var oResultado = new Resultado<InversionistaInfo>();
                oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oResultado = oNegocio.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InversionistaInfo>(ref ddlFormInversionista, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Inversionista --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Sucursal");
                }
            }
        }
    }   
}