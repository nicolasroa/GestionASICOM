using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winnovative.WnvHtmlConvert;


namespace WebSite.Administracion
{
    public partial class Auditoria : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("Inicio();");

            if (!Page.IsPostBack)
            {
                CargarTablas();
                CargarResponsables();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarAuditoria();
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            ListarAuditoria();
        }


        protected void lnkExpotarExcel_Click(object sender, EventArgs e)
        {
            GenerarXLS();
        }
        protected void lnkExpotarPdf_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        //Generacion de Excel
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control) { }



        #endregion
        #region Metodos

        private void CargarTablas()
        {
            try
            {

                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablasCriticas);
                if (Lista != null)
                {
                    Controles.CargarChekBoxList<TablaInfo>(ref chklstTablasCriticas, Lista, Constantes.StringNombre, Constantes.StringNombre);
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablasCriticas + " Sin Datos");

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCheckBoxList.ToString()) + " " + ConfigBase.TablasCriticas);
                }
            }

        }
        private void CargarResponsables()
        {
            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            var NegAuditoria = new NegAuditoria();

            try
            {
                ObjetoResultado = NegAuditoria.ListarResponsables();
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista.Count != 0)
                    {
                        Controles.CargarCombo<AuditoriaInfo>(ref ddlUsuarios, ObjetoResultado.Lista, Constantes.StringIdUsuario, Constantes.StringNombreCompleto, "-- Todos -- ", "-1");
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Respondables");
                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCheckBoxList.ToString()) + " " + ConfigBase.TablasCriticas);
                }
            }
        }

        private void ListarAuditoria()
        {
            var ObjetoAuditoria = new AuditoriaInfo();
            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            var NegAuditoria = new NegAuditoria();
            DateTime FechaInicio, FechaTermino;
            string TablasSeleccionadas = "";
            try
            {

                //Asignación de Variables
                ObjetoAuditoria.Usuario_Id = int.Parse(ddlUsuarios.SelectedValue);
                ObjetoAuditoria.Tipo = ddlMovimiento.SelectedValue;
                if (DateTime.TryParse(txtFechaInicio.Text, out FechaInicio))
                {
                    ObjetoAuditoria.FechaInicio = FechaInicio;
                }
                if (DateTime.TryParse(txtFechaTermino.Text, out FechaTermino))
                {
                    ObjetoAuditoria.FechaTermino = FechaTermino;
                }

                foreach (ListItem item in chklstTablasCriticas.Items)
                {
                    if (item.Selected)
                    {
                        TablasSeleccionadas = TablasSeleccionadas + " " + item.Text;
                    }

                }

                ObjetoAuditoria.NombreTabla = TablasSeleccionadas;


                //Priceso de Busqueda
                ObjetoResultado = NegAuditoria.Buscar(ObjetoAuditoria);
                if (ObjetoResultado.ResultadoGeneral)
                {

                    NegAuditoria.ListaAuditoriaPDF = ObjetoResultado.Lista;
                    Controles.CargarGrid<AuditoriaInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { "Id" });
                    lblContador.Text = ObjetoResultado.ValorDecimal.ToString() + " Registro(s) Encontrado(s)";

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Auditoria");
                }
            }
        }
        private void GenerarPDF()
        {
            if (NegAuditoria.ListaAuditoriaPDF == null)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");

                return;
            }

            string urlDocumento = "~/Plantillas/PDF/AuditoriaPDF.aspx";
            string ContenidoHtml = "";
            string Archivo = Funciones.NombreArchivo(Funciones.TipoArchivo.Auditoria) + ".pdf";
            Pdf.NombreDocumentoPDF = Archivo;
            Pdf.ModuloDocumentoPDF = "Auditoria de Tablas";

            StringWriter htmlStringWriter = new StringWriter();
            Server.Execute(urlDocumento, htmlStringWriter);
            ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
            htmlStringWriter.Close();
            var Texto = "222";
            if (Texto != "")
            {
                Controles.MostrarMensajeError(Texto);
                return;
            }

            Pdf.NombreDocumentoPDF = null;
            Pdf.ModuloDocumentoPDF = null;
            Pdf.MostrarPdfModal(Archivo, "Auditoria");

        }

        private void GenerarXLS()
        {
            var Resultado = "";
            if (NegAuditoria.ListaAuditoriaPDF == null || NegAuditoria.ListaAuditoriaPDF.Count == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");
                return;
            }
            Resultado = Excel.ExportarGrid<AuditoriaInfo>(gvBusqueda, NegAuditoria.ListaAuditoriaPDF, "Auditoria", "Reporte de Auditoria");

            if (Resultado != "")
            {
                Controles.MostrarMensajeError(Resultado);
            }

        }

        #endregion
        [Serializable]
        public class DolarInfo
        {
            [JsonProperty("version")]
            public string version { get; set; }
            [JsonProperty("autor")]
            public string autor { get; set; }
            [JsonProperty("codigo")]
            public string codigo { get; set; }
            [JsonProperty("nombre")]
            public string nombre { get; set; }
            [JsonProperty("unidad_medida")]
            public string unidad_medida { get; set; }
            [JsonProperty("Serie")]
            public serie Serie { get; set; }


            public class serie
            {
                [JsonProperty("fecha")]
                public string fecha { get; set; }
                [JsonProperty("valor")]
                public string valor { get; set; }
            }

        }


    }


}