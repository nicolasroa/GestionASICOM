using System;

namespace WorkFlow.Entidades
{
    public class ConservadorBase : Base
    {
        public int Estado_Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Region_Id { get; set; }
        public string Conservador { get; set; }

        public ConservadorBase()
        {
            Id = -1;
            Estado_Id = -1;
        }
    }

    public class ConservadorInfo : ConservadorBase
    {
        public string DescripcionEstado { get; set; }
    }

    public class DatosCBRBase : Base
    {
        public int Solicitud_Id { get; set; }
        public int Conservador_Id { get; set; }
        public DateTime? FechaIngresoCBR { get; set; }
        public int NroIngresoCBR { get; set; }
        public DateTime? FechaReparoCBR { get; set; }
        public DateTime? FechaCorreccionAbogadoCBR { get; set; }
        public DateTime? FechaCorreccionNotariaCBR { get; set; }
        public DateTime? FechaSalidaCBR { get; set; }
        public DateTime? FechaInscripcionHipoteca { get; set; }
       
        public DateTime? FechaIngresoHipoteca { get; set; }
        public DateTime? FechaReingresoHipoteca { get; set; }
        public DateTime? FechaReparoHipoteca { get; set; }
        public DateTime? FechaInformePrevioHip { get; set; }
        public DateTime? FechaEnvioInformeCBR { get; set; }
        public int NroDominio { get; set; }
        public string FojasDominio { get; set; }
        public string FojasHipoteca { get; set; }
        public int NroHipoteca { get; set; }
        public int NroProhibicion { get; set; }
        public string FojasProhibicion { get; set; }
        public DatosCBRBase()
        {
            Id = -1;
            Solicitud_Id = -1;
            NroIngresoCBR = -1;
            NroHipoteca = -1;
            NroDominio = -1;
            NroProhibicion = -1;


        }
    }
    public class DatosCBRInfo : DatosCBRBase
    {
        public string Conservador { get; set; }
    }


}
