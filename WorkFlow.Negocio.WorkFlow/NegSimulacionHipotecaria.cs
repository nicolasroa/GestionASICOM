using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegSimulacionHipotecaria
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de simulación
        /// </summary>
        /// <param name="Entidad">Objeto SimulacionHipotecariaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SimulacionHipotecariaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SimulacionHipotecariaInfo> RealizarSimulacion(SimulacionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SimulacionHipotecariaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SimulacionHipotecariaInfo, SimulacionHipotecariaInfo>(Entidad, GlobalDA.SP.Simulacion_PRC, BaseSQL);
                lstSimulacion = new List<SimulacionHipotecariaInfo>();
                lstSimulacion = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Simulación Hipotecaria.";
                return ObjetoResultado;
            }
        }
        public Resultado<ResultadoSimulacion> RealizarSimulacion(DatosSimulacion Entidad)
        {

            var ObjetoResultado = new Resultado<ResultadoSimulacion>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ResultadoSimulacion, DatosSimulacion>(Entidad, GlobalDA.SP.Simulacion_PRC, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Simulación Hipotecaria.";
                return ObjetoResultado;
            }
        }

        public Resultado<SimulacionHipotecariaInfo> Guardar(SimulacionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SimulacionHipotecariaInfo>();
            try
            {

                if (Administracion.NegUsuarios.Usuario != null)
                    Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.Simulacion_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        public Resultado<SimulacionHipotecariaInfo> Guardar(ref SimulacionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SimulacionHipotecariaInfo>();
            try
            {
                if (Administracion.NegUsuarios.Usuario != null)
                    Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.Simulacion_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Eventoss
        /// </summary>
        /// <param name="Entidad">Objeto SimulacionHipotecariaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SimulacionHipotecariaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SimulacionHipotecariaInfo> Buscar(SimulacionHipotecariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SimulacionHipotecariaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SimulacionHipotecariaInfo, SimulacionHipotecariaInfo>(Entidad, GlobalDA.SP.Simulacion_QRY, BaseSQL);
                lstSimulacion = new List<SimulacionHipotecariaInfo>();
                lstSimulacion = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Acciones";
                return ObjetoResultado;
            }
        }

        public Resultado<ResultadoSimulacion> ProcesarSimulacionWeb(ClienteSimulacion Cliente, DatosSimulacion Simulacion)
        {
            var rProcesarSimulacion = new Resultado<SimulacionHipotecariaInfo>();
            var nSimulacion = new NegSimulacionHipotecaria();
            var oSimulacion = new SimulacionHipotecariaInfo();
            var rCliente = new Resultado<ClientesInfo>();
            var oCliente = new ClientesInfo();
            var nCliente = new NegClientes();
            var ResultadoSimulacion = new Resultado<ResultadoSimulacion>();
            try
            {

                ResultadoSimulacion = nSimulacion.RealizarSimulacion(Simulacion);

                if (ResultadoSimulacion.ResultadoGeneral)
                {
                    oCliente.Rut = Cliente.Rut;
                    rCliente = nCliente.Buscar(oCliente);

                    if (rCliente.ResultadoGeneral)
                    {
                        oCliente = rCliente.Lista.FirstOrDefault();
                        if (oCliente != null)
                        {
                            oCliente.Mail = Cliente.Mail;
                            oCliente.TelefonoFijo = Cliente.TelefonoFijo;
                            oCliente.TelefonoMovil = Cliente.TelefonoMovil;
                        }
                        else
                        {
                            oCliente = new ClientesInfo();
                            oCliente.Rut = Cliente.Rut;
                            oCliente.Dv = Cliente.Dv;
                            oCliente.Nombre = Cliente.Nombre;
                            oCliente.Paterno = Cliente.Paterno;
                            oCliente.Materno = Cliente.Materno;
                            oCliente.Mail = Cliente.Mail;
                            oCliente.TelefonoFijo = Cliente.TelefonoFijo;
                            oCliente.TelefonoMovil = Cliente.TelefonoMovil;
                            oCliente.NombreCompleto = Cliente.Nombre + " " + Cliente.Paterno + " " + Cliente.Materno;
                            if (oCliente.Rut >= 50000000)//Persona Juridica
                                oCliente.TipoPersona_Id = (int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "J");
                            else
                                oCliente.TipoPersona_Id = (int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "N");

                        }
                        rCliente = nCliente.Guardar(oCliente);
                    }
                    else
                    {
                        ResultadoSimulacion.ResultadoGeneral = false;
                        ResultadoSimulacion.Mensaje = rCliente.Mensaje;
                    }


                    oSimulacion.RutCliente = oCliente.Rut;
                    oSimulacion.DividendoUF = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).DividendoUF;
                    oSimulacion.DividendoPesos = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).DividendoPesos;
                    oSimulacion.DividendoTotal = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).DividendoTotal;
                    oSimulacion.DividendoTotalPesos = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).DividendoTotalPesos;
                    oSimulacion.CAE = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).CAE;
                    oSimulacion.CAEMinvu = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).CAEMinvu;
                    oSimulacion.CTC = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).CTC;
                    oSimulacion.CTCMinvu = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).CTCMinvu;
                    oSimulacion.SegDesgravamen = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).SegDesgravamen;
                    oSimulacion.SegIncendio = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).SegIncendio;
                    oSimulacion.SegCesantia = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).SegCesantia;
                    oSimulacion.TasaAnual = ResultadoSimulacion.Lista.FirstOrDefault(s => s.Plazo == Simulacion.Plazo).TasaAnual;
                    //REGISTRA SIMULACION REALIDAZA
                    rProcesarSimulacion = nSimulacion.Guardar(oSimulacion);
                    if (!rProcesarSimulacion.ResultadoGeneral)
                    {
                        ResultadoSimulacion.ResultadoGeneral = false;
                        ResultadoSimulacion.Mensaje = rProcesarSimulacion.Mensaje;
                    }
                }
                return ResultadoSimulacion;
            }
            catch (Exception ex)
            {
                ResultadoSimulacion.ResultadoGeneral = false;
                ResultadoSimulacion.Mensaje = ex.Message;
                return ResultadoSimulacion;
            }
        }





        #endregion

        #region PROPIEDADES
        public static ReporteSimulacion oReporteSimulacion
        {
            get { return (ReporteSimulacion)HttpContext.Current.Session[ISesiones.oReporteSimulacion]; }
            set { HttpContext.Current.Session.Add(ISesiones.oReporteSimulacion, value); }
        }
        public static SimulacionHipotecariaInfo oSimulacion
        {
            get { return (SimulacionHipotecariaInfo)HttpContext.Current.Session[ISesiones.oSimulacion]; }
            set { HttpContext.Current.Session.Add(ISesiones.oSimulacion, value); }
        }

        public static List<SimulacionHipotecariaInfo> lstSimulacion
        {
            get { return (List<SimulacionHipotecariaInfo>)HttpContext.Current.Session[ISesiones.lstSimulacion]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstSimulacion, value); }
        }
        public static bool? indSeleccionSimulacionCliente
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.indSeleccionSimulacionCliente]; }
            set { HttpContext.Current.Session.Add(ISesiones.indSeleccionSimulacionCliente, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string oReporteSimulacion = "oReporteSimulacion";
            public static string lstSimulacion = "lstSimulacion";
            public static string oSimulacion = "oSimulacion";
            public static string indSeleccionSimulacionCliente = "indSeleccionSimulacionCliente";

        }
        #endregion
    }
}
