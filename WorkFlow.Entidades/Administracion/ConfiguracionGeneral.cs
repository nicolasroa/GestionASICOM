using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ConfiguracionGeneral : Base
    {
        public int TamanioClave { get; set; }
        public int PlazoValidez { get; set; }
        public int Notificacion { get; set; }
        public int Intentos { get; set; }
        public string UsuarioCorreo { get; set; }
        public string Correo { get; set; }
        public string ClaveCorreo { get; set; }
        public string ServidorEntradaCorreo { get; set; }
        public string ServidorSalidaCorreo { get; set; }
        public string PuertoCorreo { get; set; }
        public bool ConexionSeguraMail { get; set; }
        public string Ambiente { get; set; }
        public int ValidezAprobacionCredito { get; set; }
        public string NombreCliente { get; set; }
        public decimal DescuentoTMC { get; set; }

        public string UrlSitio { get; set; }
        public bool EnviarMail { get; set; }

        public decimal MaximoDividendoRenta { get; set; }
        //Configuracion Sistema Documental
        public int TamanioArchivoBytes { get; set; }

        


    }

    public class ConfiguracionGeneralInfo : ConfiguracionGeneral
    {
       
    }
}
