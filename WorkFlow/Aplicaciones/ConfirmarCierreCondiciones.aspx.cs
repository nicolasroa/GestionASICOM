using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Aplicaciones
{
    public partial class ConfirmarCierreCondiciones : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Anthem.Manager.Register(Page);
                NegSolicitudes.objResumenIndicadores = new ResumenIndicadores();
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
                lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
                txtValorUF.Text = string.Format("{0:0,0}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");
                if (!CargarInformacionSolicitud()) return; ;
                CargaParticipantes();
                CargaIndicador();
                CargaDeudorGarantia();
                CargaAntecedentesOperacion();
                CargaSeguros();
               

            }
        }

        #endregion

        #region Metodos

        private void CargaParticipantes()
        {

            try
            {
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                var oInfo = new ParticipanteInfo();

                oInfo.Solicitud_Id = oBandeja.Solicitud_Id;

                var ObjResultado = new Resultado<ParticipanteInfo>();
                var objNegocio = new NegParticipante();

                ////Asignacion de Variables
                ObjResultado = objNegocio.BuscarParticipante(oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gdvParticipantes, ObjResultado.Lista, new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
                    return;
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
            }
        }
        private void CargaIndicador()
        {
            //Dividendo Neto
            lblMtoDividendoNetoMO.Text = "UF " + string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF);
            lblMtoDividendoNetoCLP.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos);

            //Dividendo con Seguros
            lblMtoDividendoConSeguroMO.Text = "UF " + String.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal);
            lblMtoDividendoConSeguroCLP.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal);
        }

        private bool CargarInformacionSolicitud()
        {
            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();
                int NumeroSolicitud = 0;
                int RutCliente = 0;
                if (NegBandejaEntrada.oBandejaEntrada == null)
                {
                    if (Request.QueryString["Solicitud"] != null)
                    {
                        NumeroSolicitud = Convert.ToInt32(Seguridad.Desencriptar(Request.QueryString["Solicitud"]));
                        RutCliente = Convert.ToInt32(Seguridad.Desencriptar(Request.QueryString["RutCliente"]));
                    }
                    if (NumeroSolicitud == 0)
                    {
                        Controles.MostrarMensajeError("No se ha Indicado un Número de Solicitud");
                        return false;

                    }
                    NegBandejaEntrada.oBandejaEntrada = new BandejaEntradaInfo();
                    NegBandejaEntrada.oBandejaEntrada.Solicitud_Id = NumeroSolicitud;
                    oSolicitud.Id = NumeroSolicitud;
                }
                else
                    oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    lblEstadoCierre.Text = "";
                    if (rSolicitud.Lista.Count != 0)
                    {
                        oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                        NegSolicitudes.objSolicitudInfo = oSolicitud;

                        if (NegSolicitudes.objSolicitudInfo.NombreResponsableCierreCondiciones != null)
                        {
                            var EstadoCierre = NegSolicitudes.objSolicitudInfo.IndCierreCondiciones == true ? "Aprobado" : "Rechazado";
                            Controles.MostrarMensajeInfo("El Cierre de Condiciones ya fue " + EstadoCierre + " por " + NegSolicitudes.objSolicitudInfo.NombreResponsableCierreCondiciones);
                            btnAprobarCondiciones.Visible = false;
                            btnRechazarCondiciones.Visible = false;
                            lblEstadoCierre.Text = EstadoCierre.ToUpper() + " Por " + NegSolicitudes.objSolicitudInfo.NombreResponsableCierreCondiciones.ToUpper() + " el " + NegSolicitudes.objSolicitudInfo.FechaCierreCondiciones.Value.ToLongDateString();
                            

                        }
                        lblNumeroSolicitud.Text = NegSolicitudes.objSolicitudInfo.Id.ToString();
                        if (NumeroSolicitud == 0)
                        {
                            NegSolicitudes.objSolicitudInfo.IndCierreCondicionesInterno = true;
                            NegSolicitudes.objSolicitudInfo.ResponsableCierreCondiciones_Id = (int)NegUsuarios.Usuario.Rut;
                            btnAprobarCondiciones.Visible = true;
                            btnRechazarCondiciones.Visible = true;
                        }
                        else
                        {
                            NegSolicitudes.objSolicitudInfo.IndCierreCondicionesInterno = false;
                            NegSolicitudes.objSolicitudInfo.ResponsableCierreCondiciones_Id = RutCliente;
                            NegBandejaEntrada.oBandejaEntrada = null;
                        }
                    }
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
                    Controles.MostrarMensajeError("Error al Cargar la Información de la Solicitud");
                }
                return false;
            }
        }

        private void CargaDeudorGarantia()
        {
            alblMtoDeudaGarantiaMO.Text = String.Format("{0:F2}", 0) + "%";

            if (NegSolicitudes.objSolicitudInfo.MontoPropiedad != 0)
            {
                //Deuda/Garantia
                alblMtoDeudaGarantiaMO.Text = String.Format("{0:F2}", (NegSolicitudes.objSolicitudInfo.MontoCredito / NegSolicitudes.objSolicitudInfo.MontoPropiedad) * 100) + "%";
            }

        }
        private void CargaSeguros()
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                if (rSeguros.ResultadoGeneral)
                    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSeguros, rSeguros.Lista, new string[] { "Id" });
                else
                    Controles.MostrarMensajeError(rSeguros.Mensaje);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Seguros");
                }
            }
        }

        private void CargaAntecedentesOperacion()
        {

            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        lblTipoFinanciamiento.Text = ObjInfo.DescripcionTipoFinanciamiento;
                        lblProducto.Text = ObjInfo.DescripcionProducto.ToString();
                        lblDestino.Text = ObjInfo.DescripcionDestino.ToString();
                        lblObjetivo.Text = ObjInfo.DescripcionObjetivo.ToString();

                        lblTasa.Text = String.Format("{0:F2}", ObjInfo.TasaFinal) + "%";


                        lblPlazo.Text = ObjInfo.Plazo.ToString();
                        lblGracia.Text = ObjInfo.Gracia == 0 ? "Sin Meses de Gracia" : ObjInfo.Gracia.ToString();
                        decimal UF;
                        UF = NegParidad.ObtenerParidad((int)NegTablas.IdentificadorMaestro("MONEDAS", "UF"), DateTime.Now);
                        lblValorVentaMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoPropiedad);
                        lblValorVentaCLP.Text = String.Format("{0:C}", ObjInfo.MontoPropiedad * UF);
                        lblMontoSubsidioMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoSubsidio);
                        lblMontoSubsidioCLP.Text = String.Format("{0:C}", ObjInfo.MontoSubsidio * UF);
                        lblMontoBonoIntegracionMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoBonoIntegracion);
                        lblMontoBonoIntegracionCLP.Text = String.Format("{0:C}", ObjInfo.MontoBonoIntegracion * UF);
                        lblMontoBonoCaptacionMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoBonoCaptacion);
                        lblMontoBonoCaptacionCLP.Text = String.Format("{0:C}", ObjInfo.MontoBonoCaptacion * UF);
                        lblMontoSolicitadoMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoCredito);
                        lblMontoSolicitadoCLP.Text = String.Format("{0:C}", ObjInfo.MontoCredito * UF);
                        lblPagoContadoMO.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoContado);
                        lblPagoContadoCLP.Text = String.Format("{0:C}", ObjInfo.MontoContado * UF);
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Datos de la Operación");
                }
            }
        }

        #endregion

        protected void btnAprobarCondiciones_Click(object sender, EventArgs e)
        {
            ProcesarCierreCondiciones(true);
        }
        protected void btnRechazarCondiciones_Click(object sender, EventArgs e)
        {
            ProcesarCierreCondiciones(false);
        }
        private void ProcesarCierreCondiciones(bool Aprobado)
        {
            try
            {
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();
                NegSolicitudes oNegocio = new NegSolicitudes();

                NegSolicitudes.objSolicitudInfo.IndCierreCondiciones = Aprobado;
                NegSolicitudes.objSolicitudInfo.FechaCierreCondiciones = DateTime.Now;
                oResultado = oNegocio.Guardar(NegSolicitudes.objSolicitudInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Cierre de Condiciones Registrado Correctamente");
                    btnAprobarCondiciones.Visible = false;
                    btnRechazarCondiciones.Visible = false;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Seguros");
                }
            }
        }

    }
}