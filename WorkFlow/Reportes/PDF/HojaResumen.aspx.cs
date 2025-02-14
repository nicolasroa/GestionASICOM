using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PDF
{
    public partial class HojaResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            imgLogoCertificado.Src = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "img/LogoCliente.png");
            CargarDatosOperacion();
        }


        private void CargarDatosOperacion()
        {
            try
            {
                SolicitudInfo oSolicitud = new SolicitudInfo();

                if (NegSolicitudes.objSolicitudInfo != null)
                {
                    oSolicitud = NegSolicitudes.objSolicitudInfo;
                    lblNombreCliente.Text = oSolicitud.NombreCliente;
                    lblNombreCliente2.Text = oSolicitud.NombreCliente;
                    lblNumeroSolicitud.Text = oSolicitud.Id.ToString();
                    lblNumeroOperacion.Text = oSolicitud.NumeroOperacion;
                    lblFecha.Text = DateTime.Now.ToLongDateString();
                    lblCae.Text = string.Format("{0:0.00}", oSolicitud.CAE);
                    lblCAE2.Text = string.Format("{0:N4}", oSolicitud.CAE);
                    lblMontoCredito.Text = string.Format("{0:N4}", oSolicitud.MontoCredito);
                    lblTasa.Text = string.Format("{0:N4}", oSolicitud.TasaFinal);

                    lblDividendosMenos1.Text = (oSolicitud.Dividendos + -1).ToString();
                    lblUltimoValorCuota.Text = string.Format("{0:N4}", oSolicitud.ValorUltimoDividendoUF);
                    lblGracia.Text = oSolicitud.Gracia.ToString();
                    lblMontoNominal.Text = string.Format("{0:N4}", oSolicitud.MontoCreditoCapitalizado);
                    lblPlazo.Text = oSolicitud.Plazo.ToString();
                    lblCTC.Text = string.Format("{0:N4}", oSolicitud.CTC);
                    lblCTC2.Text = string.Format("{0:N4}", oSolicitud.CTC);
                    lblValorCuota.Text = string.Format("{0:N4}", oSolicitud.ValorDividendoUF);
                    lblValorCuota2.Text = string.Format("{0:N4}", oSolicitud.ValorDividendoUF);
                    lblModalidad.Text = oSolicitud.DescripcionProducto;
                    lblMontoBruto.Text = string.Format("{0:N4}", oSolicitud.MontoCredito);
                }


                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();
                oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oGastos.IndSimulacion = true;
                rGastos = nGastos.BuscarGastos(oGastos);
                if (rGastos.ResultadoGeneral)
                    Controles.CargarGrid<GastoOperacionalInfo>(ref gvGastosOperacionales, rGastos.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id" });
                else
                    lblNumeroOperacion.Text = rGastos.Mensaje;

                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                if (rSeguros.ResultadoGeneral)
                    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSegurosContratados, rSeguros.Lista, new string[] { "Id" });
                else
                    lblNumeroOperacion.Text = rSeguros.Mensaje;



            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblNumeroOperacion.Text = Ex.Message;
                }
                else
                {
                    lblNumeroOperacion.Text = "Error al Cargar los Seguros";
                }
            }
        }
    }
}