using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PlantillasMail
{
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgLogoCertificado.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");
            if (NegUsuarios.Usuario != null)
            {
                lblNombre.Text = NegUsuarios.NuevoUsuario.Nombre;
                lblNombreUsuario.Text = NegUsuarios.NuevoUsuario.NombreUsuario;
                lblClave.Text = Seguridad.Desencriptar(NegUsuarios.NuevoUsuario.Contraseña);
                lnkRutaSitio.NavigateUrl = NegConfiguracionGeneral.Obtener().UrlSitio + "/Login";
                imgLogoCertificado.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");

            }
        }
    }
}