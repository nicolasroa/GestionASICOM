using SelectPdf;
using System;
using System.Web;

namespace WorkFlow.General
{
    public class Pdf
    {

        #region METODOS

        //public static byte[] GenerarHtmlaDocumento(string[] Urls, string NombreArchivo)
        //{

        //    PdfConverter pdfConvertidor = new PdfConverter();
        //    pdfConvertidor.LicenseKey = "UXpjcWBkcWlmcWd/YXFiYH9gY39oaGho";
        //    pdfConvertidor.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter;
        //    pdfConvertidor.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
        //    pdfConvertidor.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
        //    string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;
        //    string baseUrl = thisPageURL;
        //    PdfPage Pagina = null;

        //    System.IO.Stream stream = new System.IO.MemoryStream();
        //    Document doc = new Document(stream);
        //    foreach (var item in Urls)
        //    {
        //        HtmlToPdfElement elemento = new HtmlToPdfElement(item, baseUrl);
        //        Pagina = doc.AddPage();
        //        Pagina.AddElement(elemento);
        //    }
        //    return doc.Save();
        //}



        public static void MostrarPdfModal(string NombreArchivo)
        {
            var Ancho = 1100;
            var Alto = 800;
            var Titulo = "Documento Pdf";
            string _StrScript = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
            _StrScript = _StrScript.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void MostrarPdfModal(string NombreArchivo, string Titulo)
        {

            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            
            //if (name == "IE" || name == "Mozilla" || name == "InternetExplorer")
            //{

            //    var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
            //    System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
            //    if (toDownload.Exists)
            //    {
            //        HttpContext.Current.Response.Clear();
            //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
            //        HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
            //        HttpContext.Current.Response.ContentType = "application/octet-stream";
            //        HttpContext.Current.Response.WriteFile(RutaPdf);
            //        HttpContext.Current.Response.Flush();

            //        Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "setTimeout($.unblockUI, 0);", true);
            //    }
            //}
            //else
            //{
            var Ancho = 1100;
            var Alto = 800;
            string _StrScript = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
            _StrScript = _StrScript.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
            //}

        }
        public static void MostrarPdfModal(string NombreArchivo, string Titulo, int Alto, int Ancho)
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            if (name == "IE")
            {
                var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
                if (toDownload.Exists)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(RutaPdf);
                    HttpContext.Current.Response.Flush();
                }
            }
            else
            {
                string _StrScript = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
                _StrScript = _StrScript.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);
                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
            }
        }
        public static void MostrarPdfDiv(string NombreArchivo, string NobreDiv)
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            if (name == "IE")
            {
                var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
                if (toDownload.Exists)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(RutaPdf);
                    HttpContext.Current.Response.Flush();
                }
            }
            else
            {
                string _StrScript = "MostrarPdfDiv('@NombreArchivo','@NombreDiv');";
                _StrScript = _StrScript.Replace("@NombreArchivo", NombreArchivo).Replace("@NombreDiv", NobreDiv);

                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
            }
        }
        public static byte[] GenerarPDF(string strHtml, string NombreArchivo)
        {
            byte[] Resultado = null;


            PdfDocument doc = new PdfDocument();
            string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;
            string baseUrl = thisPageURL;
            HtmlToPdf converter = new HtmlToPdf();

                       
            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.Letter;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.MarginBottom = 20;
            converter.Options.MarginTop = 20;
            converter.Options.MarginLeft = 5;
            converter.Options.MarginRight = 5;
            PdfPage pagina = doc.AddPage();
            // create a new pdf document converting an url
            doc = converter.ConvertHtmlString(strHtml, baseUrl);
            Resultado = doc.Save();
            doc.Close();


           






            return Resultado;

        }


       


        #endregion

        #region PROPIEDADES
        public static string NombreDocumentoPDF
        {
            get { return (string)HttpContext.Current.Session[ISesiones.NombreDocumentoPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.NombreDocumentoPDF, value); }
        }
        public static string ModuloDocumentoPDF
        {
            get { return (string)HttpContext.Current.Session[ISesiones.ModuloDocumentoPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.ModuloDocumentoPDF, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string NombreDocumentoPDF = "NombreDocumentoPDF";
            public static string ModuloDocumentoPDF = "ModuloDocumentoPDF";
        }
        #endregion

    }
}

