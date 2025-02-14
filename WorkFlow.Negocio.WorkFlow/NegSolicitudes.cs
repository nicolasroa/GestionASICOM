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
    public class NegSolicitudes
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de SolicitudInfo
        /// </summary>
        /// <param name="Entidad">Objeto SolicitudInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad SolicitudInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<SolicitudInfo> Buscar(SolicitudInfo Entidad, bool Consulta = false)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.Solicitudes_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (!Consulta)
                    {
                        lstSolicitudInfo = new List<SolicitudInfo>();
                        lstSolicitudInfo = ObjetoResultado.Lista;
                    }
                    else
                    {
                        lstSolicitudConsultaInfo = new List<SolicitudInfo>();
                        lstSolicitudConsultaInfo = ObjetoResultado.Lista;
                    }
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Solicitudes";
                return ObjetoResultado;
            }
        }

        public Resultado<SolicitudInfo> BuscarSolicitudesPorTasar(SolicitudInfo Entidad, bool Consulta = false)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.SolicitudesPorTasar_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (!Consulta)
                    {
                        lstSolicitudInfo = new List<SolicitudInfo>();
                        lstSolicitudInfo = ObjetoResultado.Lista;
                    }
                    else
                    {
                        lstSolicitudConsultaInfo = new List<SolicitudInfo>();
                        lstSolicitudConsultaInfo = ObjetoResultado.Lista;
                    }
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Solicitudes";
                return ObjetoResultado;
            }
        }
        public Resultado<SolicitudInfo> BuscarSolicitudesEstudioTitulo(SolicitudInfo Entidad, bool Consulta = false)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.SolicitudesEstudioTitulo_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (!Consulta)
                    {
                        lstSolicitudInfo = new List<SolicitudInfo>();
                        lstSolicitudInfo = ObjetoResultado.Lista;
                    }
                    else
                    {
                        lstSolicitudConsultaInfo = new List<SolicitudInfo>();
                        lstSolicitudConsultaInfo = ObjetoResultado.Lista;
                    }
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Solicitudes";
                return ObjetoResultado;
            }
        }

        public Resultado<SolicitudInfo> BuscarSolicitudesDPS(SolicitudInfo Entidad, bool Consulta = false)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.SolicitudesDPS_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (!Consulta)
                    {
                        lstSolicitudInfo = new List<SolicitudInfo>();
                        lstSolicitudInfo = ObjetoResultado.Lista;
                    }
                    else
                    {
                        lstSolicitudConsultaInfo = new List<SolicitudInfo>();
                        lstSolicitudConsultaInfo = ObjetoResultado.Lista;
                    }
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Solicitudes";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que guarda un objeto de SolicitudInfo
        /// </summary>
        /// <param name="Entidad">Objeto SolicitudInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad SolicitudInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<SolicitudInfo> Guardar(ref SolicitudInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.Solicitudes_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objSolicitudInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Solicitudes.";
                return ObjetoResultado;
            }
        }
        public Resultado<SolicitudInfo> Guardar(SolicitudInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.Solicitudes_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objSolicitudInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Solicitudes.";
                return ObjetoResultado;
            }
        }


        /// <summary>
        /// Método que realiza una consulta de AsignacionSolicitudInfo
        /// </summary>
        /// <param name="Entidad">Objeto AsignacionSolicitudInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad AsignacionSolicitudInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AsignacionSolicitudInfo> BuscarAsignacion(AsignacionSolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AsignacionSolicitudInfo, AsignacionSolicitudInfo>(Entidad, GlobalDA.SP.AsignacionSolicitud_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Asignación Solicitudes";
                return ObjetoResultado;
            }
        }


        /// <summary>
        /// Método que guarda un objeto de AsignacionSolicitudInfo
        /// </summary>
        /// <param name="Entidad">Objeto AsignacionSolicitudInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad AsignacionSolicitudInfo en el Atributo Lista del Objeto Resultado.</returns>

        public Resultado<AsignacionSolicitudInfo> GuardarAsignacion(ref AsignacionSolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.AsignacionSolicitud_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objAsignacionSolicitudesInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Asignación Solicitudes.";
                return ObjetoResultado;
            }
        }
        public Resultado<AsignacionSolicitudInfo> GuardarAsignacion(AsignacionSolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.AsignacionSolicitud_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objAsignacionSolicitudesInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Asignación Solicitudes.";
                return ObjetoResultado;
            }
        }

        public Resultado<AsignacionSolicitudInfo> BuscarSolicitudesAsignadas(AsignacionSolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AsignacionSolicitudInfo, AsignacionSolicitudInfo>(Entidad, GlobalDA.SP.ReasignacionSolicitudes_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Asignación Solicitudes";
                return ObjetoResultado;
            }
        }

        public Resultado<AsignacionSolicitudInfo> ProcesarReasignacion(AsignacionSolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionSolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.ReasignacionSolicitudes_PRC, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Asignación Solicitudes.";
                return ObjetoResultado;
            }
        }



        
        public Resultado<FinalidadCreditoInfo> BuscarFinalidad(FinalidadCreditoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<FinalidadCreditoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<FinalidadCreditoInfo, FinalidadCreditoInfo>(Entidad, GlobalDA.SP.FinalidadCredito_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Finalidad Crédito ";
                return ObjetoResultado;
            }
        }

        public Resultado<UtilidadCreditoInfo> BuscarUtilidad(UtilidadCreditoInfo Entidad)
        {
            var ObjetoResultado = new Resultado<UtilidadCreditoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<UtilidadCreditoInfo, UtilidadCreditoInfo>(Entidad, GlobalDA.SP.UtilidadCredito_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Utilidad Crédito";
                return ObjetoResultado;
            }
        }
        public Resultado<SolicitudInfo> ActivarOperacion(SolicitudInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.ActivarOperacion_PRC, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Activar la Operación:" + ex.Message;
                return ObjetoResultado;
            }
        }
        public Resultado<CaeCtcInfo> CalcularCAE(CaeCtcInfo Entidad)
        {
            var ObjetoResultado = new Resultado<CaeCtcInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<CaeCtcInfo, CaeCtcInfo>(Entidad, GlobalDA.SP.CalcularCAE_PRC, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Utilidad Crédito";
                return ObjetoResultado;
            }
        }
        public Resultado<SolicitudInfo> BuscarActivaciones(SolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.ConsultaActivaciones_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar Resumen de Activaciones";
                return ObjetoResultado;
            }
        }


        public Resultado<SolicitudInfo> ValidarActivacion(SolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.ValidarActivacionWF_PRC, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar Resumen de Activaciones";
                return ObjetoResultado;
            }
        }


        public Resultado<SolicitudInfo> ListarRechazosActivacion(SolicitudInfo Entidad)
        {
            var ObjetoResultado = new Resultado<SolicitudInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SolicitudInfo, SolicitudInfo>(Entidad, GlobalDA.SP.RechazosActivacion_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = "Error al Listar Rechazos";
                return ObjetoResultado;
            }
        }





        public static bool RecalcularDividendo()
        {
            try
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
                var UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                var rSimulacion = new Resultado<SimulacionHipotecariaInfo>();
                var nSimulacion = new NegSimulacionHipotecaria();
                var oSimulacion = new SimulacionHipotecariaInfo();
                var DividendoNeto = decimal.Zero;
                var UltimoDividendoNeto = decimal.Zero;
                var SegIncendio = decimal.Zero;
                var SegDesgravamen = decimal.Zero;
                var SegCesantia = decimal.Zero;
                var nSolicitud = new NegSolicitudes();
                var rSolicitud = new Resultado<SolicitudInfo>();
                var oSolicitud = new SolicitudInfo();
                var lstParticipantes = new List<ParticipanteInfo>();
                var lstTasaciones = new List<TasacionInfo>();
                oSolicitud = NegSolicitudes.objSolicitudInfo;


                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var rParticipante = new Resultado<ParticipanteInfo>();

                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                rParticipante = NegParticipantes.BuscarParticipante(oParticipante);
                if (rParticipante.ResultadoGeneral)
                {
                    lstParticipantes = rParticipante.Lista;
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
                    return false;
                }

                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;


                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    lstTasaciones = rTasacion.Lista;

                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                    return false;
                }
                //CALCULOS DEL DIVIDENDO SIN SEGUROS - CAE - CTC
                oSimulacion = new SimulacionHipotecariaInfo();
                oSimulacion.TipoFinanciamiento_Id = oSolicitud.TipoFinanciamiento_Id;
                oSimulacion.Producto_Id = oSolicitud.Producto_Id;
                oSimulacion.Objetivo_Id = oSolicitud.Objetivo_Id;
                oSimulacion.Destino_Id = oSolicitud.Destino_Id;
                oSimulacion.TipoPropiedad_Id = 1;
                oSimulacion.Subsidio_Id = oSolicitud.Subsidio_Id;
                oSimulacion.IndDfl2 = oSolicitud.IndDfl2;
                oSimulacion.Antiguedad_Id = 1;
                oSimulacion.MesExclusion = 0;
                oSimulacion.Gracia = oSolicitud.Gracia;
                oSimulacion.IndPrepago = true;
                oSimulacion.DiaVencimiento = 10;
                oSimulacion.ValorPropiedad = oSolicitud.MontoPropiedad;
                oSimulacion.MontoCredito = oSolicitud.MontoCredito;
                oSimulacion.MontoContado = oSolicitud.MontoContado;
                oSimulacion.Plazo = oSolicitud.Plazo;
                oSimulacion.CantidadDeudores = 1;
                oSimulacion.SeguroDesgravamen_Id = -1;
                oSimulacion.SeguroCesantia_Id = -1;
                oSimulacion.PlazoUnico = true;

                oSimulacion.TasaAnual = NegSolicitudes.objSolicitudInfo.TasaFinal;


                rSimulacion = nSimulacion.RealizarSimulacion(oSimulacion);
                if (!rSimulacion.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rSimulacion.Mensaje);
                    return false;
                }
                DividendoNeto = rSimulacion.Lista.FirstOrDefault(s => s.Plazo == oSimulacion.Plazo).DividendoUF;
                UltimoDividendoNeto = rSimulacion.Lista.FirstOrDefault(s => s.Plazo == oSimulacion.Plazo).UltimoDividendoUF;

                oSolicitud.ValorDividendoUF = DividendoNeto;
                oSolicitud.ValorUltimoDividendoUF = UltimoDividendoNeto;




                if (oSolicitud.Plazo2 > 0)//Solicitud con Dividendo Flexible
                {
                    oSimulacion.Plazo = oSolicitud.Plazo2;

                    rSimulacion = nSimulacion.RealizarSimulacion(oSimulacion);
                    if (!rSimulacion.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rSimulacion.Mensaje);
                        return false;
                    }
                    oSolicitud.ValorDividendoFlexibleUF = rSimulacion.Lista.FirstOrDefault(s => s.Plazo == oSimulacion.Plazo).DividendoUF;
                }
                else
                {
                    oSolicitud.ValorDividendoFlexibleUF = 0;
                }







                //rSolicitud = nSolicitud.Guardar(ref oSolicitud);

                //if (rSolicitud.ResultadoGeneral)
                //{
                //    NegSolicitudes.objSolicitudInfo = oSolicitud;
                //}
                //else
                //{
                //    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                //    return false;
                //}



                //CALCULO DEL SEGURO DE DESGRAVAMEN
                foreach (var participante in lstParticipantes)
                {
                    SegDesgravamen = SegDesgravamen + participante.PrimaSeguroDesgravamen;
                    SegCesantia = SegCesantia + participante.PrimaSeguroCesantia;
                }
                //CALCULO SEGURO INCENDIO
                foreach (var tasa in lstTasaciones)
                {

                    SegIncendio = SegIncendio + tasa.PrimaSeguro;
                }

                //CALCULO CAE - CTC

                CaeCtcInfo objCAE = new CaeCtcInfo();
                Resultado<CaeCtcInfo> rCAE = new Resultado<CaeCtcInfo>();


                objCAE.Solicitud_Id = oSolicitud.Id;
                rCAE = nSolicitud.CalcularCAE(objCAE);
                if (rCAE.ResultadoGeneral)
                {
                    objCAE.CAE = rCAE.Lista.FirstOrDefault().CAE;
                    objCAE.CTC = rCAE.Lista.FirstOrDefault().CTC;
                    objCAE.CAEMinvu = rCAE.Lista.FirstOrDefault().CAEMinvu;
                    objCAE.CTCMinvu = rCAE.Lista.FirstOrDefault().CTCMinvu;

                }
                else
                {
                    Controles.MostrarMensajeError(rCAE.Mensaje);
                    return false;
                }

                oSolicitud.ValorDividendoUF = DividendoNeto;
                oSolicitud.ValorUltimoDividendoUF = UltimoDividendoNeto;
                oSolicitud.ValorDividendoUfTotal = DividendoNeto + SegCesantia + SegDesgravamen + SegIncendio;
                oSolicitud.ValorDividendoSinCesantiaUF = DividendoNeto + SegDesgravamen + SegIncendio;
                oSolicitud.TasaBase = rSimulacion.Lista.FirstOrDefault(s => s.Plazo == oSimulacion.Plazo).TasaBaseAnual;
                oSolicitud.Spread = rSimulacion.Lista.FirstOrDefault(s => s.Plazo == oSimulacion.Plazo).TasaSpreadAnual;
                oSolicitud.ValorDividendoPesos = oSolicitud.ValorDividendoUF * UF;
                oSolicitud.ValorDividendoPesosTotal = oSolicitud.ValorDividendoUfTotal * UF;

                oSolicitud.ValorDividendoFlexibleUfTotal = oSolicitud.ValorDividendoFlexibleUF + SegCesantia + SegDesgravamen + SegIncendio;

                oSolicitud.ValorDividendoFlexiblePesos = oSolicitud.ValorDividendoFlexibleUF * UF;
                oSolicitud.ValorDividendoFlexiblePesosTotal = oSolicitud.ValorDividendoFlexibleUfTotal * UF;

                oSolicitud.CAE = objCAE.CAE;
                oSolicitud.CTC = objCAE.CTC;

                if (oSolicitud.Plazo2 <= 0)//Solicitud sin Dividendo Flexible
                {
                    oSolicitud.ValorDividendoFlexibleUfTotal = 0;
                    oSolicitud.ValorDividendoFlexiblePesos = 0;
                    oSolicitud.ValorDividendoFlexiblePesosTotal = 0;
                }

                    rSolicitud = nSolicitud.Guardar(ref oSolicitud);

                if (rSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = oSolicitud;
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
                }
                return true;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Recalcular el Dividendo");
                }
                return false;
            }
        }
        public static bool ActualizarSolicitud(SolicitudInfo oSolicitud)
        {
            try
            {
            
                var nSolicitud = new NegSolicitudes();
                var rSolicitud = new Resultado<SolicitudInfo>();


                rSolicitud = nSolicitud.Guardar(ref oSolicitud);

                if (rSolicitud.ResultadoGeneral)
                {
                    objSolicitudInfo = oSolicitud;
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
                }

                return true;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Recalcular el Dividendo");
                }
                return false;
            }
        }



        #endregion


        #region PROPIEDADES

        public static SolicitudInfo objSolicitudInfo
        {
            get { return (SolicitudInfo)HttpContext.Current.Session[ISesiones.objSolicitudesInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objSolicitudesInfo, value); }
        }

        public static ResumenIndicadores objResumenIndicadores
        {
            get { return (ResumenIndicadores)HttpContext.Current.Session[ISesiones.objResumenIndicadores]; }
            set { HttpContext.Current.Session.Add(ISesiones.objResumenIndicadores, value); }
        }

        public static AsignacionSolicitudInfo objAsignacionSolicitudesInfo
        {
            get { return (AsignacionSolicitudInfo)HttpContext.Current.Session[ISesiones.objAsignacionSolicitudesInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objAsignacionSolicitudesInfo, value); }
        }

        public static bool? IndComentario
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.IndTipoComentario]; }
            set { HttpContext.Current.Session.Add(ISesiones.IndTipoComentario, value); }
        }

        public static List<SolicitudInfo> lstSolicitudInfo
        {
            get { return (List<SolicitudInfo>)HttpContext.Current.Session[ISesiones.lstSolicitudesInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstSolicitudesInfo, value); }
        }
        public static List<SolicitudInfo> lstSolicitudConsultaInfo
        {
            get { return (List<SolicitudInfo>)HttpContext.Current.Session[ISesiones.lstSolicitudConsultaInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstSolicitudConsultaInfo, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string objAsignacionSolicitudesInfo = "objAsignacionSolicitudesInfo";
            public const string objSolicitudesInfo = "objSolicitudesInfo";
            public const string IndTipoComentario = "IndTipoComentario";
            public const string lstSolicitudesInfo = "lstSolicitudesInfo";
            public const string lstSolicitudConsultaInfo = "lstSolicitudConsultaInfo";
            public const string objResumenIndicadores = "objResumenIndicadores";
        }
        #endregion



    }
}
