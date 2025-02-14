using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Drawing;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosFlujoSolicitud : System.Web.UI.UserControl
    {
        private Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargarEventos();
            }
        }

        private void CargarEventos()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                List<BandejaEntradaInfo> lstBandeja = new List<BandejaEntradaInfo>();

                //Asignación de Variables de Búsqueda
                oFiltro.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEstadoAvance(oFiltro);

                
                if (oResultado.ResultadoGeneral)
                {
                    // Lista de eventos por Malla con SCROLL Y DRAG
                    List<BandejaEntradaInfo> mallas = oResultado.Lista.GroupBy(x => x.Malla_Id).Select(x => x.FirstOrDefault()).OrderBy(x=>x.Malla_Id).ToList();

                    DivEventos.InnerHtml = "";
                    int cont = 1;
                    
                    foreach (var elem in mallas)
                    {
                        String saltopalabra = "";
                        String margintop = "30px;";
                        String[] color = new string[] { "#2b1727", "#6d5265", "#51374a", "#7a6173", "#b29eaa", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce", "#d3c2ce" };

                        List<BandejaEntradaInfo> EventosMalla = oResultado.Lista.Where(x => x.Malla_Id == elem.Malla_Id).ToList().OrderByDescending(x => x.FechaInicio).ToList();
                        if (elem.DescripcionMalla.Length > 10)
                        {
                            saltopalabra = "style = 'white-space:normal; word-wrap:break-word;'";
                            margintop = "20px;";
                        }
                        if (EventosMalla.Count > 0)
                        {
                            DivEventos.InnerHtml += "<div id='Container"+cont.ToString()+"' name='flujos' runat='server' class='containerFlows'>";
                            DivEventos.InnerHtml += "<div class='CirculoMalla' style='color:white; background-color:"+color[cont-1]+ "; margin-right:5px;'>";
                            DivEventos.InnerHtml += "<div style='margin-top: "+margintop+"' class='panel-body'>";
                            DivEventos.InnerHtml += "<h5 "+saltopalabra+">" + elem.DescripcionMalla + "</h5>";
                            DivEventos.InnerHtml += "</div>";
                            DivEventos.InnerHtml += "</div>";
                            //DivEventos.InnerHtml += "<span class='glyphicon glyphicon-triangle-left' style='padding: 50px 5px 0px 5px;' aria-hidden='true'></span>";

                            foreach (var item in EventosMalla)
                            {
                                String termino = "";
                                String inicio = "";
                                if (item.FechaTermino == null) termino = "--";
                                else
                                {
                                    termino = Convert.ToDateTime(item.FechaTermino).ToString("dd/MM/yyyy H:mm");
                                }
                                if (item.FechaInicio == null) termino = "--";
                                else
                                {
                                    inicio = Convert.ToDateTime(item.FechaInicio).ToString("dd/MM/yyyy H:mm");
                                }
                                DivEventos.InnerHtml += "<div class='evento " + item.DescripcionEstado + "'>";
                                DivEventos.InnerHtml += "<div class='panel-heading " + item.DescripcionEstado + "top'>";
                                DivEventos.InnerHtml += "<h3 class='evento-title'>" + item.DescripcionEtapa + "</h3>";
                                DivEventos.InnerHtml += "</div>";
                                DivEventos.InnerHtml += "<div class='panel-body' style='height:100px;'>";
                                DivEventos.InnerHtml += "<div class='descripcionEvento'>" + item.DescripcionEvento + "</div>";
                                DivEventos.InnerHtml += "<span class='descripcionEstado'>Estado: " + item.DescripcionEstado + "</span><br>";
                                DivEventos.InnerHtml += "<div class='responsable'>Responsable: " + item.UsuarioResponsable + "</div>";
                                DivEventos.InnerHtml += "</div>";
                                DivEventos.InnerHtml += "<div class='evento-footer " + item.DescripcionEstado + "top'>";
                                DivEventos.InnerHtml += "<span style='font-size:9px;float:left'>" + inicio + "</span>";
                                DivEventos.InnerHtml += "<span style='font-size:9px;float:right'>" + termino + "</span>";
                                DivEventos.InnerHtml += "</div>";
                                DivEventos.InnerHtml += "</div>";
                                DivEventos.InnerHtml += "<span class='glyphicon glyphicon-triangle-left' style='margin: 50px 5px 0px 5px;' aria-hidden='true'></span>";

                            }
                            DivEventos.InnerHtml += "</div>";
                            cont += 1;
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
                    Controles.MostrarMensajeError("Error al Cargar Resumen por Etapas");
                }
            }
        }
    }
}