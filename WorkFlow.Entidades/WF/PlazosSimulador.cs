namespace WorkFlow.Entidades
{
    public class PlazosSimuladorBase : Base
    {
        public int Plazo { get; set;  }
        public int PlazoAdicional1 { get; set; }
        public int PlazoAdicional2 { get; set; }
        public int PlazoAdicional3 { get; set; }
        public int Producto_Id { get; set; }

    }

    public class PlazosSimuladorInfo : PlazosSimuladorBase {

    }
}
