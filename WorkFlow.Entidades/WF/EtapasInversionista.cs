using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class EtapasInversionistaFiltro
    {
        public int Inversionista_Id { get; set; }
        public int TipoConsulta { get; set; }
        public int Etapa_Id { get; set; }
        public int IndVenta { get; set; }

    }


    public class EtapasInversionistaResumen
    {
        public int IdEtapa { get; set; }
        public string DescripcionEtapa { get; set; }
        public int CantidadPorPromesar { get; set; }
        public decimal PorcPorPromesar { get; set; }
        public int CantidadPromesadas { get; set; }
        public decimal PorcPromesadas { get; set; }
        public int CantidadVendidas { get; set; }
        public decimal PorcVendidas { get; set; }
    }

    public class EtapasInversionistaDetalle
    {
        public string EstadoVenta { get; set; }
        public int NumeroSolicitud { get; set; }
        public string NumeroOperacion { get; set; }
        public string NombreCliente { get; set; }
        public string RutCliente { get; set; }

        public decimal MontoCredito { get; set; }
        public decimal PorcentajeFinanciamiento { get; set; }
        public int Plazo { get; set; }
        public decimal TasaCredito { get; set; }
        
        public decimal MontoVenta { get; set; }
        public DateTime? FechaInicioEtapa { get; set; }

        public DateTime? FechaTerminoEtapa { get; set; }
        public int DiasEnEtapa { get; set; }
        public int? CaratulaCBR { get; set; }
        public DateTime? FechaIngresoCBR { get; set; }
        public string DescripcionCBR { get; set; }




    }

}
