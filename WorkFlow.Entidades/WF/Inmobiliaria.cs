using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class InmobiliariaBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public string RutInmobiliaria { get; set; }
        public string Contacto { get; set; }
        public string CargoContacto { get; set; }
        public string MailContacto { get; set; }
        public string FonoFijoContacto { get; set; }

        public string CelularContacto { get; set; }

        public InmobiliariaBase()
        {
        }
    }

    public class InmobiliariaInfo : InmobiliariaBase
    {
        public bool IndDesembolso { get; set; }
        public int EventoDesembolso_Id { get; set; }
        public string DescripcionEventoDesembolso { get; set; }
        public string DescripcionEstado { get; set; }


    }
    public class InmobiliariaRep
    {
        
        public string Descripcion { get; set; }
        public string DescripcionEstado { get; set; }
        public string RutInmobiliaria { get; set; }
        public string Contacto { get; set; }
        public string CargoContacto { get; set; }
        public string MailContacto { get; set; }
        public string FonoFijoContacto { get; set; }
        public string CelularContacto { get; set; }
        public string DescripcionEventoDesembolso { get; set; }

    }

}
