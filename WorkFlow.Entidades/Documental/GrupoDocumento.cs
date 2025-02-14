using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades.Documental
{
    public class GruposDocumentos : Base
    {
        public string CodGrupoDocumento { get; set; }
        public string GrupoDocumento { get; set; }
        public int Estado_Id { get; set; }
    }
}
