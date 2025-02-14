using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class SucursalBase : Base
    {
        public int Estado_Id { set; get; }
        public string Descripcion { set; get; }

        public SucursalBase()
        {
            this.Estado_Id = -1;
        }
    }

    public class SucursalInfo : SucursalBase
    {
        public string DescripcionEstado { get; set; }
    }
}
