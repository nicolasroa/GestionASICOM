using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WorkFlow.ServiciosWeb
{
    /// <summary>
    /// Descripción breve de Seguridad
    /// </summary>
    [WebService(Namespace = "seguridad")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Seguridad : System.Web.Services.WebService
    {

        [WebMethod]
        public string Encriptar(string usuario, string clave, string texto)
        {
            string ValUsuario = "servicioasicom";
            string ValClave = "8YZhEyufBPu93QV9b2KK";

            if (!ValUsuario.Equals(usuario, StringComparison.Ordinal) || !ValClave.Equals(clave, StringComparison.Ordinal))
                return "Usuario o Clave son Incorrectas";

            return General.Seguridad.Encriptar(texto);

        }
        [WebMethod]
        public string Desencriptar(string usuario, string clave, string texto)
        {
            string ValUsuario = "servicioasicom";
            string ValClave = "8YZhEyufBPu93QV9b2KK";

            if (!ValUsuario.Equals(usuario, StringComparison.Ordinal) || !ValClave.Equals(clave, StringComparison.Ordinal))
                return "Usuario o Clave son Incorrectas";

            return General.Seguridad.Desencriptar(texto);

        }


    }
}
