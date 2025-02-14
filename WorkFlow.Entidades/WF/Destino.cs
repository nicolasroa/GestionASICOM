using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
   public class DestinoBase: Base
    {
        public int Objetivo_Id { get; set; }
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }

    }
    public class DestinoInfo : DestinoBase
    {
        public string DescripcionObjetivo { get; set; }
        public string DescripcionEstado { get; set; }
    }
}
