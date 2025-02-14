using System;
using System.Web;

namespace WorkFlow.Entidades
{
    public class Base
    {
        public int Id { get; set; }
        public int? Usuario_Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdRolWF { get; set; }
        public int? IdUsuarioWF { get; set; }
        
        

        public Base()
        {
            Id = -1;
            if (HttpContext.Current.Session["UsuarioId"] != null)
            {
                int? IdUsuario = (int)HttpContext.Current.Session["UsuarioId"];
                Usuario_Id = IdUsuario;
                IdUsuarioWF = IdUsuario;
            }

            if (HttpContext.Current.Session["IdRolWF"] != null)
            {
                IdRolWF = (int)HttpContext.Current.Session["IdRolWF"];
            }

            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }
    }
    public class Log : Base
    {
        public string Procedimiento { get; set; }
        public string Mensaje { get; set; }
        public string PilaEventos { get; set; }


    }

    public static class ConfigBase
    {

        #region Nombre Tablas Fijas

        public static string TablaEstado = "ESTADOS";
        public static string TablaRegiones = "REGIONES";
        public static string TablaRoles = "ROLES";
        public static string TablasCriticas = "TABLASCRITICAS";
        public static string TablaComunas = "COMUNAS";
        public static string TablaRenta = "TIPO_RENTA";

        #endregion

        #region Nombre Vistas WF

        public static string TablaTipoPersona = "TIPO_PERSONA";
        public static string TablaEstadoCivil = "ESTADO_CIVIL";
        public static string TablaNacionalidad = "NACIONALIDAD";
        public static string TablaSexo = "SEXO";
        public static string TablaTipoActividad = "TIPO_ACTIVIDAD";



        #endregion



        #region Codigos Fijos

        public static string CodigoActivo = "A";
        public static string CodigoInactivo = "I";
        #endregion

        public static string ConexionSQL_WF = "ConexionSQL_WF";
        public static string ConexionSQL_SEG = "ConexionSQL_SEG";
        public static string ConexionSQL_ADM = "ConexionSQL_ADM";
        public static string ConexionSQL_LOG = "ConexionSQL_LOG";
        public static string ConexionSQL_DOC = "ConexionSQL_DOC";
        public static string ConexionSQL_GGOO = "ConexionSQL_GGOO";


    }
}

