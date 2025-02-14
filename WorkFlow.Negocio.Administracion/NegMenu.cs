using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Negocio.Administracion
{
    public class NegMenus
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_SEG;
        public NegMenus()
        {

        }

        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Menu según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Menu</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<MenuInfo> Guardar(MenuInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MenuInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<MenuInfo>(Entidad, GlobalDA.SP.Menu_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Menu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que Inserta o Modifica una Entidad RolMenu según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad RolMenu</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<RolMenu> AsignarMenu(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<RolMenu>(Entidad, GlobalDA.SP.RolMenu_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RolMenu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Menu
        /// </summary>
        /// <param name="Entidad">Objeto MenuInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad MenuInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<MenuInfo> Buscar(MenuInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MenuInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuInfo, MenuInfo>(Entidad, GlobalDA.SP.Menu_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Menu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que valida los permisos que tiene un Usuario en relacion a un Menú
        /// </summary>
        /// <param name="Entidad">Objeto Rol Menu con los atributos para realizar la valodación (Usuario_Id y la URL del Menú ) </param>
        /// <returns>Lista RolMenu con el resultado de la valodacion de los permisos</returns>
        public RolMenu ValidarAcceso(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                RolMenu oFiltro = new RolMenu();
                oFiltro.Usuario_Id = Entidad.Usuario_Id;
                if (ListaRolMenu == null)
                    ListaRolMenu = new List<RolMenu>();
                if (ListaRolMenu.Where(rm=>rm.Url == Entidad.Url).Count() == 0)
                {

                    ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RolMenu, RolMenu>(oFiltro, GlobalDA.SP.ValidarAccesoMenu_QRY, BaseSQL);
                    ListaRolMenu = new List<RolMenu>();
                    ListaRolMenu = ObjetoResultado.Lista.Where(rm => rm.Url == Entidad.Url).ToList<RolMenu>();
                }
                if (ListaRolMenu == null)
                {
                    ObjetoResultado.ResultadoGeneral = false;
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolMenu";
                    return null;
                }
                else if (ListaRolMenu.Count() == 0)
                {
                    return new RolMenu
                    {
                        Acceso = false,
                        PermisoCrear = false,
                        PermisoModificar = false,
                        PermisoEliminar = false,
                        TituloMenu = null
                    };

                }
                else if (ListaRolMenu.Count() > 1)
                {
                    var ObjetoRolMenu = new RolMenu();
                    var Acceso = from x in ListaRolMenu
                                 where x.Acceso == true
                                 && x.Url== Entidad.Url
                                 select x;

                    var Crear = from x in ListaRolMenu
                                where x.PermisoCrear == true
                                 && x.Url == Entidad.Url
                                select x;

                    var Modificar = from x in ListaRolMenu
                                    where x.PermisoModificar == true
                                     && x.Url == Entidad.Url
                                    select x;

                    var Eliminar = from x in ListaRolMenu
                                   where x.PermisoEliminar == true
                                    && x.Url == Entidad.Url
                                   select x;

                    ObjetoRolMenu.Acceso = (Acceso.Count() != 0);
                    ObjetoRolMenu.PermisoCrear = (Crear.Count() != 0);
                    ObjetoRolMenu.PermisoModificar = (Modificar.Count() != 0);
                    ObjetoRolMenu.PermisoEliminar = (Eliminar.Count() != 0);
                    ObjetoRolMenu.TituloMenu = ListaRolMenu.FirstOrDefault(rm=>rm.Url==Entidad.Url).TituloMenu;

                    return ObjetoRolMenu;

                }
                else
                {
                    return ListaRolMenu.FirstOrDefault(rm => rm.Url == Entidad.Url);
                }

            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// Método que lista el menu que tiene asignado un Usuario
        /// </summary>
        /// <param name="Entidad">Objeto RolMenu con el Usuario_Id</param>
        /// <returns>Lista de MenuUsuario, usado para vargar el menu del sistema</returns>
        public Resultado<MenuUsuario> ListarMenuUsuario(UsuarioInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MenuUsuario>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuUsuario, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " MenuUsuario";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que retorna un objeto RolMenu
        /// </summary>
        /// <param name="Entidad">Objeto RolMenu con los atributos correspondientes</param>
        /// <returns>Objeto RolMenu en el atributo Objeto del resultado</returns>
        public Resultado<RolMenu> RolMenu(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<RolMenu, RolMenu>(Entidad, GlobalDA.SP.RolMenu_QRY, BaseSQL);
                ObjetoResultado.Objeto = ObjetoResultado.Lista.FirstOrDefault();
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolMenu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Metodo que carga el menú principal según los permisos del rol del Usuario
        /// </summary>
        /// <param name="rdMenu">Control RadMenu de la Master Page</param>
        /// <param name="Entidad">Objeto de la Clase Usuario con el ID del usuario validado.</param>
        /// <returns>Retorna el resultado de la carga asdemas del Menú por Referencia.</returns>
        public Resultado<MenuUsuario> CargarMenu(ref System.Web.UI.WebControls.Menu rdMenu, UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr;
            var ObjetoResultado = new Resultado<MenuUsuario>();

            if (ListaMenuUsuario != null)
            {
                if (ListaMenuUsuario.Count != 0)
                {
                    ObjetoResultado.Lista = ListaMenuUsuario;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuUsuario, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);
                    ListaMenuUsuario = ObjetoResultado.Lista;
                }
            }
            else
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuUsuario, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);
                ListaMenuUsuario = ObjetoResultado.Lista;
            }


            dt.Columns.Add("Id");
            dt.Columns.Add("MenuPadre_Id");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Url");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Visible");
            dt.Columns.Add("Orden");

            try
            {
                foreach (MenuUsuario ObjetoMenu in ObjetoResultado.Lista)
                {
                    dr = dt.NewRow();
                    dr["Id"] = ObjetoMenu.Id;
                    dr["MenuPadre_Id"] = ObjetoMenu.MenuPadre_Id;
                    dr["Descripcion"] = ObjetoMenu.Descripcion;
                    dr["Url"] = ObjetoMenu.Url;
                    dr["Nivel"] = ObjetoMenu.Nivel;
                    dr["Visible"] = ObjetoMenu.Visible;
                    dr["Orden"] = ObjetoMenu.Orden;
                    dt.Rows.Add(dr);
                }

                dsPadres.Tables.Add(dt);

                dsPadres.Relations.Add("NodeRelation", dsPadres.Tables[0].Columns["Id"], dsPadres.Tables[0].Columns["MenuPadre_Id"]);
                ArbolMenuRaiz(dsPadres.Tables[0].Rows, ref rdMenu);



            }
            catch (Exception Ex)
            {
                ObjetoResultado.Mensaje = Ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }



        public Resultado<MenuUsuario> CargarMenu(UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            string HtmlMenu = "";
            var ObjetoResultado = new Resultado<MenuUsuario>();
            if (ListaMenuUsuario != null)
            {
                if (ListaMenuUsuario.Count != 0)
                {
                    ObjetoResultado.Lista = ListaMenuUsuario;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuUsuario, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);
                    ListaMenuUsuario = ObjetoResultado.Lista;
                }
            }
            else
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuUsuario, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);
                ListaMenuUsuario = ObjetoResultado.Lista;
            }
            try
            {




                HtmlMenu = "";

                foreach (var menu1 in ListaMenuUsuario.Where(m => m.Nivel == 1 && m.Visible == true).OrderBy(m => m.Orden))
                {
                    var lstMenu2 = ListaMenuUsuario.Where(m => m.MenuPadre_Id == menu1.Id && m.Visible == true).OrderBy(m => m.Orden);

                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "<li class=\"dropdown - menu\">";
                        HtmlMenu = HtmlMenu + "<a runat=\"server\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" href=\"#\">" + menu1.Descripcion + "<span class=\"caret\"></span></a>";
                    }
                    else
                    {
                        HtmlMenu = HtmlMenu + "<li><a runat=\"server\" href=\"" + (HttpContext.Current.Handler as Page).ResolveUrl(menu1.UrlHtml) + "\">" + menu1.Descripcion + "</a></li>";
                    }

                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "<ul class=\"dropdown-menu\" role=\"menu\">";
                        foreach (var menu2 in lstMenu2)
                        {
                            var lstMenu3 = ListaMenuUsuario.Where(m => m.MenuPadre_Id == menu2.Id && m.Visible == true).OrderBy(m => m.Orden);
                            if (lstMenu3.Count() != 0)
                            {

                                HtmlMenu = HtmlMenu + "<li class=\"dropdown-submenu\">";
                                HtmlMenu = HtmlMenu + "<a runat=\"server\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"  href=\"#\">" + menu2.Descripcion + "</a>";
                            }
                            else
                            {
                                HtmlMenu = HtmlMenu + "<li><a runat=\"server\" href=\"" + (HttpContext.Current.Handler as Page).ResolveUrl(menu2.UrlHtml) + "\">" + menu2.Descripcion + "</a></li>";
                            }

                            if (lstMenu3.Count() != 0)
                            {
                                HtmlMenu = HtmlMenu + "<ul class=\"dropdown-menu\">";
                                foreach (var menu3 in lstMenu3)
                                {
                                    HtmlMenu = HtmlMenu + "<li><a runat=\"server\" href=\"" + (HttpContext.Current.Handler as Page).ResolveUrl(menu3.UrlHtml) + "\">" + menu3.Descripcion + "</a></li>";
                                }
                                HtmlMenu = HtmlMenu + "</ul>";
                            }

                            if (lstMenu3.Count() != 0)
                            {
                                HtmlMenu = HtmlMenu + "</li>";
                            }

                        }
                        HtmlMenu = HtmlMenu + "</ul>";
                    }
                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "</li>";
                    }
                }


                ObjetoResultado.ValorString = HtmlMenu;

            }
            catch (Exception Ex)
            {
                ObjetoResultado.Mensaje = Ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }











        public Resultado<MenuInfo> CargarMenuASP(ref System.Web.UI.WebControls.Menu rdMenu, UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr;
            var ObjetoResultado = new Resultado<MenuInfo>();


            ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, GlobalDA.SP.MenuUsuario_QRY, BaseSQL);

            dt.Columns.Add("Id");
            dt.Columns.Add("MenuPadre_Id");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Url");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Visible");
            dt.Columns.Add("Orden");

            try
            {
                foreach (MenuInfo ObjetoMenu in ObjetoResultado.Lista)
                {
                    dr = dt.NewRow();
                    dr["Id"] = ObjetoMenu.Id;
                    dr["MenuPadre_Id"] = ObjetoMenu.MenuPadre_Id;
                    dr["Descripcion"] = ObjetoMenu.Descripcion;
                    dr["Url"] = ObjetoMenu.Url;
                    dr["Nivel"] = ObjetoMenu.Nivel;
                    dr["Visible"] = ObjetoMenu.Visible;
                    dr["Orden"] = ObjetoMenu.Orden;
                    dt.Rows.Add(dr);
                }

                dsPadres.Tables.Add(dt);

                dsPadres.Relations.Add("NodeRelation", dsPadres.Tables[0].Columns["Id"], dsPadres.Tables[0].Columns["MenuPadre_Id"]);
                ArbolMenuRaizASP(dsPadres.Tables[0].Rows, ref rdMenu);



            }
            catch (Exception Ex)
            {
                ObjetoResultado.Mensaje = Ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }
        /// <summary>
        /// Metodo que carga los Nodos Principales del Menu
        /// </summary>
        /// <param name="filas">Data con la lista de menu relacionada</param>
        /// <param name="menu"> ref del Menu</param>
        private void ArbolMenuRaiz(DataRowCollection filas, ref System.Web.UI.WebControls.Menu menu)
        {
            bool ConHijos = false;

            foreach (System.Data.DataRow fila in filas)
            {
                System.Web.UI.WebControls.MenuItem hijo = new MenuItem();

                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["Url"].ToString();
                else
                    hijo.NavigateUrl = "";

                if (fila.IsNull("MenuPadre_Id") & fila["Visible"].ToString() == "True")
                {




                    ArbolMenuNodo(fila.GetChildRows("NodeRelation"), ref hijo, ref ConHijos);
                    if (ConHijos)
                        menu.Items.Add(hijo);




                }
            }
        }

        private void ArbolMenuRaizASP(DataRowCollection filas, ref System.Web.UI.WebControls.Menu menu)
        {
            bool ConHijos = false;

            foreach (System.Data.DataRow fila in filas)
            {
                MenuItem hijo = new MenuItem();

                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["Url"].ToString();

                if (fila.IsNull("MenuPadre_Id") & fila["Visible"].ToString() == "True")
                {




                    ArbolMenuNodoASP(fila.GetChildRows("NodeRelation"), ref hijo, ref ConHijos);
                    if (ConHijos)
                        menu.Items.Add(hijo);




                }
            }
        }
        /// <summary>
        /// Metodo que carga los hijos.
        /// </summary>
        /// <param name="filas"> Data del Nodo Padre</param>
        /// <param name="nodo">Nodo a llenar</param>
        private void ArbolMenuNodo(DataRow[] filas, ref System.Web.UI.WebControls.MenuItem nodo, ref bool ConHijos)
        {
            bool Hijos = false;
            foreach (DataRow fila in filas)
            {
                System.Web.UI.WebControls.MenuItem hijo = new MenuItem();


                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["url"].ToString();
                else
                    hijo.NavigateUrl = "";
                var asd = fila["Visible"].ToString();

                if (fila["Visible"].ToString() == "True")
                {
                    ConHijos = true;
                    nodo.ChildItems.Add(hijo);
                    ArbolMenuNodo(fila.GetChildRows("NodeRelation"), ref hijo, ref Hijos);
                }
            }
        }

        private void ArbolMenuNodoASP(DataRow[] filas, ref MenuItem nodo, ref bool ConHijos)
        {
            bool Hijos = false;
            foreach (DataRow fila in filas)
            {
                MenuItem hijo = new MenuItem();


                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["url"].ToString();
                var asd = fila["Visible"].ToString();

                if (fila["Visible"].ToString() == "True")
                {
                    ConHijos = true;
                    nodo.ChildItems.Add(hijo);
                    ArbolMenuNodoASP(fila.GetChildRows("NodeRelation"), ref hijo, ref Hijos);
                }
            }
        }
        public Resultado<RolMenu> AsignacionMenu(GridView Grid, RolMenu ObjetoRolMenu, int MenuPadre_Id)
        {

            var ResultadoRolMenu = new Resultado<RolMenu>();
            var NegMenu = new NegMenus();
            bool AsignacionPadre = false;
            int ContadorActivos = 0;
            if (MenuPadre_Id != -1)
            {
                foreach (GridViewRow Row in Grid.Rows)
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
                        ObjetoRolMenu.Menu_Id = MenuPadre_Id;
                        ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }
                else
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = false;
                        ObjetoRolMenu.Menu_Id = MenuPadre_Id;
                        ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }

            }

            foreach (GridViewRow Row in Grid.Rows)
            {
                CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);

                ObjetoRolMenu.Acceso = chkAcceso.Checked;


                if (Grid.ID == Constantes.gvHijosMantenedores || Grid.ID == Constantes.gvMantenedores)
                {
                    CheckBox chkCrear = (CheckBox)Row.FindControl(Constantes.chkCrear);
                    CheckBox chkModificar = (CheckBox)Row.FindControl(Constantes.chkModificar);
                    CheckBox chkEliminar = (CheckBox)Row.FindControl(Constantes.chkEliminar);

                    ObjetoRolMenu.PermisoCrear = chkCrear.Checked;
                    ObjetoRolMenu.PermisoModificar = chkModificar.Checked;
                    ObjetoRolMenu.PermisoEliminar = chkEliminar.Checked;
                }
                ObjetoRolMenu.Menu_Id = int.Parse(Grid.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);

            }


            return ResultadoRolMenu;



        }

        #endregion

        #region PROPIEDADES
        public static RolMenu Permisos
        {
            get { return (RolMenu)HttpContext.Current.Session[ISesiones.Permisos]; }
            set { HttpContext.Current.Session.Add(ISesiones.Permisos, value); }
        }

        public static List<MenuInfo> lstMenus
        {
            get { return (List<MenuInfo>)HttpContext.Current.Session[ISesiones.lstMenus]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstMenus, value); }
        }


        public static List<MenuUsuario> ListaMenuUsuario
        {
            get { return (List<MenuUsuario>)HttpContext.Current.Session[ISesiones.MenuUsuario]; }
            set { HttpContext.Current.Session.Add(ISesiones.MenuUsuario, value); }
        }
        public static List<RolMenu> ListaRolMenu
        {
            get { return (List<RolMenu>)HttpContext.Current.Session[ISesiones.ListaRolMenu]; }
            set { HttpContext.Current.Session.Add(ISesiones.ListaRolMenu, value); }
        }
        public static List<RolMenu> ListaPermisosRolMenu
        {
            get { return (List<RolMenu>)HttpContext.Current.Session[ISesiones.ListaPermisosRolMenu]; }
            set { HttpContext.Current.Session.Add(ISesiones.ListaPermisosRolMenu, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string Permisos = "PermisosDeAcceso";
            public static string MenuUsuario = "LstMenuUsuario";
            public static string ListaRolMenu = "ListaRolMenu";
            public static string lstMenus = "lstMenus";
            public static string ListaPermisosRolMenu = "ListaPermisosRolMenu";
        }
        #endregion

    }
}
