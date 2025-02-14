using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Master
{
    public partial class PDF : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                imgLogoHeader.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");
            }
        }
    }
}