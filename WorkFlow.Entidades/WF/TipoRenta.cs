using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class TipoRentaBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeCastigo { get; set; }

    }

    public class TipoRentaInfo : TipoRentaBase
    {

    }
}
