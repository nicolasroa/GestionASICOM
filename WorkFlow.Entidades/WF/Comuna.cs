using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ComunaBase : Base
    {
        public int Provincia_Id { get; set; }

        public ComunaBase() {
            this.Provincia_Id = -1;
        }
    }
    public class ComunaInfo : ComunaBase
    {
        public string Descripcion { get; set; }
    }
    public class ComunaSiiInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Tesoreria_Id { get; set; }
        public int Svs_Id { get; set; }

    }
}
