using System;

namespace WorkFlow.Entidades
{
    public class SeguroBase : Base
    {
        public int Estado_Id { get; set;  }
        public string Poliza { get; set; }
        public int CompañiaSeguro_Id { get; set; }
        public int TipoSeguro_Id { get; set; }
        public decimal TasaMensual { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaTerminoVigencia { get; set;  }
        public bool PorValorCuota { get; set; }
        public SeguroBase() {
            this.Estado_Id = -1;
        }

}

    public class SeguroInfo : SeguroBase {
        public string DescripcionCompañia { get; set; }
        public string DescripcionTipo { get; set; }
        public string DescripcionGrupo { get; set; }
        public string DescripcionCategoria { get; set; }
        public int IdEstadoCompañia { get; set; }
        public int IdEstadoTipo { get; set; }
        public int IdEstadoGrupo { get; set; }
        public int GrupoSeguro_Id { get; set; }
        public int IdEstadoCategoria { get; set; }
        public int Subsidio_Id { get; set; }
    }
}
