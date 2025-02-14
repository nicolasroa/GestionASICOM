using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ObservacionBase : Base
    {
        public int Solicitud_Id { get; set; }
        public int Evento_Id { get; set; }
        public int Estado_Id { get; set; }
        public int TipoObservacion_Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int UsuarioIngreso_Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSubsano { get; set; }
        public int UsuarioSubsano_Id { get; set; }
        public ObservacionBase()
        {
            this.Estado_Id = -1;
            this.Solicitud_Id = -1;
            this.Evento_Id = -1;
            this.TipoObservacion_Id = -1;
            this.UsuarioIngreso_Id = -1;
            this.UsuarioSubsano_Id = -1;
            FechaSubsano = null;
        }
    }

    public class ObservacionInfo : ObservacionBase
    {
        public string DescripcionEstado { get; set; }
        public string ResponsableIngreso { get; set; }
        public string ResponsableSubsano { get; set; }
        public string DescripcionTipo { get; set; }
        public string DescripcionEvento { get; set; }
    }

}
