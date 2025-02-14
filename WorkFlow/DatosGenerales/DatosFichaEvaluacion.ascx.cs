using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosFichaEvaluacion : System.Web.UI.UserControl
    {
        public decimal UF = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                
                CargarFlujosMensuales();
               
                CargarCredito();
                CargaSeguros();
                CargarPropiedades();

                CargaEjecutivo();

            }
        }

     


        private void CargarFlujosMensuales()
        {
            try
            {
                var StrHtml = "";

                StrHtml = StrHtml + "<td class='tdTitulo02'>Flujos Mensuales</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td>";
                StrHtml = StrHtml + "<table style = 'width: 100%;'>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' rowspan ='2'>INGRESOS</td >";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Deudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Codeudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Fiador/Aval</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "</tr>";



                FlujosMensualesInfo oFlujosMensuales = new FlujosMensualesInfo();
                NegRentasCliente nRentas = new NegRentasCliente();
                Resultado<FlujosMensualesInfo> rFlujosMensuales = new Resultado<FlujosMensualesInfo>();
                oFlujosMensuales.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rFlujosMensuales = nRentas.BuscarFlujoMensual(oFlujosMensuales);
                decimal TotalRentaDedor = 0;
                decimal TotalRentaCodeudor = 0;
                decimal TotalRentaAval = 0;

                if (rFlujosMensuales.ResultadoGeneral)
                {
                    foreach (var flujo in rFlujosMensuales.Lista)
                    {



                        StrHtml = StrHtml + "<tr>";
                        StrHtml = StrHtml + "<td class='tdInfo'>" + flujo.Descripcion + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaDeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", flujo.RentaDeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaCodeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", flujo.RentaCodeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaAval) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", flujo.RentaAval / UF) + "</td>";

                        StrHtml = StrHtml + "</tr> ";

                        TotalRentaDedor = TotalRentaDedor + flujo.RentaDeudor;
                        TotalRentaCodeudor = TotalRentaCodeudor + flujo.RentaCodeudor;
                        TotalRentaAval = TotalRentaAval + flujo.RentaAval;


                    }

                    StrHtml = StrHtml + "<tr>";
                    StrHtml = StrHtml + "<td class='tdInfoData'>Total Ingresos</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaDedor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", TotalRentaDedor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaCodeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", TotalRentaCodeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaAval) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:F4}", TotalRentaAval / UF) + "</td>";

                    StrHtml = StrHtml + "</tr> ";

                    decimal Totalngresos = TotalRentaDedor + TotalRentaCodeudor + TotalRentaAval;


                    StrHtml = StrHtml + "<tr class='tdInfo'>";
                    StrHtml = StrHtml + "<td class='tdInfo'>TOTAL INGRESOS COMPLEMENTADOS</td>";
                    StrHtml = StrHtml + "<td  colspan ='6'>" + string.Format("{0:C}", Totalngresos) + "</td>";


                    StrHtml = StrHtml + "</tr> ";

                    StrHtml = StrHtml + "</table> ";
                    StrHtml = StrHtml + "</tr>";
                    StrHtml = StrHtml + "</td>";
                }
                else
                {
                    Controles.MostrarMensajeError(rFlujosMensuales.Mensaje);
                }



                DivFlujosMensuales.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Fujos Mensuales");
                }
            }
        }

       
        private void CargarCredito()
        {
            try
            {


                SolicitudInfo oSolicitud = new SolicitudInfo();
                oSolicitud = NegSolicitudes.objSolicitudInfo;

                lblTipoFinanciamiento.Text = oSolicitud.DescripcionTipoFinanciamiento;
                lblProducto.Text = oSolicitud.DescripcionProducto;
                lblObjetivo.Text = oSolicitud.DescripcionObjetivo;

                lblDestino.Text = oSolicitud.DescripcionDestino;
                lblNombreSubsidio.Text = oSolicitud.DescripcionSubsidio == "" ? "Sin Subsidio" : oSolicitud.DescripcionSubsidio;
                lblPrecioVenta.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoPropiedad);
                lblMontoSubsidio.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoSubsidio);
                lblMontoBonoIntegracion.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoBonoIntegracion);
                lblMontoBonoCaptacion.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoBonoCaptacion);
                lblMontoContado.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoContado);
                lblMontoCredito.Text = "UF " + string.Format("{0:F4}", oSolicitud.MontoCredito);
                lblPorcFinanciamiento.Text = "% " + oSolicitud.PorcentajeFinanciamiento;

                lblPrecioVentaPesos.Text = string.Format("{0:C}", oSolicitud.MontoPropiedad * UF);
                lblMontoSubsidioPesos.Text = string.Format("{0:C}", oSolicitud.MontoSubsidio * UF);
                lblMontoBonoIntegracionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoIntegracion * UF);
                lblMontoBonoCaptacionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoIntegracion * UF);
                lblMontoContadoPesos.Text = string.Format("{0:C}", oSolicitud.MontoContado * UF);
                lblMontoCreditoPesos.Text = string.Format("{0:C}", oSolicitud.MontoCredito * UF);

                lblDividendoNeto.Text = "UF " + string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF);
                lblDividendoNetoPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos);

                //Dividendo con Seguros
                lblDividendoConSeguros.Text = "UF " + String.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal);
                lblDividendoConSegurosPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal);


                lblPlazo.Text = oSolicitud.Plazo.ToString();
                lblTasa.Text = "% " + string.Format("{0:F4}", oSolicitud.TasaFinal);
                lblGracia.Text = oSolicitud.Gracia == 0 ? "Sin Meses de Gracia" : oSolicitud.Gracia.ToString();


                lblValorUF.Text = string.Format("{0:C}", UF);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los datos de la Solicitud");
                }
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
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Seguros Contratados");
                }
            }
        }



        private void CargarPropiedades()
        {
            try
            {
                NegPropiedades nPropiedades = new NegPropiedades();
                Resultado<TasacionInfo> rTasaciones = new Resultado<TasacionInfo>();
                TasacionInfo oTasaciones = new TasacionInfo();
                oTasaciones.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                rTasaciones = nPropiedades.BuscarTasacion(oTasaciones);
                if (rTasaciones.ResultadoGeneral)
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedades, NegPropiedades.lstTasaciones, new string[] { "Id" });
                else
                    Controles.MostrarMensajeError(rTasaciones.Mensaje);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los datos de la Solicitud");
                }
            }
        }


        private void CargaEjecutivo()
        {
            try
            {
                Resultado<UsuarioInfo> rUsuario = new Resultado<UsuarioInfo>();
                NegUsuarios nUsuario = new NegUsuarios();
                UsuarioInfo oUsuario = new UsuarioInfo();

                oUsuario.Rut = NegSolicitudes.objSolicitudInfo.EjecutivoComercial_Id;
                rUsuario = nUsuario.Buscar(oUsuario);
                if (rUsuario.ResultadoGeneral)
                {
                    var objEjecutivo = rUsuario.Lista.FirstOrDefault(u => u.Rut == NegSolicitudes.objSolicitudInfo.EjecutivoComercial_Id);
                    lblEjecutivoNombre.Text = objEjecutivo.Nombre + " (" + objEjecutivo.Email + ")";

                }
                else
                    Controles.MostrarMensajeError(rUsuario.Mensaje);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el Ejecutuvo");
                }
            }
        }



    }
}
