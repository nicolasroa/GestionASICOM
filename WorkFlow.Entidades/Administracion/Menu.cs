using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class Menu : Base
    {
        public string Descripcion { get; set; }
        public int MenuPadre_Id { get; set; }
        public string Url { get; set; }
        public int Nivel { get; set; }
        public bool? Visible { get; set; }
        public bool? Mantenedor { get; set; }
        public int Orden { get; set; }
        public bool? Administracion { get; set; }
        public bool? MenuPortal { get; set; }
    }



    public class MenuInfo : Menu
    {
        public string DescripcionMenuPadre { get; set; }
        public string DescripcionVisible { get; set; }
        public string DescripcionMantenedor { get; set; }
        public string DescripcionMenuCombo { get; set; }
        public string UrlHtml { get; set; }
    }


    public class MenuUsuario : Base
    {
        public int MenuPadre_Id { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public string UrlHtml { get; set; }
        public int Nivel { get; set; }
        public bool Visible { get; set; }
        public bool Mantenedor { get; set; }
        public int Orden { get; set; }
        public bool Acceso { get; set; }
    }


    public class RolMenu : Base
    {
        public string Url { get; set; }
        public int Rol_Id { get; set; }
        public int Menu_Id { get; set; }
        public bool Acceso { get; set; }
        public bool? PermisoCrear { get; set; }
        public bool? PermisoModificar { get; set; }
        public bool? PermisoEliminar { get; set; }
        public string TituloMenu { get; set; }
    }


    public class ConfigMenu
    {
        public const string UrlControles = "~/Administracion/ControlesMenu.aspx";
        public const string UrlConfiguraControles = "~/Administracion/AsignarControles.aspx";
    }
}
