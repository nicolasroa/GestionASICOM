using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class Tabla : Base
    {
        public int TablaPadre_Id { get; set; }
        public string Nombre { get; set; }
        public string NombreTablaPadre { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }

        public int CodigoInterno { get; set; }
        public int Estado_Id { get; set; }
        public Tabla()
        { Estado_Id = -1; }

    }

    public class TablaInfo : Tabla
    {
        public string NombreEstado { get; set; }
        public string CodigoEstado { get; set; }
        public string ConceptoPadre { get; set; }




    }
}
