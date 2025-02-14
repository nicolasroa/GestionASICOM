using System;

namespace WorkFlow.Entidades
{
    public class ParidadInfo
    {
        public int CodigoMoneda_Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        public int Plazo { get; set; }
        public decimal MontoCredito { get; set; }
        public DateTime FechaParidad { get; set; }
    }
}
