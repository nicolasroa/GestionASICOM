namespace WorkFlow.Entidades
{
    public class ObjetivoBase : Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }


        public ObjetivoBase()
        {
            this.Estado_Id = -1;
        }
    }

    public class ObjetivoInfo : ObjetivoBase
    {
        public string DescripcionEstado { get; set; }
    }
}
