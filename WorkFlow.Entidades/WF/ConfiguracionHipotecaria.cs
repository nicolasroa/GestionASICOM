using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class ConfiguracionHipotecariaBase : Base
    {
        public int Producto_Id { get; set; }
        public int Destino_Id { get; set; }
        public int Subsidio_Id { get; set; }
        public int AntiguedadVivienda_Id { get; set; }
        public decimal MontoCreditoMaximo { get; set; }
        public decimal MontoCreditoMinimo { get; set; }
        public decimal PorcentajeFinanciamientoMaximo { get; set; }
        public decimal PorcentajeFinanciamientoMinimo { get; set; }
        public int PlazoMaximo { get; set; }
        public int PlazoMinimo { get; set; }


    }
    public class ConfiguracionHipotecariaInfo : ConfiguracionHipotecariaBase
    {
        public string DescripcionProducto { get; set; }
        public string DescripcionDestino { get; set; }

        public string DescripcionSubsidio { get; set; }
        public string DescripcionAntiguedadVivienda { get; set; }


    }
}
