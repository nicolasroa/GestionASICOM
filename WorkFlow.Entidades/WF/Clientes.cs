using System;

namespace WorkFlow.Entidades
{
    public class Clientes : Base
    {

        public int Rut { get; set; }
        public string Dv { get; set; }
        public int TipoPersona_Id { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Mail { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Sexo_Id { get; set; }
        public int Nacionalidad_Id { get; set; }
        public int EstadoCivil_Id { get; set; }
        public int RegimenMatrimonial_Id { get; set; }
        public int Profesion_Id { get; set; }
        public int Educacion_Id { get; set; }
        public string TituloEducacional { get; set; }
        public int Residencia_Id { get; set; }
        public int NivelEducacional_Id { get; set; }
        public int CategoriaSii1_Id { get; set; }
        public int SubCategoriaSii1_Id { get; set; }
        public int ActividadSii1_Id { get; set; }

        public int CategoriaSii2_Id { get; set; }
        public int SubCategoriaSii2_Id { get; set; }
        public int ActividadSii2_Id { get; set; }

        public int CategoriaSii3_Id { get; set; }
        public int SubCategoriaSii3_Id { get; set; }
        public int ActividadSii3_Id { get; set; }

        public int NumeroHijos { get; set; }
        public int CargasFamiliares { get; set; }

        public Clientes()
        {
            this.Rut = -1;
        }
    }

    public class ClientesInfo : Clientes
    {
        public string DescripcionEstado { get; set; }
        public string NombreCompleto { get; set; }
        public string RutCompleto { get; set; }
        public string DescripcionResidencia { get; set; }
        public string Accion { get; set; }
        public string strEdad { get; set; }
        public int Edad { get; set; }
        public string DescripcionEstadoCivil { get; set; }
        
        public string DescripcionNacionalidad { get; set; }
        public string DescripcionRegimenMatrimonial { get; set; }

        public string Direccion { get; set; }
        public string DireccionCompleta { get; set; }
        public string Numero { get; set; }
        public string Departamento { get; set; }
        public int Region_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Comuna_Id { get; set; }
        public string DescripcionProfesion { get; set; }
        public string DescripcionRegion { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionComuna { get; set; }
        public string DescripcionSexo { get; set; }
        public string DescripcionNivelEducacional { get; set; }
        public string DescripcionActividadSii1 { get; set; }
        public string DescripcionActividadSii2 { get; set; }
        public string DescripcionActividadSii3 { get; set; }



    }


    public class ClienteSimulacion
    {
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Mail { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
    }


    public class CategoriaSiiInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

    }
    public class SubCategoriaSiiInfo : CategoriaSiiInfo
    {
        public int Categoria_Id { get; set; }

    }

    public class ActividadSiiInfo : SubCategoriaSiiInfo
    {
        public int SubCategoria_Id { get; set; }
    }
    public class ProfesionesInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }



    public static class ConfigClientes
    {
        public const string ValidacionNoEncontrado = "NoEncontrado";
        public const string ValidacionInactivo = "Inactivo";
        public const string ValidacionAprobado = "Aprobado";
    }

    public class DireccionClienteInfo:Base
    {
        public int Cliente_Id { get; set; }
        public int TipoDireccion_Id { get; set; }
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string Departamento { get; set; }
        public int Region_Id { get; set; }
        public int Provincia_Id { get; set; }
        public int Comuna_Id { get; set; }
        public string DescripcionTipoDireccion { get; set; }
        public string DescripcionRegion { get; set; }
        public string DescripcionProvincia { get; set; }
        public string DescripcionComuna { get; set; }
        public string DireccionCompleta { get; set; }
    }



}
