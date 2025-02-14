using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.General;

namespace WorkFlow.Reportes.PlantillasMail
{
    public partial class EnvioAdjunto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgLogoCertificado.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");
            lblNombreAdjunto.Text = Mail.objInformacionAdjunto.NombreAdjunto;
            lblNombreCliente.Text = Mail.objInformacionAdjunto.NombreCliente;

        }
    }
}