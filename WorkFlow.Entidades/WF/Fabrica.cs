using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class FabricaBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public string NombreResponsable { get; set; }
        public string ContactoResponsable { get; set; }
        public FabricaBase()
        {
            Id = -1;
            Estado_Id = -1;
        }
    }

    public class FabricaInfo : FabricaBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class AsignacionTipoFabricaBase : Base
    {
        public int Fabrica_Id { get; set; }
        public int TipoFabrica_Id { get; set; }
    }

    public class AsignacionTipoFabricaInfo : AsignacionTipoFabricaBase
    {
        public string DescripcionFabrica { get; set; }
        public string DescripcionTipoFabrica { get; set; }
        public int EstadoFabrica_Id { get; set; }
        public int EstadoTipoFabrica_Id { get; set; }

    }
}
