using WorkFlow.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WorkFlow.General
{
    public class Mail
    {
        public static Resultado<MailInfo> EnviarMail(MailInfo oMail, ConfiguracionGeneralInfo oConfiguracion)
        {
            Resultado<MailInfo> oResultado = new Resultado<MailInfo>();

            if (oConfiguracion.EnviarMail == false)
            {
                return oResultado;
            }
           
            try
            {
                var smtp = new SmtpClient   //();
                {
                    Host = oConfiguracion.ServidorSalidaCorreo,
                    Port = int.Parse(oConfiguracion.PuertoCorreo),
                    EnableSsl = oConfiguracion.ConexionSeguraMail,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(oConfiguracion.Correo, oConfiguracion.ClaveCorreo),
                };

                var mensaje = new MailMessage(oConfiguracion.Correo, oMail.Destinatarios);
                mensaje.IsBodyHtml = true;
                mensaje.Body = oMail.Texto;
                mensaje.Subject = oMail.Asunto;
                mensaje.From = new MailAddress(oConfiguracion.Correo, oConfiguracion.UsuarioCorreo);

                //Agrega Adjuntos
                foreach (var Adjunto in oMail.Adjuntos)
                {
                    Stream stream = new MemoryStream(Adjunto.Archivo);
                    mensaje.Attachments.Add(new Attachment(stream, Adjunto.NombreArchivo));
                }
                smtp.Send(mensaje);

            }
            catch (Exception Ex)
            {
                oResultado.ResultadoGeneral = false;
                oResultado.Mensaje = Ex.Message;
            }
            return oResultado;
        }



        public class MailInfo
        {
            public string Destinatarios { get; set; }
            public string Asunto { get; set; }
            public string Texto { get; set; }
            public List<AdjuntosMail> Adjuntos { get; set; }

            public MailInfo()
            {
                Adjuntos = new List<AdjuntosMail>();
            }
        }


        public class AdjuntosMail
        {
            public byte[] Archivo { get; set; }
            public string NombreArchivo { get; set; }
        }

        public class InformacionAdjunto
        {
            public string NombreCliente { get; set; }
            public string NombreAdjunto { get; set; }
        }


        #region PROPIEDADES
        public static InformacionAdjunto objInformacionAdjunto
        {
            get { return (InformacionAdjunto)HttpContext.Current.Session[ISesiones.objInformacionAdjunto]; }
            set { HttpContext.Current.Session.Add(ISesiones.objInformacionAdjunto, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string objInformacionAdjunto = "objInformacionAdjunto";

        }
        #endregion


    }
}
