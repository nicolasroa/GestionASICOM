using WorkFlow.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace WorkFlow.AccesoDatos
{
    public static class GlobalDA
    {

        /// <summary>
        /// Lista de Procedimientos Almacenados
        /// </summary>
        public enum SP
        {

            #region Procedimientos de Usuarios

            Usuario_GRB,
            Usuario_QRY,
            UsuarioRol_QRY,
            UsuarioRol_INS,
            UsuarioRol_DEL,

            #endregion

            #region Procedimientos de Roles
            Roles_GRB,
            Roles_QRY,
            #endregion  

            #region Procedimientos de Menu

            Menu_GRB,
            Menu_QRY,
            RolMenu_GRB,
            RolMenu_DEL,
            RolMenu_QRY,
            MenuUsuario_QRY,
            ValidarAccesoMenu_QRY,



            #endregion

            #region Procedimientos de Controles

            Controles_GRB,
            Controles_QRY,
            RolMenuControles_GRB,
            RolMenuControles_QRY,
            MenuControlesUsuario_QRY,

            #endregion

            #region Procedimientos de Tablas

            Tablas_GRB,
            Tablas_QRY,
            IdentificadorMaestro_QRY,
            #endregion

            #region Procedimientos de ConfiguracionGeneral

            ConfiguracionGeneral_GRB,
            ConfiguracionGeneral_QRY,

            #endregion

            #region Procedimientos de Log

            Log_GRB,
            Auditoria_QRY,
            ResponsablesAuditoria_QRY,

            #endregion

            #region Procedimientos WF
            //// Simulador Hipotecario
            Tarifado_GRB,
            Tarifado_QRY,
            Paridad_QRY,
            TMC_QRY,
            Clientes_QRY,
            Clientes_GRB,
            DireccionesCliente_GRB,
            DireccionesCliente_QRY,
            DireccionesCliente_DEL,
            Profesiones_QRY,
            CategoriasSii_QRY,
            SubCategoriasSii_QRY,
            ActividadesSii_QRY,
            Eventos_GRB,
            Eventos_QRY,
            ResponsablesEvento_QEY,
            EventoRoles_QRY,
            EventoRoles_INS,
            EventoRoles_DEL,

            Acciones_GRB,
            Acciones_QRY,
            AccionesEvento_QRY,

            Flujo_QRY,
            Flujo_DEL,
            Flujo_INS,
            Etapas_QRY,
            Etapas_GRB,
            TerminarEvento_PRC,
            IniciarFlujo_PRC,
            IniciarFlujoTasacion_PRC,
            IniciarFlujoEETT_PRC,
            IniciarFlujoDPS_PRC,
            TipoFinanciamiento_GRB,
            TipoFinanciamiento_QRY,

            Sucursales_QRY,
            Sucursales_GRB,

            Inmobiliarias_QRY,
            Inmobiliarias_GRB,

            Proyectos_QRY,
            Proyectos_GRB,
            Notarias_QRY,
            Notarias_GRB,
            UsoPropiedad_QRY,
            TipoInmueble_QRY,
            TipoInmueble_GRB,
            TipoConstruccion_QRY,
            TipoConstruccion_GRB,
            Propiedad_QRY,
            Propiedad_GRB,
            Propiedad_DEL,
            Tasaciones_QRY,
            Tasaciones_GRB,
            Tasaciones_DEL,
            DocumentosEstudioTitulo_GRB,
            DocumentosEstudioTitulo_QRY,
            RegistroDocumentosEstudioTitulo_DEL,
            RegistroDocumentosEstudioTitulo_INS,
            RegistroDocumentosEstudioTitulo_QRY,
            DocumentosPersonales_GRB,
            DocumentosPersonales_QRY,
            RegistroDocumentosPersonales_DEL,
            RegistroDocumentosPersonales_INS,
            RegistroDocumentosPersonales_QRY,
            RegistroDocumentosPersonalesSolicitados_QRY,
            RegistroDocumentosOriginacionComercial_DEL,
            RegistroDocumentosOriginacionComercial_INS,
            RegistroDocumentosOriginacionComercial_QRY,
            RegistroDocumentosOriginacionComercialSolicitados_QRY,

            RegistroAntecedentesSalud_DEL,
            RegistroAntecedentesSalud_INS,
            RegistroAntecedentesSalud_QRY,
            Regiones_QRY,

            Provincias_QRY,

            Comunas_QRY,
            ComunasSii_QRY,

            Producto_QRY,
            Producto_GRB,
            ConfiguracionHipotecaria_QRY,
            ConfiguracionHipotecaria_GRB,
            BeneficiarioDestinoFondo_QRY,
            TipoDestinoFondo_GRB,
            TipoDestinoFondo_QRY,
            DestinoFondo_GRB,
            DestinoFondo_QRY,
            DestinoFondo_DEL,
            Objetivo_QRY,
            Objetivo_GRB,

            Destino_QRY,
            Destino_GRB,

            Subsidio_QRY,
            Gracia_QRY,
            PlazosSimulador_QRY,

            Seguros_QRY,
            SegurosContratados_QRY,
            SegurosContratados_GRB,
            TipoGastoOperacional_QRY,
            TipoGastoOperacional_GRB,
            GastosOperacionales_QRY,
            GastosOperacionales_GRB,
            GastosOperacionales_DEL,
            QuienPaga_QRY,
            ComoPaga_QRY,
            ValorizarGastoOperacional_PRC,
            GastosOperacionalesSimulacion_QRY,


            Simulacion_QRY,
            Simulacion_PRC,
            Simulacion_GRB,

            TextoReportes_QRY,
            TipoParticipante_QRY,
            TipoParticipante_GRB,

            AntecedentesLaborales_GRB,
            AntecedentesLaborales_QRY,
            AntecedentesLaborales_DEL,
            ClientesRelacionados_GRB,
            ClientesRelacionados_QRY,
            ClientesRelacionados_DEL,
            Participante_QRY,
            Participante_GRB,
            Participante_DEL,
            Solicitudes_QRY,
            Solicitudes_GRB,
            SolicitudesPorTasar_QRY,    
            SolicitudesEstudioTitulo_QRY,
            SolicitudesDPS_QRY,
            ConsultaActivaciones_QRY,
            DetalleActivaciones_QRY,
            ValidarActivacionWF_PRC,
            RechazosActivacion_QRY,
            ActivarOperacion_PRC,
            AsignacionSolicitud_GRB,
            AsignacionSolicitud_QRY,
            ReasignacionSolicitudes_QRY,
            ReasignacionSolicitudes_PRC,
            FinalidadCredito_QRY,
            UtilidadCredito_QRY,
            CalcularCAE_PRC,
            BandejaEntrada_QRY,
            ReporteBandejaEntrada_QRY,
            BandejaEntradaEtapas_QRY,
            BandejaEntradaEventos_QRY,
            BandejaSeguimiento_QRY,

            Observaciones_QRY,
            Observaciones_GRB,

            Inversionista_QRY,
            ControlEventosInversionista_QRY,
            SolicitudInversionistas_GRB,
            SolicitudInversionistas_DEL,
            SolicitudInversionistas_QRY,
            TipoActivo_QRY,
            TipoActivo_GRB,
            ActivosCliente_QRY,
            ActivosCliente_GRB,
            ActivosCliente_DEL,
            ActivosClienteTotal_QRY,
            ActivosSolicitud_QRY,
            TipoPasivo_GRB,
            TipoPasivo_QRY,
            PasivosCliente_QRY,
            PasivosCliente_GRB,
            PasivosCliente_DEL,
            PasivosClienteTotal_QRY,
            PasivosSolicitud_QRY,
            RentasCliente_QRY,
            RentaClientePromedio_QRY,
            RentasCliente_GRB,
            RentasCliente_DEL,
            FlujosMensuales_QRY,
            DicomCliente_QRY,
            DicomCliente_GRB,
            DicomCliente_DEL,
            DicomClienteTotal_QRY,

            TipoRenta_GRB,
            TipoRenta_QRY,

            AsignacionTipoFabrica_INS,
            AsignacionTipoFabrica_QRY,
            AsignacionTipoFabrica_DEL,

            Fabricas_GRB,
            Fabricas_QRY,

            InstitucionesFinancieras_QRY,

            ApoderadoTasacion_QRY,
            ApoderadoTasacion_GRB,
            ApoderadoTasacion_DEL,

            DocumentosCartaResguardo_QRY,
            RegistroDocumentosCartaResguardo_QRY,
            RegistroDocumentosCartaResguardo_INS,
            RegistroDocumentosCartaResguardo_DEL,

            Conservador_QRY,
            DatosCBR_QRY,
            DatosCBR_GRB,

            ControlEventos_QRY,
            ControlEventos_GRB,
            BandejaDesembolso_QRY,
            IniciarFlujoDesembolso_PRC,
            BandejaEndoso_QRY,
            BandejaEntradaAvance_QRY,
            #endregion


            #region Integracion ADM GGOO
            ResumenCuenta_QRY,
            AgregarPoliticaGGOO_GRB,

            #endregion
            #region GPS


            GPS_EncabezadoSolicitud_QRY,
            GPS_ControlEtapas_QRY,
            GPS_GastosOperacionales_QRY,
            GPS_Observaciones_QRY,
            GPS_Seguros_QRY,
            GPS_Participantes_QRY,
            #endregion

            #region Procedimientos Documental
            GruposDocumentos_QRY,
            GruposDocumentosPortal_QRY,
            GruposDocumentosPortalNew_QRY,
            GruposDocumentos_INS,
            TiposDocumentos_QRY,
            TiposDocumentosPortal_QRY,
            TiposDocumentosPortalNew_QRY,
            TiposDocumentos_INS,
            DOC_DatosWorkflow_QRY,
            Bitacora_QRY,
            ArchivosRepositoriosNew_QRY,
            BandejaGrupoDocumentoSolicitud_QRY,
            BandejaGrupoDocumentoRepositorio_QRY,
            ArchivosRepositorios_DEL,
            ArchivosRepositorios_INS,
            AsignarDocumentoSolicitud





            #endregion



        }
        /// <summary>
        /// Lista de Acciones de la capa de Acceso a Datos
        /// </summary>
        public enum Accion
        {
            Buscar,
            Guardar,
            Eliminar,
            Validar
        }
        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(T Entidad, string Procedimiento, String BaseSQL)
        {
            var ObjResultado = new Resultado<T>();

            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                var db = factory.Create(BaseSQL);
                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 20;
                    var mensaje = "";
                    mensaje = db.SetParametros(store, Entidad);
                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        return ObjResultado;
                    }

                    db.ExecuteNonQuery(store);
                    store.Connection.Close();
                    store.Connection.Dispose();

                }
                return ObjResultado;

            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }





        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(ref T Entidad, string Procedimiento, string BaseSQL)
        {
            var ObjResultado = new Resultado<T>();
            try
            {


                List<T> Lista = new List<T>();
                IDataReader dr;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                var db = factory.Create(BaseSQL);

                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 20;
                    var mensaje = "";
                    mensaje = db.SetParametros(store, Entidad);
                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        return ObjResultado;
                    }
                    dr = db.ExecuteReader(store);
                    var resultado = GetResultado<T>(ref mensaje, dr);
                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        store.Connection.Close();
                        store.Connection.Dispose();
                        return ObjResultado;
                    }


                    Entidad = resultado.FirstOrDefault();
                    store.Connection.Close();
                    store.Connection.Dispose();

                }
                return ObjResultado;


            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);

                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }
        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoLog<T>(T Entidad, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();
            try
            {


                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                var db = factory.Create(ConfigBase.ConexionSQL_LOG);
                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 20;
                    var mensaje = "";
                    mensaje = db.SetParametros(store, Entidad);
                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        return ObjResultado;
                    }
                    db.ExecuteNonQuery(store);
                    store.Connection.Close();
                    store.Connection.Dispose();

                }
                return ObjResultado;
            }
            catch (Exception Ex)
            {
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }
        public static Resultado<T> EjecutarProcedimientoBusqueda<T, X>(X Entidad, string Procedimiento, String BaseSQL)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                var db = factory.Create(BaseSQL);

                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 20;
                    var mensaje = "";
                    mensaje = db.SetParametros(store, Entidad);
                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        return ObjResultado;
                    }
                    dr = db.ExecuteReader(store);



                    ObjResultado.Lista = GetResultado<T>(ref mensaje, dr);

                    if (mensaje != "")
                    {
                        ObjResultado.ResultadoGeneral = false;
                        ObjResultado.Mensaje = mensaje;
                        store.Connection.Close();
                        store.Connection.Dispose();
                        return ObjResultado;
                    }
                    store.Connection.Close();
                    store.Connection.Dispose();

                }
                return ObjResultado;
            }
            catch (Exception Ex)
            {
                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                ObjResultado.Lista = new List<T>();
                return ObjResultado;
            }
        }



        #region Métodos Generales
        /// <summary>
        /// Asocia los Atributos de una Entidad con los parametros de entrada de un Procedimiento Almacenado
        /// </summary>
        /// <param name="db">Base de Datos</param>
        /// <param name="dbCommand">Comando con el Procedimiento Almacenado a Asociar</param>
        /// <param name="input">Entidad a Asociar</param>
        public static string SetParametros(this Database db, DbCommand dbCommand, object input)
        {
            string parameterName = "";
            try
            {
                db.DiscoverParameters(dbCommand);

                foreach (DbParameter param in dbCommand.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        parameterName = GetName(param.ParameterName);

                        if (!parameterName.Equals("RETURN_VALUE") && !parameterName.Equals("PromotePrivate"))
                        {
                            object data = GetPropertyValue(input, parameterName);
                            param.Value = ValidateData(data);
                        }
                    }
                }
                return "";
            }
            catch (Exception Ex)
            {

                return Ex.Message + "Parametro " + parameterName;
            }
        }



        public static void GetResultado(this Database db, DbCommand dbCommand, object input)
        {
            try
            {


                db.DiscoverParameters(dbCommand);

                foreach (DbParameter param in dbCommand.Parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                    {
                        string parameterName = GetName(param.ParameterName);

                        if (!parameterName.Equals("RETURN_VALUE"))
                        {
                            try
                            {
                                object value = db.GetParameterValue(dbCommand, parameterName);
                                SetPropertyValue(input, parameterName, value);
                            }
                            catch (Exception)
                            {
                                object value = db.GetParameterValue(dbCommand, parameterName);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static List<T> GetResultado<T>(ref string Mensaje, IDataReader table)
        {
            try
            {


                if (table == null)
                {
                    return null;
                }

                List<T> Lista = new List<T>();

                while (table.Read())
                {
                    T obj = default(T);
                    obj = Activator.CreateInstance<T>();
                    for (int x = 0; x <= table.FieldCount - 1; x++)
                    {
                        var NombreCampo = table.GetName(x);
                        PropertyInfo prop = obj.GetType().GetProperty(table.GetName(x));
                        try
                        {
                            object value = table[table.GetName(x)];
                            if (value == System.DBNull.Value)
                            {
                                value = null;
                            }
                            prop.SetValue(obj, value, null);

                        }
                        catch (Exception e)
                        {
                            var x2 = obj;
                            Mensaje = e.Message + " Variable: " + NombreCampo;
                            return Lista;
                        }

                    }
                    Lista.Add(obj);
                }
                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region "Helper methods"
        private static object ValidateData(object value)
        {
            if (value == null)
                return null;
            else
            {
                Type tipo = value.GetType();

                if (tipo == typeof(int))
                    return (int)value == 0 ? null : value;

                if (tipo == typeof(DateTime))
                    return ((DateTime)value).Equals(DateTime.MinValue) ? null : value;

                return value;
            }
        }

        private static string GetName(string parameterName)
        {
            if (parameterName.StartsWith("@"))
                parameterName = parameterName.Replace("@", "").Trim();

            return parameterName;
        }

        private static object GetPropertyValue(object input, string parameterName)
        {
            Type type = input.GetType();
            PropertyInfo propInfo = type.GetProperty(parameterName);

            object value = propInfo.GetValue(input, null);

            if (value == null && propInfo.PropertyType == typeof(Byte[]))
                return new byte[] { };

            return value;
        }

        private static void SetPropertyValue(object input, string parameterName, object value)
        {
            Type type = input.GetType();
            PropertyInfo propInfo = type.GetProperty(parameterName);

            object data = null;

            if (string.IsNullOrEmpty(value.ToString()) == false)
                data = Convert.ChangeType(value, propInfo.PropertyType);

            propInfo.SetValue(input, data, null);
        }





        public static DataTable ListToDataTable<T>(List<T> Lista)
        {
            try
            {

                DataTable table = new DataTable();

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {

                        T obj = Lista.FirstOrDefault();
                        foreach (PropertyInfo proinfo in obj.GetType().GetProperties())
                        {
                            table.Columns.Add(proinfo.Name);
                        }
                        foreach (T Objeto in Lista)
                        {
                            DataRow fila = table.NewRow();

                            foreach (PropertyInfo proinfo in Objeto.GetType().GetProperties())
                            {
                                var Tipo = proinfo.PropertyType.Name;


                                var x = proinfo.GetValue(Objeto, null);
                                fila[proinfo.Name] = proinfo.GetValue(Objeto, null);

                            }
                            table.Rows.Add(fila);
                        }
                    }
                }
                return table;
            }
            catch
            {
                return null;
                throw;
            }
        }

        #endregion

    }
}
