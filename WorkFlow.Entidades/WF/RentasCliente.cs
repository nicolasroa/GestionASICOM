using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class RentasClienteBase : Base
    {
        public int RutCliente { get; set; }
        public int TipoRenta_Id { get; set; }
        public decimal Mes1 { get; set; }
        public decimal Mes2 { get; set; }
        public decimal Mes3 { get; set; }
        public decimal Mes4 { get; set; }
        public decimal Mes5 { get; set; }
        public decimal Mes6 { get; set; }
        public decimal RentaPromedio { get; set; }
    }

    public class RentasClienteInfo : RentasClienteBase
    {
        public string DescripcionTipoRenta { get; set; }
    }


    public class FlujosMensualesInfo
    {
        public int Solicitud_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal RentaDeudor { get; set; }
        public decimal RentaCodeudor { get; set; }
        public decimal RentaAval { get; set; }

    }
}
