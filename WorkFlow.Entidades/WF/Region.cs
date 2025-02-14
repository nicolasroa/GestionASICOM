namespace WorkFlow.Entidades
{
    public class RegionBase : Base
    {
        public string Descripcion { get; set; }
        

        public RegionBase() {
            this.Id = -1;
        }
    }
    public class RegionInfo : ProvinciaBase
    {
 
    }
}
