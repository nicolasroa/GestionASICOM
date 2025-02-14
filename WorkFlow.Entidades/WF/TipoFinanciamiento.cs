using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class TipoFinanciamiento : Base
    {
        public string Descripcion { get; set; }
        public int Estado_Id { get; set; }

    }
    public class TipoFinanciamientoInfo : TipoFinanciamiento
    {
        public string DescripcionEstado { get; set; }
    }
}
