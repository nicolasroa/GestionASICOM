using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
    public class UsuarioBase : Base
    {

        public int? Rut { get; set; }
        public string Dv { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Sucursal_Id { get; set; }
        public int Estado_Id { get; set; }
        public bool ForzarCambioClave { get; set; }
        public DateTime FechaUltimoCambioClave { get; set; }
        public int IntentosFallidos { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
        public bool PrimerInicio { get; set; }
        public int Fabrica_Id { get; set; }
        public int Inversionista_Id { get; set; }
        public int Inmobiliaria_Id { get; set; }
        public decimal? MontoMaximoAprobacion { get; set; }
        public UsuarioBase()
        {
            this.Rut = -1;
        }
    }

    public class HistorialCambioClave
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public string ClaveUsada { get; set; }
        public DateTime FechaCambio { get; set; }
    }

    public class UsuarioRolBase : Base
    {
       public int Responsable_Id { get; set; }
        public int Rol_Id { get; set; }

    }
    public class UsuarioRolInfo : UsuarioRolBase
    {
        public string DescripcionRol { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public int Sucursal_Id { get; set; }
        public int Fabrica_Id { get; set; }
    }

    public class UsuarioInfo : UsuarioBase
    {
        public string DescripcionEstado { get; set; }
        public string NombreCompleto { get; set; }
        public int Rol_Id { get; set; }
        public string DescripcionSucursal { get; set; }
        public string TelefonoFijo { get; set; }
        public string DescripcionFabrica { get; set; }

    }


    public static class ConfigUsuario
    {

        public const string ValidacionNoEncontrado = "NoEncontrado";
        public const string ValidacionInactivo = "Inactivo";
        public const string ValidacionErrorClave = "ErrorClave";
        public const string ValidacionCambioClave = "CambioClave";
        public const string ValidacionPrimerInicio = "PrimarInicio";
        public const string ValidacionAvisoCambioClave = "AvisoCambioClave";
        public const string ValidacionAprobado = "Aprobado";

        public static string MensajeNoExiste = "MensajeNoExiste";
        public static string MensajeInactivo = "MensajeInactivo";
        public static string MensajeErrorClave = "MensajeErrorClave";
        public static string MensajeCambioClave = "MensajeCambioClave";

        public static string TablaPreguntasSeguridad = "PREGUNTASSEGURIDAD";

        public static string UrlRecuperarContraseña = "~/OperacionesLogin/RecuperarContraseña.aspx";
        public static string UrlCambioContraseña = "~/OperacionesLogin/CambioContraseña.aspx";
        public static string UrlAvisoCambioContraseña = "~/OperacionesLogin/AvisoCambioContraseña.aspx";



      
    }



}
