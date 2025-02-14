using SpreadsheetGear;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WorkFlow.General
{
    public class Excel
    {

        private static string nombrePlantilla = string.Empty;
        private static string nombreDocumento = string.Empty;

        private static IWorkbookSet oWorkBookSet = Factory.GetWorkbookSet(CultureInfo.CurrentCulture);
        

        #region METODOS

        //EJEMPLO DE LLAMADO
        // Excel.ExportarGrid<AuditoriaInfo>(gvBusqueda, NegAuditoria.ListaAuditoriaPDF, "Auditoria","Reporte de Auditoria", new int[] { 1, 3 });

        //Agregar Método
        //public override void VerifyRenderingInServerForm(Control control){ }

        //Agregar en el Encabezado del Código HTML
        // EnableEventValidation="false"


        public static string ExportarGrid<T>(Anthem.GridView Grid, List<T> Lista, string NombreArchivo, string TituloReporte, int[] ColumnasAEliminar = null)
        {
            try
            {
                string Codigo = "";

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                if (ColumnasAEliminar != null)
                {
                    foreach (var Columna in ColumnasAEliminar)
                    {
                        if (Columna <= Grid.Columns.Count && Columna >= 1)
                            Grid.Columns[Columna - 1].Visible = false;
                    }
                }

                Grid.DataBind();
                Grid.AllowPaging = false;
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.EnableViewState = false;
                Grid.RenderControl(htw);

                Codigo = Codigo + "<p>" + TituloReporte + "</p> <br />";

                Codigo = Codigo + sb.ToString();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo + ".xls");
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.Default;
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();





                Grid.AllowPaging = true;
                if (ColumnasAEliminar != null)
                {
                    foreach (var Columna in ColumnasAEliminar)
                    {
                        if (Columna <= Grid.Columns.Count && Columna >= 1)
                            Grid.Columns[Columna - 1].Visible = true;
                    }
                }
                Grid.DataSource = Lista;
                Grid.DataBind();
                return "";

            }
            catch (Exception Ex)
            {
                return Ex.Message;

            }
        }

        public static string ExportarDataSet(DataSet ds, string tituloExcel)
        {
            try
            {
                HttpResponse response = HttpContext.Current.Response;

                SLDocument sl = new SLDocument();
                sl.ImportDataTable(2, 1, ds.Tables[0], true);
                SLStyle style = sl.CreateStyle();
                style.Font.FontSize = 14;
                style.Font.Bold = true;

                sl.SetCellValue(1, 1, tituloExcel);
                sl.SetCellStyle(1, 1, style);
                sl.MergeWorksheetCells(1, 1, 1, 4);
                sl.ImportDataTable(2, 1, ds.Tables[0], true);


                sl.SetCellStyle(2, 1, 2, ds.Tables[0].Columns.Count, style);

                sl.AutoFitColumn(1, ds.Tables[0].Columns.Count);
                response.Clear();
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("Content-Disposition", "attachment; filename=" + tituloExcel + ".xlsx");
                sl.SaveAs(response.OutputStream);
                response.End();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string ExportarArray(List<string[]> lstArreglo)
        {
            SLDocument archivo = new SLDocument();
            int fila = 1;


            foreach (var item in lstArreglo)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    archivo.SetCellValue(fila, i, item[i]);

                }
                fila = fila + 1;

            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            response.AddHeader("Content-Disposition", "attachment; filename=Reporte" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx");
            archivo.SaveAs(response.OutputStream);
            response.End();
            return "";


        }

        private static void ConstruirNombres(string plantilla, string NombreArchivo = "")
        {
            //string usuSesion = ISeguridad.getUsuario.Usuario;

            nombrePlantilla = HttpContext.Current.Server.MapPath(ArchivoRecursos.ObtenerValorNodo("Plantilla_Path")) + "" + plantilla;
            if (NombreArchivo == "")
                nombreDocumento = "XLS_PAS_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            else
                nombreDocumento = NombreArchivo + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            //if (!string.IsNullOrEmpty(usuSesion))
            //{
            //    nombreDocumento = "XLS_PAS_" + usuSesion.ToUpper() + "_" + IConfiguraciones.getFechaProceso.ToString("ddMMyyyyhhmmss");
            //}
        }

        private static bool ExportarAExcel(IWorkbook excel)
        {


            try
            {
                byte[] data = excel.SaveToMemory(FileFormat.XLS97);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreDocumento + ".xls");
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.Flush();

                return true;
            }
            catch 
            {
                return false;
            }


        }
        public static DataTable GenerarDataTable<T>(List<T> Lista)
        {
            try
            {

                DataTable table = new DataTable();

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {

                        T obj = Lista.FirstOrDefault();
                        foreach (PropertyInfo proinfo in obj.GetType().GetProperties())
                        {
                            table.Columns.Add(proinfo.Name);
                        }
                        foreach (T Objeto in Lista)
                        {
                            DataRow fila = table.NewRow();

                            foreach (PropertyInfo proinfo in Objeto.GetType().GetProperties())
                            {
                                var Tipo = proinfo.PropertyType.Name;

                                try
                                {
                                    switch (Tipo)
                                    {
                                        case "DateTime":
                                            fila[proinfo.Name] = string.Format("{0:d}", proinfo.GetValue(Objeto, null));
                                            break;
                                        case "Decimal":
                                            fila[proinfo.Name] = string.Format("{0:N2}", proinfo.GetValue(Objeto, null));
                                            break;
                                        default:
                                            fila[proinfo.Name] = proinfo.GetValue(Objeto, null).ToString();
                                            break;
                                    }
                                }
                                catch (Exception)
                                {

                                    fila[proinfo.Name] = "";
                                }
                            }
                            table.Rows.Add(fila);
                        }
                    }
                }
                return table;
            }
            catch
            {
                return null;

            }

        }
        #endregion

        #region PROPIEDADES
        public static string NombreDocumentoXls
        {
            get { return (string)HttpContext.Current.Session[ISesiones.NombreDocumentoXLS]; }
            set { HttpContext.Current.Session.Add(ISesiones.RutaDocumentoXLS, value); }
        }
        public static string RutaDocumentoXls
        {
            get { return (string)HttpContext.Current.Session[ISesiones.RutaDocumentoXLS]; }
            set { HttpContext.Current.Session.Add(ISesiones.RutaDocumentoXLS, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {

            public static string RutaDocumentoXLS = "ModuloDocumentoXLS";
            public static string NombreDocumentoXLS = "ModuloDocumentoXLS";
        }
        #endregion
    }
}
