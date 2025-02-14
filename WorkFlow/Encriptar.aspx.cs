using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WorkFlow.General;

namespace WorkFlow
{
    public partial class Encriptar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEncriptat_Click(object sender, EventArgs e)
        {
            lblClaveDesencriptada.Text = Seguridad.Desencriptar(txtClave.Text);
        }
    }
}