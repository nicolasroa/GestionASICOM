using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WorkFlow
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            ValidarPagina();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            ValidarSession();

            Usuario.Text = NegUsuarios.Usuario.Nombre.Split(' ')[0].ToString()[0].ToString() + NegUsuarios.Usuario.Nombre.Split(' ')[1].ToString()[0].ToString(); ;

            if (!Page.IsPostBack)
            {
                CargarMenu();
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
                txtValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");

            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesion();
        }
        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {

            Controles.AbrirPopup("~/OperacionesLogin/CambioContraseña.aspx", 400, 600, "Cambio de Contraseña");
        }

        #region "Métodos"

        private void ValidarPagina()
        {
            try
            {
                var ObjetoRolMenu = new RolMenu();
                var NegMenu = new NegMenus();
                string Pagina = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                String[] url = HttpContext.Current.Request.RawUrl.Split('?');

                if (NegUsuarios.Usuario != null)
                    NegUsuarios.Usuario.Rut = (int)NegUsuarios.Usuario.Rut;
                else
                {
                    Controles.MostrarMensajeInfo("Su Sesión ha Caducado, Favor realizar validacion nuevamente");
                    Response.Redirect(Constantes.UrlLogin);
                }

                if (url.Count() > 1 && (url[1].Contains("%7e") || url[1].Contains("~")))
                    Pagina = url[1].Replace("%2f", "/").Replace("%7e", "~");
                ObjetoRolMenu.Usuario_Id = NegUsuarios.Usuario.Rut;
                ObjetoRolMenu.Url = Pagina;
                ObjetoRolMenu = NegMenu.ValidarAcceso(ObjetoRolMenu);
                if (ObjetoRolMenu == null)
                {
                    Response.Redirect(Constantes.UrlSinAcceso);
                }
                if (ObjetoRolMenu.Acceso == false)
                {
                    Response.Redirect(Constantes.UrlSinAcceso);

                }
                NegMenus.Permisos = ObjetoRolMenu;
                this.Page.Title = ObjetoRolMenu.TituloMenu;
            }
            catch (Exception Ex)
            {
                Controles.MostrarMensajeError(Ex.Message, Ex);
            }
        }
        private void ValidarSession()
        {
            if (NegUsuarios.Usuario == null)
            {
                Response.Redirect(Constantes.UrlLogin);
            }
        }
        private void CargarMenu()
        {
            try
            {
                var ObjetoUsuario = new UsuarioInfo();
                ObjetoUsuario = NegUsuarios.Usuario;
                var ObjetoResultado = new Resultado<MenuUsuario>();
                var NegMenu = new NegMenus();
                if (ObjetoUsuario.Rut != null)
                {
                    ObjetoUsuario.Rut = (int)ObjetoUsuario.Rut;
                }
                ObjetoResultado = NegMenu.CargarMenu(ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral == true)
                    NavMenu.InnerHtml = ObjetoResultado.ValorString;
                else
                {
                    if (Constantes.ModoDebug == true)
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    else
                        Controles.MostrarMensajeError("Error al Cargar el Menú");

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
                    Controles.MostrarMensajeError("Error al Cargar el Menú");
                }
            }
        }
        private void CerrarSesion()
        {
            Session.Clear();
            Response.Redirect(Constantes.UrlLogin);
        }



        #endregion


    }
}