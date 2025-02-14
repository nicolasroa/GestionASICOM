using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ProyectoBase : Base
    {
        
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int ComunaSII_Id { get; set; }
        public string RolMatriz { get; set; }
        public int TipoInmueble_Id { get; set; }
        public ProyectoBase() {

        }
    }

    public class ProyectoInfo : ProyectoBase
    {
        public int Inmobiliaria_Id { get; set; }       
        public int Region_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Comuna_Id { get; set; }
        
        public string DescripcionTipoInmueble { get; set; }
        public string Direccion { get; set; }
        public string DescripcionInmobiliaria { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionComuna { get; set; }
        public string DescripcionComunaSII { get; set; }
    }
}
