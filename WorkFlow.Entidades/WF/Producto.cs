namespace WorkFlow.Entidades
{
    public class ProductoBase : Base
    {
        public int TipoFinanciamiento_Id { get; set; }
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public bool? IndMesesGracia { get; set; }
        public int MaximoPeriodoGracia { get; set; }
        public int Moneda_Id { get; set; }
        public bool? IndDoblePlazo { get; set; }
        public ProductoBase()
        {
            this.Estado_Id = -1;
        }
    }

    public class ProductoInfo : ProductoBase
    {
        public string DescripcionTipoFinanciamiento { get; set; }
        public string DescripcionEstado { get; set; }
    }


    public class GraciaBase : Base
    {
        public int Producto_Id { get; set; }
       
    }


    public class GraciaInfo : GraciaBase
    {
        public string Descripcion  { get; set; }

    }
}
