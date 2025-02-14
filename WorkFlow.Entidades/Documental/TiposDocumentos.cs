using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades.Documental
{
    public class TiposDocumentos : Base
    {
        public string CodTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public int IdGrupoDocumento { get; set; }
        public int Estado_Id { get; set; }
        public int DiasCaducidad { get; set; }
        public int DiasAvisoCaducidad { get; set; }
    }

    public class TiposDocumentosInfo : TiposDocumentos
    {
        public string GrupoDocumento { get; set; }
        public bool PermisoSubir { get; set; }
    }
}
