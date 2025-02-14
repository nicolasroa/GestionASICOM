namespace WorkFlow.Entidades
{
    public class EventosBase : Base
    {
        public string Descripcion { get; set; }
        public int Malla_Id { get; set; }
        public int Etapa_Id { get; set; }
        public int Estado_Id { get; set; }
        public bool? IndDesembolso { get; set; }
        public bool? IndEndoso { get; set; }
        public int DuracionEstandar { get; set; }
        public int Rol_Id { get; set; }
        public bool? EventoInicial { get; set; }
        public bool? EventoFinal { get; set; }
        public string DescripcionPlantilla { get; set; }
        public bool IndModificaDatosCredito { get; set; }
        public bool IndModificaDatosParticipantes { get; set; }
        public bool IndModificaDatosPropiedades { get; set; }
        public bool IndModificaDatosSeguros { get; set; }
        public bool IndFlujoEspecial { get; set; }
        public string ProcedimientoDeTermino { get; set; }

    }
    public class EventosInfo : EventosBase
    {
        public string DescripcionMalla { get; set; }
        public string DescripcionEtapa { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionCompleta { get; set; }

        public int RutResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public int Solicitud_Id { get; set; }

    }

    public class EventoRolesBase : Base
    {
        public int Evento_Id { get; set; }
        public int Rol_Id { get; set; }
    }
    public class EventoRolesInfo : EventoRolesBase
    {
        public string DescripcionEvento { get; set; }
        public string DescripcionRol { get; set; }
    }




}
