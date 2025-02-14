namespace WorkFlow.Entidades
{
    public class TarifadoBase : Base
    {
        public int Inmobiliaria_Id { get; set; }
        public int Producto_Id { get; set; }
        public int Objetivo_Id { get; set; }
        public int Destino_Id { get; set; }
        public int Plazo { get; set; }
        public decimal MontoDesde { get; set; }
        public decimal MontoHasta { get; set; }
        public bool? IndSubsidio { get; set; }
        public decimal TasaMensualBase { get; set; }

        public decimal SpreadMensual { get; set; }
    }
    public class TarifadoInfo : TarifadoBase
    {
        public decimal MontoCredito { get; set; }
        public decimal TasaMensualTotal { get; set; }
        public string DescripcionProducto { get; set; }
        public string DescripcionObjetivo { get; set; }
        public string DescripcionDestino { get; set; }
    }
}
