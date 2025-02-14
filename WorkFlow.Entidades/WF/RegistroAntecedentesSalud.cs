using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class RegistroAntecedentesSaludBase : Base
    {
        public int Solicitud_Id { get; set; }
        public int Antecedente_Id { get; set; }
        public int Participante_Id { get; set; }
        public string DescripcionAntecedente { get; set; }
        
        public string Observacion { get; set; }
    }
    public class RegistroAntecedentesSaludInfo : RegistroAntecedentesSaludBase
    {
        public bool Seleccionado { get; set; }
        public int Estado_Id { get; set; }
    }
}
