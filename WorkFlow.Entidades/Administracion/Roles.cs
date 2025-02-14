using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class Roles : Base
    {
        public string Descripcion { get; set; }
        public int Estado_Id { get; set; }
        public int Sistema_Id { get; set; }

    }
    public class RolesInfo : Roles
    {
        public string DescripcionSistema { get; set; }
        public string DescripcionEstado { get; set; }
    }
}
