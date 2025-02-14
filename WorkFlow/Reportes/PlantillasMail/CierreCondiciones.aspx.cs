using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PlantillasMail
{
    public partial class CierreCondiciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarInformacion();
        }


        private void CargarInformacion()
        {
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
            lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblNombreCliente.Text = NegParticipante.ObjParticipante.NombreCompleto;
            lnkCierreCondiciones.Text = "Confirmar Cierre de Condiciones";
            
            CargarInformacionSolicitud();
            CargaIndicador();
            CargaParticipantes();
            CargaDeudorGarantia();
            CargaSeguros();
            CargaAntecedentesOperacion();
            lnkCierreCondiciones.NavigateUrl = NegConfiguracionGeneral.Obtener().UrlSitio + "/Aplicaciones/ConfirmarCierreCondiciones?Solicitud=" + Seguridad.Encriptar(NegSolicitudes.objSolicitudInfo.Id.ToString())+"&RutCliente="+ Seguridad.Encriptar(NegParticipante.ObjParticipante.Rut.ToString());


        }
        private void CargarInformacionSolicitud()
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
                        oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                        NegSolicitudes.objSolicitudInfo = oSolicitud;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CargaDeudorGarantia()
        {
            lblDeudaGarantia.Text = String.Format("{0:F2}", 0) + "%";

            if (NegSolicitudes.objSolicitudInfo.MontoPropiedad != 0)
            {
                //Deuda/Garantia
                lblDeudaGarantia.Text = String.Format("{0:F2}", (NegSolicitudes.objSolicitudInfo.MontoCredito / NegSolicitudes.objSolicitudInfo.MontoPropiedad) * 100) + "%";
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

                        lblTasaMensual.Text = String.Format("{0:F2}", ObjInfo.TasaFinal) + "%";


                        lblPlazo.Text = ObjInfo.Plazo.ToString();
                        lblGracia.Text = ObjInfo.Gracia == 0 ? "Sin Meses de Gracia" : ObjInfo.Gracia.ToString();
                        decimal UF;
                        UF = NegParidad.ObtenerParidad((int)NegTablas.IdentificadorMaestro("MONEDAS", "UF"), DateTime.Now);
                        lblPrecioVentaUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoPropiedad);
                        lblPrecioVentaPesos.Text = String.Format("{0:C}", ObjInfo.MontoPropiedad * UF);
                        lblSubsidioUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoSubsidio);
                        lblSubsidioPesos.Text = String.Format("{0:C}", ObjInfo.MontoSubsidio * UF);
                        lblBonoIntegracionUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoBonoIntegracion);
                        lblBonoIntegracionPesos.Text = String.Format("{0:C}", ObjInfo.MontoBonoIntegracion * UF);
                        lblBonoCaptacionUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoBonoCaptacion);
                        lblBonoCaptacionPesos.Text = String.Format("{0:C}", ObjInfo.MontoBonoCaptacion * UF);
                        lblMontoSolicitadoUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoCredito);
                        lblMontoSolicitadoPesos.Text = String.Format("{0:C}", ObjInfo.MontoCredito * UF);
                        lblMontoContadoUF.Text = "UF " + String.Format("{0:F4}", ObjInfo.MontoContado);
                        lblMontoContadoPesos.Text = String.Format("{0:C}", ObjInfo.MontoContado * UF);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Simular");
                }
            }

        }
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
            lblDividendoNetoUF.Text = "UF " + string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF);
            lblDividendoNetoPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos);

            //Dividendo con Seguros
            lblDividendoConSegurosUF.Text = "UF " + String.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal);
            lblDividendoConSegurosPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal);


        }
    }
}