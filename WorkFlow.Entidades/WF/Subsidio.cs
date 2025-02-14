namespace WorkFlow.Entidades
{
    public class SubsidioBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public int CodigoProgramaMinvu { get; set; }
        public decimal DescuentoCesantiaMinvu { get; set; }
        public decimal MontoMaximoPropiedad { get; set; }
        public int SeguroCesantia_Id { get; set; }
        public int SeguroDesgravamen_Id { get; set; }
        public bool IndDescuentoDividendoMinvu { get; set; }
    }

    public class SubsidioInfo : SubsidioBase
    {
        public string DescripcionEstado { get; set; }
        public string DescripcionSeguroCesantia { get; set; }
    }
}
