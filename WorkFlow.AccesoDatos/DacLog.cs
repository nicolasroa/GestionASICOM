using WorkFlow.General;
using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using ENTIDADES = WorkFlow.Entidades;
namespace WorkFlow.AccesoDatos
{
    public static class DacLog
    {
        public static void Registrar(Exception Ex, string Procedimiento)
        {

            try
            {
                ENTIDADES.Log ObjetoLog = new ENTIDADES.Log();
                ObjetoLog.Mensaje = Ex.Message;
                ObjetoLog.PilaEventos = Ex.StackTrace;
                ObjetoLog.Procedimiento = Procedimiento;

                if (AccesoDatos.OperacionLog(ObjetoLog, GlobalDA.SP.Log_GRB).ResultadoGeneral == false)
                {
                    RegistrarEnWindows(Ex, EventLogEntryType.Error, Procedimiento);
                }
            }
            catch
            {
                RegistrarEnWindows(Ex, EventLogEntryType.Error, Procedimiento);
            }

        }


        private static void RegistrarEnWindows(Exception Ex, EventLogEntryType Tipo, string Procedimiento = "")
        {

            try
            {
                var NombreArchivo = HttpContext.Current.Server.MapPath("~/Recursos/Errores/LogErrores.txt");

                if (!File.Exists(NombreArchivo))
                    File.Create(NombreArchivo);

                StreamWriter WriteReportFile = File.AppendText(NombreArchivo);
                WriteReportFile.WriteLine("###########################################");
                WriteReportFile.WriteLine("Fecha: " + DateTime.Now);
                WriteReportFile.WriteLine("Aplicación: " + ArchivoRecursos.ObtenerValorNodo("NombreAplicacion"));
                WriteReportFile.WriteLine("Mensaje: " + Ex.Message);
                WriteReportFile.WriteLine("Procedimiento: " + Procedimiento);
                WriteReportFile.WriteLine("###########################################");
                WriteReportFile.Close();

            }
            catch
            {


            }

        }

    }
}
