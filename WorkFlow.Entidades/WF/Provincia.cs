namespace WorkFlow.Entidades
{
    public class ProvinciaBase : Base
    {
        public string Descripcion { get; set; }
        

        public ProvinciaBase() {
            this.Id = -1;
        }
    }
    public class ProvinciaInfo : ProvinciaBase
{
        public int Region_Id { get; set; }
    }
}
