using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
   public class EtapaBase:Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int Malla_Id { get; set; }
        public int Orden { get; set; }
    }
    public class EtapaInfo : EtapaBase
    {
        public string DescripcionMalla { get; set; }
        public string DescripcionEstado { get; set; }
    }

}
