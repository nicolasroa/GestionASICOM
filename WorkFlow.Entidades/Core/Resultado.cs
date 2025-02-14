using System.Collections.Generic;

namespace WorkFlow.Entidades
{
    public class Resultado<T>
    {
        public bool ResultadoGeneral { get; set; }
        public T Objeto { get; set; }
        public List<T> Lista { get; set; }
        public string Mensaje { get; set; }
        public int Id { get; set; }
        public decimal? ValorDecimal { get; set; }
        public int ValorInt { get; set; }
        public bool ValorBool { get; set; }
        public string ValorString { get; set; }

        public Resultado()
        {
            ResultadoGeneral = true;
            Mensaje = "";
        }
    }
}
