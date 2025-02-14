using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;

namespace WebSite.Administracion
{
    public partial class AsignacionMenu : System.Web.UI.Page
    {

     #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "Inicio();", true);
            if (!Page.IsPostBack)
            {
                CargaRoles();
                CargarSecciones();
            }

        }
        private void CargaRoles()
        {
            try
            {
                var oNegRoles = new NegRoles();
                var oRoles = new RolesInfo();
                var oResultado = new Resultado<RolesInfo>();

                oRoles.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");


                oResultado = oNegRoles.Buscar(oRoles);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<RolesInfo>(ref ddlRol, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Roles--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado", null);
                }
            }


        }
        private void CargarSecciones()
        {
            try
            {
                //Declaracion de Varibles
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();
                var ObjetoResultado = new Resultado<MenuInfo>();

                //Asignacion de Variables
                ObjetoMenu.Id = -1;
                ObjetoMenu.MenuPadre_Id = -1;
                ObjetoMenu.Descripcion = "";
                ObjetoMenu.Nivel = 0;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegMenu.Buscar(ObjetoMenu);
                if (ObjetoResultado.ResultadoGeneral)
                {

                    NegMenus.lstMenus = new List<MenuInfo>();
                    NegMenus.lstMenus = ObjetoResultado.Lista;


                    Controles.CargarCombo<MenuInfo>(ref ddlSeccion, NegMenus.lstMenus.Where(m => m.Nivel == 1).ToList(), Constantes.StringId, Constantes.StringDescripcion, "", "");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Menu");
                }
            }
        }

        private void CargarRolMenu()
        {
            var ObjetoRolMenu = new RolMenu();
            var NegMenu = new NegMenus();
            var ResultadoRolMenu = new Resultado<RolMenu>();

            //Asignacion de Variables
            ObjetoRolMenu.Menu_Id = -1;
            ObjetoRolMenu.Rol_Id = int.Parse(ddlRol.SelectedValue);


            ResultadoRolMenu = NegMenu.RolMenu(ObjetoRolMenu);
            if (ResultadoRolMenu.ResultadoGeneral)
            {
                NegMenus.ListaPermisosRolMenu = new List<RolMenu>();
                NegMenus.ListaPermisosRolMenu = ResultadoRolMenu.Lista;
            }

        }
        private void CargarMenus()
        {

            int MenuPadre_Id = int.Parse(ddlSeccion.SelectedValue);


            if (NegMenus.lstMenus.Where(m => m.Nivel == 2).ToList().Count > 0)
            {
                Controles.CargarGrid<MenuInfo>(ref gvOperacionales, NegMenus.lstMenus.Where(m => m.Nivel == 2 && m.Mantenedor == false && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });
                Controles.CargarGrid<MenuInfo>(ref gvMantenedores, NegMenus.lstMenus.Where(m => m.Nivel == 2 && m.Mantenedor == true && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });
                

            }
        }
        public string MyNewRow(object Menu)
        {
            return String.Format(@"</td></tr><tr id ='tr{0}' class='collapsed-row'><td></td><td colspan='100' style='padding:0px; margin:0px;'>", Menu);
        }
        protected void gvOperacionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    var ObjetoRolMenu = new RolMenu();
                    int MenuPadre_Id = (int)gvOperacionales.DataKeys[e.Row.RowIndex].Value;
                    ObjetoRolMenu = NegMenus.ListaPermisosRolMenu.FirstOrDefault(x => x.Menu_Id == MenuPadre_Id);

                    var gvHijosMantenedores = (GridView)e.Row.FindControl("gvHijosMantenedores");
                    var gvHijosOperacionales = (GridView)e.Row.FindControl("gvHijosOperacionales");

                    Controles.CargarGrid<MenuInfo>(ref gvHijosMantenedores, NegMenus.lstMenus.Where(m => m.Nivel == 3 && m.Mantenedor == true && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });
                    Controles.CargarGrid<MenuInfo>(ref gvHijosOperacionales, NegMenus.lstMenus.Where(m => m.Nivel == 3 && m.Mantenedor == false && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });

                    if (ObjetoRolMenu != null)
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);
                    else
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = false;

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
                    Controles.MostrarMensajeError("Error al Cargar Menus Operacionales");
                }
            }
        }
        protected void gvMantenedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {



                    var ObjetoRolMenu = new RolMenu();
                    int MenuPadre_Id = (int)gvMantenedores.DataKeys[e.Row.RowIndex].Value;
                    ObjetoRolMenu = NegMenus.ListaPermisosRolMenu.FirstOrDefault(x => x.Menu_Id == MenuPadre_Id);
                    var gvHijosMantenedores = (GridView)e.Row.FindControl("gvHijosMantenedores");
                    var gvHijosOperacionales = (GridView)e.Row.FindControl("gvHijosOperacionales");

                    Controles.CargarGrid<MenuInfo>(ref gvHijosMantenedores, NegMenus.lstMenus.Where(m => m.Nivel == 3 && m.Mantenedor == true && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });
                    Controles.CargarGrid<MenuInfo>(ref gvHijosOperacionales, NegMenus.lstMenus.Where(m => m.Nivel == 3 && m.Mantenedor == false && m.MenuPadre_Id == MenuPadre_Id).ToList(), new string[] { "Id" });


                    if (ObjetoRolMenu != null)
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkCrear)).Checked = (ObjetoRolMenu.PermisoCrear == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkModificar)).Checked = (ObjetoRolMenu.PermisoModificar == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkEliminar)).Checked = (ObjetoRolMenu.PermisoEliminar == true);
                    }
                    else
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkCrear)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkModificar)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkEliminar)).Checked = false;
                    }


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
                    Controles.MostrarMensajeError("Error al Cargar Menus Operacionales");
                }
            }
        }
        protected void gvHijoMantenedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var ObjetoRolMenu = new RolMenu();
                    int Menu_Id = (int)((GridView)sender).DataKeys[e.Row.RowIndex].Value;
                    ObjetoRolMenu = NegMenus.ListaPermisosRolMenu.FirstOrDefault(x => x.Menu_Id == Menu_Id);

                    if (ObjetoRolMenu != null)
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkCrear)).Checked = (ObjetoRolMenu.PermisoCrear == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkModificar)).Checked = (ObjetoRolMenu.PermisoModificar == true);
                        ((CheckBox)e.Row.FindControl(Constantes.chkEliminar)).Checked = (ObjetoRolMenu.PermisoEliminar == true);
                    }
                    else
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkCrear)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkModificar)).Checked = false;
                        ((CheckBox)e.Row.FindControl(Constantes.chkEliminar)).Checked = false;
                    }


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
                    Controles.MostrarMensajeError("Error al Cargar Menus Operacionales");
                }
            }
        }
        protected void gvHijoOperacionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var ObjetoRolMenu = new RolMenu();
                    int Menu_Id = (int)((GridView)sender).DataKeys[e.Row.RowIndex].Value;
                    ObjetoRolMenu = NegMenus.ListaPermisosRolMenu.FirstOrDefault(x => x.Menu_Id == Menu_Id);

                    if (ObjetoRolMenu != null)
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);
                    }
                    else
                    {
                        ((CheckBox)e.Row.FindControl(Constantes.chkAcceso)).Checked = false;
                    }


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
                    Controles.MostrarMensajeError("Error al Cargar Menus Operacionales");
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarRolMenu();
            CargarMenus();
        }


        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            ProcesarAsignacion();
        }
        private void ProcesarAsignacion()
        {
            var ObjetoRolMenu = new RolMenu();
            var NegMenu = new NegMenus();
            var ObjetoResultado = new Resultado<RolMenu>();
            bool AsignacionPadre = false;
            int ContadorActivos = 0;
            try
            {
                ObjetoRolMenu.Rol_Id = int.Parse(ddlRol.SelectedValue);
                foreach (GridViewRow Row in gvMantenedores.Rows)
                {
                    CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);
                    if (chkAcceso.Checked)
                    {
                        ContadorActivos = ContadorActivos + 1;
                    }
                }
                foreach (GridViewRow Row in gvOperacionales.Rows)
                {
                    CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);
                    if (chkAcceso.Checked)
                    {
                        ContadorActivos = ContadorActivos + 1;
                    }
                }

                if (ContadorActivos != 0)
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = true;
                        ObjetoRolMenu.Menu_Id = int.Parse(ddlSeccion.SelectedValue);
                        ObjetoResultado = new NegMenus().AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }
                else
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = false;
                        ObjetoRolMenu.Menu_Id = int.Parse(ddlSeccion.SelectedValue);
                        ObjetoResultado = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }

                if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }



                if (gvMantenedores.Rows.Count != 0)
                {
                    ObjetoResultado = NegMenu.AsignacionMenu(gvMantenedores, ObjetoRolMenu, -1);
                    if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                    foreach (GridViewRow Row in gvMantenedores.Rows)
                    {
                        GridView gvHijosMantenedores = (GridView)Row.FindControl(Constantes.gvHijosMantenedores);
                        GridView gvHijosOperacionales = (GridView)Row.FindControl(Constantes.gvHijosOperacionales);
                        if (gvHijosMantenedores.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvMantenedores.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosMantenedores, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }

                        if (gvHijosOperacionales.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvMantenedores.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosOperacionales, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }
                    }
                }
                if (gvOperacionales.Rows.Count != 0)
                {
                    ObjetoResultado = NegMenu.AsignacionMenu(gvOperacionales, ObjetoRolMenu, -1);
                    if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                    foreach (GridViewRow Row in gvOperacionales.Rows)
                    {
                        GridView gvHijosMantenedores = (GridView)Row.FindControl(Constantes.gvHijosMantenedores);
                        GridView gvHijosOperacionales = (GridView)Row.FindControl(Constantes.gvHijosOperacionales);
                        if (gvHijosMantenedores.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvOperacionales.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosMantenedores, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }

                        if (gvHijosOperacionales.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvOperacionales.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosOperacionales, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }
                    }
                }
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.AsignacionMenuCompleta.ToString()));
                return;
                Error:
                if (ObjetoResultado.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    return;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorProcesarAsignacionMenu.ToString()));
                }

            }
        }

        #endregion




    }
}