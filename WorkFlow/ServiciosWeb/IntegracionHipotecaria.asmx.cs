using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.Entidades.GPS;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Services;

namespace WorkFlow.ServiciosWeb
{
    /// <summary>
    /// Descripción breve de IntegracionHipotecaria
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class IntegracionHipotecaria : WebService
    {
        /** Simulación Hipotecaria*/
        [WebMethod]
        public ResultadoSimulacionWeb RealizarSimulacion(ClienteSimulacion Cliente, DatosSimulacion Simulacion)
        {
            var ResultadoSimulacion = new ResultadoSimulacionWeb();
            try
            {
                var ObjetoResultadoSimulacion = new Resultado<ResultadoSimulacion>();
                ObjetoResultadoSimulacion = AccesoDatos.AccesoDatos.Buscar<ResultadoSimulacion, DatosSimulacion>(Simulacion, GlobalDA.SP.Simulacion_PRC, "ConexionSQL_WF");

                if (!ObjetoResultadoSimulacion.ResultadoGeneral)
                {
                    ResultadoSimulacion.ResultadoGeneral = false;
                    ResultadoSimulacion.Mensaje = ObjetoResultadoSimulacion.Mensaje;
                    return ResultadoSimulacion;
                }
                ResultadoSimulacion.lstResultadoSimulacion = ObjetoResultadoSimulacion.Lista;



                var ObjetoResultadoCliente = new Resultado<ClientesSimulacionWeb>();
                ClientesSimulacionWeb objCliente = new ClientesSimulacionWeb();

                objCliente.Rut = Cliente.Rut;

                ObjetoResultadoCliente = AccesoDatos.AccesoDatos.Buscar<ClientesSimulacionWeb, ClientesSimulacionWeb>(objCliente, GlobalDA.SP.Clientes_QRY, "ConexionSQL_WF");

                if (ObjetoResultadoCliente.ResultadoGeneral)
                {
                    if (ObjetoResultadoCliente.Lista.Count > 0)
                    {
                        objCliente = ObjetoResultadoCliente.Lista.FirstOrDefault();
                        objCliente.Mail = Cliente.Mail;
                        objCliente.TelefonoFijo = Cliente.TelefonoFijo;
                        objCliente.TelefonoMovil = Cliente.TelefonoMovil;
                    }
                    else
                    {
                        objCliente.Rut = Cliente.Rut;
                        objCliente.Dv = Cliente.Dv;
                        objCliente.Nombre = Cliente.Nombre;
                        objCliente.Paterno = Cliente.Paterno;
                        objCliente.Materno = Cliente.Materno;
                        objCliente.Mail = Cliente.Mail;
                        objCliente.TelefonoFijo = Cliente.TelefonoFijo;
                        objCliente.TelefonoMovil = Cliente.TelefonoMovil;

                        if (objCliente.Rut >= 50000000)
                            objCliente.TipoPersona_Id = 2;//Persona Juridica
                        else
                            objCliente.TipoPersona_Id = 1;//Persona Natural
                    }
                }
                else
                {
                    ResultadoSimulacion.ResultadoGeneral = false;
                    ResultadoSimulacion.Mensaje = "Cliente " + ObjetoResultadoCliente.Mensaje;
                    return ResultadoSimulacion;
                }

                ObjetoResultadoCliente = AccesoDatos.AccesoDatos.Operacion(objCliente, GlobalDA.SP.Clientes_GRB, GlobalDA.Accion.Guardar, "ConexionSQL_WF");
                if (!ObjetoResultadoCliente.ResultadoGeneral)
                {
                    ResultadoSimulacion.ResultadoGeneral = false;
                    ResultadoSimulacion.Mensaje = ObjetoResultadoCliente.Mensaje;
                    return ResultadoSimulacion;
                }
                var ObjetoResultadoGGOO = new Resultado<GastoOperacionaSimulacionWeb>();
                GastoOperacionaSimulacionWeb objGastos = new GastoOperacionaSimulacionWeb();

                objGastos.MontoCredito = Simulacion.MontoCredito;
                objGastos.ValorPropiedad = Simulacion.ValorPropiedad;
                objGastos.IndDfl2 = Simulacion.IndDfl2;
                objGastos.Destino_Id = Simulacion.Destino_Id;


                ObjetoResultadoGGOO = AccesoDatos.AccesoDatos.Buscar<GastoOperacionaSimulacionWeb, GastoOperacionaSimulacionWeb>(objGastos, GlobalDA.SP.GastosOperacionalesSimulacion_QRY, "ConexionSQL_WF");
                var oGastos = new GastosOperacionalesSimulacion();

                if (ObjetoResultadoGGOO.ResultadoGeneral)
                {
                    foreach (var item in ObjetoResultadoGGOO.Lista)
                    {
                        oGastos = new GastosOperacionalesSimulacion();
                        oGastos.DescripcionTipoGasto = item.DescripcionTipoGasto;
                        oGastos.TipoGastoOperacional_Id = item.TipoGastoOperacional_Id;
                        oGastos.Valor = item.Valor;
                        oGastos.ValorPesos = item.ValorPesos;
                        oGastos.Moneda_Id = item.Moneda_Id;
                        ResultadoSimulacion.lstGastosOperacionales.Add(oGastos);
                    }
                }
                else
                {
                    ResultadoSimulacion.ResultadoGeneral = false;
                    ResultadoSimulacion.Mensaje = "GGOO " + ObjetoResultadoGGOO.Mensaje;
                    return ResultadoSimulacion;
                }
                return ResultadoSimulacion;
            }
            catch (Exception ex)
            {

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string nombreMetodo = frame.GetMethod().DeclaringType.Name;
                int linea = frame.GetFileLineNumber();
                int columna = frame.GetFileColumnNumber();
                int codigo = frame.GetHashCode();
                ResultadoSimulacion.ResultadoGeneral = false;
                ResultadoSimulacion.Mensaje = "1 " + ex.Message + " " + " fileName:  " + fileName + " linea:" + linea;
                return ResultadoSimulacion;

            }
        }
        /*****************************************************************************************************************/
        /*gps*/
        [WebMethod]
        public ResultadoGPS SolicitudesPorRut(string Rut)
        {
            var ResultadoGPS = new ResultadoGPS();
            try
            {
                SolicitudGPS objSolicitud = new SolicitudGPS();
                if (Rut.Split('-')[0] != null)
                    objSolicitud.Rut = int.Parse(Rut.Split('-')[0]);
                else
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = "Debe Indicar un Rut para Continuar";
                    return ResultadoGPS;
                }


                var ObjetoResultadoSolicitud = new Resultado<SolicitudGPS>();
                ObjetoResultadoSolicitud = AccesoDatos.AccesoDatos.Buscar<SolicitudGPS, SolicitudGPS>(objSolicitud, GlobalDA.SP.GPS_EncabezadoSolicitud_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoSolicitud.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoSolicitud.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstSolicitudes = ObjetoResultadoSolicitud.Lista;


                return ResultadoGPS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [WebMethod]
        public ResultadoGPS DetalleSolicitud(int NumeroSolicitud)
        {
            var ResultadoGPS = new ResultadoGPS();
            try
            {
                SolicitudGPS objSolicitud = new SolicitudGPS();
                ParticipantesGPS objParticipantes = new ParticipantesGPS();
                SegurosGPS objSeguros = new SegurosGPS();
                ObservacionesGPS objObservaciones = new ObservacionesGPS();
                GastosOperacionalesGPS objGastos = new GastosOperacionalesGPS();
                ControlEtapasGPS objEtapas = new ControlEtapasGPS();

                if (NumeroSolicitud != 0)
                {
                    objSolicitud.NumeroSolicitud = NumeroSolicitud;
                    objParticipantes.NumeroSolicitud = NumeroSolicitud;
                    objSeguros.NumeroSolicitud = NumeroSolicitud;
                    objObservaciones.NumeroSolicitud = NumeroSolicitud;
                    objGastos.NumeroSolicitud = NumeroSolicitud;
                    objEtapas.NumeroSolicitud = NumeroSolicitud;
                }
                else
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = "Debe Indicar un Número de Solicitud para Continuar";
                    return ResultadoGPS;
                }

                //Datos de la Solicitud
                var ObjetoResultadoSolicitud = new Resultado<SolicitudGPS>();
                ObjetoResultadoSolicitud = AccesoDatos.AccesoDatos.Buscar<SolicitudGPS, SolicitudGPS>(objSolicitud, GlobalDA.SP.GPS_EncabezadoSolicitud_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoSolicitud.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoSolicitud.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstSolicitudes = ObjetoResultadoSolicitud.Lista;
                                                          
                //Datos de Participantes
                var ObjetoResultadoParticipantes = new Resultado<ParticipantesGPS>();
                ObjetoResultadoParticipantes = AccesoDatos.AccesoDatos.Buscar<ParticipantesGPS, ParticipantesGPS>(objParticipantes, GlobalDA.SP.GPS_Participantes_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoParticipantes.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoParticipantes.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstParticipantes = ObjetoResultadoParticipantes.Lista;


                //Datos de Seguros
                var ObjetoResultadoSeguros = new Resultado<SegurosGPS>();
                ObjetoResultadoSeguros = AccesoDatos.AccesoDatos.Buscar<SegurosGPS, SegurosGPS>(objSeguros, GlobalDA.SP.GPS_Seguros_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoSeguros.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoSeguros.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstSeguros = ObjetoResultadoSeguros.Lista;


                //Datos de Observaciones
                var ObjetoResultadoObservaciones = new Resultado<ObservacionesGPS>();
                ObjetoResultadoObservaciones = AccesoDatos.AccesoDatos.Buscar<ObservacionesGPS, ObservacionesGPS>(objObservaciones, GlobalDA.SP.GPS_Observaciones_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoObservaciones.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoObservaciones.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstObservaciones = ObjetoResultadoObservaciones.Lista;



                //Datos de GGOO
                var ObjetoResultadoGastos = new Resultado<GastosOperacionalesGPS>();
                ObjetoResultadoGastos = AccesoDatos.AccesoDatos.Buscar<GastosOperacionalesGPS, GastosOperacionalesGPS>(objGastos, GlobalDA.SP.GPS_GastosOperacionales_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoGastos.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoGastos.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstGastosOperacionales= ObjetoResultadoGastos.Lista;




                //Datos de Control de Etapas
                var ObjetoResultadoEtapas = new Resultado<ControlEtapasGPS>();
                ObjetoResultadoEtapas = AccesoDatos.AccesoDatos.Buscar<ControlEtapasGPS, ControlEtapasGPS>(objEtapas, GlobalDA.SP.GPS_ControlEtapas_QRY, "ConexionSQL_WF");

                if (!ObjetoResultadoEtapas.ResultadoGeneral)
                {
                    ResultadoGPS.ResultadoGeneral = false;
                    ResultadoGPS.Mensaje = ObjetoResultadoEtapas.Mensaje;
                    return ResultadoGPS;
                }
                ResultadoGPS.lstControlEtapas = ObjetoResultadoEtapas.Lista;








                return ResultadoGPS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /*****************************************************************************************************************/

    }
}
