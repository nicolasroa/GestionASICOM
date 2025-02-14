using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ControlesBase : Base
    {
        public int Estado_Id { get; set; }
        public string IdInterno { get; set; }
        public int Menu_Id { get; set; }
    }

    public class ControlesInfo : ControlesBase
    {

        public string DescripcionEstado { get; set; }
        public string DescripcionMenu { get; set; }
    }

    public class RolMenuControlInfo : ControlesBase
    {
        public int Rol_Id { get; set; }
        public string Url { get; set; }
        public int Control_Id { get; set; }
        public bool Visible { get; set; }
        public bool Activo { get; set; }
        public int EstadoControl_Id { get; set; }

        public RolMenuControlInfo()
        {
            this.Activo = true;
            this.Visible = true;
        }
    }


    public static class ConfigControles
    {
        public static string StringIdControl = "Control_Id";
    }
}
